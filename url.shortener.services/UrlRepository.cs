using System;

using Microsoft.Extensions.Logging;

namespace url.shortener.services
{
    public class UrlRepository
    {
        private readonly ILogger<UrlRepository> _logger;

        public UrlRepository(ILogger<UrlRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
