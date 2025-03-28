using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    public class WaitingDispenseMedicineDetailListRequest : RequestBase
    {
        /// <summary>
        /// 门诊排药处方明细查询
        /// </summary>
        [Required]
        public string cfh { get; set; }
    }
}
