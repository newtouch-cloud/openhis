using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_sfmb")]
    public class SysChargeTemplateEntity : IEntity<SysChargeTemplateEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string sfmbbh { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfmb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfmbmc { get; set; }

        /// <summary>
        /// 0  全部  1 门诊 2 住院
        /// </summary>
        public byte mzzybz { get; set; }

        /// <summary>
        /// 0 全院模版   1 科室模版   2 个人模版   
        /// </summary>
        public byte kffw { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 
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

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        public string py { get; set; }
        public string yfdm { get; set; }
        public string yfmc { get; set; }

    }
}
