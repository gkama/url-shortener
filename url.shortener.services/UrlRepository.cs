using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using System.Diagnostics;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using url.shortener.data;

namespace url.shortener.services
{
    public class UrlRepository : IUrlRepository
    {
        private readonly UrlContext _context;
        private readonly ILogger<UrlRepository> _logger;

        private readonly Stopwatch _sw;

        public UrlRepository(UrlContext context, ILogger<UrlRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sw = new Stopwatch();
        }

        private IQueryable<GkamaUrl> GetGkamaUrlsQuery(bool asNoTracking = true)
        {
            return asNoTracking
                ? _context.Urls
                    .Include(x => x.Metadata)
                    .AsNoTracking()
                    .AsQueryable()
                : _context.Urls
                    .Include(x => x.Metadata)
                    .AsQueryable();
        }

        private IQueryable<GkamaUrlMetadata> GetGkamaUrlMetadataQuery()
        {
            return _context.UrlMetadata
                .AsNoTracking()
                .AsQueryable();
        }

        public async Task<IEnumerable<IGkamaUrl>> GetUrlsAsync()
        {
            return await GetGkamaUrlsQuery()
                .OrderByDescending(x => x.Id)
                .Take(100)
                .ToListAsync();
        }

        public async Task<IGkamaUrl> GetUrlAsync(int id, bool asNoTracking = true)
        {
            return await GetGkamaUrlsQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IGkamaUrl> GetUrlAsync(Guid publicKey)
        {
            return await GetGkamaUrlsQuery()
                .FirstOrDefaultAsync(x => x.PublicKey == publicKey);
        }

        public async Task<IGkamaUrl> GetUrlAsync(string shortUrl)
        {
            return await GetGkamaUrlsQuery()
                .FirstOrDefaultAsync(x => x.ShortUrl == shortUrl);
        }

        public async Task<IGkamaUrl> GetUrlAsync(string target, string shortUrl)
        {
            return await GetGkamaUrlsQuery()
                .FirstOrDefaultAsync(x => x.Target == target
                    && x.ShortUrl == shortUrl);
        }

        public async Task<IEnumerable<IGkamaUrl>> GetUnsecureUrlsAsync()
        {
            return await GetGkamaUrlsQuery()
                .Where(x => x.Target.Take(4).ToString() == "http")
                .ToListAsync();
        }

        public async Task<GkamaUrlMetadata> GetUrlMetadataAsync(int id)
        {
            return await GetGkamaUrlMetadataQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IGkamaUrl> AddUrlAsync(string target)
        {
            var url = new GkamaUrl()
            {
                Target = target
            };

            await _context.Urls
                .AddAsync(url);

            await _context.SaveChangesAsync();

            return url;
        }

        public async Task<IGkamaUrl> AddUrlNotExistAsync(string target)
        {
            if (await GetGkamaUrlsQuery()
                .FirstOrDefaultAsync(x => x.Target == target) != null)
                throw new UrlException(HttpStatusCode.Found,
                    $"url with target='{target}' already exists");

            var url = new GkamaUrl()
            {
                Target = target
            };

            await _context.Urls
                .AddAsync(url);

            await _context.UrlMetadata
                .AddAsync(url.Target.ParseUri());

            await _context.SaveChangesAsync();

            return url;
        }

        public async Task<IGkamaUrl> AddUrlAsync(GkamaUrl url)
        {
            await _context.Urls
                .AddAsync(url);

            await _context.SaveChangesAsync();

            return url;
        }

        public async Task DeleteUrlAsync(int id)
        {
            var url = await GetUrlAsync(id, asNoTracking: false) as GkamaUrl
                ?? throw new UrlException(HttpStatusCode.BadRequest,
                    $"error while deleting url with id='{id}'. it doesn't exist");

            _context.Urls
                .Remove(url);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUrlMetadataAsync(int id)
        {
            var urlMetadata = await GetUrlMetadataAsync(id)
                ?? throw new UrlException(HttpStatusCode.BadRequest,
                    $"error while deleting url metadata with id='{id}'. it doesn't exist");

            _context.UrlMetadata
                .Remove(urlMetadata);

            await _context.SaveChangesAsync();
        }

        public async Task<IGkamaUrl> ShortenUrlAsync(int id)
        {
            var url = await GetUrlAsync(id);

            if (url == null)
                throw new UrlException(HttpStatusCode.BadRequest,
                    $"url with id='{id}' doesn't exist in our records");

            url.ShortUrl = string.IsNullOrWhiteSpace(url.ShortUrl)
                ? ShortenUrl()
                : throw new UrlException(HttpStatusCode.BadRequest,
                    $"url has already been shortened. url='{JsonSerializer.Serialize(url)}'");

            await _context.SaveChangesAsync();

            return url;
        }

        public async Task<IGkamaUrl> ShortenUrlAsync(string target)
        {
            var url = await AddUrlNotExistAsync(target);

            _sw.Start();
            url.ShortUrl = ShortenUrl(url.Id);
            _sw.Stop();

            _logger.LogInformation($"shorten algorithm took: {_sw.Elapsed.TotalMilliseconds * 1000} µs (microseconds)");

            await _context.SaveChangesAsync();

            return url;
        }

        public string ShortenUrl(string target)
        {
            var uri = GetUrl(target);


            return null;
        }

        public string ShortenUrl(int? id = null)
        {
            return $"{Constants.BaseUrl}{RandomString(id)}";
        }

        public string RandomString(int? id = null)
        {
            return id == null
                ? $"{RandomString()}"
                : $"{RandomString()}_{Encode((int)id)}";
        }

        public string RandomString()
        {
            return string.Create(10, 2, (buffer, value) =>
            {
                var alphaNumeric = Constants.AlphaNumeric.AsSpan();
                var random = new Random();

                buffer[9] = alphaNumeric[random.Next(alphaNumeric.Length)];
                buffer[8] = alphaNumeric[random.Next(alphaNumeric.Length)];
                buffer[7] = alphaNumeric[random.Next(alphaNumeric.Length)];
                buffer[6] = alphaNumeric[random.Next(alphaNumeric.Length)];
                buffer[5] = alphaNumeric[random.Next(alphaNumeric.Length)];
                buffer[4] = alphaNumeric[random.Next(alphaNumeric.Length)];
                buffer[3] = alphaNumeric[random.Next(alphaNumeric.Length)];
                buffer[2] = alphaNumeric[random.Next(alphaNumeric.Length)];
                buffer[1] = alphaNumeric[random.Next(alphaNumeric.Length)];
                buffer[0] = alphaNumeric[random.Next(alphaNumeric.Length)];
            });
        }

        public string Encode(int id)
        {
            if (id < Constants.AlphaLowerNumericLength) return Constants.AlphaLowerNumeric[id].ToString();

            var s = new StringBuilder();

            while (id > 0)
            {
                s.Insert(0, Constants.AlphaLowerNumeric[id % Constants.AlphaLowerNumericLength]);
                id = id / Constants.AlphaLowerNumericLength;
            }

            return s.ToString();
        }

        public bool IsUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
        public Uri GetUrl(string url)
        {
            Uri.TryCreate(url, UriKind.Absolute, out var uri);

            return uri;
        }
    }
}
