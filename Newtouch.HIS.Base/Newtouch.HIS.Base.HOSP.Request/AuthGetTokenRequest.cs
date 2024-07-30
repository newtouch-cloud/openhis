using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Base.HOSP.Request
{
    /// <summary>
    /// 以关键信息（用户名、密码等）换取访问令牌
    /// </summary>
    public class AuthGetTokenRequest : RequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TopOrganizeId { get; set; }
    }

}
