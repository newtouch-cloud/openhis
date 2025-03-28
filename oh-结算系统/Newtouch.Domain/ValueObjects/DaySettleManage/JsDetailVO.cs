using System;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationManage
{
    public class JsDetailVO
    {
        /// <summary>
        /// 
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// 本次结算金额
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fph { get; set; }
    }
}
