using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{

    /// <summary>
    /// 系统用法联动
    /// </summary>
    [Table("xt_usageLinkage")]
    public class SysUsageLinkageEntity : IEntity<SysUsageLinkageEntity>
    {
        
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 组织结构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 用法编码
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 项目大类编码
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 状态     1:有效  0：无效
        /// </summary>
        public string zt { get; set; }
    }
}
