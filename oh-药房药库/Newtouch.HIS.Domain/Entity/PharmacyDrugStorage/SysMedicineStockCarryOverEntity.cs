using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统药品库存结转
    /// </summary>
    [Table("xt_yp_kcjz")]
    public class SysMedicineStockCarryOverEntity : IEntity<SysMedicineStockCarryOverEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string kcId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// 默认值Getdate()
        /// </summary>
        public DateTime? Yxq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Kcsl { get; set; }

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
        /// 进价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Jj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// 默认值Getdate()
        /// </summary>
        public DateTime jssj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime kssj { get; set; }

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

        /// <summary>
        /// 账期
        /// </summary>
        public string zq{ get; set; }
    }
}
