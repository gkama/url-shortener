using System;
using System.Collections.Generic;
using System.Text;

namespace url.shortener.data
{
    public interface IGkamaUrl
    {
        int Id { get; set; }
        Guid PublicKey { get; set; }
        DateTime CreatedAt { get; set; }
        string Target { get; set; }
        string ShortUrl { get; set; }
    }
}
