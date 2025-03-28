using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Booking.Response
{
    public class ContrastBillResp
    {
        public List<ContrastBill> contrastBill { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int Record { get; set; }
    }
    public class ContrastBill
    {
        public string IDCard { get; set; }
        public string PatientName { get; set; }
        public DateTime? PayTime { get; set; }
        public string TradeStatus { get; set; }
        public string PayLsh { get; set; }
        public string OrderNo { get; set; }
        public decimal Amount { get; set; }
        public decimal PayAmount { get; set; }
    }
}
