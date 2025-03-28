using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Base.HOSP.Request
{
    public class UpdateMedicinePropertyRequest : RequestBase
    {
        [Required]
        public string sfxmCode { get; set; }
        public string ybbz { get; set; }
        public string ybdm { get; set; }
        public string xnhybdm { get; set; }
        public string yzlx { get; set; }
        public string orgId { get; set; }
    }
}
