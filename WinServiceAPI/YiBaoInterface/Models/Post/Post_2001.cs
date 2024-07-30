using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Post
{
    public class Post_2001
    {
        ///<summary>
        // 操作员代码
        /// </summary>
        public string operatorId { get; set; }

        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string operatorName { get; set; }
        /// <summary>
        /// 参保地
        /// </summary>
        public string insuplc_admdvs { get; set; }

        //1  psn_no 人员编号  字符型  30  Y 
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
        public string insutype { get; set; }
        //public string fixmedins_code { get; set; }
        public string med_type { get; set; }
        public string begntime { get; set; }
    }
}
