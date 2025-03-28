using System;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// 申领主信息
    /// </summary>
    public class VApplyBillInfoEntity
    {
        /// <summary>
        /// 单据类型 1-科室申领；2-库房申领
        /// </summary>
        public int applyType { get; set; }

        /// <summary>
        /// 处理过程   0:待处理；1:审核通过；2:审核不通过；3:配送；4:部分完成；5:完成；6:拒收
        /// </summary>
        public int applyProcess { get; set; }

        /// <summary>
        /// 申领单号
        /// </summary>
        public string sldh { get; set; }

        /// <summary>
        /// 出库部门名称
        /// </summary>
        public string ckbmmc { get; set; }

        /// <summary>
        /// 出库部门ID
        /// </summary>
        public string ckbmId { get; set; }

        /// <summary>
        /// 入库部门名称
        /// </summary>
        public string rkbmmc { get; set; }

        /// <summary>
        /// 入库部门ID
        /// </summary>
        public string rkbmId { get; set; }

        /// <summary>
        /// 入库科室名称
        /// </summary>
        public string rkksmc { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatorName { get; set; }
    }
}