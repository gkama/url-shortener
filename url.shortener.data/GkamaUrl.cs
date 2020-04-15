using System;

namespace url.shortener.data
{
    public class GkamaUrl : IPublicKeyId, IGkamaUrl
    {
        public int Id { get; set; }
        public Guid PublicKey { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Target { get; set; }
        public string ShortUrl { get; set; }
    }
}
