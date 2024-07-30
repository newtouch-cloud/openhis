using Newtouch.HIS.Sett.Request.Booking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request
{
    public class DepartmentSchedulingDTO : BookingReqBaseDto
    {
        public int? SchedulingDay { get; set; }
        public string Dept { get; set; }
        public string DeptName { get; set; }
        public string Doctor { get; set; }
        public string DoctorName { get; set; }
        public string RegType { get; set; }
    }

    public class MzKsPbDto : BookingReqBaseDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "OutDate is required")]
        public DateTime OutDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "RegType is required")]
        public string RegType { get; set; }
        public string Dept { get; set; }
        public string DeptName { get; set; }
        public string Doctor { get; set; }
        public string DoctorName { get; set; }
        public int? ScheduId { get; set; }
    }
}
