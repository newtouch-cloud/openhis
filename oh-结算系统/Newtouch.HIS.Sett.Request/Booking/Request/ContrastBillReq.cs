using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Booking.Request
{
    public class ContrastBillReq : BookingReqBaseDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Begindate is required")]
        public string Begindate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "EndDate is required")]
        public string EndDate { get; set; }
        public string Mzh { get; set; }
        public string CardNo { get; set; }
        public string OrderNo { get; set; }
        public string PayLsh { get; set; }
        //public int PageNo { get; set; }
        //public int PageSize { get; set; }
    }
}
