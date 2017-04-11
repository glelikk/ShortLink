using System.Data.Entity;
using ShortLink.DataAccess.Models;

namespace ShortLink.DataAccess.Repositories
{
    public class ClientRepository : Repository<Client, int>, IClientRepository
    {
        public ClientRepository(DbContext context) : base(context)
        {
        }
    }
}
