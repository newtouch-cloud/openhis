using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// 证照_证照类型
    /// </summary>
    [Table("lic_licence")]
    public class LicLicenceEntity : IEntity<LicLicenceEntity>
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
        /// 所属Id 物资、供应商
        /// </summary>
        public string belongedId { get; set; }

        /// <summary>
        /// 供应商ID、物资ID
        /// </summary>
        public string objectId { get; set; }

        /// <summary>
        /// 证照对象名称
        /// </summary>
        public string objectName { get; set; }

        /// <summary>
        /// 证照名称，证件类型Id
        /// </summary>
        public string licenceTypeId { get; set; }

        /// <summary>
        /// 证照号
        /// </summary>
        public string licenceNo { get; set; }

        /// <summary>
        /// 起效日期
        /// </summary>
        public DateTime? qxrq { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? sxrq { get; set; }

        /// <summary>
        /// 文件地址
        /// </summary>
        public string fileUrl { get; set; }

        /// <summary>
        /// 状态  0:作废；1.有效
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
