using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using url.shortener.data;

namespace url.shortener.services
{
    public class UrlRepository : IUrlRepository
    {
        private readonly UrlContext _context;
        private readonly ILogger<UrlRepository> _logger;

        public UrlRepository(UrlContext context, ILogger<UrlRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private IQueryable<GkamaUrl> GetGkamaUrlsQuery()
        {
            return _context.Urls
                .AsNoTracking()
                .AsQueryable();
        }

        public async Task<IEnumerable<IGkamaUrl>> GetUrlsAsync()
        {
            return await GetGkamaUrlsQuery()
                .OrderBy(x => x.Target)
                .ToListAsync();
        }

        public async Task<IGkamaUrl> GetUrlAsync(int id)
        {
            return await GetGkamaUrlsQuery()
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

        public async Task<IGkamaUrl> AddUrlAsync(string target)
        {
            var url = new GkamaUrl()
            {
                Target = target
            };

            await _context.Urls
                .AddAsync(url);

            return url;
        }

        public async Task<IGkamaUrl> AddUrlAsync(GkamaUrl url)
        {
            await _context.Urls
                .AddAsync(url);

            return url;
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
            var url = new GkamaUrl()
            {
                Target = target
            };

            await _context.Urls
                .AddAsync(url);

            url.ShortUrl = ShortenUrl(url.Id);

            await _context.SaveChangesAsync();

            return url;
        }

        public string ShortenUrl(string target)
        {
            var splitTarget = target.Replace("https://", "")
                .Replace("http://", "")
                .Replace("www.", "")
                .Replace("www", "")
                .Split("/");

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
            if (id < Constants.AlphaNumericLength) return Constants.AlphaLowerNumeric[id].ToString();

            var s = new StringBuilder();

            while (id > 0)
            {
                s.Insert(0, Constants.AlphaLowerNumeric[id % Constants.AlphaNumericLength]);
                id = id / Constants.AlphaNumericLength;
            }

            return s.ToString();
        }
    }
}
