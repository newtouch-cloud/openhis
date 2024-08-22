using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request.Patient
{
    /// <summary>
    /// 病历查询
    /// </summary>
    public class PatientMedicalRecordQueryRequest : RequestBase
    {
        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 病历Id（确定唯一病历）
        /// </summary>
        public string blId { get; set; }

    }
}
