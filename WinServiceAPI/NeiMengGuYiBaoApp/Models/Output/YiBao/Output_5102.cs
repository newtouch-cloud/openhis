using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_5102 : OutputBase
    {
        public List<Output5102> data { get; set; }
    }
    public class Output5102 {
        // 人员证件类型 字符型	6
        public string psn_cert_type { get; set; }
        //证件号码	字符型	50
        public string certno { get; set; }
        //执业人员自编号	字符型	20
        public string prac_psn_no { get; set; }
        //执业人员代码	字符型	20
        public string prac_psn_code { get; set; }
        //执业人员姓名	字符型	50  
        public string prac_psn_name { get; set; }
        //执业人员分类	字符型	6
        public string prac_psn_type { get; set; }
        //执业人员资格证书编码	字符型	30
        public string prac_psn_cert { get; set; }
        //执业证书编号	字符型	30
        public string prac_cert_no { get; set; }
        //医保医师标志	字符型	3
        public string hi_dr_flag { get; set; }
        //开始时间	日期时间型	　
        public string begntime { get; set; }
        //结束时间	日期时间型   
        public string endtime { get; set; }
        //变更原因	字符型	200
        public string chg_rea { get; set; }
    }
}
