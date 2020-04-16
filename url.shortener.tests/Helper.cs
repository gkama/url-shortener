using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using url.shortener.data;
using url.shortener.services;

namespace url.shortener.tests
{
    public class Helper<T>
    {
        public T GetRequiredService()
        {
            var provider = Provider();

            provider.GetRequiredService<FakeManager>()
                .UseFakeContext();

            return provider.GetRequiredService<T>();
        }

        private IServiceProvider Provider()
        {
            var services = new ServiceCollection();

            services.AddScoped<IUrlRepository, UrlRepository>();
            services.AddScoped<FakeManager>();

            services.AddDbContext<UrlContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddLogging();

            return services.BuildServiceProvider();
        }
    }
}
