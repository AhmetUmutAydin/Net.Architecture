using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.DataAccess.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class, IEntity, new()
    {
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
        Task<bool> AllAsync(Expression<Func<TEntity, bool>> filter);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter = null);
    }
}
