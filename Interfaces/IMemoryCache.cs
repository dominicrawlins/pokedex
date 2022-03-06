using System;
using System.Threading.Tasks;
public interface IMemoryCache<T>{
    Task<T> GetObject(Func<Task<T>> updateFunction, int cacheTimeMinutes);
}