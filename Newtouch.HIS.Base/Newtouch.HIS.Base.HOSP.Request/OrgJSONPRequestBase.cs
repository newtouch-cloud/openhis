using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Base.HOSP.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class OrgJSONPRequestBase : JSONRequestBase
    {
        /// <summary>
        /// 组织机构Id（已经定位到了具体医院）
        /// </summary>
        [Required]
        public string OrganizeId { get; set; }

    }
}
