using Newtouch.HIS.Domain.ValueObjects;
using System;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    /// <summary>
    /// 住院结算 病人 分类收费 预览
    /// </summary>
    public class HosSettPatClassifyChargePreviewDto
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }
        ///// <summary>
        ///// 卡号
        ///// </summary>
        //public string kh { get; set; }
        /// <summary>
        /// 记账
        /// </summary>
        public decimal jzfy { get; set; }
        /// <summary>
        /// 分类自负
        /// </summary>
        public decimal flzf { get; set; }
        /// <summary>
        /// 自费
        /// </summary>
        public decimal zifei { get; set; }
        /// <summary>
        /// 自负
        /// </summary>
        public decimal zifu { get; set; }
        /// <summary>
        /// 减免金额（其他）
        /// </summary>
        public decimal jmje { get; set; }
        /// <summary>
        /// 应收款
        /// </summary>
        public decimal yingshoukuan { get; set; }
        /// <summary>
        /// 找零
        /// </summary>
        public decimal zhaoling { get; set; }
        /// <summary>
        /// 舍入差额
        /// </summary>
        public decimal srce { get; set; }
        /// <summary>
        /// 实收款
        /// </summary>
        public decimal ssk { get; set; }

        /// <summary>
        /// 账户余额（实收款）（还未扣项目/药品计费）
        /// </summary>
        public decimal zhye { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 发票日期
        /// </summary>
        public string fprq { get; set; }

        /// <summary>
        /// 住院病人基本信息
        /// </summary>
        public HospSettPatInfoVO HospSettPatInfo { get; set; }

        /// <summary>
        /// 结算金额
        /// </summary>
        public decimal expectedjsje { get; set; }

        /// <summary>
        /// 出院日期
        /// </summary>
        public DateTime expectedcyrq { get; set; }

    }
}
