using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 系统诊断
    /// </summary>
    [NotMapped]
    public class SysDiagnosisVO : SysDiagnosisEntity
    {
        /// <summary>
        /// 是否允许编辑
        /// </summary>
        public bool AllowEdit { get; set; }
    }
}
