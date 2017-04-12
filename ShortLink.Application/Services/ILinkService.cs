using System.Collections.Generic;
using System.Threading.Tasks;
using ShortLink.Application.DTO;

namespace ShortLink.Application.Services
{
    public interface ILinkService
    {
        Task<LinkShortDTO> CreateLink(string link, string clientKey);
        Task<IEnumerable<LinkDTO>> ClientLinks(string clientKey);
        Task<string> GetLink(string shortLink);
    }
}
