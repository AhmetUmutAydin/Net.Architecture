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
        TEntity Get(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter = null);
        List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null);
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
    }

}
