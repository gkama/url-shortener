using System;

namespace url.shortener.data
{
    public interface IGkamaUrlMetadata
    {
        string Target { get; set; }
        Guid PublicKey { get; set; }
        DateTime CreatedAt { get; set; }
        string Scheme { get; set; }
        string Domain { get; set; }
        int Port { get; set; }
        string Path { get; set; }
        string Query { get; set; }
        string Fragment { get; set; }
    }
}