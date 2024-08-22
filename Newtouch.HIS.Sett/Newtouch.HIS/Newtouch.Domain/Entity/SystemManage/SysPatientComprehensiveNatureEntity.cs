using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统病人性质组合表
    /// </summary>
    [Table("xt_brxzzhb")]
    public class SysPatientComprehensiveNatureEntity : IEntity<SysPatientComprehensiveNatureEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int brxzzhbh { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zbrxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cbrxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int jssx { get; set; }

        /// <summary>
        /// 是否排除已计算项目 1：排除 0 不排除
        /// </summary>
        public string sfpc { get; set; }

        /// <summary>
        /// 1：有效 0 无效
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
