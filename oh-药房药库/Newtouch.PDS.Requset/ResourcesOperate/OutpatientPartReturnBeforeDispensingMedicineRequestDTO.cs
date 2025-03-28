using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// （未发药前）部分退药请求报文
    /// </summary>
    public class OutpatientPartReturnBeforeDispensingMedicineRequestDTO : RequestBase
    {
        /// <summary>
        /// 取消明细
        /// </summary>
        [Required]
        public List<ReturnItemData> Items { get; set; }

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
    /// 退药明细内容
    /// </summary>
    public class ReturnItemData
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
        /// 药房部门
        /// </summary>
        public string Yfbm { get; set; }

        /// <summary>
        /// 退数量 （部门单位数量）
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 转化因子 与sl联用  sl*zhyz=最小单位数量
        /// </summary>
        public int zhyz { get; set; }
    }
}