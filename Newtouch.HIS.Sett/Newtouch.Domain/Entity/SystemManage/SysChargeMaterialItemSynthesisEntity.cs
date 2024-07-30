using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统收费材料项目综合
    /// </summary>
    [Table("xt_sfclxmzh")]
    public class SysChargeMaterialItemSynthesisEntity : IEntity<SysChargeMaterialItemSynthesisEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int clzhxmbh { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string clzhxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string clzhxmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 确定自负现金比例，无关这部分的性质（分类自负、自理）   见字段zfxz      注意：当自负比例为负数时，表示定额自负   (例：某材料12300，可报10000,那么自负比例＝-2300)
        /// </summary>
        public decimal? zfbl { get; set; }

        /// <summary>
        /// 一次性材料综合的可报最高限额
        /// </summary>
        public decimal kbfwsx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ybdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? qfbs { get; set; }

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
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

    }
}
