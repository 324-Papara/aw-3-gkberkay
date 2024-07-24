using System.Linq.Expressions;

namespace Para.Data.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task Save();
        Task<TEntity> GetById(long Id);
        Task<TEntity> Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity> Delete(long Id);
        Task<List<TEntity>> GetAll();
        Task<IQueryable<TEntity>> GetAllAsIQueryable();
        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> conditions);
        Task<TEntity> GetWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> GetAllWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

    }
}
