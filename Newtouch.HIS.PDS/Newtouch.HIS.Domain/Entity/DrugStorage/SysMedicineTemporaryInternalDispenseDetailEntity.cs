    using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统药品临时内部发药明细
    /// </summary>
    [Table("xt_yp_ls_nbfymx")]
    public class SysMedicineTemporaryInternalDispenseDetailEntity : IEntity<SysMedicineTemporaryInternalDispenseDetailEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string fymxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

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
        public DateTime? Yxrq { get; set; }

        /// <summary>
        /// 批发价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? Pfj { get; set; }

        /// <summary>
        /// 零售价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? Lsj { get; set; }

        /// <summary>
        /// 批发总额
        /// </summary>
        public decimal? Pfze { get; set; }

        /// <summary>
        /// 零售总额
        /// </summary>
        public decimal? Ljze { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Sl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Slsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Dw { get; set; }

        /// <summary>
        /// 现有库存
        /// </summary>
        public int? Xykz { get; set; }

        /// <summary>
        /// 拆零数
        /// </summary>
        public int? Fyzhyz { get; set; }

        /// <summary>
        /// 拆零数
        /// </summary>
        public int? Lyzhyz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Jqm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Sldxh { get; set; }

        /// <summary>
        /// 现有总库存
        /// </summary>
        public int? Xyzkz { get; set; }

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
