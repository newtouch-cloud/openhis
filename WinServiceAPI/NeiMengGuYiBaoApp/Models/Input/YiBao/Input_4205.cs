using System;
using System.Collections.Generic;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_4205 : InputBase
    {
        //  自费病人门诊就诊信息
        public MdtrtInfo mdtrtinfo { get; set; }
        //  自费病人门诊诊断信息
        public List<DiseInfo> diseinfo { get; set; }
        //   自费病人门诊费用明细信息
        public List<FeeDetail4205> feedetail { get; set; }
    }

    /// <summary>
    /// 自费病人门诊就诊信息（节点标识：mdtrtinfo）
    /// </summary>
    public class MdtrtInfo
    {
        /// <summary>
        /// 医药机构就诊 ID (长度: 30)
        /// </summary>
        public string fixmedins_mdtrt_id { get; set; }

        /// <summary>
        /// 定点医药机构编号 (长度: 30)
        /// </summary>
        public string fixmedins_code { get; set; }

        /// <summary>
        /// 定点医药机构名称 (长度: 200)
        /// </summary>
        public string fixmedins_name { get; set; }

        /// <summary>
        /// 人员证件类型 (长度: 6)
        /// </summary>
        public string psn_cert_type { get; set; }

        /// <summary>
        /// 证件号码 (长度: 600)
        /// </summary>
        public string certno { get; set; }

        /// <summary>
        /// 人员姓名 (长度: 50)
        /// </summary>
        public string psn_name { get; set; }

        /// <summary>
        /// 性别 (长度: 6)
        /// </summary>
        public string gend { get; set; }

        /// <summary>
        /// 民族 (长度: 3)
        /// </summary>
        public string naty { get; set; }

        /// <summary>
        /// 出生日期 (格式: yyyy-MM-dd)
        /// </summary>
        public string brdy { get; set; }

        /// <summary>
        /// 年龄 (数值型: 4,1)
        /// </summary>
        public decimal? age { get; set; }

        /// <summary>
        /// 联系人姓名 (长度: 50)
        /// </summary>
        public string coner_name { get; set; }

        /// <summary>
        /// 联系电话 (长度: 50)
        /// </summary>
        public string tel { get; set; }

        /// <summary>
        /// 联系地址 (长度: 500)
        /// </summary>
        public string addr { get; set; }

        /// <summary>
        /// 开始时间 (格式: yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public string begntime { get; set; }

        /// <summary>
        /// 结束时间 (格式: yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public string endtime { get; set; }

        /// <summary>
        /// 医疗类别 (长度: 6)
        /// </summary>
        public string med_type { get; set; }

        /// <summary>
        /// 主要病情描述 (长度: 1000)
        /// </summary>
        public string main_cond_dscr { get; set; }

        /// <summary>
        /// 病种编码 (长度: 30)
        /// </summary>
        public string dise_codg { get; set; }

        /// <summary>
        /// 病种名称 (长度: 500)
        /// </summary>
        public string dise_name { get; set; }

        /// <summary>
        /// 计划生育手术类别 (长度: 6)
        /// </summary>
        public string birctrl_type { get; set; }

        /// <summary>
        /// 计划生育手术或生育日期 (格式: yyyy-MM-dd)
        /// </summary>
        public string birctrl_matn_date { get; set; }

        /// <summary>
        /// 生育类别 (长度: 6)
        /// </summary>
        public string matn_type { get; set; }

        /// <summary>
        /// 孕周数 (数值型: 2)
        /// </summary>
        public int? geso_val { get; set; }

        /// <summary>
        /// 电子票据代码 (长度: 50)
        /// </summary>
        public string elec_bill_code { get; set; }

        /// <summary>
        /// 电子票据号码 (长度: 50)
        /// </summary>
        public string elec_billno_code { get; set; }

        /// <summary>
        /// 电子票据校验码 (长度: 6)
        /// </summary>
        public string elec_bill_chk_code { get; set; }

        /// <summary>
        /// 字段扩展 (长度: 4000)
        /// </summary>
        public string exp_content { get; set; }
    }

    /// <summary>
    /// 自费病人门诊诊断信息（节点标识：diseinfo）
    /// </summary>
    public class DiseInfo
    {
        /// <summary>
        /// 诊断类别 (长度: 3)
        /// </summary>
        public string diag_type { get; set; }

        /// <summary>
        /// 诊断排序号 (数值型: 2)
        /// </summary>
        public int diag_srt_no { get; set; }

        /// <summary>
        /// 诊断代码 (长度: 30)
        /// </summary>
        public string diag_code { get; set; }

        /// <summary>
        /// 诊断名称 (长度: 100)
        /// </summary>
        public string diag_name { get; set; }

        /// <summary>
        /// 诊断科室 (长度: 50)
        /// </summary>
        public string diag_dept { get; set; }

        /// <summary>
        /// 诊断医生编码 (长度: 30)
        /// </summary>
        public string diag_dr_code { get; set; }

        /// <summary>
        /// 诊断医生姓名 (长度: 50)
        /// </summary>
        public string diag_dr_name { get; set; }

        /// <summary>
        /// 诊断时间 (格式: yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public string diag_time { get; set; }

        /// <summary>
        /// 有效标志 (长度: 3)
        /// </summary>
        public string vali_flag { get; set; }
    }

    /// <summary>
    /// 自费病人门诊费用明细信息（节点标识：feedetail）
    /// </summary>
    public class FeeDetail4205
    {
        /// <summary>
        /// 医药机构就诊 ID (长度: 30)
        /// </summary>
        public string fixmedins_mdtrt_id { get; set; }

        /// <summary>
        /// 医疗类别 (长度: 6)
        /// </summary>
        public string med_type { get; set; }

        /// <summary>
        /// 记账流水号 (长度: 30)
        /// </summary>
        public string bkkp_sn { get; set; }

        /// <summary>
        /// 费用发生时间 (格式: yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public string fee_ocur_time { get; set; }

        /// <summary>
        /// 定点医药机构编号 (长度: 30)
        /// </summary>
        public string fixmedins_code { get; set; }

        /// <summary>
        /// 定点医药机构名称 (长度: 200)
        /// </summary>
        public string fixmedins_name { get; set; }

        /// <summary>
        /// 数量 (数值型: 16,4)
        /// </summary>
        public decimal cnt { get; set; }

        /// <summary>
        /// 单价 (数值型: 16,6)
        /// </summary>
        public decimal pric { get; set; }

        /// <summary>
        /// 明细项目费用总额 (数值型: 16,2)
        /// </summary>
        public decimal det_item_fee_sumamt { get; set; }

        /// <summary>
        /// 医疗目录编码 (长度: 50)
        /// </summary>
        public string med_list_codg { get; set; }

        /// <summary>
        /// 医药机构目录编码 (长度: 150)
        /// </summary>
        public string medins_list_codg { get; set; }

        /// <summary>
        /// 医药机构目录名称 (长度: 100)
        /// </summary>
        public string medins_list_name { get; set; }

        /// <summary>
        /// 医疗收费项目类别 (长度: 6)
        /// </summary>
        public string med_chrgitm_type { get; set; }

        /// <summary>
        /// 商品名 (长度: 200)
        /// </summary>
        public string prodname { get; set; }

        /// <summary>
        /// 开单科室编码 (长度: 30)
        /// </summary>
        public string bilg_dept_codg { get; set; }

        /// <summary>
        /// 开单科室名称 (长度: 100)
        /// </summary>
        public string bilg_dept_name { get; set; }

        /// <summary>
        /// 开单医生编码 (长度: 30)
        /// </summary>
        public string bilg_dr_code { get; set; }

        /// <summary>
        /// 开单医师姓名 (长度: 50)
        /// </summary>
        public string bilg_dr_name { get; set; }

        /// <summary>
        /// 受单科室编码 (长度: 30)
        /// </summary>
        public string acord_dept_codg { get; set; }

        /// <summary>
        /// 受单科室名称 (长度: 100)
        /// </summary>
        public string acord_dept_name { get; set; }

        /// <summary>
        /// 受单医生编码 (长度: 30)
        /// </summary>
        public string orders_dr_code { get; set; }

        /// <summary>
        /// 受单医生姓名 (长度: 50)
        /// </summary>
        public string orders_dr_name { get; set; }

        /// <summary>
        /// 医院收费项目等级 (长度: 6)
        /// </summary>
        public string hosp_appr_flag { get; set; }

        /// <summary>
        /// 中药使用方式 (长度: 6)
        /// </summary>
        public string tcmherb_foote { get; set; }

        /// <summary>
        /// 外检标志 (长度: 6)
        /// </summary>
        public string etip_flag { get; set; }

        /// <summary>
        /// 外检医院编码 (长度: 30)
        /// </summary>
        public string etip_hosp_code { get; set; }

        /// <summary>
        /// 出院带药标志 (长度: 6)
        /// </summary>
        public string dscg_tcmherb_flag { get; set; }

        /// <summary>
        /// 备注 (长度: 500)
        /// </summary>
        public string memo { get; set; }
    }

}
