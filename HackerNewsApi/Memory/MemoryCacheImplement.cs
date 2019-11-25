using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace HackerNewsApi.Memory
{
    public class MemoryCacheImplement<Story> : ICacheInterface<Story>
    {
        #region Private Fields

        private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        private MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(60))
            .SetSlidingExpiration(TimeSpan.FromSeconds(30));

        private ConcurrentDictionary<object, SemaphoreSlim> _locks = new ConcurrentDictionary<object, SemaphoreSlim>();

        #endregion Private Fields

        #region Public Methods

        public async Task<Story> GetOrCreate(object key, Func<Task<Story>> createItem)
        {
            Story cacheEntry;

            if (!_cache.TryGetValue(key, out cacheEntry))
            {
                SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));

                await mylock.WaitAsync();
                try
                {
                    if (!_cache.TryGetValue(key, out cacheEntry))
                    {
                        // Key not in cache, so get data.
                        cacheEntry = await createItem();

                        _cache.Set(key, cacheEntry, cacheEntryOptions);
                    }
                }
                finally
                {
                    mylock.Release();
                }
            }
            return cacheEntry;
        }

        #endregion Public Methods
    }
}