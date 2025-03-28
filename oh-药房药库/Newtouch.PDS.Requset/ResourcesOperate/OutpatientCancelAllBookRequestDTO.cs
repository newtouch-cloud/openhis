using System.ComponentModel.DataAnnotations;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// 取消指定处方所有预定
    /// </summary>
    public class OutpatientCancelAllBookRequestDTO : RequestBase
    {
        /// <summary>
        /// 处方号
        /// </summary>
        [Required]
        public string cfh { get; set; }

        /// <summary>
        /// 发药药房代码
        /// </summary>
        public string fyyf { get; set; }

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