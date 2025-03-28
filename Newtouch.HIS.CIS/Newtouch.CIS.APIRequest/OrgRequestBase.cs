using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.CIS.APIRequest
{
    public class OrgRequestBase: JSONRequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string OrganizeId { get; set; }
    }
}
