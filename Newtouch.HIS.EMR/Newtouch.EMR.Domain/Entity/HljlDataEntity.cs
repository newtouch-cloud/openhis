using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.Entity
{
    [Table("bl_hljldata")]
    public class HljlDataEntity : IEntity<HljlDataEntity>
    {
        [Key]
        public string Id { get; set; }
        public string blId { get; set; }
        public string jlrq { get; set; }
        public string tw { get; set; }
        public string mb { get; set; }
        public string hx { get; set; }
        public string xy { get; set; }
        public string ybhd { get; set; }
        public string cxxdjc { get; set; }
        public string xroyx { get; set; }
        public string hljb { get; set; }
        public string xzjs { get; set; }
        public string pbjyxkt { get; set; }
        public string ycyf { get; set; }
        public string ddyf { get; set; }
        public string qtjh { get; set; }
        public string zkhl { get; set; }
        public string dglb { get; set; }
        public string hlzd { get; set; }
        public string nl { get; set; }
        public string wy { get; set; }
        public string bqhlcontent { get; set; }
        public string hsqm { get; set; }
        public string zt { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string OrganizeId { get; set; }
        public DateTime jlwzsj { get; set; }


    }
}
