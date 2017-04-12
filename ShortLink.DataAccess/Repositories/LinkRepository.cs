using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShortLink.DataAccess.Models;

namespace ShortLink.DataAccess.Repositories
{
    public class LinkRepository : Repository<Link, string>, ILinkRepository
    {
        public LinkRepository(DbContext context) : base(context)
        {
        }

        public Link LastOrDefault()
        {
            return Context.Set<Link>().OrderByDescending(x => x.CreationDate).FirstOrDefault();
        }

        public Task<Link> LastOrDefaultAsync()
        {
            return Context.Set<Link>().OrderByDescending(x => x.CreationDate).FirstOrDefaultAsync(); ;
        }

        public override async Task<IEnumerable<Link>> FindAsync(Expression<Func<Link, bool>> where)
        {
            return await Context.Set<Link>().Include(x => x.Clicks).Where(where).ToListAsync();
        }

        public override IEnumerable<Link> Find(Expression<Func<Link, bool>> where)
        {
            return Context.Set<Link>().Include(x => x.Clicks).Where(where).ToList();
        }
    }
}
