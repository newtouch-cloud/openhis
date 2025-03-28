using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 出院结算 提交结算
    /// </summary>
    public class OutHospSettDBDataUpdateCollectVO
    {
        /// <summary>
        /// 住院结算
        /// </summary>
        public HospSettlementEntity zy_js { get; set; }

        /// <summary>
        /// 住院结算明细
        /// </summary>
        public IList<HospSettlementDetailEntity> zy_jsmxList { get; set; }

        /// <summary>
        /// 住院交易结算
        /// </summary>
        public IList<HospTransactionSettlementEntity> zy_jyjsList { get; set; }

        /// <summary>
        /// 住院交易结算明细
        /// </summary>
        public IList<HospTransactionSettlementDetailEntity> zy_jyjsmxList { get; set; }

        /// <summary>
        /// 住院结算支付方式
        /// </summary>
        public IList<HospSettlementPaymentModelEntity> zy_jszffsList { get; set; }

        /// <summary>
        /// 系统病人帐户收支记录
        /// </summary>
        public IList<SysPatientAccountRevenueAndExpenseEntity> xt_brzhszjlList { get; set; }

        /// <summary>
        /// 住院结算大类
        /// </summary>
        public IList<HospSettlementCategoryEntity> zy_jsdlList { get; set; }

        //update 住院病人基本信息
        //insert 住院病人基本信息变更
        //update 财务发票 更新当前发票号

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
        /// 发票号
        /// </summary>
        public string fph { get; set; }

    }

}
