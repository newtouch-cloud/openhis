using Newtouch.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.DTO.InputDto.Inpatient
{
    [NotMapped]
   public class InpatientMedicineGrantDto: InpatientMedicineGrantEntity
    {
        public int tsl { get; set; }
        public int ktsl { get; set; }
        public int? yzlx { get; set; }
    }
}
