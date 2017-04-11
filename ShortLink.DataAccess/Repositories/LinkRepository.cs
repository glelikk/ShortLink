using System.Data.Entity;
using ShortLink.DataAccess.Models;

namespace ShortLink.DataAccess.Repositories
{
    public class LinkRepository : Repository<Link, string>, ILinkRepository
    {
        public LinkRepository(DbContext context) : base(context)
        {
        }
    }
}
