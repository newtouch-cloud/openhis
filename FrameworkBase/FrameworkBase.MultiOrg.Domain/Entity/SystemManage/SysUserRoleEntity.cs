using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:19
    /// 描 述：用户角色表
    /// </summary>
    [Table("Sys_UserRole")]
    public class SysUserRoleEntity : IEntity<SysUserRoleEntity>
    {
        /// <summary>
        /// 角色授权主键
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }
        /// <summary>
        /// 角色主键
        /// </summary>
        /// <returns></returns>
        public string RoleId { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? px { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
    }
}