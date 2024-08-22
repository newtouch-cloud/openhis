using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Booking.Request
{
    public class RegSettReq: BookingReqBaseDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "BookingID is required")]
        public int BookID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "CardNo is required")]
        public string CardNo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "PayLsh is required")]
        public string PayLsh { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "PayFee is required")]
        public decimal? PayFee { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "PayTime is required")]
        public DateTime PayTime { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "IsBooking is required")]
        public string IsBooking { get; set; }
    }
}
