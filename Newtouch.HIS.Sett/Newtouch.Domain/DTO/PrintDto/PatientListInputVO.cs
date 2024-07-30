using System;

namespace Newtouch.HIS.Domain.DTO.PrintDto
{
    public class PatientListInputVO
    {
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string flag { get; set; }
        public string sort { get; set; }
        public string adsc { get; set; }
    }
}
