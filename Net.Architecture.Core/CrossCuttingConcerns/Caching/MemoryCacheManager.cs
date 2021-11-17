using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Net.Architecture.DataAccess.UnitOfWork;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Core.CrossCuttingConcerns.Caching
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _cache;
        private readonly IUnitOfWork _unitOfWork;

        public MemoryCacheManager(IMemoryCache cache, IUnitOfWork unitOfWork)
        {
            _cache = cache;
            _unitOfWork = unitOfWork;
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public async Task<List<T>> GetEntities<T>(Func<Task<List<T>>> func, string key, int duration = 1440) where T : class, IEntity, new()
        {
            if (IsAdded<T>())
            {
                return Get<T>();
            }
            else
            {
                var result = await func();
                Add<T>(result, key, duration);
                return result;
            }
        }

        public async Task<List<T>> GetEntities<T>(int duration = 1440) where T : class, IEntity, new()
        {
            return await GetEntities<T>(GetDefaultValueFunction<T>(), "List", duration);
        }

        public List<T> Get<T>(string key = "List") where T : class, IEntity, new()
        {
            return _cache.Get<List<T>>($"{typeof(T).Name}_{key}");
        }

        public void Add<T>(List<T> data, string key = "List", int duration = 1440) where T : class, IEntity, new()
        {
            _cache.Set($"{typeof(T).Name}_{key}", data, TimeSpan.FromMinutes(duration));
        }

        public bool IsAdded<T>(string key = "List") where T : class, IEntity, new()
        {
            return _cache.TryGetValue($"{typeof(T).Name}_{key}", out _);
        }

        public void Remove<T>(string key = "List") where T : class, IEntity, new()
        {
            _cache.Remove($"{typeof(T).Name}_{key}");
        }

        private Func<Task<List<T>>> GetDefaultValueFunction<T>() where T : class, IEntity, new()
        {
            return () =>
            {
                return _unitOfWork.Repository<T>().GetListAsync(x => x.Status);
            };
        }
    }
}
