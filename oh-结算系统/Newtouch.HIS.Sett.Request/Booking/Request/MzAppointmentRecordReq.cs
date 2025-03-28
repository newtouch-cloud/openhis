using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Booking.Request
{
    public class MzAppointmentRecordReq: BookingReqBaseDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "BookId is required")]
        public string BookId { get; set; }
        public string Lxdh { get; set; }
        
    }

    public class MzAppointmentRecordListReq : BookingReqBaseDto
    {
        public string CardNo { get; set; }
        public string IDCard { get; set; }
        public string RegType { get; set; }
        public DateTime? OutDate { get; set; }
        public string Dept { get; set; }
        public string DeptName { get; set; }
    }
}
