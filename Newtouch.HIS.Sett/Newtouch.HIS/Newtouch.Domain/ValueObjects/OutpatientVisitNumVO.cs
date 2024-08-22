using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class OutpatientVisitNumVO
    {
        //public string isType { get; set; }

        //public string groupDate { get; set; }

        public int? num { get; set; }
    }

    public class OutpatientWeekVisitNumVO
    {
        public string wbegin { get; set; }
        public string wend { get; set; }
        public int num { get; set; }
    }

    public class LastWeekInfo
    {
        public string wbegin { get; set; }
        public string wend { get; set; }
    }

    public class OutpatientSCNumVO
    {
        public decimal? num { get; set; }
    }

    public class PatInfoQuery
    {
        public string type { get; set; }
        public string jzid { get; set; }
        public string jzsj { get; set; }
        public string zymzh { get; set; }
        public int? jsnm { get; set; }
        public string jylsh { get; set; }
        public string jssj { get; set; }
    }
}
