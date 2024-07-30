using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 住院项目计费 项目 的 详细信息
    /// </summary>
    public class MedicalRecordDiagnosisVO
    {
        /// <summary>
        /// 诊断Id
        /// </summary>
        public string zdId { get; set; }
        /// <summary>
        /// 诊断代码
        /// </summary>
        public string zddm { get; set; }
        /// <summary>
        /// 诊断名称
        /// </summary>
        public string zdmc { get; set; }
    }

}
