using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
   public class Input_4402:InputBase
    {
        public List<data_4402> data { get; set; }
    }

    public class data_4402
    {
        /// <summary>
        /// 1|就医流水号|字符型|30||Y|院内唯一号
        /// </summary> 
        public string mdtrt_sn { get; set; }

        /// <summary>
        /// 2|就诊ID|字符型|30|||医保病人必填
        /// </summary> 
		public string mdtrt_id { get; set; }

        /// <summary>
        /// 3|人员编号|字符型|30|||医保病人必填
        /// </summary> 
		public string psn_no { get; set; }

        /// <summary>
        /// 4|住院床号|字符型|30|||
        /// </summary> 
		public string ipt_bedno { get; set; }

        /// <summary>
        /// 5|医嘱号|字符型|30|||
        /// </summary> 
		public string drord_no { get; set; }

        /// <summary>
        /// 6|下达科室代码|字符型|30|||参照科室代码（dept）
        /// </summary> 
		public string isu_dept_code { get; set; }

        /// <summary>
        /// 7|医嘱下达时间|日期型||||
        /// </summary> 
		public string drord_isu_no { get; set; }

        /// <summary>
        /// 8|执行科室代码|字符型|30|||参照科室代码（dept）
        /// </summary> 
		public string exe_dept_code { get; set; }

        /// <summary>
        /// 9|执行科室名称|字符型|50|||
        /// </summary> 
		public string exedept_name { get; set; }

        /// <summary>
        /// 10|医嘱审核人姓名|字符型|50|||
        /// </summary> 
		public string drord_chker_name { get; set; }

        /// <summary>
        /// 11|医嘱执行人姓名|字符型|50|||
        /// </summary> 
		public string drord_ptr_name { get; set; }

        /// <summary>
        /// 12|医嘱组号|字符型|30|||
        /// </summary> 
		public string drord_grpno { get; set; }

        /// <summary>
        /// 13|医嘱类别|字符型|3|Y||
        /// </summary> 
		public string drord_type { get; set; }

        /// <summary>
        /// 14|医嘱项目分类代码|字符型|30|||
        /// </summary> 
		public string drord_item_type { get; set; }

        /// <summary>
        /// 15|医嘱项目分类名称|字符型|100|||
        /// </summary> 
		public string drord_item_name { get; set; }

        /// <summary>
        /// 16|医嘱明细代码|字符型|30|||院内医嘱明细编码
        /// </summary> 
		public string drord_detl_code { get; set; }

        /// <summary>
        /// 17|医嘱明细名称|字符型|100|||院内医嘱明细名称
        /// </summary> 
		public string drord_detl_name { get; set; }

        /// <summary>
        /// 18|药物类型代码|字符型|100|||
        /// </summary> 
		public string medn_type_code { get; set; }

        /// <summary>
        /// 19|药物类型名称|字符型|100|||
        /// </summary> 
		public string medn_type_name { get; set; }

        /// <summary>
        /// 20|药品剂型|字符型|30|Y||【dosform】代码
        /// </summary> 
		public string drug_dosform { get; set; }

        /// <summary>
        /// 21|药品剂型名称|字符型|110|||
        /// </summary> 
		public string drug_dosform_name { get; set; }

        /// <summary>
        /// 22|药品规格|字符型|50|||
        /// </summary> 
		public string drug_spec { get; set; }

        /// <summary>
        /// 23|发药数量|数值型|5,2|||
        /// </summary> 
		public string dismed_cnt { get; set; }

        /// <summary>
        /// 24|发药数量单位|字符型|30|||
        /// </summary> 
		public string dismed_cnt_unt { get; set; }

        /// <summary>
        /// 25|药物使用-频率|字符型|20|||
        /// </summary> 
		public string medn_use_frqu { get; set; }

        /// <summary>
        /// 26|药物使用-剂量单位|字符型|10|||
        /// </summary> 
		public string medn_used_dosunt { get; set; }

        /// <summary>
        /// 27|药物使用-次剂量|数值型|16，4|||
        /// </summary> 
		public string drug_used_sdose { get; set; }

        /// <summary>
        /// 28|药物使用-总剂量|数值型|16，4|||
        /// </summary> 
		public string drug_used_idose { get; set; }

        /// <summary>
        /// 29|药物使用-途径代码|字符型|30|Y||
        /// </summary> 
		public string drug_used_way_code { get; set; }

        /// <summary>
        /// 30|药物使用-途径|字符型|100|||
        /// </summary> 
		public string drug_used_way { get; set; }

        /// <summary>
        /// 31|用药天数|数值型|5,0|||
        /// </summary> 
		public string medc_days { get; set; }

        /// <summary>
        /// 32|用药开始时间|日期时间型||||
        /// </summary> 
		public string medc_begntime { get; set; }

        /// <summary>
        /// 33|用药停止时间|日期时间型||||
        /// </summary> 
		public string medc_endtime { get; set; }

        /// <summary>
        /// 34|皮试判别|字符型|10|Y||
        /// </summary> 
		public string skintst_dicm { get; set; }

        /// <summary>
        /// 35|草药脚注|字符型|200|||
        /// </summary> 
		public string tcmherb_foote { get; set; }

        /// <summary>
        /// 36|医嘱结束时间|日期时间型||||
        /// </summary> 
		public string drord_endtime { get; set; }

        /// <summary>
        /// 37|住院科室代码|字符型|30|||
        /// </summary> 
		public string ipt_dept_code { get; set; }

        /// <summary>
        /// 38|医疗机构组织机构代码|字符型|50|||
        /// </summary> 
		public string medins_orgcode { get; set; }

        /// <summary>
        /// 39|统一采购药品标志|字符型|3|Y||
        /// </summary> 
		public string unif_purc_drug_flag { get; set; }

        /// <summary>
        /// 40|药品管理平台代码|字符型|50|||
        /// </summary> 
		public string drug_mgt_plaf_code { get; set; }

        /// <summary>
        /// 41|药品采购代码|字符型|20|||
        /// </summary> 
		public string drug_purc_code { get; set; }

        /// <summary>
        /// 42|基本药物标志|字符型|3|Y||
        /// </summary> 
		public string bas_medn_flag { get; set; }

        /// <summary>
        /// 43|有效标志|字符型|3|Y  |Y|
        /// </summary> 
		public string vali_flag { get; set; }

        /// <summary>
        /// 44|病案医嘱明细 id|字符型|30||Y|主键
        /// </summary> 
		public string medcas_drord_detl_id { get; set; }
    }
}
