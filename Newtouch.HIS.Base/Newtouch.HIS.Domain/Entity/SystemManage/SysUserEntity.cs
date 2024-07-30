using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统（登录）用户
    /// </summary>
    [Table("Sys_User")]
    public class SysUserEntity : IEntity<SysUserEntity>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 组织机构Id（顶级，系统配置）
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 登录账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 语言类型：en英文
        /// </summary>
        public string LanguageType { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>
        public int? px { get; set; }

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
        public string zt { get; set; }

    }
}
