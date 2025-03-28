using Newtouch.HIS.Domain.DTO;
using System;

namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    /// <summary>
    /// 门诊结算查询 Result VO
    /// </summary>
    public class OutpatientSettlementVO : OutpatientSettYbFeeRelatedDTO
    {
        public int jsnm { get; set; }

        public string mzh { get; set; }

        public string blh { get; set; }

        public string xm { get; set; }

        public string xb { get; set; }

        public string brxzmc { get; set; }

        public string zdmc { get; set; }

        public int jszt { get; set; }

        /// <summary>
        /// 结算日期
        /// </summary>
        public DateTime jsrq { get; set; }

        public string fph { get; set; }

        public decimal jszje { get; set; }

        public decimal? jsxjzf { get; set; }

        public string zffsmcstr { get; set; }


    }
}
