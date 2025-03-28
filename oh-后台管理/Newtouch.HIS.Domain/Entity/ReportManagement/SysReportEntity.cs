using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_Report")]
    public class SysReportEntity : IEntity<SysReportEntity>
    {
        /// <summary>
        /// 角色主键
        /// </summary>
        [Key]
        public int ReportID { get; set; }

        /// <summary>
        /// 组织机构Id（科室所对应的OrganizeId）
        /// </summary>
        public string HospitalCode { get; set; }
        /// <summary>
        /// 系统代码
        /// </summary>
        public string SystemCode { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; }
        /// <summary>
        /// 报表对应Code
        /// </summary>
        public string ReportCode { get; set; }
        /// <summary>
        /// 报表名称
        /// </summary>
        public string ReportName { get; set; }
        /// <summary>
        /// 拼音
        /// </summary>
        public string PinYin { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string ReportDes { get; set; }
        /// <summary>
        /// 1 功能报表 2查询报表
        /// </summary>
        public int? ReportType { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? StatusTemplateID { get; set; }
        /// <summary>
        /// 父级id
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// 是否是系统 1是 0不是
        /// </summary>
        public int? DirectoryFlag { get; set; }
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
