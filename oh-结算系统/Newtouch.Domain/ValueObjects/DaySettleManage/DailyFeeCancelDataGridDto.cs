using System;

namespace Newtouch.HIS.Domain.ValueObjects.DaySettleManage
{
    public class DailyFeeCancelDataGridDto
    {
        public string Id { get; set; }
        public DateTime? kssj { get; set; }
        public DateTime? jssj { get; set; }
        public decimal zje { get; set; }
        public decimal xjzf { get; set; }
        public string fpd1 { get; set; }
        public int cnt { get; set;}
    }
}
