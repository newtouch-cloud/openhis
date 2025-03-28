using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Booking.Request
{
    public class RegisterReq: BookingReqBaseDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "IDCard is required")]
        public string IDCard { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "PatName is required")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "PatType is required")]
        public string PatType { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "BithDay is required")]
        public DateTime BithDay { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
        /// <summary>
        /// 医保卡
        /// </summary>
        public string CardNo { get; set; }
    }
}
