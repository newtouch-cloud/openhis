using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Booking.Request
{
    public class MzAppointmentReq : BookingReqBaseDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "ScheduId is required")]
        public int ScheduId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "CardNo is required")]
        public string CardNo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "ghxz is required")]
        public string ghxz { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "OutDate is required")]
        public DateTime OutDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "IsBooking is required")]
        public string IsBooking { get; set; }
    }
}
