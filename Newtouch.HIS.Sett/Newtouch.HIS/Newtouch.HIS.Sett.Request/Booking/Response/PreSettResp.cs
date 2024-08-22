using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Booking.Response
{
    public class PreSettResp
    {
        public decimal ybzf { get; set; }
        public decimal grzf { get; set; }
        /// <summary>
        /// 现金支付合计
        /// </summary>
        public decimal xjzf { get; set; }
    }
    public class SettResp: PreSettResp
    {
        public string RegId { get; set; }

    }
    public class PrescriptionChargeStatusUpdateRequestDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public string cfh { get; set; }
    }

    public class CfOrderVo
    {
        public decimal TotalAmount { get; set; }
        public string Orderstatus { get; set; }
    }
}
