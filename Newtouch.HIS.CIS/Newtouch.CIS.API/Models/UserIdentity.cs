using Newtouch.HIS.API.Common.Models;

namespace Newtouch.CIS.API.Models
{
    /// <summary>
    /// 用户身份（根据项目需要 扩展用户身份的字段）
    /// </summary>
    public class UserIdentity : Identity
    {
        /// <summary>
        /// 医疗机构Id
        /// </summary>
        public string OrganizeId { get; set; }
    }
}
