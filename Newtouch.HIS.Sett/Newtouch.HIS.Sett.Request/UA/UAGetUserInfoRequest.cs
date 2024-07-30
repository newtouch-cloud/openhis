using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request.UA
{
    /// <summary>
    /// 以访问令牌换取用户身份信息
    /// </summary>
    public class UAGetUserInfoRequest : RequestBase
    {
        /// <summary>
        /// 访问令牌
        /// </summary>
        public string access_token { get; set; }
    }
}
