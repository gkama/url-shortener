using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace url.shortener.data
{
    public class FakeManager
    {
        private readonly UrlContext _context;

        public FakeManager(UrlContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task UseFakeContextAsync()
        {
            await _context.Urls
                .AddRangeAsync(GetFakeGkamaUrls());
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
