using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using url.shortener.data;

namespace url.shortener.services
{
    public interface IUrlRepository
    {
        Task<IEnumerable<IGkamaUrl>> GetUrlsAsync();
        Task<IGkamaUrl> GetUrlAsync(int id);
        Task<IGkamaUrl> GetUrlAsync(Guid publicKey);
        Task<IGkamaUrl> GetUrlAsync(string shortUrl);
        Task<IGkamaUrl> GetUrlAsync(string target, string shortUrl);
        Task<IGkamaUrl> AddUrlAsync(string target, string shortUrl = null, bool shorten = false);
        Task<IGkamaUrl> AddUrlAsync(GkamaUrl url);
        Task<IGkamaUrl> ShortenUrlAsync(int id);
        Task<IGkamaUrl> ShortenUrlAsync(string target);
        string ShortenUrl();
        string RandomString();
    }
}
