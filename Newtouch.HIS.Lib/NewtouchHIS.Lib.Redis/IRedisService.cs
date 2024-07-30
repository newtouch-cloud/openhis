using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Lib.Redis
{
    /// <summary>
    /// 暂不用
    /// </summary>
    public interface IRedisService
    {
    
            /// <summary>
            /// 设置缓存
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="key"></param>
            /// <param name="value"></param>
            /// <param name="expireTime"></param>
            /// <returns></returns>
            Task<bool> SetCache<T>(string key, T value, DateTime? expireTime = null);
            Task<bool> SetCache<T>(string key, T value, TimeSpan? expireTime = null);

            /// <summary>
            /// 获取缓存
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="key"></param>
            /// <returns></returns>
            Task<T> GetCache<T>(string key);

            /// <summary>
            /// 根据键精准删除
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            Task<bool> RemoveCache(string key);

            /// <summary>
            /// 模糊删除，左侧匹配，右模糊
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            Task RemoveKeysLeftLike(string key);

            /// <summary>
            /// 获取自增id
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            Task<long> GetIncr(string key);

            /// <summary>
            /// 获取自增id
            /// </summary>
            /// <param name="key"></param>
            /// <param name="expTimeSpan"></param>
            /// <returns></returns>
            Task<long> GetIncr(string key, TimeSpan expTimeSpan);

            #region Hash
            Task<int> SetHashFieldCache<T>(string key, string fieldKey, T fieldValue);
            Task<int> SetHashFieldCache<T>(string key, Dictionary<string, T> dict);
            Task<T> GetHashFieldCache<T>(string key, string fieldKey);
            Task<Dictionary<string, T>> GetHashFieldCache<T>(string key, Dictionary<string, T> dict);
            Task<Dictionary<string, T>> GetHashCache<T>(string key);
            Task<List<T>> GetHashToListCache<T>(string key);
            Task<bool> RemoveHashFieldCache(string key, string fieldKey);
            Task<Dictionary<string, bool>> RemoveHashFieldCache(string key, Dictionary<string, bool> dict);
            #endregion


            #region 列表(List) 操作

            Task<long> ListLeftPushAsync(string redisKey, string redisValue);
            Task<long> ListRightPushAsync(string redisKey, string redisValue);

            #endregion


            #region 有序集合(Sorted Set)

            //Task<bool> SortedSetAddAsync(string redisKey, string redisValue, DateTime time);

            #endregion

            ISubscriber GetSubscriber();
        }

    
}
