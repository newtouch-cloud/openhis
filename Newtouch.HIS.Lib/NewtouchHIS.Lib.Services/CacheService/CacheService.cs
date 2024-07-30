using Microsoft.Extensions.Caching.Distributed;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NewtouchHIS.Lib.Base.BaseEnum;

namespace NewtouchHIS.Lib.Services.CacheService
{
    public interface ICacheService
    {
        #region 设置缓存 
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">值</param>
        void SetCache(string key, object value);
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">值</param>
        Task SetCacheAsync(string key, object value);

        /// <summary>
        /// 设置缓存
        /// 注：默认过期类型为绝对过期
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">值</param>
        /// <param name="timeout">过期时间间隔</param>
        void SetCache(string key, object value, TimeSpan timeout);

        /// <summary>
        /// 设置缓存
        /// 注：默认过期类型为绝对过期
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">值</param>
        /// <param name="timeout">过期时间间隔</param>
        Task SetCacheAsync(string key, object value, TimeSpan timeout);

        /// <summary>
        /// 设置缓存
        /// 注：默认过期类型为绝对过期
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">值</param>
        /// <param name="timeout">过期时间间隔</param>
        /// <param name="expireType">过期类型</param>  
        void SetCache(string key, object value, TimeSpan timeout, ExpireType expireType);

        /// <summary>
        /// 设置缓存
        /// 注：默认过期类型为绝对过期
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">值</param>
        /// <param name="timeout">过期时间间隔</param>
        /// <param name="expireType">过期类型</param>  
        Task SetCacheAsync(string key, object value, TimeSpan timeout, ExpireType expireType);
        #endregion

        #region 获取缓存

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        string GetCache(string key);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        Task<string> GetCacheAsync(string key);
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        T GetCache<T>(string key);
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        Task<T> GetCacheAsync<T>(string key);

        #endregion

        #region 删除缓存

        /// <summary>
        /// 清除缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        void RemoveCache(string key);

        /// <summary>
        /// 清除缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        Task RemoveCacheAsync(string key);

        #endregion

        #region 刷新缓存
        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        void RefreshCache(string key);
        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        Task RefreshCacheAsync(string key);
        #endregion
    }



    public class CacheService : ICacheService
    {

        readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        protected string BuildKey(string idKey)
        {
            return $"OHIS_{GetType().FullName}_{idKey}";
        }
        public void SetCache(string key, object value)
        {
            string cacheKey = BuildKey(key);
            _cache.SetString(cacheKey, value.ToJson());
        }

        public async Task SetCacheAsync(string key, object value)
        {
            string cacheKey = BuildKey(key);
            await _cache.SetStringAsync(cacheKey, value.ToJson());
        }

        public void SetCache(string key, object value, TimeSpan timeout)
        {
            string cacheKey = BuildKey(key);
            _cache.SetString(cacheKey, value.ToJson(), new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = new DateTimeOffset(DateTime.Now + timeout)
            });
        }

        public async Task SetCacheAsync(string key, object value, TimeSpan timeout)
        {
            string cacheKey = BuildKey(key);
            await _cache.SetStringAsync(cacheKey, value.ToJson(), new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = new DateTimeOffset(DateTime.Now + timeout)
            });
        }

        public void SetCache(string key, object value, TimeSpan timeout, ExpireType expireType)
        {
            string cacheKey = BuildKey(key);
            if (expireType == ExpireType.Absolute)
            {
                //这里没转换标准时间，Linux时区会有问题？
                _cache.SetString(cacheKey, value.ToJson(), new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = new DateTimeOffset(DateTime.Now + timeout)
                });
            }
            else
            {
                _cache.SetString(cacheKey, value.ToJson(), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = timeout
                });
            }
        }

        public async Task SetCacheAsync(string key, object value, TimeSpan timeout, ExpireType expireType)
        {
            string cacheKey = BuildKey(key);
            if (expireType == ExpireType.Absolute)
            {
                //这里没转换标准时间，Linux时区会有问题？
                await _cache.SetStringAsync(cacheKey, value.ToJson(), new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = new DateTimeOffset(DateTime.Now + timeout)
                });
            }
            else
            {
                await _cache.SetStringAsync(cacheKey, value.ToJson(), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = timeout
                });
            }
        }

        public string GetCache(string idKey)
        {
            if (string.IsNullOrEmpty(idKey))
            {
                return null;
            }
            string cacheKey = BuildKey(idKey);
            var cache = _cache.GetString(cacheKey);
            return cache;
        }
        public async Task<string> GetCacheAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            string cacheKey = BuildKey(key);
            var cache = await _cache.GetStringAsync(cacheKey);
            return cache;
        }

        public T GetCache<T>(string key)
        {
            var cache = GetCache(key);
            if (!string.IsNullOrEmpty(cache))
            {
                return cache.ToObject<T>();
            }
            return default(T);
        }

        public async Task<T> GetCacheAsync<T>(string key)
        {
            var cache = await GetCacheAsync(key);
            if (!string.IsNullOrEmpty(cache))
            {
                return cache.ToObject<T>();
            }
            return default(T);
        }

        public void RemoveCache(string key)
        {
            _cache.Remove(BuildKey(key));
        }

        public async Task RemoveCacheAsync(string key)
        {
            await _cache.RemoveAsync(BuildKey(key));
        }

        public void RefreshCache(string key)
        {
            _cache.Refresh(BuildKey(key));
        }

        public async Task RefreshCacheAsync(string key)
        {
            await _cache.RefreshAsync(BuildKey(key));
        }


    }
}
