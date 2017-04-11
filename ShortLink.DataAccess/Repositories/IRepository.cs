using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShortLink.DataAccess.Repositories
{
    public interface IRepository<T, in TKey> : IDisposable where T : class
    {
        Task<T> CreateAsync(T entity);
        T Create(T entity);
        Task<T> FindByIdAsync(TKey id);
        T FindById(TKey id);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where);
        T FirstOrDefault(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> where);
        IEnumerable<T> Find(Expression<Func<T, bool>> where);
        Task UpdateAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(T entity);
        void Delete(T entity);
    }
}
