using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 机构开放权限配置（菜单、按钮）
    /// </summary>
    [Table("Sys_OrganizeAuthorize")]
    public class SysOrganizeAuthorizeEntity : IEntity<SysOrganizeAuthorizeEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 项目主键
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// 项目类型1-模块2-按钮3-列表
        /// </summary>
        public int ItemType { get; set; }

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
    }

}
