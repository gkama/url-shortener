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
        Task<IEnumerable<IGkamaUrl>> GetUnsecureUrlsAsync();
        Task<IGkamaUrl> AddUrlAsync(string target);
        Task<IGkamaUrl> AddUrlAsync(GkamaUrl url);
        Task<IGkamaUrl> AddUrlNotExistAsync(string target);
        Task DeleteUrlAsync(int id);
        Task<IGkamaUrl> ShortenUrlAsync(int id);
        Task<IGkamaUrl> ShortenUrlAsync(string target);
        string ShortenUrl(string target);
        string ShortenUrl(int? id = null);
        string RandomString(int? id = null);
        string RandomString();
        string Encode(int id);
        bool IsUrl(string url);
        Uri GetUrl(string url);
    }
}
