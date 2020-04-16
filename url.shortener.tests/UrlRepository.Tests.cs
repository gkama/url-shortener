using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Xunit;

using url.shortener.data;
using url.shortener.services;

namespace url.shortener.tests
{
    public class UrlRepositoryTests
    {
        private readonly IUrlRepository _repo;

        public UrlRepositoryTests()
        {
            var provider = Provider();

            _repo = provider.GetRequiredService<IUrlRepository>() ?? throw new ArgumentNullException();

            provider.GetRequiredService<FakeManager>()
                .UseFakeContext();
        }

        private IServiceProvider Provider()
        {
            var services = new ServiceCollection();

            services.AddScoped<IUrlRepository, UrlRepository>();
            services.AddScoped<FakeManager>();

            services.AddDbContext<UrlContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddLogging();
            services.AddMemoryCache();

            return services.BuildServiceProvider();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetUrlAsync_Valid(int id)
        {
            var url = await _repo.GetUrlAsync(id);

            Assert.NotNull(url);
            Assert.Equal(id, url.Id);
            Assert.NotEqual(Guid.Empty, url.PublicKey);
            Assert.NotNull(url.Target);
        }

        [Theory]
        [InlineData(997)]
        [InlineData(998)]
        [InlineData(999)]
        public async Task GetUrlAsync_NotFound(int id)
        {
            var url = await _repo.GetUrlAsync(id);

            Assert.Null(url);
        }
    }
}
