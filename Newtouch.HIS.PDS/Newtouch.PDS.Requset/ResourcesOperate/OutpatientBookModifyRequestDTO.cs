using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// 修改预定请求报文
    /// </summary>
    public class OutpatientBookModifyRequestDTO : RequestBase
    {
        /// <summary>
        /// 药品代码
        /// </summary>
        [Required]
        public List<ItemDetail> Items { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        [Required]
        public string OrganizeId { get; set; }

        /// <summary>
        /// 操作员代码
        /// </summary>
        public string CreatorCode { get; set; }
    }

}