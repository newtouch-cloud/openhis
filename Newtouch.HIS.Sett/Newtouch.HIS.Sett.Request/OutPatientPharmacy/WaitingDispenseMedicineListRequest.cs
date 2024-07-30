using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    public class WaitingDispenseMedicineListRequest : RequestBase
    {
        /// <summary>
        /// 门诊药房 药房部门Code
        /// </summary>
        [Required]
        public string yfbmCode { get; set; }

        [Required]
        public int yxq { get; set; }


    }
}
