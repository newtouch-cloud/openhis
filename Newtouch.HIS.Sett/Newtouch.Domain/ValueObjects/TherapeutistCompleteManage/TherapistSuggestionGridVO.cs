using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.TherapeutistCompleteManage
{
    [NotMapped]
    public class TherapistSuggestionGridVO: TherapistSuggestionEntity
    {
        public string pcmc { get; set; }
        public string itemName { get; set; }
    }
}
