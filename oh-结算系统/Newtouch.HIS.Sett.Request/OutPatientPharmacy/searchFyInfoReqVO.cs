using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    public class searchFyInfoReqVO : RequestBase
    {
        public string begindate { get; set; }
        public string enddate { get; set; }
        public string kh { get; set; }
        public string xm { get; set; }
        public string fph { get; set; }
        public string yfbmcode { get; set; }
    }
}
