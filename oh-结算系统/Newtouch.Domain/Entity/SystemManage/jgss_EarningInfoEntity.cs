using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity.SystemManage
{
    /// <summary>
    /// GRS收入信息
    /// </summary>
    [Table("jgss_srxx")]
   public class jgss_EarningInfoEntity : IEntity<jgss_EarningInfoEntity>
    {
        [Key]
        public string Id { get; set; }
        public string siteId { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        /// <summary>
        /// 机构实收
        /// </summary>
        public decimal jgss { get; set; }
        /// <summary>
        /// grs实收
        /// </summary>
        public decimal grsss { get; set; }
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
        public string shzt { get; set; }
        public decimal mzsfzje { get; set; }
        public decimal zysfzje { get; set; }
        /// <summary>
        /// 总收入
        /// </summary>
        public decimal zsr { get; set; }
        public string shr { get; set; }
        public DateTime? shsj { get; set; }
    }
}
