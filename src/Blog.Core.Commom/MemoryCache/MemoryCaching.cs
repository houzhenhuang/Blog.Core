using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Common.MemoryCache
{
    public class MemoryCaching : ICaching
    {
        private readonly IMemoryCache _memoryCache;
        public MemoryCaching(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public object Get(string cacheKey)
        {
            return _memoryCache.Get(cacheKey);
        }

        public void Set(string cacheKey, object cacheValue)
        {
            _memoryCache.Set(cacheKey, cacheValue, TimeSpan.FromSeconds(7200));
        }
    }
}
