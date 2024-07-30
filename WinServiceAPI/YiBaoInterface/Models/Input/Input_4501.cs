using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_4501 : InputBase
    {
        /// <summary>
        /// 表 174 输入-检查记录（节点标识：examinfo）
        /// </summary>
        public examinfo_4501 examinfo { get; set; }
        /// <summary>
        /// 表 175 检查项目信息（节点标识：iteminfo
        /// </summary>
        public List<iteminfo_4501> iteminfo { get; set; }
        /// <summary>
        /// 表 176 检查标本信息（节点标识：sampleinfo）
        /// </summary>
        public List<sampleinfo_4501> sampleinfo { get; set; }
        /// <summary>
        /// 表 177 检查影像信息（节点标识：imageinfo）
        /// </summary>
        public List<imageinfo_4501> imageinfo { get; set; }

    }

    public class examinfo_4501
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
        /// 5|申请单据名称|字符型|50|||
        /// </summary> 
		public string appy_doc_name { get; set; }

        /// <summary>
        /// 6|报告单号|字符型|30|||
        /// </summary> 
		public string rpotc_no { get; set; }

        /// <summary>
        /// 7|报告单类别代码|字符型|30|Y||
        /// </summary> 
		public string rpotc_type_code { get; set; }

        /// <summary>
        /// 8|检查报告单名称|字符型|50|||
        /// </summary> 
		public string exam_rpotc_name { get; set; }

        /// <summary>
        /// 9|检查日期|日期型||||
        /// </summary> 
		public string exam_date { get; set; }

        /// <summary>
        /// 10|报告日期|日期型||||
        /// </summary> 
		public string rpt_date { get; set; }

        /// <summary>
        /// 11|送检日期|日期型||||
        /// </summary> 
		public string cma_date { get; set; }

        /// <summary>
        /// 12|采样日期|日期型||||
        /// </summary> 
		public string sapl_date { get; set; }

        /// <summary>
        /// 13|标本号|字符型|30|||
        /// </summary> 
		public string spcm_no { get; set; }

        /// <summary>
        /// 14|标本名称|字符型|30|||
        /// </summary> 
		public string spcm_name { get; set; }

        /// <summary>
        /// 15|检查类别代码|字符型|10|||
        /// </summary> 
		public string exam_type_code { get; set; }

        /// <summary>
        /// 16|检查-项目代码|字符型|30|||
        /// </summary> 
		public string exam_item_code { get; set; }

        /// <summary>
        /// 17|检查-类别名称|字符型|100|||
        /// </summary> 
		public string exam_type_name { get; set; }

        /// <summary>
        /// 18|检查-项目名称|字符型|300|||
        /// </summary> 
		public string exam_item_name { get; set; }

        /// <summary>
        /// 19|院内检查-项目代码|字符型|30|||
        /// </summary> 
		public string inhosp_exam_item_code { get; set; }

        /// <summary>
        /// 20|院内检查-项目名称|字符型|300|||
        /// </summary> 
		public string inhosp_exam_item_name { get; set; }

        /// <summary>
        /// 21|检查部位|字符型|500|||
        /// </summary> 
		public string exam_part { get; set; }

        /// <summary>
        /// 22|检查结果阳性标志|字符型|2|Y||
        /// </summary> 
		public string exam_rslt_poit_flag { get; set; }

        /// <summary>
        /// 23|检查 / 检验结果异常标志|字符型|10|Y||
        /// </summary> 
		public string exam_rslt_abn { get; set; }

        /// <summary>
        /// 24|检查结论|字符型|1000|||
        /// </summary> 
		public string exam_ccls { get; set; }

        /// <summary>
        /// 25|申请机构名称|字符型|50|||
        /// </summary> 
		public string appy_org_name { get; set; }

        /// <summary>
        /// 26|申请科室代码|字符型|30|||参照科室代码（dept）
        /// </summary> 
		public string appy_dept_code { get; set; }

        /// <summary>
        /// 27|检查科室代码|字符型|30|||参照科室代码（dept）
        /// </summary> 
		public string exam_dept_code { get; set; }

        /// <summary>
        /// 28|住院科室代码|字符型|30|||参照科室代码（dept）
        /// </summary> 
		public string ipt_dept_code { get; set; }

        /// <summary>
        /// 29|住院科室名称|字符型|50|||
        /// </summary> 
		public string ipt_dept_name { get; set; }

        /// <summary>
        /// 30|开单医生代码|字符型|30|||
        /// </summary> 
		public string bilg_dr_codg { get; set; }

        /// <summary>
        /// 31|开单医生姓名|字符型|30|||
        /// </summary> 
		public string bilg_dr_name { get; set; }

        /// <summary>
        /// 32|执行机构名称|字符型|200|||
        /// </summary> 
		public string exe_org_name { get; set; }

        /// <summary>
        /// 33|有效标志|字符型|3|Y  |Y|
        /// </summary> 
		public string vali_flag { get; set; }
    }

    public class iteminfo_4501
    {
        /// <summary>
        /// 1|申请单号|字符型|50|||
        /// </summary> 
		public string appy_no { get; set; }

        /// <summary>
        /// 2|报告单号|字符型|30|||
        /// </summary> 
		public string rpotc_no { get; set; }

        /// <summary>
        /// 3|检查-项目代码|字符型|30|||
        /// </summary> 
		public string exam_item_code { get; set; }

        /// <summary>
        /// 4|检查-项目名称|字符型|300|||
        /// </summary> 
		public string exam_item_name { get; set; }

        /// <summary>
        /// 5|院内检查-项目代码|字符型|30|||
        /// </summary> 
		public string inhosp_exam_item_code { get; set; }

        /// <summary>
        /// 6|院内检查-项目名称|字符型|300|||
        /// </summary> 
		public string inhosp_exam_item_name { get; set; }

        /// <summary>
        /// 7|检查费用|数值型|16,2|||
        /// </summary> 
		public string exam_charge { get; set; }
    }

    public class sampleinfo_4501
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
        /// 3|采样日期|日期型||||
        /// </summary> 
		public string sapl_date { get; set; }

        /// <summary>
        /// 4|标本号|字符型|30|||
        /// </summary> 
		public string spcm_no { get; set; }

        /// <summary>
        /// 5|标本名称|字符型|30|||
        /// </summary> 
		public string spcm_name { get; set; }
    }
    public class imageinfo_4501
    {
        /// <summary>
        /// 1|报告单号|字符型|30||Y|
        /// </summary> 
		public string rpotc_no { get; set; }

        /// <summary>
        /// 2|全局唯一号|字符型|200||Y|图像中的study_u id
        /// </summary> 
		public string study_uid { get; set; }

        /// <summary>
        /// 3|检查号|字符型|30|||
        /// </summary> 
		public string patient_id { get; set; }

        /// <summary>
        /// 4|患者姓名|字符型|30|||图像中的患者姓名
        /// </summary> 
		public string patient_name { get; set; }

        /// <summary>
        /// 5|图像流水号|字符型|30|||图像中的Accessi onNumbe r
        /// </summary> 
		public string acession_no { get; set; }

        /// <summary>
        /// 6|检查时间|日期型||||
        /// </summary> 
		public string study_time { get; set; }

        /// <summary>
        /// 7|检查类型|字符型|20|||
        /// </summary> 
		public string modality { get; set; }

        /// <summary>
        /// 8|存储路径|字符型|300|||
        /// </summary> 
		public string store_path { get; set; }

        /// <summary>
        /// 9|序列数量|数值型|8,0|||
        /// </summary> 
		public string series_count { get; set; }

        /// <summary>
        /// 10|图像数量|数值型|8,0|||
        /// </summary> 
		public string image_count { get; set; }
    }
}
