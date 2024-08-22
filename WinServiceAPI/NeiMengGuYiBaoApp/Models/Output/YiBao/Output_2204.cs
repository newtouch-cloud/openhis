using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_2204 : OutputBase
    {
        public List<result2204> result { get; set; }

    }


    public class result2204
    {
        /// <summary>
        /// 费用明细流水号 字符型 30 Y
        /// </summary>
        public string feedetl_sn { get; set; }

        /// <summary>
        /// 明细项目费用总额 数值型 16,2 Y
        /// </summary>
        public decimal det_item_fee_sumamt { get; set; }

        /// <summary>
        /// 数量 数值型 16,4 Y
        /// </summary>
        public decimal cnt { get; set; }

        /// <summary>
        /// 单价 数值型 16,6 Y
        /// </summary>
        public decimal pric { get; set; }

        /// <summary>
        /// 定价上限金额 数值型 16,6 Y
        /// </summary>
        public decimal pric_uplmt_amt { get; set; }

        /// <summary>
        /// 自付比例 数值型 5,4
        /// </summary>
        public decimal selfpay_prop { get; set; }

        /// <summary>
        /// 全自费金额 数值型 16,2
        /// </summary>
        public decimal fulamt_ownpay_amt { get; set; }

        /// <summary>
        /// 超限价金额 数值型 16,2
        /// </summary>
        public decimal overlmt_amt { get; set; }

        /// <summary>
        /// 先行自付金额 数值型 16,2
        /// </summary>
        public decimal preselfpay_amt { get; set; }

        /// <summary>
        ///  符合政策范围金额 数值型 16,2
        /// </summary>
        public decimal inscp_scp_amt { get; set; }

        /// <summary>
        /// 收费项目等级 字符型 3 Y Y
        /// </summary>
        public string chrgitm_lv { get; set; }

        /// <summary>
        /// med_chrgitm_type 医疗收费项目类别 字符型 6 Y Y
        /// </summary>
        public string med_chrgitm_type { get; set; }

        /// <summary>
        /// 基本药物标志 字符型 3 Y
        /// </summary>
        public string bas_medn_flag { get; set; }

        /// <summary>
        /// 医保谈判药品标志 字符型 3 Y
        /// </summary>
        public string hi_nego_drug_flag { get; set; }

        /// <summary>
        /// 儿童用药标志 字符型 3 Y
        /// </summary>
        public string chld_medc_flag { get; set; }

        /// <summary>
        /// 目录特项标志 字符型 3 Y 特检特治项目或特殊药品
        /// </summary>
        public string list_sp_item_flag { get; set; }

        /// <summary>
        /// 限制使用标志 字符型 3 Y Y
        /// </summary>
        public string lmt_used_flag { get; set; }

        /// <summary>
        /// 直报标志 字符型 3 Y
        /// </summary>
        public string drt_reim_flag { get; set; }

        /// <summary>
        /// 备注 字符型 500 明细分割错误信息
        /// </summary>
        public string memo { get; set; }
    }
}
