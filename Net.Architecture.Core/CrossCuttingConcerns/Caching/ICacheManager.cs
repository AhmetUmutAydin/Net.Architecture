using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Architecture.Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        Task<List<T>> GetEntities<T>(Func<Task<List<T>>> func, string key, int duration = 1440) where T : class, IEntity, new();
        Task<List<T>> GetEntities<T>(int duration = 1440) where T : class, IEntity, new();
        List<T> Get<T>(string key) where T : class, IEntity, new();
        object Get(string key);
        void Add<T>(List<T> data, string key = "List", int duration = 1440) where T : class, IEntity, new();
        bool IsAdded<T>(string key = "List") where T : class, IEntity, new();
        void Remove<T>(string key = "List") where T : class, IEntity, new();
    }
}
