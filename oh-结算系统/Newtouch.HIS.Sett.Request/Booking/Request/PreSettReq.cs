using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Booking.Request
{
    public class PreSettReq : BookingReqBaseDto
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "CardNo is required")]
        public string CardNo { get; set; }
        /// <summary>
        /// 0: 挂号 1:处方
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "FeeType is required")]
        public string FeeType { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "TotalAmount is required")]
        public decimal TotalAmount { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "OrderNo is required")]
        public string OrderNo { get; set; }
        /// <summary>
        /// 0:自费 1:职工 2:居民 3:离休 11:普通医保
        /// </summary>
        public string PatType { get; set; }
        public string Mzh { get; set; }
        /// <summary>
        /// 预约号 挂号预结比传
        /// </summary>
        public int? BookID { get; set; }
        /// <summary>
        /// 是否当日挂号性质 Y:是 N:否 挂号预结比传
        /// </summary>
        public string IsBooking { get; set; }
    }

    public class SettReq : BookingReqBaseDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "CardNo is required")]
        public string CardNo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mzh is required")]
        public string Mzh { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "TotalAmount is required")]
        public decimal TotalAmount { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "OrderNo is required")]
        public string OrderNo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "PayLsh is required")]
        public string PayLsh { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "PayFee is required")]
        public decimal PayFee { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "PayTime is required")]
        public DateTime PayTime { get; set; }

        public string PatType { get; set; }
    }
}
