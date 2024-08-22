using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Patient
{
    public class MZhistorybillRequest : BookingReqBaseDto
    {
        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime kssj { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime jssj { get; set; }
    }

    public class MZhistorybillMXRequest : BookingReqBaseDto
    {
        public string jsnm { get; set; }
    }
}
