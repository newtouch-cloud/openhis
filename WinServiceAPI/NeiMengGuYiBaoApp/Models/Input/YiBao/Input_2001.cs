using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_2001:InputBase
    {
        public data2001 data { get; set; }
    }
    public class data2001
    {
        //        1  psn_no 人员编号  字符型  30  Y 
        //2  insutype 险种类型  字符型  6  Y Y 
        //3  fixmedins_code 定点医药机构编号字符型  12  Y 
        //4  med_type 医疗类别  字符型  6  Y Y 

        //5  begntime 开始时间  日期时间型 Y  yyyy-MM-dd HH:mm:ss
        //6  endtime 结束时间  日期时间型 yyyy-MM-dd HH:mm:ss
        //7  dise_codg 病种编码  字符型  
        //8  dise_name 病种名称  字符型  500 
        //9  oprn_oprt_code 手术操作代

        //10 oprn_oprt_name 手术操作名称字符型  500 
        //11  matn_type 生育类别  字符型  6  Y 
        //12  birctrl_type 计划生育手术类别字符型  6  Y

        public string psn_no { get; set; }
        /// <summary>
        /// 险种类型
        /// </summary>
        public string insutype { get; set; }
        /// <summary>
        /// 定点医药机构编号
        /// </summary>
        public string fixmedins_code { get; set; }
        /// <summary>
        /// 医疗类别
        /// </summary>
        public string med_type { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string begntime { get; set; }
        /// <summary>`
        /// 结束时间
        /// </summary>
        public string endtime { get; set; }
        /// <summary>
        /// 病种编码
        /// </summary>
        public string dise_codg { get; set; }
        /// <summary>
        /// 病种名称
        /// </summary>
        public string dise_name { get; set; }
        /// <summary>
        /// 手术操作代码
        /// </summary>
        public string oprn_oprt_code { get; set; }
        /// <summary>
        /// 手术操作名称
        /// </summary>
        public string oprn_oprt_name { get; set; }
        /// <summary>
        /// 计划生育手术类别
        /// </summary>
        public string matn_type { get; set; }
        /// <summary>
        /// 计划生育手术类别
        /// </summary>
        public string birctrl_type { get; set; }
        public string exp_content { get; set; }
    }
}
