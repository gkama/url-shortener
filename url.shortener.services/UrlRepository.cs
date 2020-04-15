using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                .AsQueryable();
        }

        public async Task<IEnumerable<IGkamaUrl>> GetUrlsAsync()
        {
            return await GetGkamaUrlsQuery()
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

        public async Task<IGkamaUrl> GetUrlAsync(string target)
        {
            return await GetGkamaUrlsQuery()
                .FirstOrDefaultAsync(x => x.Target == target);
        }
    }
}
