
namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 当前科室门诊有效时长
    /// </summary>
    public class MzActiveDurationVO
    {
        /// <summary>
        /// 科室
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 门诊有效时长
        /// </summary>
        public int mz { get; set; }

        /// <summary>
        /// 急诊有效时长
        /// </summary>
        public int jz { get; set; }
    }
}
