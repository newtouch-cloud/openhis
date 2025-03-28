using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    /// <summary>
    /// 住院费用 按 大类 分类
    /// </summary>
    public class HospFeeClassifyWithDLBO
    {
        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// 大类结算金额合计（未除去减免的）
        /// </summary>
        public decimal jsje { get; set; }

        /// <summary>
        /// 项目计费列表
        /// </summary>
        public IList<HospItemFeeDetailVO> ItemFeeList { get; set; }

        /// <summary>
        /// 药品计费列表
        /// </summary>
        public IList<HospMedicinFeeDetailVO> MedicinFeeList { get; set; }



    }

}
