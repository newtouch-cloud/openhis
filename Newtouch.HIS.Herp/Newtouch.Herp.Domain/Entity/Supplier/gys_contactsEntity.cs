using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// 供应商联系人
    /// </summary>
    [Table("gys_contacts")]
    public class GysContactsEntity : IEntity<GysContactsEntity>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string contactName { get; set; }

		/// <summary>
        /// 供应商ID
        /// </summary>
        public string supplierId { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string telphone { get; set; }

        /// <summary>
        /// 职责
        /// </summary>
        public string duties { get; set; }

        /// <summary>
        /// 固话
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改人	
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
