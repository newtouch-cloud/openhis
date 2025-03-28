using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class HospCancelSettDBDataUpdateCollectVO
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 在院状态
        /// </summary>
        public string zybz { get; set; }

        /// <summary>
        /// 出院日期
        /// </summary>
        public DateTime? cyrq { get; set; }

        /// <summary>
        /// 撤销结算
        /// </summary>
        public HospSettlementEntity cxjs { get; set; }

        /// <summary>
        /// 撤销结算明细列表
        /// </summary>
        public IList<HospSettlementDetailEntity> cx_zyjsmxList { get; set; }

        /// <summary>
        /// 撤销 系统病人账户收支记录
        /// </summary>
        public IList<SysPatientAccountRevenueAndExpenseEntity> cx_xt_brzhszjlList { get; set; }

        /// <summary>
        /// 撤销 住院结算支付方式
        /// </summary>
        public IList<HospSettlementPaymentModelEntity> cx_zy_jszffsList { get; set; }



    }

}
