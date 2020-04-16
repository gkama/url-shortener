using System;
using System.Threading.Tasks;

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
                .GetRequiredService() ?? throw new ArgumentNullException(nameof(IUrlRepository));
        }

        [Fact]
        public async Task GetUrlsAsync()
        {
            var urls = await _repo.GetUrlsAsync();

            Assert.NotEmpty(urls);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetUrlAsync_ById(int id)
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
        public async Task GetUrlAsync_ByPublicKey(string publicKey)
        {
            var urlPublicKey = new Guid(publicKey);

            var url = await _repo.GetUrlAsync(urlPublicKey);

            Assert.NotNull(url);
            Assert.Equal(urlPublicKey, url.PublicKey);
            Assert.NotNull(url.Target);
        }

        [Theory]
        [InlineData("https://google.com")]
        public async Task GetUrlAsync_ByTarget(string target)
        {
            var url = await _repo.GetUrlAsync(target);

            Assert.NotNull(url);
            Assert.NotNull(url.Target);
            Assert.Equal(target, url.Target);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData("a733b9f0-d716-40b4-921b-aef79b2f1a04")]
        [InlineData("2f340700-a698-4644-a6a2-069f73b92ad4")]
        public async Task GetUrlAsync_PublicKeyId(object id)
        {
            var publicKeyId = id is string
                ? new Guid(id.ToString())
                : id;

            var url = await _repo.GetUrlAsync(publicKeyId);

            Assert.NotNull(url);
            Assert.NotEqual(Guid.Empty, url.PublicKey);
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
