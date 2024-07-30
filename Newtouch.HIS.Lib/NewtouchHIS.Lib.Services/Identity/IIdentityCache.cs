using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Lib.Services
{
    /// <summary>
    /// API接口授权获取的用户身份信息-接口
    /// </summary>
    public interface IIdentityCache
    {
        UserIdentity identity { get; }

        /// <summary>
        /// 把用户信息设置到缓存中去
        /// </summary>
        /// <param name="info">用户登陆信息</param>
        /// <param name="channel">默认为空</param>
        void SetIdentityInfo(UserIdentity info, string appId);
    }

    /// <summary>
    /// 提供一个空白实现类，具体使用IIdentityCache的时候，会使用其他实现类
    /// </summary>
    public class NullIdentityCache : IIdentityCache
    {
        /// <summary>
        /// 单件实例
        /// </summary>
        public static NullIdentityCache Instance { get; } = new NullIdentityCache();

        public UserIdentity identity => null;

        /// 设置信息（保留为空）
        /// </summary>
        public void SetIdentityInfo(UserIdentity info, string appId)
        {
        }
    }
}
