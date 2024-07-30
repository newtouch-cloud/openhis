using Newtouch.Core.Common;
using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Base.HOSP.Request
{
    public class MedicineQueryRequest:RequestBase
    {
        public string orgId { get; set; }
        public string xmbm { get; set; }
        public string xmmc { get; set; }
        public string ck_kc { get; set;}
        public Pagination pagination { get; set; }
    }
}
