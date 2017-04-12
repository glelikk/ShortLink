using System.Collections.Generic;
using System.Threading.Tasks;
using ShortLink.Application.DTO;

namespace ShortLink.Application.Services
{
    /// <summary>
    /// ShortLink generation service
    /// </summary>
    public interface ILinkService
    {
        /// <summary>
        /// Create short link
        /// </summary>
        /// <param name="link">Original URL</param>
        /// <param name="clientKey">User client key</param>
        /// <returns></returns>
        Task<LinkShortDTO> CreateLink(string link, string clientKey);
        /// <summary>
        /// Get links created by user
        /// </summary>
        /// <param name="clientKey">User client key</param>
        /// <returns></returns>
        Task<IEnumerable<LinkDTO>> ClientLinks(string clientKey);
        /// <summary>
        /// Get full link
        /// </summary>
        /// <param name="shortLink">Short link hash</param>
        /// <returns></returns>
        Task<string> GetLink(string shortLink);
    }
}
