using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Booking.Request
{
    public class CardInfoReq: BookingReqBaseDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "IDCard is required")]
        public string IDCard { get; set; }
        public string CardNo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public string Name { get; set; }
       /// <summary>
       /// 病人性质
       /// </summary>
        public string PatType { get; set; }
    }
}
