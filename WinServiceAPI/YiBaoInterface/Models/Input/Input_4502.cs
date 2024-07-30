using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
   public class Input_4502 :InputBase
    {
        /// <summary>
        /// 检查记录（节点标识：examinfo）
        /// </summary>
        public labinfo_4502 labinfo { get; set; }
        /// <summary>
        /// 表 175 检查项目信息（节点标识：iteminfo）
        /// </summary>
        public List<iteminfo_4502> iteminfo { get; set; }
        /// <summary>
        /// 表 176 检查标本信息（节点标识：sampleinfo）
        /// </summary>
        public List<sampleinfo_4502> sampleinfo { get; set; }

        //
        /// <summary>
        /// 表 177 检查影像信息（节点标识：imageinfo）
        /// </summary>
        public List<imageinfo_4502> imageinfo { get; set; }
    }

    public class imageinfo_4502
    {
        //        1  rpotc_no 报告单号  字符型  30  Y 
        //2  study_uid 全局唯一号  字符型  200  Y图像中的study_uid
        //3  patient_id 检查号  字符型  30 
        //4  patient_name 患者姓名  字符型  30 
        //5  acession_no 图像流水号  字符型  30 图像中的AccessionNumber
        //6  study_time 检查时间  日期型 
        //7  modality 检查类型  字符型  20 
        //8  store_path 存储路径  字符型  300 
        //9  series_count 序列数量  数值型  8,0 
        //10  image_count 图像数量  数值型  8,0 
        public string rpotc_no { get; set; }
        public string study_uid { get; set; }
        public string patient_id { get; set; }
        public string patient_name { get; set; }
        public string acession_no { get; set; }
        public string study_time { get; set; }
        public string modality { get; set; }
        public string store_path { get; set; }
        public string series_count { get; set; }
        public string image_count { get; set; }

    }
    public class labinfo_4502
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
        /// 4|申请单号|字符型|50|||
        /// </summary> 
		public string appy_no { get; set; }

        /// <summary>
        /// 5|申请机构代码|字符型|50|||
        /// </summary> 
		public string appy_org_code { get; set; }

        /// <summary>
        /// 6|申请机构名称|字符型|50|||
        /// </summary> 
		public string appy_org_name { get; set; }

        /// <summary>
        /// 7|开单医生代码|字符型|30|||
        /// </summary> 
		public string bilg_dr_codg { get; set; }

        /// <summary>
        /// 8|开单医生姓名|字符型|30|||
        /// </summary> 
		public string bilg_dr_name { get; set; }

        /// <summary>
        /// 9|检验机构代码|字符型|50|||
        /// </summary> 
		public string exam_org_code { get; set; }

        /// <summary>
        /// 10|检验机构名称|字符型|100|||
        /// </summary> 
		public string exam_org_name { get; set; }

        /// <summary>
        /// 11|申请科室代码|字符型|30|||参照科室代码（dept）
        /// </summary> 
		public string appy_dept_code { get; set; }

        /// <summary>
        /// 12|检查科室代码|字符型|30|||参照科室代码（dept）
        /// </summary> 
		public string exam_dept_code { get; set; }

        /// <summary>
        /// 13|检验方法|字符型|50|||
        /// </summary> 
		public string exam_mtd { get; set; }

        /// <summary>
        /// 14|报告单号|字符型|30|||
        /// </summary> 
		public string rpotc_no { get; set; }

        /// <summary>
        /// 15|检验 - 项目代码|字符型|30|||
        /// </summary> 
		public string exam_item_code { get; set; }

        /// <summary>
        /// 16|检验 - 项目名称|字符型|300|||
        /// </summary> 
		public string exam_item_name { get; set; }

        /// <summary>
        /// 17|院内检验-项目代码|字符型|30|||
        /// </summary> 
		public string inhosp_exam_item_code { get; set; }

        /// <summary>
        /// 18|院内检验-项目名称|字符型|300|||
        /// </summary> 
		public string inhosp_exam_item_name { get; set; }

        /// <summary>
        /// 19|报告日期|日期型||||
        /// </summary> 
		public string rpt_date { get; set; }

        /// <summary>
        /// 20|报告医师|字符型|50|||
        /// </summary> 
		public string rpot_doc { get; set; }

        /// <summary>
        /// 21|检查费用|数值型|16,2|||
        /// </summary> 
		public string exam_charge { get; set; }

        /// <summary>
        /// 22|有效标志|字符型|3||Y|
        /// </summary> 
		public string vali_flag { get; set; }
    }

    public class iteminfo_4502
    {
        /// <summary>
        /// 1|报告单号|字符型|30|||
        /// </summary> 
        public string rpotc_no { get; set; }

        /// <summary>
        /// 2|申请单号|字符型|50|||
        /// </summary> 
		public string appy_no { get; set; }

        /// <summary>
        /// 3|检验方法|字符型|50|||
        /// </summary> 
		public string exam_mtd { get; set; }

        /// <summary>
        /// 4|参考值|字符型|20|||
        /// </summary> 
		public string ref_val { get; set; }

        /// <summary>
        /// 5|检验-计量单位|字符型|200|||
        /// </summary> 
		public string exam_unt { get; set; }

        /// <summary>
        /// 6|检验-结果(数值)|数值型|11,0|||
        /// </summary> 
		public string exam_rslt_val { get; set; }

        /// <summary>
        /// 7|检验 - 结果(定性)|字符型|2000|||
        /// </summary> 
		public string exam_rslt_dicm { get; set; }

        /// <summary>
        /// 8|检验 - 项目明细代码|字符型|30|||
        /// </summary> 
		public string exam_item_detl_code { get; set; }

        /// <summary>
        /// 9|检验 - 项目明细名称|字符型|300|||
        /// </summary> 
		public string exam_item_detl_name { get; set; }

        /// <summary>
        /// 10|检查 / 检验结果异常标识|字符型|10|||
        /// </summary> 
		public string exam_rslt_abn { get; set; }
    }

    public class sampleinfo_4502
    {
        /// <summary>
        /// 2|报告单号|字符型|30|||
        /// </summary> 
        public string rpotc_no { get; set; }

        /// <summary>
        /// 3|申请单号|字符型|50|||
        /// </summary> 
		public string appy_no { get; set; }

        /// <summary>
        /// 4|采样日期|日期型||||
        /// </summary> 
		public string sapl_date { get; set; }

        /// <summary>
        /// 5|标本号|字符型|30|||
        /// </summary> 
		public string spcm_no { get; set; }

        /// <summary>
        /// 6|标本名称|字符型|30|||
        /// </summary> 
		public string spcm_name { get; set; }
    }
}
