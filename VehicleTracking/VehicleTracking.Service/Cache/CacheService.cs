using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Service.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<CacheService> _logger;

        public CacheService(IDistributedCache distributedCache, ILogger<CacheService> logger)
        {
            _distributedCache = distributedCache;
            _logger = logger;
        }

        public async Task<List<T>> GetMany<T>(string schema, string[] keys)
        {
            var result = new List<T>();

            foreach (var key in keys)
            {
                var value = await _distributedCache.GetStringAsync(string.Format("{0}:{1}", schema, key));

                if (value != null)
                {
                    result.Add(JsonConvert.DeserializeObject<T>(value));
                }
            }

            return result;
        }

        public async Task<string> Get(string schema, string key)
        {
            return await _distributedCache.GetStringAsync(string.Format("{0}:{1}", schema, key));
        }

        public async Task<T> Get<T>(string schema, string key)
        {
            var value = await _distributedCache.GetStringAsync(string.Format("{0}:{1}", schema, key));

            if (value != null)
            {
                return await Task.FromResult<T>(JsonConvert.DeserializeObject<T>(value));
            }

            return default;
        }

        public async Task<T> GetAndRemove<T>(string schema, string key)
        {
            var cacheKey = string.Format("{0}:{1}", schema, key);
            var value = await _distributedCache.GetStringAsync(cacheKey);

            if (value != null)
            {
                var result = await Task.FromResult<T>(JsonConvert.DeserializeObject<T>(value));
                await _distributedCache.RemoveAsync(cacheKey);

                return result;
            }

            return default;
        }

        public async Task<bool> Refresh(string schema, string key)
        {
            var cacheKey = string.Format("{0}:{1}", schema, key);
            await _distributedCache.RefreshAsync(cacheKey);

            return true;
        }

        public async Task Remove(string schema, string key)
        {
            var cacheKey = string.Format("{0}:{1}", schema, key);

            await _distributedCache.RemoveAsync(cacheKey);
        }

        public async Task<bool> Store<T>(string schema, string key, T data, double? seconds = null)
        {
            try
            {
                if (seconds != null && seconds.HasValue)
                {
                    await _distributedCache.SetStringAsync(string.Format("{0}:{1}", schema, key),
                        JsonConvert.SerializeObject(data), new DistributedCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(seconds.Value)
                        });
                }
                else
                {
                    // Save data in cache.
                    await _distributedCache.SetStringAsync(string.Format("{0}:{1}", schema, key),
                        JsonConvert.SerializeObject(data));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CacheService - Store error: ", ex);
                return false;
            }

            return true;
        }
    }
}
