using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    /// <summary>
    /// 出院结算 费用（项目/药品）详情
    /// </summary>
    public class HospFeeItemOrMedicinDetailBO
    {
        public HospFeeItemOrMedicinDetailBO()
        {
            this.sfjsfymx = new HospFeeItemOrMedicinSuanfaFeeDetailBO();
        }

        /// <summary>
        /// 是否需要医保交易
        /// </summary>
        public bool isNeedYBJY { get; set; }

        /// <summary>
        /// 收费大类
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 收费大类 名称
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public HospItemFeeDetailVO Item { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public HospMedicinFeeDetailVO Medicin { get; set; }

        #region

        /// <summary>
        /// 减免后金额
        /// </summary>
        public decimal jmhje { get; set; }

        /// <summary>
        /// 自理费用
        /// </summary>
        public decimal zlfy { get; set; }

        /// <summary>
        /// 可报金额
        /// </summary>
        public decimal kbje { get; set; }

        /// <summary>
        /// 分类自负
        /// </summary>
        public decimal flzf { get; set; }

        #endregion

        /// <summary>
        /// 医保交易金额（计算之后得到的数据）
        /// </summary>
        public decimal ybjyje { get; set; }

        /// <summary>
        /// 医保交易范围金额（计算之后得到的数据）
        /// </summary>
        public decimal ybjyfwje { get; set; }

        /// <summary>
        /// 算法计算 后 的 费用明细
        /// </summary>
        public HospFeeItemOrMedicinSuanfaFeeDetailBO sfjsfymx { get; set; }

        /// <summary>
        /// 一次性材料 收费项目 综合
        /// </summary>
        public SysChargeMaterialItemSynthesisEntity sfclxmzhEntity { get; set; }

    }

}
