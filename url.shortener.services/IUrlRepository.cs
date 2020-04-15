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
    }
}
