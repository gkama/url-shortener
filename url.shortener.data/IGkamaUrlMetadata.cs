using System;

namespace url.shortener.data
{
    public interface IGkamaUrlMetadata
    {
        Guid PublicKey { get; set; }
        string Target { get; set; }
        string Scheme { get; set; }
        string Domain { get; set; }
        string Port { get; set; }
        string Path { get; set; }
        string Query { get; set; }
        string Fragment { get; set; }
    }
}