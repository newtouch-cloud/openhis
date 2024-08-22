using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统病案收费大类
    /// </summary>
    [Table("xt_basfdl")]
    [Obsolete("please use the view")]
    public class SysMedicalRecordChargeCategoryEntity : IEntity<SysMedicalRecordChargeCategoryEntity>
    {
        /// <summary>
        /// 大类编号
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int dlbh { get; set; }

        /// <summary>
        /// 大类（对应首页分类）
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 大类名称
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建用户ID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户ID
        /// </summary>
        public string LastModifierCode { get; set; }

    }
}
