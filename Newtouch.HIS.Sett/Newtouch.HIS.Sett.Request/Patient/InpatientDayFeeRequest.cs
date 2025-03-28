using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Patient
{
    public class InpatientDayFeeRequest : BookingReqBaseDto
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime kssj { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime jssj { get; set; }
        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int pageNum { get; set; }
    }
}
