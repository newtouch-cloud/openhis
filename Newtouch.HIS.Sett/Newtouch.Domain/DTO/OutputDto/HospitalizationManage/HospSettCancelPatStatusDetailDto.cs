using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    /// <summary>
    /// 住院结算 取消操作 病人 当前状态详情（病人基本信息、项目计费、药品计费、账户收支）
    /// </summary>
    public class HospSettCancelPatStatusDetailDto
    {
        /// <summary>
        /// 住院病人基本信息
        /// </summary>
        public HospSettPatInfoVO SettPatInfo { get; set; }

        /// <summary>
        /// 最后一次（未退的）结算
        /// </summary>
        public HospSettlementEntity LastUnCancelledSett { get; set; }

    }

}
