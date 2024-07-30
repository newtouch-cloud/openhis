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
    [Table("mzzy_barcode")]
    public class MzZyBarCodeEntity : IEntity<MzZyBarCodeEntity>
    {
        [Key]
        public string Id { get; set; }
        public string yzh { get; set; }

        public string barcode { get; set; }

        public string OrganizeId { get; set; }

        public string zt { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
    }
}
