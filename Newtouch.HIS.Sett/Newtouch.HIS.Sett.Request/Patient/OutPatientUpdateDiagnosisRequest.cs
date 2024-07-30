using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Patient
{
    /// <summary>
    /// 
    /// </summary>
    public class OutPatientUpdateDiagnosisRequest : RequestBase
    {
        /// <summary>
        /// 门诊号
        /// </summary>
        [Required]
        public string outpatientNo { get; set; }

        /// <summary>
        /// 诊断icd10
        /// </summary>
        [Required]
        public string zdicd10 { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        [Required]
        public string zdmc { get; set; }
    }
}
