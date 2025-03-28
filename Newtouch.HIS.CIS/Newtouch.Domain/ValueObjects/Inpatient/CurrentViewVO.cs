using System;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    /// <summary>
    /// 当日查看
    /// </summary>
    public class CurrentViewVO
    {
        public string yzId { get; set; }
        public string clbz { get; set; }
        public DateTime kssj { get; set; }
        public string yznr { get; set; }
        public DateTime? tzsj { get; set; }
        public int yzzt { get; set; }
        public int? zh { get; set; }
        public string lrz { get; set; }
    }
}
