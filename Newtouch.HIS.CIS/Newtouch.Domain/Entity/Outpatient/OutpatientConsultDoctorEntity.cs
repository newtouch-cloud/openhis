using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity.Outpatient
{
    [Table("mz_zsys")]
    public class OutpatientConsultDoctorEntity : IEntity<OutpatientConsultDoctorEntity>
    {
        [Key]
        public int Id { get; set; }
        public string OrganizeId { get; set; }
        public string gh { get; set; }
        public string zsCode { get; set; }
        public DateTime rq { get; set; }
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
        public int px { get; set; }
    }
}
