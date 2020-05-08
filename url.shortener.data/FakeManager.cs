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

            _context.UrlMetadata
                .AddRange(GetFakeGkamaUrlMetadata());

            _context.SaveChanges();
        }

        public IEnumerable<GkamaUrl> GetFakeGkamaUrls()
        {
            return new List<GkamaUrl>()
            {
                new GkamaUrl()
                {
                    Id = 1,
                    PublicKey = new Guid("a733b9f0-d716-40b4-921b-aef79b2f1a04"),
                    Target = "https://www.google.com/maps/place/London,+UK/@51.5287718,-0.2416793,11z/data=!3m1!4b1!4m5!3m4!1s0x47d8a00baf21de75:0x52963a5addd52a99!8m2!3d51.5073509!4d-0.1277583",
                    ShortUrl = "https://gkama.it/london"
                },
                new GkamaUrl()
                {
                    Id = 2,
                    PublicKey = new Guid("2f340700-a698-4644-a6a2-069f73b92ad4"),
                    Target = "https://www.google.com/maps/place/Sydney+NSW,+Australia/@-33.8478796,150.7918936,11z/data=!3m1!4b1!4m5!3m4!1s0x6b129838f39a743f:0x3017d681632a850!8m2!3d-33.8688197!4d151.2092955",
                    ShortUrl = "https://gkama.it/sydney"
                },
                new GkamaUrl()
                {
                    Id = 3,
                    PublicKey = new Guid("f7578931-6b73-4606-98ed-4dbfe5e2b0b8"),
                    Target = "https://www.google.com/maps/place/Sofia,+Bulgaria/@42.695537,23.2539074,12z/data=!3m1!4b1!4m5!3m4!1s0x40aa8682cb317bf5:0x400a01269bf5e60!8m2!3d42.6977082!4d23.3218675"
                },
                new GkamaUrl()
                {
                    Id = 4,
                    PublicKey = new Guid("e6866071-382d-4176-864a-1e7b1aa73b67"),
                    Target = "https://www.google.com/maps/place/Berlin,+Germany/@52.5069704,13.2846512,11z/data=!3m1!4b1!4m5!3m4!1s0x47a84e373f035901:0x42120465b5e3b70!8m2!3d52.5200066!4d13.404954"
                },
                new GkamaUrl()
                {
                    Id = 5,
                    PublicKey = new Guid("ec3171ff-b029-4b73-8994-005a3712d54b"),
                    Target = "https://www.google.com/maps",
                    ShortUrl = "https://gkama.it/5"
                }
            };
        }

        public IEnumerable<GkamaUrlMetadata> GetFakeGkamaUrlMetadata()
        {
            return new List<GkamaUrlMetadata>()
            {
                new GkamaUrlMetadata()
                {
                    Id = 1,
                    UrlId = 5,
                    Domain = "google.com"
                },
                new GkamaUrlMetadata()
                {
                    Id = 2,
                    UrlId = 4,
                    Domain = "google.com"
                }
            };
        }
    }
}
