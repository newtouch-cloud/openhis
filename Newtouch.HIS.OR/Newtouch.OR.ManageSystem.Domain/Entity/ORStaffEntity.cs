using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.OR.ManageSystem.Domain.Entity
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-06 10:22
    /// 描 述：手术人员
    /// </summary>
    [Table("OR_Staff")]
    public class ORStaffEntity : IEntity<ORStaffEntity>
    {
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        /// <returns></returns>
        public string Code { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }

        /// <summary>
        /// zjm
        /// </summary>
        /// <returns></returns>
        public string zjm { get; set; }

        /// <summary>
        /// 状态     1:有效  0：无效
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }

    }
}