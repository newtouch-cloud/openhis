using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 住院执行医嘱减负项目表
    /// </summary>
    [Table("zy_zxyzjfxm")]
    public class HospExecuteMedicalOrderReduceBurdenItemEntity : IEntity<HospExecuteMedicalOrderReduceBurdenItemEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int jfxmbh { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? jfbbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? sfxmbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? ypbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? zxrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? zs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? yzsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? bys { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jfbz { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
