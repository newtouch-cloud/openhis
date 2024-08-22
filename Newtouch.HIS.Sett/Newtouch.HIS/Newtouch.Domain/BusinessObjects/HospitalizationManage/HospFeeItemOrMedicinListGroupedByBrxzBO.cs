using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    /// <summary>
    /// 出院结算 项目/药品 计费 列表 按 病人性质 分组结算
    /// </summary>
    public class HospFeeItemOrMedicinListGroupedByBrxzBO
    {
        /// <summary>
        /// 病人性质（组合表中）
        /// </summary>
        public SysPatientComprehensiveNatureEntity brxzzhEntity { get; set; }

        /// <summary>
        /// 病人性质
        /// </summary>
        public SysPatientNatureEntity brxzEntity { get; set; }

        /// <summary>
        /// 项目 计费项目 List
        /// </summary>
        public IList<HospFeeItemOrMedicinDetailBO> FeeList { get; set; }

        /// <summary>
        /// 记账超额部分（要转入自负）
        /// </summary>
        public decimal jzce { get; set; }

        /// <summary>
        /// 计算后的起付金额（要转入自负）
        /// </summary>
        public decimal qfje { get; set; }

    }
}
