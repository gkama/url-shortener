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
                    Target = "https://www.google.com/maps/place/London,+UK/@51.5287718,-0.2416793,11z/data=!3m1!4b1!4m5!3m4!1s0x47d8a00baf21de75:0x52963a5addd52a99!8m2!3d51.5073509!4d-0.1277583",
                    ShortUrl = "https://gkama.it/london"
                },
                new GkamaUrl()
                {
                    PublicKey = new Guid("2f340700-a698-4644-a6a2-069f73b92ad4"),
                    Target = "https://www.google.com/maps/place/Sydney+NSW,+Australia/@-33.8478796,150.7918936,11z/data=!3m1!4b1!4m5!3m4!1s0x6b129838f39a743f:0x3017d681632a850!8m2!3d-33.8688197!4d151.2092955",
                    ShortUrl = "https://gkama.it/sydney"
                }
            };
        }
    }
}
