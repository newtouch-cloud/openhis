using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Booking.Request
{
    public class CostOrderReq : BookingReqBaseDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "PatientName is required")]
        public string PatientName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "CardNo is required")]
        public string CardNo { get; set; }
        // [Required(AllowEmptyStrings = false, ErrorMessage = "DiagDay is required")]
        public int? DiagDay { get; set; }
        public DateTime? DiagDate { get; set; }
        /// <summary>
        /// 0 待结 1已结
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "CostType is required")]
        public string CostType { get; set; }
    }

    public class CostOrderDetailReq : BookingReqBaseDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mzh is required")]
        public string Mzh { get; set; }
        /// <summary>
        /// 0 待结 1已结
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "CostType is required")]
        public string CostType { get; set; }
    }

    public class CancalOrderReq : BookingReqBaseDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mzh is required")]
        public string Mzh { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "CardNo is required")]
        public string CardNo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "OrderNo is required")]
        public string OrderNo { get; set; }

    }
}
