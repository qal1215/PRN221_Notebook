using Eyeglasses_Repo.DbContext2024;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Eyeglasses_Repo.Repository
{
    public class BaseRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly Eyeglasses2024DBContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(Eyeglasses2024DBContext dBContext)
        {
            _dbContext = dBContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public Eyeglasses2024DBContext DbContext
        {
            get
            {
                return _dbContext;
            }
        }

        public DbSet<TEntity> DbSet
        {
            get
            {
                return _dbSet;
            }
        }

        public async Task<IList<TEntity>> GetPagination(int page, int pageSize = 4)
        {
            return await _dbSet
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetById(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(TKey id)
        {
            var entity = await GetById(id);
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsExist(TKey id)
        {
            return await _dbSet.FindAsync(id) != null;
        }

        public async Task<bool> IsExist(TEntity entity)
        {
            return await _dbSet.ContainsAsync(entity);
        }

        public async Task<int> Count()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.CountAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<TEntity?> FindFirst(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();
        }
    }
}
