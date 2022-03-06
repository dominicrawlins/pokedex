using System;
using System.Threading.Tasks;

public class MemoryCache<T> : IMemoryCache<T>{
    private static DateTime cacheExpiry;
    private static T cachedObject;

    public MemoryCache(){
        cacheExpiry = DateTime.MinValue;
    }

    public async Task<T> GetObject(Func<Task<T>> updateFunction, int cacheTimeMinutes){
        if(cacheExpiry < DateTime.UtcNow){
            cachedObject = await updateFunction();
            cacheExpiry = DateTime.UtcNow.AddMinutes(cacheTimeMinutes);
        }

        return cachedObject;
    }
}