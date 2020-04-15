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

        public FakeManager(UrlContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void UseFakeContext()
        {
            _context.Urls
                .AddRange(GetFakeGkamaUrls());

            _context.SaveChanges();
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
