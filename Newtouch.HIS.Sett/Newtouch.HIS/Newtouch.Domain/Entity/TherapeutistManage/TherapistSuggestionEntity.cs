using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("zls_zljy")]
    public class TherapistSuggestionEntity : IEntity<TherapistSuggestionEntity>
    {
        [Key]
        public string jyId { get; set; }

        public string OrganizeId { get; set; }
        public string blh { get; set; }
        public string brlx { get; set; }
        public string mzzyh { get; set; }
        public string itemCode { get; set; }
        public decimal mczll { get; set; }
        public decimal sl { get; set; }
        public string pc { get; set; }
        public string zxksdm { get; set; }
        public string bz { get; set; }
        public string zhbz { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastModifierCode { get; set; }
        public DateTime LastModifyTime { get; set; }
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bw { get; set; }


    }
}
