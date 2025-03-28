using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 住院项目计费 项目 的 详细信息
    /// </summary>
    public class MedicalRecordOperationVO
    {
        /// <summary>
        /// 手术ID
        /// </summary>
        public string ssId { get; set; }
        /// <summary>
        /// 诊断代码
        /// </summary>
        public string ssdm { get; set; }
        /// <summary>
        /// 诊断名称
        /// </summary>
        public string ssmc { get; set; }
    }

}
