using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统药品库存结转
    /// </summary>
    [Table("xt_yp_kcjzk")]
    public class SysMedicineStockCarryDownEntity : IEntity<SysMedicineStockCarryDownEntity>
    {
        /// <summary>
        /// 组件
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 药房部门代码
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 默认值Getdate()
        /// </summary>
        public DateTime? Yxq { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public int Kcsl { get; set; }

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
        /// 药库进价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Jj { get; set; }

        /// <summary>
        /// 转化因子
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// 结转时间
        /// </summary>
        public DateTime Jzsj { get; set; }

        /// <summary>
        /// 状态 0：无效  1：有效
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 排序
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
