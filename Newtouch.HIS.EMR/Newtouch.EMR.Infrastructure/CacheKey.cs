namespace Newtouch.EMR.Infrastructure
{
    /// <summary>
    /// 缓存Key
    /// </summary>
    public class CacheKey
    {
        /// <summary>
        /// 用户登陆第三方授权token {0} 当前系统AppId {1} 机构代码 {2} 用户
        /// </summary>
        public static string AccessToken = "${0}${1}:accesstoken_{2}";
    }
}