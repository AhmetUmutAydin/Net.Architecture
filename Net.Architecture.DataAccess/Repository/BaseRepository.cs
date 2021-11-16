using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Net.Architecture.DataAccess.Helpers;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.DataAccess.Repository
{
    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity> where TEntity : class, IEntity, new() where TContext : DbContext
    {
        protected readonly TContext _context;
        protected DbSet<TEntity> _entities;

        public BaseRepository(TContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _entities.FirstOrDefaultAsync(filter);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _entities.SingleOrDefaultAsync(filter);
        }

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? _entities : _entities.Where(filter);
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? await _entities.ToListAsync() : await _entities.Where(filter).ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _entities.AnyAsync(filter);
        }

        public async Task<bool> AllAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _entities.AllAsync(filter);
        }

        public async Task AddAsync(TEntity entity)
        {
            SetAuditable.SetAuditablCreate<TEntity>(ref entity);
            await _entities.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _entities.AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            SetAuditable.SetAuditablUpdate<TEntity>(ref entity);
            _entities.Update(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                entity.Status = false;
            _entities.UpdateRange(entities);
        }

        public void Delete(TEntity entity)
        {
            SetAuditable.SetAuditablUpdate<TEntity>(ref entity);
            entity.Status = false;
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
