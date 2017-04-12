using System.Threading.Tasks;
using ShortLink.DataAccess.Models;

namespace ShortLink.DataAccess.Repositories
{
    /// <summary>
    /// Access to links
    /// </summary>
    public interface ILinkRepository : IRepository<Link, string>
    {
        Link LastOrDefault();
        Task<Link> LastOrDefaultAsync();
    }
}
