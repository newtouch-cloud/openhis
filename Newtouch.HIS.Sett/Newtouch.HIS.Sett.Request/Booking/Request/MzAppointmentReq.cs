using System;
using System.ComponentModel.DataAnnotations;

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
        public DateTime OutDate { get; set; }
        public string IsBooking { get; set; }
    }
}
