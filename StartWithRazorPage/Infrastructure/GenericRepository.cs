using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected ProjectDBContext _context;
        protected DbSet<T> dbSet;
        protected readonly ILogger _logger;
        public GenericRepository(
            ProjectDBContext context,
            ILogger logger)
        {
            _context = context;
            _logger = logger;
            dbSet = _context.Set<T>();
        }

        async Task<T?> IGenericRepository<T>.GetById(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        async Task<IEnumerable<T>> IGenericRepository<T>.GetAll()
        {
            return await dbSet.ToListAsync();
        }

        IEnumerable<T> IGenericRepository<T>.Find(Expression<Func<T, bool>> expression)
        {
            return dbSet.Where(expression);
        }

        async Task<bool> IGenericRepository<T>.Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        async Task<bool> IGenericRepository<T>.Remove(Guid id)
        {
            var t = await dbSet.FindAsync(id);
            if (t != null)
            {
                dbSet.Remove(t);
                return true;
            }
            return false;
        }

        async Task<bool> IGenericRepository<T>.Upsert(T entity)
        {
            var t = await dbSet.FindAsync(entity);

            if (t == null)
            {
                await dbSet.AddAsync(entity);
                return true;
            }

            dbSet.Update(entity);
            return true;
        }
    }
}
