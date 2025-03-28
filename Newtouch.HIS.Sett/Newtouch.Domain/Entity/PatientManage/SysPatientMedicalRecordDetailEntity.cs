using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统病人历史病历明细
    /// </summary>
    [Table("xt_brlsblmx")]
    public class SysPatientMedicalRecordDetailEntity : IEntity<SysPatientMedicalRecordDetailEntity>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 组织机构Id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 附件类型（Word、Excel等） 枚举EnumFileType
        /// </summary>
        public int attachType { get; set; }

        /// <summary>
        /// 病历主表Id
        /// </summary>
        public string blId { get; set; }

        /// <summary>
        /// 附件名称
        /// </summary>
        public string attachName { get; set; }

        /// <summary>
        /// 附件路径
        /// </summary>
        public string attachPath { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 创建用户ID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户ID
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

    }
}
