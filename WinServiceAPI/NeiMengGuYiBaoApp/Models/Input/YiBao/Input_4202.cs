using System;
using System.Collections.Generic;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_4202 : InputBase
    {
        // 自费病人就诊信息
        public OwnPayPatnMdtrtD ownPayPatnMdtrtD { get; set; }
        // 自费病人诊断信息
        public List<OwnPayPatnDiagListD> ownPayPatnDiagListD { get; set; }
    }

    public class OwnPayPatnMdtrtD
    {
        public string fixmedins_mdtrt_id { get; set; }  // 医药机构就诊ID (必填, 字符型 30)
        public string fixmedins_code { get; set; }  // 定点医药机构编号 (必填, 字符型 30)
        public string fixmedins_name { get; set; }  // 定点医药机构名称 (必填, 字符型 200)
        public string psn_cert_type { get; set; }  // 人员证件类型 (必填, 字符型 6)
        public string certno { get; set; }  // 证件号码 (必填, 字符型 600)
        public string psn_name { get; set; }  // 人员姓名 (必填, 字符型 50)
        public string gend { get; set; }  // 性别 (字符型 6)
        public string naty { get; set; }  // 民族 (字符型 3)
        public string brdy { get; set; }  // 出生日期 (日期型 yyyy-MM-dd)
        public decimal? age { get; set; }  // 年龄 (数值型 4,1)
        public string coner_name { get; set; }  // 联系人姓名 (字符型 50)
        public string tel { get; set; }  // 联系电话 (字符型 50)
        public string addr { get; set; }  // 联系地址 (字符型 500)
        public string begntime { get; set; }  // 开始时间 (必填, 日期型 yyyy-MM-dd HH:mm:ss)
        public string endtime { get; set; }  // 结束时间 (日期型 yyyy-MM-dd HH:mm:ss)
        public string med_type { get; set; }  // 医疗类别 (必填, 字符型 6)
        public string ipt_otp_no { get; set; }  // 住院/门诊号 (字符型 30)
        public string medrcdno { get; set; }  // 病历号 (字符型 30)
        public string chfpdr_code { get; set; }  // 主诊医师代码 (字符型 30)
        public string adm_diag_dscr { get; set; }  // 入院诊断描述 (字符型 200)
        public string adm_dept_codg { get; set; }  // 入院科室编码 (字符型 30)
        public string adm_dept_name { get; set; }  // 入院科室名称 (字符型 100)
        public string adm_bed { get; set; }  // 入院床位 (字符型 30)
        public string wardarea_bed { get; set; }  // 病区床位 (字符型 50)
        public string traf_dept_flag { get; set; }  // 转科室标志 (字符型 6)
        public string dscg_maindiag_code { get; set; }  // 出院主诊断代码 (字符型 30)
        public string dscg_dept_codg { get; set; }  // 出院科室编码 (字符型 30)
        public string dscg_dept_name { get; set; }  // 出院科室名称 (字符型 100)
        public string dscg_bed { get; set; }  // 出院床位 (字符型 30)
        public string dscg_way { get; set; }  // 离院方式 (字符型 3)
        public string main_cond_dscr { get; set; }  // 主要病情描述 (字符型 1000)
        public string dise_no { get; set; }  // 病种编号 (字符型 30)
        public string dise_name { get; set; }  // 病种名称 (字符型 500)
        public string oprn_oprt_code { get; set; }  // 手术操作代码 (字符型 30)
        public string oprn_oprt_name { get; set; }  // 手术操作名称 (字符型 500)
        public string otp_diag_info { get; set; }  // 门诊诊断信息 (字符型 200)
        public string inhosp_stas { get; set; }  // 在院状态 (字符型 3)
        public string die_date { get; set; }  // 死亡日期 (日期型 yyyy-MM-dd)
        public int? ipt_days { get; set; }  // 住院天数 (数值型 16)
        public string fpsc_no { get; set; }  // 计划生育服务证号 (字符型 50)
        public string matn_type { get; set; }  // 生育类别 (字符型 6)
        public string birctrl_type { get; set; }  // 计划生育手术类别 (字符型 6)
        public string latechb_flag { get; set; }  // 晚育标志 (字符型 3)
        public int? geso_val { get; set; }  // 孕周数 (数值型 2)
        public int? fetts { get; set; }  // 胎次 (数值型 3)
        public int? fetus_cnt { get; set; }  // 胎儿数 (数值型 3)
        public string pret_flag { get; set; }  // 早产标志 (字符型 3)
        public string prey_time { get; set; }  // 妊娠时间 (日期型 yyyy-MM-dd)
        public string birctrl_matn_date { get; set; }  // 计划生育手术或生育日期 (日期型 yyyy-MM-dd)
        public string cop_flag { get; set; }  // 伴有并发症标志 (字符型 3)
        public string vali_flag { get; set; }  // 有效标志 (必填, 字符型 3)
        public string memo { get; set; }  // 备注 (字符型 500)
        public string opter_id { get; set; }  // 经办人 ID (字符型 20)
        public string opter_name { get; set; }  // 经办人姓名 (字符型 50)
        public string opt_time { get; set; }  // 经办时间 (日期型 yyyy-MM-dd HH:mm:ss)
        public string chfpdr_name { get; set; }  // 主诊医师姓名 (字符型 50)
        public string dscg_maindiag_name { get; set; }  // 住院主诊断名称 (字符型 300)
        public decimal? medfee_sumamt { get; set; }  // 医疗总费用 (必填, 数值型 16,2)
        public string elec_bill_code { get; set; }  // 电子票据代码 (字符型 50)
        public string elec_billno_code { get; set; }  // 电子票据号码 (字符型 50)
        public string elec_bill_chkcode { get; set; }  // 电子票据校验码 (字符型 6)
        public string exp_content { get; set; }  // 扩展字段 (字符型 4000)
    }

    public class OwnPayPatnDiagListD
    {
        public string inout_diag_type { get; set; }  // 出入院诊断类别 (必填, 字符型 6)
        public string diag_type { get; set; }  // 诊断类别 (必填, 字符型 3)
        public string maindiag_flag { get; set; }  // 主诊断标志 (必填, 字符型 3)
        public string diag_srt_no { get; set; }  // 诊断排序号 (必填, 字符型 2)
        public string diag_code { get; set; }  // 诊断代码 (必填, 字符型 30)
        public string diag_name { get; set; }  // 诊断名称 (必填, 字符型 255)
        public string diag_dept { get; set; }  // 诊断科室 (字符型 50)
        public string diag_dr_code { get; set; }  // 诊断医师代码 (字符型 30)
        public string diag_dr_name { get; set; }  // 诊断医师姓名 (字符型 50)
        public string diag_time { get; set; }  // 诊断时间 (日期型 yyyy-MM-dd HH:mm:ss)
        public string vali_flag { get; set; }

    }
}
