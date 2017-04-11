using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ShortLink.Application.DTO;
using ShortLink.DataAccess.Models;
using ShortLink.DataAccess.Repositories;

namespace ShortLink.Application.Services
{
    public class LinkService : ILinkService, IDisposable
    {
        private readonly ILinkRepository _linkRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IUniqueIdGenerator _idGenerator;

        public LinkService(ILinkRepository linkRepository, IClientRepository clientRepository, IUniqueIdGenerator idGenerator)
        {
            _linkRepository = linkRepository;
            _clientRepository = clientRepository;
            _idGenerator = idGenerator;
        }

        public async Task<string> CreateLink(string link, string clientKey)
        {
            var clientId = (await _clientRepository.FirstOrDefaultAsync(x => x.ClientKey == clientKey))?.Id ?? 0;
            var linkObj = new Link
            {
                ClientId = clientId,
                CreationDate = DateTime.Now,
                OriginalLink = link,
                Id = _idGenerator.GetId()
            };
            await _linkRepository.CreateAsync(linkObj);
            return linkObj.Id;
        }

        public async Task<IEnumerable<LinkDTO>> ClientLinks(string clientKey)
        {
            var clientId = (await _clientRepository.FirstOrDefaultAsync(x => x.ClientKey == clientKey))?.Id ?? 0;
            if (clientId == 0)
            {
                throw new ObjectNotFoundException("Клиент не существует");
            }
            var links = await _linkRepository.FindAsync(x => x.ClientId == clientId);
            var result = links.Select( x => new LinkDTO
            {
                Id = x.Id,
                OriginalLink = x.OriginalLink,
                CreationDate = x.CreationDate,
                Count = x.Clicks.Count
            });
            return result;
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
