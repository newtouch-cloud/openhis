using Newtouch.HIS.API.Common;
using Newtouch.HIS.Sett.Request.Booking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request
{
    public class DepartmentDTO : BookingReqBaseDto
    {
        public string Dept { get; set; }
        public string DeptName { get; set; }
    }
}
