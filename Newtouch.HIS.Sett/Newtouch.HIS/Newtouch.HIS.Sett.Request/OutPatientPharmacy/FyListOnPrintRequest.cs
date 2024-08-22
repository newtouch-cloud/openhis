using Newtouch.HIS.API.Common;
using System;

namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    public class FyListOnPrintRequest : RequestBase
    {
        public string ksdm { get; set; }
        public string fph { get; set; }
        public string xm { get; set; }
        public string kh { get; set; }
        public DateTime kssj { get; set; }
        public DateTime jssj { get; set; }
    }
}
