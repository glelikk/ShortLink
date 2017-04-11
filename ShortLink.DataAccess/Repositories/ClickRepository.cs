using System.Data.Entity;
using ShortLink.DataAccess.Models;

namespace ShortLink.DataAccess.Repositories
{
    public class ClickRepository : Repository<Click, int>, IClickRepository
    {
        public ClickRepository(DbContext context) : base(context)
        {
        }
    }
}
