using System;
using System.Collections.Generic;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    //每次接口调用只上传一位患者的信息
    public class Input_4201A : InputBase
    {
        public List<FsiOwnpayPatnFeeListDDTO> fsiOwnpayPatnFeeListDDTO { get; set; }
    }

    public class FsiOwnpayPatnFeeListDDTO
    {
        /// <summary>
        /// 医药机构就诊ID (必填，字符型，长度30)
        /// 必须与 4202 交易中fixmedins_mdtrt_id对应
        /// </summary>
        public string fixmedins_mdtrt_id { get; set; }

        /// <summary>
        /// 医疗类别 (必填，字符型，长度6)
        /// </summary>
        public string med_type { get; set; }

        /// <summary>
        /// 记账流水号 (必填，字符型，长度30)
        /// 单次就诊内唯一
        /// </summary>
        public string bkkp_sn { get; set; }

        /// <summary>
        /// 费用发生时间 (必填，日期时间型，格式yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public string fee_ocur_time { get; set; }

        /// <summary>
        /// 定点医药机构编号 (必填，字符型，长度30)
        /// </summary>
        public string fixmedins_code { get; set; }

        /// <summary>
        /// 定点医药机构名称 (必填，字符型，长度200)
        /// </summary>
        public string fixmedins_name { get; set; }

        /// <summary>
        /// 数量 (必填，数值型，精度16,4)
        /// </summary>
        public decimal cnt { get; set; }

        /// <summary>
        /// 单价 (必填，数值型，精度16,6)
        /// </summary>
        public decimal pric { get; set; }

        /// <summary>
        /// 明细项目费用总额 (必填，数值型，精度16,2)
        /// </summary>
        public decimal det_item_fee_sumamt { get; set; }

        /// <summary>
        /// 医疗目录编码 (必填，字符型，长度50)
        /// </summary>
        public string med_list_codg { get; set; }

        /// <summary>
        /// 医药机构目录编码 (必填，字符型，长度150)
        /// </summary>
        public string medins_list_codg { get; set; }

        /// <summary>
        /// 医药机构目录名称 (必填，字符型，长度100)
        /// </summary>
        public string medins_list_name { get; set; }

        /// <summary>
        /// 医疗收费项目类别 (必填，字符型，长度6)
        /// </summary>
        public string med_chrgitm_type { get; set; }

        /// <summary>
        /// 商品名 (非必填，字符型，长度200)
        /// </summary>
        public string prodname { get; set; }

        /// <summary>
        /// 开单科室编码 (必填，字符型，长度30)
        /// </summary>
        public string bilg_dept_codg { get; set; }

        /// <summary>
        /// 开单科室名称 (必填，字符型，长度100)
        /// </summary>
        public string bilg_dept_name { get; set; }

        /// <summary>
        /// 开单医生编码 (必填，字符型，长度30)
        /// </summary>
        public string bilg_dr_code { get; set; }

        /// <summary>
        /// 开单医生姓名 (必填，字符型，长度50)
        /// </summary>
        public string bilg_dr_name { get; set; }

        /// <summary>
        /// 受单科室编码 (非必填，字符型，长度30)
        /// </summary>
        public string acord_dept_codg { get; set; }

        /// <summary>
        /// 受单科室名称 (非必填，字符型，长度100)
        /// </summary>
        public string acord_dept_name { get; set; }

        /// <summary>
        /// 受单医生编码 (非必填，字符型，长度30)
        /// </summary>
        public string acord_dr_code { get; set; }

        /// <summary>
        /// 受单医生姓名 (非必填，字符型，长度50)
        /// </summary>
        public string acord_dr_name { get; set; }

        /// <summary>
        /// 中药使用方式 (必填，字符型，长度6)
        /// </summary>
        public string tcmdrug_used_way { get; set; }

        /// <summary>
        /// 外检标志 (必填，字符型，长度3)
        /// </summary>
        public string etip_flag { get; set; }

        /// <summary>
        /// 外检医院编码 (非必填，字符型，长度30)
        /// </summary>
        public string etip_hosp_code { get; set; }

        /// <summary>
        /// 出院带药标志 (必填，字符型，长度3)
        /// </summary>
        public string dscg_tkdrug_flag { get; set; }

        /// <summary>
        /// 单次剂量描述 (非必填，字符型，长度200)
        /// </summary>
        public string sin_dos_dscr { get; set; }

        /// <summary>
        /// 使用频次描述 (非必填，字符型，长度200)
        /// </summary>
        public string used_frqu_dscr { get; set; }

        /// <summary>
        /// 周期天数 (非必填，数值型，精度4,2)
        /// </summary>
        public decimal prd_days { get; set; }

        /// <summary>
        /// 用药途径描述 (非必填，字符型，长度200)
        /// </summary>
        public string medc_way_dscr { get; set; }

        /// <summary>
        /// 备注 (非必填，字符型，长度500)
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// 全自费金额 (必填，数值型，精度16,2)
        /// </summary>
        public decimal fulamt_ownpay_amt { get; set; }

        /// <summary>
        /// 超限价自费金额 (必填，数值型，精度16,2)
        /// </summary>
        public decimal overlmt_selfpay { get; set; }

        /// <summary>
        /// 先行自付金额 (必填，数值型，精度16,2)
        /// </summary>
        public decimal preselfpay_amt { get; set; }

        /// <summary>
        /// 符合政策范围金额 (必填，数值型，精度16,2)
        /// </summary>
        public decimal inscp_amt { get; set; }
    }

}
