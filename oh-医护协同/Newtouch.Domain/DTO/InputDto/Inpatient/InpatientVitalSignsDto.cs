using Newtouch.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.InputDto.Inpatient
{
    [NotMapped]
    public class InpatientVitalSignsDto : InpatientVitalSignsEntity
    {
        public string xm { get; set; }
        public string hljb { get; set; }
        public string hlysname { get; set; }
        public string brfoodname { get; set; }
        public string pfqkname { get; set; }
        public string gdhlname { get; set; }
    }
}