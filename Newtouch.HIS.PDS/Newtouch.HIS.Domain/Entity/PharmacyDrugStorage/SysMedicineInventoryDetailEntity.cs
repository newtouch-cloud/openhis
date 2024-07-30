using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统药品盘点信息明细
    /// </summary>
    [Table("xt_yp_pdxxmx")]
    public class SysMedicineInventoryDetailEntity : IEntity<SysMedicineInventoryDetailEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string pdmxId { get; set; }

        /// <summary>
        /// 盘点单ID
        /// </summary>
        public string pdId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Yxq { get; set; }

        /// <summary>
        /// 理论数  最小单位
        /// </summary>
        public int Llsl { get; set; }

        /// <summary>
        /// 实际数  最小单位
        /// </summary>
        public int Sjsl { get; set; }

        /// <summary>
        /// 批发价（部门单位）
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Pfj { get; set; }

        /// <summary>
        /// 零售价（部门单位）
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Lsj { get; set; }

        /// <summary>
        /// 药库批发价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Ykpfj { get; set; }

        /// <summary>
        /// 药库零售价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Yklsj { get; set; }

        /// <summary>
        /// 转化因子
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

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
