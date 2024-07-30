using Newtouch.HIS.API.Common;
using System;

namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    /// <summary>
    /// 已结算处方明细查询接口请求报文
    /// </summary>
    public class OutpatientSettledRpQueryRequestDTO: RequestBase
    {

        /// <summary>
        /// 结算开始时间
        /// </summary>
        public DateTime kssj { get; set; }

        /// <summary>
        /// 结算结束时间
        /// </summary>
        public DateTime jssj { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// 用法
        /// </summary>
        public string yfCode { get; set; }

    }
}
