using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    /// <summary>
    /// 住院结算 病人 当前状态详情（病人基本信息、项目计费、药品计费、账户收支）
    /// </summary>
    public class HosSettPatStatusDetailDto
    {
        /// <summary>
        /// 住院病人基本信息
        /// </summary>
        public HospSettPatInfoVO HospSettPatInfo { get; set; }

        /// <summary>
        /// 大类计费List
        /// </summary>
        public IList<HospFeeClassifyWithDLBO> DLFeeList { get; set; }

        /// <summary>
        /// 交款历史（系统账户收支记录）
        /// </summary>
        public IList<HosPatAccPayVO> ChargeList { get; set; }
        
        /// <summary>
        /// 账户预交金
        /// </summary>
        public decimal zhyjj { get; set; }

        /// <summary>
        /// 结算金额（未除去减免的）
        /// </summary>
        public decimal jsje { get; set; }


    }

}
