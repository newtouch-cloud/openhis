using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("XT_YP_KCXXK")]
    [Obsolete("不该在此库")]
    public class SysDrugInventoryEntity : IEntity<SysDrugInventoryEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Kcxh { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ksdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Yxq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Kcsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ypkw { get; set; }

        /// <summary>
        /// 0：不控制1：控制
        /// </summary>
        public short Kzbz { get; set; }

        /// <summary>
        /// 已配药未发（领）药的数量
        /// </summary>
        public int Djsl { get; set; }

        /// <summary>
        /// 0：启用1：停用
        /// </summary>
        public short Tybz { get; set; }

        /// <summary>
        /// 来自 表 XT_YP_CRKMXK 
        /// </summary>
        public int Crkmxxh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? jj { get; set; }

        /// <summary>
        /// 对应药房药房的拆零数
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// 产地目录
        /// </summary>
        public int? cd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pc { get; set; }

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
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
