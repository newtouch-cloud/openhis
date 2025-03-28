using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_sfxm_dyy")]
    [Obsolete("please use the view")]
    public class SysChargeItemMultiLanguageEntity : IEntity<SysChargeItemMultiLanguageEntity>
    {
        /// <summary>
        /// 收费项目多语言编号
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sfxmdyybh { get; set; }

        /// <summary>
        /// 收费项目
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// 收费项目名称
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 收费项目繁体名称
        /// </summary>
        public string sfxmmcFanti { get; set; }

        /// <summary>
        /// 收费项目英文名称
        /// </summary>
        public string sfxmmcEnglish { get; set; }

        /// <summary>
        /// 收费项目日文名称
        /// </summary>
        public string sfxmmcJpan { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 建档人员
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 建档日期
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 变更人员
        /// </summary>
        public DateTime? LastModifierCode { get; set; }

        /// <summary>
        /// 变更日期
        /// </summary>
        public string LastModifyTime { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }

    }
}
