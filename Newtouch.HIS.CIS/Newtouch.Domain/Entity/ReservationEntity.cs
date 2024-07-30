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
    [Table("mz_yykz")]
    public class ReservationEntity : IEntity<ReservationEntity>
    {
        [Key]
        public string Id { get; set; }
        public string mzh { get; set; }

        public string yyrq { get; set; }

        public string yysj { get; set; }
        public string yyks { get; set; }
        public string yyys { get; set; }
        public string yylxfs { get; set; }
        public string OrganizeId { get; set; }
        public string zt { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
    }
}
