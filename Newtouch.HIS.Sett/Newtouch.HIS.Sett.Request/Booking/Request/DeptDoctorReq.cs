using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Booking.Request
{
    public class DeptDoctorReq : BookingReqBaseDto
    {
        public string Dept { get; set; }
        public string DeptName { get; set; }
        public string Doctor { get; set; }
        public string DoctorName { get; set; }
    }
}
