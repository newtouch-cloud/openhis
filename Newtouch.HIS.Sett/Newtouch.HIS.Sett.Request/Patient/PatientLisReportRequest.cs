using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request
{
    public class PatientLisReportRequest: RequestBase
    {
        public string mzh { get; set; }
        public string zyh { get; set; }
        public string kh { get; set; }
        public string sqdzt { get; set; }
        public string ywlx { get; set; }
    }

    public class PatientLisReportDetailRequest : RequestBase
    {
        public string sqdh { get; set; }
        public string xmdm { get; set; }
        public string ywlx { get; set; } 
    }
}
