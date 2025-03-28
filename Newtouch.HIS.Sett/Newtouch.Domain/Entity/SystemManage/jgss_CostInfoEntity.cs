using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity.SystemManage
{
    /// <summary>
    /// GRS成本信息
    /// </summary>
    [Table("jgss_cbxx")]
    public class jgss_CostInfoEntity : IEntity<jgss_CostInfoEntity>
    {
        [Key]
        public string Id { get; set; }
        public string srxxId { get; set; }
        public string cblb { get; set; }
        public string kmcode { get; set; }
        public decimal je { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastModifierCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string zt { get; set; }
    }
}
