using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity.SystemManage
{
    [Table("jgss_fjxx")]
    public class jgss_AttachmentInfoEntity : IEntity<jgss_AttachmentInfoEntity>
    {
        [Key]
        public string Id { get; set; }
        public string srxxId { get; set; }
        /// <summary>
        /// 附件名称
        /// </summary>
        public string fjmc { get; set; }
        /// <summary>
        /// 附件类型
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 附件类型
        /// </summary>
        public string fjPath { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastModifierCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string zt { get; set; }
    }
}
