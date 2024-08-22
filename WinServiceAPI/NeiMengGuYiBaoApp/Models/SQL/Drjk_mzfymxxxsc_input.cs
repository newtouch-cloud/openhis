using NeiMengGuYiBaoApp.Models.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.SQL
{
    public class Drjk_mzfymxxxsc_input:SqlBase
    {
        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// 状态 1 正常 0作废
        /// </summary>
        public int zt { get; set; }
        /// <summary>
        /// 状态操作员
        /// </summary>
        public string zt_czy { get; set; }
        /// <summary>
        /// 状态日期
        /// </summary>
        public DateTime zt_rq { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string czydm { get; set; }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime czrq { get; set; }

        /// <summary>
        /// 费用明细流水号 字符型 30 Y 单次就诊内唯一
        /// </summary>
        public string feedetl_sn { get; set; }

        /// <summary>
        /// 就诊 ID 字符型 30 Y
        /// </summary>
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 人员编号 字符型 30 Y
        /// </summary>
        public string psn_no { get; set; }

        /// <summary>
        /// 收费批次号 字符型 30 Y 同一收费批次号病种编号必须一致
        /// </summary>
        public string chrg_bchno { get; set; }

        /// <summary>
        /// 病种编码 字符型 30 按照标准编码填写：按病种结算病种目录代码(bydise_setl_list_code)、门诊慢特病病种目录代码(opsp_dise_cod)
        /// </summary>
        public string dise_codg { get; set; }

        /// <summary>
        /// 处方号 字符型 30 外购处方时，传入外购处方的处方号；非外购处方，传入医药机构处方号
        /// </summary>
        public string rxno { get; set; }

        /// <summary>
        /// 外购处方标志 字符型 3 Y Y
        /// </summary>
        public string rx_circ_flag { get; set; }

        /// <summary>
        /// 费用发生时间 日期时间型 Y yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string fee_ocur_time { get; set; }

        /// <summary>
        /// 医疗目录编码 字符型 50 Y
        /// </summary>
        public string med_list_codg { get; set; }

        /// <summary>
        /// 医药机构目录编码 字符型 150 Y
        /// </summary>
        public string medins_list_codg { get; set; }

        /// <summary>
        /// 明细项目费用总额 数值型 16,2 Y
        /// </summary>
        public decimal det_item_fee_sumamt { get; set; }

        /// <summary>
        /// cnt 数量 数值型 16,4 Y
        /// </summary>
        public decimal cnt { get; set; }

        /// <summary>
        /// 单价 数值型 16,6 Y
        /// </summary>
        public decimal pric { get; set; }

        /// <summary>
        /// 单次剂量描述 字符型 200
        /// </summary>
        public string sin_dos_dscr { get; set; }

        /// <summary>
        /// 使用频次描述 字符型 200
        /// </summary>
        public string used_frqu_dscr { get; set; }

        /// <summary>
        /// 周期天数 数值型 4,2
        /// </summary>
        public decimal prd_days { get; set; }

        /// <summary>
        /// 用药途径描述 字符型 200
        /// </summary>
        public string medc_way_dscr { get; set; }

        /// <summary>
        /// 开单科室编码 字符型 30 Y
        /// </summary>
        public string bilg_dept_codg { get; set; }

        /// <summary>
        /// 开单科室名称 字符型 100 Y
        /// </summary>
        public string bilg_dept_name { get; set; }

        /// <summary>
        /// 开单医生编码 字符型 30 Y 按照标准编码填写
        /// </summary>
        public string bilg_dr_codg { get; set; }

        /// <summary>
        /// 开单医师姓名 字符型 50 Y
        /// </summary>
        public string bilg_dr_name { get; set; }

        /// <summary>
        /// 受单科室编码字符型 30
        /// </summary>
        public string acord_dept_codg { get; set; }

        /// <summary>
        /// 受单科室名称 字符型 100
        /// </summary>
        public string acord_dept_name { get; set; }

        /// <summary>
        /// 受单医生编码 字符型 30 按照标准编码填写
        /// </summary>
        public string orders_dr_code { get; set; }

        /// <summary>
        /// 受单医生姓名 字符型 50
        /// </summary>
        public string orders_dr_name { get; set; }

        /// <summary>
        /// 医院审批标志 字符型 3 Y Y
        /// </summary>
        public string hosp_appr_flag { get; set; }

        /// <summary>
        /// 中药使用方式 字符型 6 Y
        /// </summary>
        public string tcmdrug_used_way { get; set; }

        /// <summary>
        /// 外检标志 字符型 3 Y
        /// </summary>
        public string etip_flag { get; set; }

        /// <summary>
        /// 外检医院编码 字符型 30 按照标准编码填写
        /// </summary>
        public string etip_hosp_code { get; set; }

        /// <summary>
        /// 出院带药标志 字符型 3 Y
        /// </summary>
        public string dscg_tkdrug_flag { get; set; }

        /// <summary>
        /// 生育费用标志 字符型 6 Y
        /// </summary>
        public string matn_fee_flag { get; set; }



    }
}
