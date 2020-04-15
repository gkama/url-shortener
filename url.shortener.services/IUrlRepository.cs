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
        Task<IGkamaUrl> GetUrlAsync(string target);
    }
}
