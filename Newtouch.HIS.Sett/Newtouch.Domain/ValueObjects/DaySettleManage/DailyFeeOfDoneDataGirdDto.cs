using System;

namespace Newtouch.HIS.Domain.ValueObjects.DaySettleManage
{
    public class DailyFeeOfDoneDataGirdDto
    {
        public string Id { get; set; }

        public string fpd1 { get; set; }

        public DateTime kssfsj { get; set; }

        public DateTime jssfsj { get; set; }

        //public string sfsj_fw
        //{
        //    get
        //    {
        //        return $"{kssfsj.ToString("yyyy-MM-dd HH:mm:ss")} / {jssfsj.ToString("yyyy-MM-dd HH:mm:ss")}";
        //    }
        //}

        public DateTime CreateTime { get; set; }

        public decimal zje { get; set; }

        public string zjedx { get; set; }
    }
}
