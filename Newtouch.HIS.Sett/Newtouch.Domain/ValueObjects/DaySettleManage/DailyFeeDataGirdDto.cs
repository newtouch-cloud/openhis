using System;

namespace Newtouch.HIS.Domain.ValueObjects.DaySettleManage
{
    public class DailyFeeDataGirdDto
    { 
        /// <summary>
      /// 结算内码
      /// </summary>
        public int jsnm { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        public DateTime? jzsj { get; set; }
        /// <summary>
        /// 结算对应挂号的门诊号
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// 病人性质名称
        /// </summary>
        public string brxzmc { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; } 
        public decimal jszje { get; set; }

        public decimal ybbx { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal? jsxjzf { get; set; }
        public string xb { get; set; }
        public string zjedx { get; set; }
        
    }
}
