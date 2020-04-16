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
                    PublicKey = new Guid("a733b9f0-d716-40b4-921b-aef79b2f1a04"),
                    Target = "https://google.com",
                    ShortUrl = "https://gkama.it/g"
                },
                new GkamaUrl()
                {
                    PublicKey = new Guid("2f340700-a698-4644-a6a2-069f73b92ad4"),
                    Target = "https://google.com",
                    ShortUrl = "https://gkama.it/google"
                }
            };
        }
    }
}
