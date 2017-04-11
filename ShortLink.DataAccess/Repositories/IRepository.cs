using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShortLink.DataAccess.Repositories
{
    public interface IRepository<T, in TKey> : IDisposable where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<T> FindByIdAsync(TKey id);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> where);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
