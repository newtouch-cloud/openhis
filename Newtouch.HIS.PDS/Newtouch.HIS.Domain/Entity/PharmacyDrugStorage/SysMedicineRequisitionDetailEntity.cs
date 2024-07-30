using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// xt_yp_sldmx
    /// </summary>
    [Table("xt_yp_sldmx")]
    public class SysMedicineRequisitionDetailEntity : IEntity<SysMedicineRequisitionDetailEntity>
    {
        /// <summary>
        /// 申领单明细Id
        /// </summary>
        [Key]
        public string sldmxId { get; set; }

        /// <summary>
        /// 申领单Id
        /// </summary>
        public string sldId { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 有效日期
        /// </summary>
        public DateTime? Yxrq { get; set; }

        /// <summary>
        /// 转换因子 发药药库的转化因子
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 申领数量 最小单位
        /// </summary>
        public int slsl { get; set; }

        /// <summary>
        /// 已发数量 最小单位
        /// </summary>
        public int yfsl { get; set; }

        /// <summary>
        /// 状态  1：有效  0：无效
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

    }
}
