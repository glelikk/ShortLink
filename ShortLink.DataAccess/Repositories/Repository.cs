using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShortLink.DataAccess.Repositories
{
    public class Repository<T, TKey> : IRepository<T, TKey> where T : class
    {
        private readonly DbContext _context;
        private bool _disposed;

        public Repository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }
        
        public virtual async Task<T> CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> FindByIdAsync(TKey id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(where);
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> where)
        {
            return await _context.Set<T>().Where(where).ToListAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        #region IDisposable implementation

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
