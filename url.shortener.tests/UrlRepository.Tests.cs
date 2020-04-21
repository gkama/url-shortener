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
        [InlineData("https://gkama.it/london")]
        public async Task GetUrlAsync_ByShortUrl(string shortUrl)
        {
            var url = await _repo.GetUrlAsync(shortUrl);

            Assert.NotNull(url);
            Assert.NotNull(url.ShortUrl);
            Assert.Equal(shortUrl, url.ShortUrl);
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

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public async Task ShortenUrlAsync_ById(int id)
        {
            var url = await _repo.ShortenUrlAsync(id);

            Assert.NotNull(url);
            Assert.NotNull(url.Target);
            Assert.NotNull(url.ShortUrl);
        }

        [Theory]
        [InlineData("https://test-url-that-im-trying-to-shorten-1.com")]
        [InlineData("https://test-url-that-im-trying-to-shorten-2.com")]
        [InlineData("https://test-url-that-im-trying-to-shorten-3.com")]
        public async Task ShortenUrlAsync_ByTarget(string target)
        {
            var url = await _repo.ShortenUrlAsync(target);

            Assert.NotNull(url);
            Assert.NotNull(url.Target);
            Assert.NotNull(url.ShortUrl);
        }

        [Theory]
        [InlineData("https://google.com/test/ing")]
        public void ShortenUrl_ByTarget(string target)
        {
            var shortUrl = _repo.ShortenUrl(target);

            Assert.NotNull(shortUrl);
        }

        [Theory]
        [InlineData(5)]
        public async Task AddShortUrl_AlreadyExists_ViolatesUniqueness(int id)
        {
            var url = new GkamaUrl()
            {
                Id = id,
                Target = "https://www.google.com/maps",
                ShortUrl = "https://gkama.it/5"
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => _repo.AddUrlAsync(url));
        }
    }
}
