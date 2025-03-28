using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Patient
{
    /// <summary>
    /// 更新门诊病人（门诊挂号）就诊状态
    /// </summary>
    public class OutPatientUpdateConsultationStatusRequest : RequestBase
    {
        /// <summary>
        /// 门诊号
        /// </summary>
        [Required]
        public string outpatientNo { get; set; }

        /// <summary>
        /// 就诊标志
        /// </summary>
        [Required]
        public string jiuzhenbz { get; set; }

        /// <summary>
        /// 看诊医生
        /// </summary>
        public string jzys { get; set; }
    }
}
