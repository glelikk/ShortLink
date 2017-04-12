using ShortLink.DataAccess.Models;

namespace ShortLink.DataAccess.Repositories
{
    /// <summary>
    /// Access to clicks
    /// </summary>
    public interface IClickRepository : IRepository<Click, int>
    {
    }
}
