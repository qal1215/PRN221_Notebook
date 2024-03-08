using Eyeglasses.DAO.DbContext2024;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Eyeglasses.Repository
{
    public class GenericRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly Eyeglasses2024DbContext _context;
        public GenericRepository(Eyeglasses2024DbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        public async Task<TEntity?> GetById(TKey id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task Insert(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity?> GetWithPredecate(Expression<Func<TEntity, bool>> predecate)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predecate);
        }

        public async Task<int> CountAll()
        {
            return await _context.Set<TEntity>().CountAsync();
        }
    }
}
