using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    /// <summary>
    /// 站点收支统计表
    /// </summary>
    public class SiteCostEarningVO
    {
        /// <summary>
        /// 收入信息
        /// </summary>
        public jgssEarningVO srxx { get; set; }
        /// <summary>
        /// 收入信息的 gridlist
        /// </summary>
        public List<jgssEarningGridVO> srxxList { get; set; }
        /// <summary>
        /// 成本信息
        /// </summary>
        public List<jgssCostVO> cbxxList { get; set; }

        public List<jgssAttachmentVO> fjxx { get; set; }
    }
}
