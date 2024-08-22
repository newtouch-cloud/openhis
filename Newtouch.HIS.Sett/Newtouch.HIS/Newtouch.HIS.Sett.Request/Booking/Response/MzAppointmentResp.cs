using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Booking.Response
{
    public class MzAppointmentResp
    {
        /// <summary>
        /// 预约号
        /// </summary>
        public string BookID { get; set; }

        public int QueueNo { get; set; }
    }
}
