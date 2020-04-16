using System;
using System.Collections.Generic;
using System.Text;

namespace url.shortener.data
{
    public interface IPublicKeyId
    {
        int Id { get; set; }
        Guid PublicKey { get; set; }
    }
}
