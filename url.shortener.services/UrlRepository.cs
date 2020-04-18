using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        public async Task<IGkamaUrl> ShortenUrlAsync(int id)
        {
            var url = await GetUrlAsync(id);

            url.ShortUrl = string.IsNullOrWhiteSpace(url.ShortUrl)
                ? $"https://gkama.it/{url.Id}"
                : throw new Exception($"url='{JsonSerializer.Serialize(url)}'");

            await _context.SaveChangesAsync();

            return url;
        }

        public async Task<IGkamaUrl> ShortenUrlAsync(GkamaUrl url)
        {
            if (await GetUrlAsync(url.Id) != null)
                throw new UrlException(HttpStatusCode.BadRequest,
                    $"url already exists. url='{JsonSerializer.Serialize(url)}'");

            //TODO: update algorithm

            return url;
        }
    }
}
