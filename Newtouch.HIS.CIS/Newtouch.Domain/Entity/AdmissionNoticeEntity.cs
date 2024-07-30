using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity
{
    [Table("mz_zytzd")]
    public class AdmissionNoticeEntity : IEntity<AdmissionNoticeEntity>
    {
        [Key]
        public string Id { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string nl { get; set; }
        public string rq { get; set; }
        public string mzh { get; set; }
        public string cbzd { get; set; }
        public string ks { get; set; }
        public string cw { get; set; }
        public string jtzz { get; set; }
        public string gzdw { get; set; }
        public string yjj { get; set; }
        public string qzys { get; set; }
        public string OrganizeId { get; set; }
        public string zt { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
    }
}
