
namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    /// <summary>
    /// 病人收费算法计算出的大类费用明细
    /// </summary>
    public class PatientChargeAlgorythm_Category_FeesDetailVO
    {
        /// <summary>
        /// 大类
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 明细编码（暂时仅供新农合使用）
        /// </summary>
        //public string mxbm { get; set; }

        /// <summary>
        /// 收费项目（暂时仅供新农合使用）
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// 明细内码（暂时仅供新农合使用）
        /// </summary>
        public int mxnm { get; set; }

        /// <summary>
        /// 处方明细内码（暂时仅供新农合使用）
        /// </summary>
        public int cf_mxnm { get; set; }

        /// <summary>
        /// 成组号（暂时仅供新农合使用）
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// 分类自负
        /// </summary>
        public decimal flzf { get; set; }

        /// <summary>
        /// 自理
        /// </summary>
        public decimal zl { get; set; }

        /// <summary>
        /// 可记账费用
        /// </summary>
        public decimal jzfy { get; set; }

        /// <summary>
        /// 现金
        /// </summary>
        public decimal xj { get; set; }

        /// <summary>
        /// 减免金额
        /// </summary>
        public decimal jmje { get; set; }

        /// <summary>
        /// 算法自负（通过算法计算出的自负金额）
        /// </summary>
        public decimal sfzf { get; set; }

        /// <summary>
        /// 算法自理（通过算法计算出的自负金额）
        /// </summary>
        public decimal sfzl { get; set; }

        /// <summary>
        /// 费用合计（自费病人需要要支付的现金）
        /// </summary>
        public decimal total { get; set; }

        /// <summary>
        /// 住院项目计费表编号
        /// </summary>
        public int xmjfbbh { get; set; }

        /// <summary>
        /// 住院药品计费表编号
        /// </summary>
        public int ypjfbbh { get; set; }

        /// <summary>
        /// 医保交易金额（计算之后得到的数据）
        /// </summary>
        public decimal jyje { get; set; }

        /// <summary>
        /// 医保交易范围金额（计算之后得到的数据）
        /// </summary>
        public decimal jyfwje { get; set; }

        /// <summary>
        /// 医嘱类型1 药品 2 项目
        /// </summary>
        public string yzlx { get; set; }
    }
}
