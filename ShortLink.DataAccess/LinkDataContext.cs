using System.Data.Entity;
using ShortLink.DataAccess.Models;

namespace ShortLink.DataAccess
{
    public class LinkDataContext : DbContext
    {
        public LinkDataContext() : base("LinkDB")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<LinkDataContext>());
        }

        public DbSet<Click> Clicks { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Link> Links { get; set; }
    }
}
