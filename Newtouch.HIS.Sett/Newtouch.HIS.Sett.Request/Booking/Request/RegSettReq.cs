using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Booking.Request
{
    public class RegSettReq: BookingReqBaseDto
    {
        public int? BookID { get; set; }
        public decimal? ScheduId { get; set; }
        public string ghxz { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "CardNo is required")]
        public string CardNo { get; set; }
        public string PayLsh { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "PayFee is required")]
        public decimal? PayFee { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "PayTime is required")]
        public DateTime PayTime { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public string PayWay { get; set; }
        public string IsBooking { get; set; }
    }

    
}
