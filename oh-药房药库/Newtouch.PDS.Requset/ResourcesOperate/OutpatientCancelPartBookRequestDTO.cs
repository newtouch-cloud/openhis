using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// 取消预定
    /// </summary>
    public class OutpatientCancelPartBookRequestDTO : RequestBase
    {
        /// <summary>
        /// 取消明细
        /// </summary>
        public List<CancelItemData> Items { get; set; }

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

    /// <summary>
    /// 取消预定内容
    /// </summary>
    public class CancelItemData
    {

        /// <summary>
        /// 处方号
        /// </summary>
        [Required]
        public string cfh { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        [Required]
        public string ypdm { get; set; }

        /// <summary>
        /// 药房部门代码
        /// </summary>
        [Required]
        public string yfbmCode { get; set; }

        /// <summary>
        /// 取消数量 （部门单位数量）
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 转化因子 与sl联用  sl*zhyz=最小单位数量
        /// </summary>
        public int zhyz { get; set; }
    }
}