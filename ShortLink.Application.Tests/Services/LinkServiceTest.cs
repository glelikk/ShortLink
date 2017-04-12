using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using ShortLink.Application.Preferences;
using ShortLink.Application.Services;
using ShortLink.DataAccess.Models;
using ShortLink.DataAccess.Repositories;
using Xunit;

namespace ShortLink.Application.Tests.Services
{
    [Collection("Application collection")]
    public class LinkServiceTest
    {
        private readonly ServicesFixture _fixture;
        private Mock<ILinkRepository> _linkRepositoryMock;
        private Mock<IClientRepository> _clientRepositoryMock;
        private Mock<IUniqueIdGenerator> _uniqueIdGeneratorMock;
        private Mock<IClickRepository> _clickRepositoryMock;

        public LinkServiceTest(ServicesFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CreateLink()
        {
            var target = CreateTarget();

            _uniqueIdGeneratorMock.Setup(m => m.GetId()).Returns(_fixture.TestHash);
            _linkRepositoryMock.Setup(m => m.CreateAsync(It.IsAny<Link>())).ReturnsAsync(() => new Link
            {
                Id = _uniqueIdGeneratorMock.Object.GetId(),
                ClientId = 0,
                OriginalLink = _fixture.TestLink
            });
            _clientRepositoryMock.Setup(m => m.FirstOrDefaultAsync(It.IsAny<Expression<Func<Client, bool>>>()))
                .ReturnsAsync(() => new Client
                {
                    Id = 0,
                    ClientKey = _fixture.TestClientKey
                });

            var result = await target.CreateLink(_fixture.TestLink, _fixture.TestClientKey);

            Assert.Equal(_fixture.TestHash, result.Hash);
            Assert.Equal("http://localhost/xxxzzz123", result.ShortLink);
            Assert.Equal(_fixture.TestLink, result.OriginalLink);
            _clientRepositoryMock.Verify(m => m.CreateAsync(It.IsAny<Client>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task GetLink()
        {
            var target = CreateTarget();
            _linkRepositoryMock.Setup(m => m.FindByIdAsync(null)).ReturnsAsync(() => null);
            _linkRepositoryMock.Setup(m => m.FindByIdAsync(It.IsNotNull<string>())).ReturnsAsync(() => new Link
            {
                Id = _fixture.TestHash,
                ClientId = 0,
                OriginalLink = _fixture.TestLink
            });
            var link = await target.GetLink(null);
            Assert.Null(link);
            link = await target.GetLink(_fixture.TestHash);
            Assert.NotNull(link);
            Assert.Equal(_fixture.TestLink, link);

            _clickRepositoryMock.Verify(m => m.CreateAsync(It.IsAny<Click>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task ClientLinks()
        {
            var target = CreateTarget();

            _clientRepositoryMock.Setup(m => m.FirstOrDefaultAsync(It.IsAny<Expression<Func<Client, bool>>>()))
                .ReturnsAsync(() => new Client
                {
                    Id = 0,
                    ClientKey = _fixture.TestClientKey
                });

            _linkRepositoryMock.Setup(m => m.FindAsync(It.IsAny<Expression<Func<Link, bool>>>()))
                .ReturnsAsync(() => new List<Link>
                {
                    new Link
                    {
                        Clicks = new List<Click> {new Click {LinkId = _fixture.TestHash}}
                    },
                    new Link(),
                    new Link()
                });
            var result = await target.ClientLinks(_fixture.TestClientKey);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.Any(x => x.Count > 0));
            _clientRepositoryMock.Verify(m => m.CreateAsync(It.IsAny<Client>()), Times.AtLeastOnce);
        }

        private ILinkService CreateTarget()
        {
            _linkRepositoryMock = new Mock<ILinkRepository>();
            _clientRepositoryMock = new Mock<IClientRepository>();
            _uniqueIdGeneratorMock = new Mock<IUniqueIdGenerator>();
            _clickRepositoryMock = new Mock<IClickRepository>();

            var preferences = new LinkServicePreferences
            {
                CurrentDomain = "http://localhost/"
            };

            return new LinkService(
                _linkRepositoryMock.Object,
                _clientRepositoryMock.Object,
                _uniqueIdGeneratorMock.Object,
                _clickRepositoryMock.Object,
                preferences);
        }
    }
}
