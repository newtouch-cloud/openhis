using NeiMengGuYiBaoApp.Models.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.SQL
{
    public class Drjk_szsh_input : SqlBase
    {
        //	参保人标识	字符型	50	参保人唯一ID
        public string patn_id { get; set; }
        //	姓名	字符型	50	
        //public string patn_name { get; set; }
        ////	性别	字符型	3	参考字典表
        //public string gend { get; set; }
        ////	出生日期	日期型		格式：yyyy-MM-dd
        //public string brdy { get; set; }
        ////	统筹区编码	字符型	10	参保人所属统筹区
        //public string poolarea { get; set; }
        ////	当前就诊标识	字符型	50	本次就诊记录唯一ID
        //public string curr_mdtrt_id { get; set; }
        ////	就诊信息集合	就诊信息集合		
        ////public string fsi_encounter_dtos { get; set; }
        //public List<InputFED3102> fsi_encounter_dtos { get; set; }
        ////	医院信息集合	医院信息集合		
        //public string fsi_his_data_dto { get; set; }
        //public DateTime? czrq { get; set; }
        //public string czydm { get; set; }
        //public string zyh { get; set; }


        //public int zt { get; set; }
        //public string zt_czy { get; set; }
        //public DateTime? zt_rq { get; set; }
    }
}
