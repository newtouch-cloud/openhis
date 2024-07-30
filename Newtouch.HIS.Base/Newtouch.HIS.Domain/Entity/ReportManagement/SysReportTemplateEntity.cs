using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_ReportTemplate")]
    public class SysReportTemplateEntity : IEntity<SysReportTemplateEntity>
    {
        /// <summary>
        /// 角色主键
        /// </summary>
        [Key]
        public int TemplateID { get; set; }

        /// <summary>
        /// 组织机构Id（科室所对应的OrganizeId）
        /// </summary>
        public string HospitalCode { get; set; }
        /// <summary>
        /// 系统代码
        /// </summary>
        public string SystemCode { get; set; }
        /// <summary>
        /// 报表对应Code
        /// </summary>
        public string ReportCode { get; set; }
        /// <summary>
        /// 报表名称
        /// </summary>
        public string ReportNameDes { get; set; }
        /// <summary>
        /// xml报表内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 是否启用 1启用 0停用
        /// </summary>
        public int ReportStatus { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int zt { get; set; }

    }
}
