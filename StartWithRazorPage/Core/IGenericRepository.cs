using System.Linq.Expressions;

namespace Core
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<T?> GetById(Guid id);
        public Task<IEnumerable<T>> GetAll();
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        public Task<bool> Add(T entity);
        public Task<bool> Remove(Guid id);
        public Task<bool> Upsert(T entity);
    }
}
