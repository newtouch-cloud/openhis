using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    public class fyMainRequest : RequestBase
    {
        public string keyword { get; set; }
        public string cfh { get; set; }
        public string usercode { get; set; }
    }
}
