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
            _repo = new Helper<IUrlRepository>()
                .GetRequiredService() ?? throw new ArgumentNullException();
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
        [InlineData("a733b9f0-d716-40b4-921b-aef79b2f1a04")]
        [InlineData("2f340700-a698-4644-a6a2-069f73b92ad4")]
        public async Task GetUrlAsync_ByPublicKey_Valid(string publicKey)
        {
            var urlPublicKey = new Guid(publicKey);
            var url = await _repo.GetUrlAsync(urlPublicKey);

            Assert.NotNull(url);
            Assert.Equal(urlPublicKey, url.PublicKey);
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
