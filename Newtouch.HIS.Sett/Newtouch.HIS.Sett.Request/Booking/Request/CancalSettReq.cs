using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Booking.Request
{
    public class CancalSettReq: BookingReqBaseDto
    {
        //[Required(AllowEmptyStrings = false, ErrorMessage = "CardNo is required")]
        //public string CardNo { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Mzh is required")]
        //public string Mzh { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "RegId is required")]
        public string RegId { get; set; }

    }
}
