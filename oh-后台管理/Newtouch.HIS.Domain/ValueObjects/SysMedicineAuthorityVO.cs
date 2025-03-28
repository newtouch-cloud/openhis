using Newtouch.HIS.Domain.Entity.Settlement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    [NotMapped]
    public class SysMedicineAuthorityVO : SysMedicineAuthorityEntity
    {
        public string rel_lxname { get; set; }
        public string rel_name { get; set; }
    }
}
