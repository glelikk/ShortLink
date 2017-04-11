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
        protected readonly DbContext Context;
        private bool _disposed;

        public Repository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            Context = context;
        }
        
        public virtual async Task<T> CreateAsync(T entity)
        {
            Context.Set<T>().Add(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public virtual T Create(T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
            return entity;
        }

        public virtual async Task<T> FindByIdAsync(TKey id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public virtual T FindById(TKey id)
        {
            return Context.Set<T>().Find(id);
        }

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(where);
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> where)
        {
            return Context.Set<T>().FirstOrDefault(where);
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> where)
        {
            return await Context.Set<T>().Where(where).ToListAsync();
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> where)
        {
            return Context.Set<T>().Where(where).ToList();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public virtual void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            await Context.SaveChangesAsync();
        }

        public virtual void Delete(T entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        #region IDisposable implementation

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
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
