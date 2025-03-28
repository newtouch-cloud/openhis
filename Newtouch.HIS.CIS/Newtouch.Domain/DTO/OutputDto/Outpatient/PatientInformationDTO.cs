using Newtouch.Domain.BusinessObjects;
using Newtouch.Domain.ValueObjects;
using Newtouch.Infrastructure.EF.Attributes;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto
{
    public class PatientInformationDTO
    {
        public string xzlx { get; set; }
        public string yllb { get; set; }
        public string ydjy { get; set; }
        public string ryzd { get; set; }
        public string ryzdmc { get; set; }
        public string cyzd { get; set; }
        public string cyzdmc { get; set; }
        public string cyqk { get; set; }
        public string ksmc { get; set; }
        public string rqrq { get; set; }
        public string cqrq { get; set; }
        public string kscode { get; set; }
    }
    public class DrugProjectDTO
    {
        [DecimalPrecision(11, 3)]
        public decimal dj { get; set; }
        public string gjybdm { get; set; }
    }
}
