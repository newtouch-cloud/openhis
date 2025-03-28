using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 角色快捷菜单授权配置
    /// </summary>
    [Table("Sys_RoleShortcutMenu")]
    public class SysRoleShortcutMenuEntity : IEntity<SysRoleShortcutMenuEntity>
    {
        /// <summary>
        /// 快捷菜单角色授权 主键
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 角色主键
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 快捷菜单
        /// </summary>
        public string SCMId { get; set; }

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
        public string zt { get; set; }

        /// <summary>
        /// 医疗机构Id
        /// </summary>
        public string OrganizeId { get; set; }

    }
}
