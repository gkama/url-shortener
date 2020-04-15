using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace url.shortener.data
{
    public class FakeManager
    {
        private readonly UrlContext _context;
        private readonly ILogger<FakeManager> _logger;

        public FakeManager(UrlContext context, ILogger<FakeManager> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task UseFakeContextAsync()
        {
            await _context.Urls
                .AddRangeAsync(GetFakeGkamaUrls());

            await _context.SaveChangesAsync();

            _logger.LogInformation($"updated the database with fake gkama urls");
        }

        public IEnumerable<GkamaUrl> GetFakeGkamaUrls()
        {
            return new List<GkamaUrl>()
            {
                new GkamaUrl()
                {
                    Target = "https://google.com",
                    ShortUrl = "https://gkama.it/g"
                },
                new GkamaUrl()
                {
                    Target = "https://google.com",
                    ShortUrl = "https://gkama.it/google"
                }
            };
        }
    }
}
