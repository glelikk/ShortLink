using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ShortLink.Application.DTO;
using ShortLink.Application.Preferences;
using ShortLink.DataAccess.Models;
using ShortLink.DataAccess.Repositories;

namespace ShortLink.Application.Services
{
    public class LinkService : ILinkService, IDisposable
    {
        private readonly ILinkRepository _linkRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IUniqueIdGenerator _idGenerator;
        private readonly IClickRepository _clickRepository;
        private readonly LinkServicePreferences _preferences;

        public LinkService(ILinkRepository linkRepository, IClientRepository clientRepository,
            IUniqueIdGenerator idGenerator, IClickRepository clickRepository, LinkServicePreferences preferences)
        {
            _linkRepository = linkRepository;
            _clientRepository = clientRepository;
            _idGenerator = idGenerator;
            _preferences = preferences;
            _clickRepository = clickRepository;
        }

        public async Task<LinkShortDTO> CreateLink(string link, string clientKey)
        {
            var clientId = (await _clientRepository.FirstOrDefaultAsync(x => x.ClientKey == clientKey))?.Id ?? 0;
            if (clientId == 0)
            {
                clientId = await AddClient(clientKey);
            }
            var linkObj = new Link
            {
                ClientId = clientId,
                CreationDate = DateTime.Now,
                OriginalLink = link,
                Id = _idGenerator.GetId()
            };
            await _linkRepository.CreateAsync(linkObj);
            return new LinkShortDTO
            {
                OriginalLink = link,
                Hash = linkObj.Id,
                ShortLink = _preferences.CurrentDomain + linkObj.Id
            };
        }

        public async Task<IEnumerable<LinkDTO>> ClientLinks(string clientKey)
        {
            var clientId = (await _clientRepository.FirstOrDefaultAsync(x => x.ClientKey == clientKey))?.Id ?? 0;
            if (clientId == 0)
            {
                clientId = await AddClient(clientKey);
            }
            var links = await _linkRepository.FindAsync(x => x.ClientId == clientId);
            var result = links.Select( x => new LinkDTO
            {
                ShortLink = _preferences.CurrentDomain + x.Id,
                OriginalLink = x.OriginalLink,
                CreationDate = x.CreationDate,
                Count = x.Clicks.Count
            }).ToList();
            return result;
        }

        public async Task<string> GetLink(string shortLink)
        {
            var link = await _linkRepository.FindByIdAsync(shortLink);
            
            if (link == null)
            {
                return null;
            }
            await _clickRepository.CreateAsync(new Click
            {
                LinkId = link.Id,
                Timestamp = DateTime.Now
            });
            return link.OriginalLink;
        }

        private async Task<int> AddClient(string clientKey)
        {
            var client = new Client { ClientKey = clientKey };
            await _clientRepository.CreateAsync(client);
            return client.Id;
        }

        #region IDisposable Implementatiom

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _linkRepository?.Dispose();
                    _clientRepository?.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
