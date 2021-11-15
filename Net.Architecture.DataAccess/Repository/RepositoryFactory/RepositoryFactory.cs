using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.DataAccess.Repository.RepositoryFactory
{
    public class RepositoryFactory<TContext> : IRepositoryFactory where TContext : DbContext
    {
        protected Dictionary<Type, object> _repositories;
        //protected readonly DbContext _context;
        protected readonly TContext _context;

        public RepositoryFactory(TContext context)
        {
            _context = context;
        }

        public IBaseRepository<T> CreateBaseRepository<T>() where T : class, IEntity, new()
        {
            throw new NotImplementedException();
        }

        public T CreateDal<T>() where T : class
        {
            _repositories ??= new Dictionary<Type, object>();
            var type = typeof(T);

            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = CreateDalInstance<T>();
                _repositories.Add(type, repositoryInstance);
            }
            return ((T)_repositories[type]);
        }

        private T CreateDalInstance<T>()
        {
            return (T)Activator.CreateInstance(typeof(T), _context);
        }
    }
}
