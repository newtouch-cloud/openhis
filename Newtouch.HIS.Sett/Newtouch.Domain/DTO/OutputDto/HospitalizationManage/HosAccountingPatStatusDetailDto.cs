using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    /// <summary>
    /// 
    /// </summary>
    public class HosAccountingPatStatusDetailDto
    {
        /// <summary>
        /// 此次住院 已 记账 计划
        /// </summary>
        public IList<HospAccountingPlanVO> jzjhList { get; set; }

        /// <summary>
        /// 住院病人基本信息
        /// </summary>
        public HospPatientInfoVO patInfo { get; set; }

        /// <summary>
        /// 中途结算日期
        /// </summary>
        public DateTime? zjrq { get; set; }

    }
}
