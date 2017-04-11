using System.Threading.Tasks;
using ShortLink.DataAccess.Models;

namespace ShortLink.DataAccess.Repositories
{
    public interface ILinkRepository : IRepository<Link, string>
    {
        Link LastOrDefault();
        Task<Link> LastOrDefaultAsync();
    }
}
