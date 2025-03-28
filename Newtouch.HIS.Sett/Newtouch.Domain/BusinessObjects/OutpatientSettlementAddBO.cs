using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    /// <summary>
    /// 新增结算 所需
    /// </summary>
    public class OutpatientSettlementAddBO
    {
        /// <summary>
        /// 
        /// </summary>
        public IList<int> cfnmList { get; set; }
        /// <summary>
        /// 项目未关联处方时，门诊项目主键xmnm List
        /// </summary>
        public IList<int> xmnmList { get; set; }
        /// <summary>
        /// （产生新结算）是否不同的收费日期产生作为一个结算 默认false
        /// </summary>
        public bool sfrqWithSameJs { get; set; }
        /// <summary>
        /// （收费，作为一个结算）指定唯一收费日期
        /// </summary>
        public DateTime? settSfrq { get; set; }
        /// <summary>
        /// 自动生成门诊计划
        /// </summary>
        public bool autoGenePlan { get; set; }
        /// <summary>
        /// 是否是欠费预结
        /// </summary>
        public bool isQfyj { get; set; }
        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

    }
}
