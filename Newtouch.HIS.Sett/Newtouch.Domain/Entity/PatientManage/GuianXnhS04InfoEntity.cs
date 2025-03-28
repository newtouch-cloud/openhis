using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity.PatientManage
{
    [Table("Gaxnh_S04")]
    public class GuianXnhS04InfoEntity : IEntity<GuianXnhS04InfoEntity>
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string xnhgrbm { get; set; }
        public string xnhylzh { get; set; }
        public string zyh { get; set; }
        public string inpId { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastModifierCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string zt { get; set; }
    }
}
