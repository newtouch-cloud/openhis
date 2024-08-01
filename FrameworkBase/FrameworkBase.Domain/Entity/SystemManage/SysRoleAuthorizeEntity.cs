using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.Domain.Entity
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:15
    /// 描 述：角色菜单授权表
    /// </summary>
    [Table("Sys_RoleAuthorize")]
    public class SysRoleAuthorizeEntity : IEntity<SysRoleAuthorizeEntity>
    {
        /// <summary>
        /// 角色授权主键
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 对象主键
        /// </summary>
        /// <returns></returns>
        public string RoleId { get; set; }
        /// <summary>
        /// 项目主键
        /// </summary>
        /// <returns></returns>
        public string ItemId { get; set; }
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