using System;
using System.Collections.Generic;
using System.Text;

namespace url.shortener.data
{
    public class GkamaUrlMetadata : IPublicKeyId, IGkamaUrlMetadata
    {
        public int Id { get; set; }
        public int UrlId { get; set; }
        public Guid PublicKey { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Scheme { get; set; }
        public string Domain { get; set; }
        public int Port { get; set; }
        public string Path { get; set; }
        public string Query { get; set; }
        public string Fragment { get; set; }
    }
}
