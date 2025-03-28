using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 医保诊疗项目
    /// （所有医保项目，编辑xt_sfxm时自动带出ybdm、wjdm等）
    /// </summary>
    [Table("yb_setup_zlxm")]
    public class MIDiagnosisAndTreatmentItemEntity : IEntity<MIDiagnosisAndTreatmentItemEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int setup_zlxmbh { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>

        public string OrganizeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ybdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string wjdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xmnh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cwnr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jjdw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zfbf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fylb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xdnr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xxzt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? xxqsrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? xxsxrq { get; set; }

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
        public string zt { get; set; }

    }
}
