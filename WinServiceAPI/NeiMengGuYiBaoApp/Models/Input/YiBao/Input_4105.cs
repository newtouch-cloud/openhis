using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_4105: InputBase
    {
        public data4105 data { get; set; }
    }

    public class data4105
    {
        public string fixmedins_code { get; set; }//定点医药机构编号 字符型 30 Y
        public string medtype_list { get; set; }//医疗类别的集合 Y对应字典值med_type
        public string stt_begntime { get; set; }//日期时间型
        public string stt_endtime { get; set; }//日期时间型
        public string pay_loc_list { get; set; }//支付地点的集合对应字典值pay_loc
        public string clr_way_list { get; set; }//清算方式的集合对应字典值clr_way
        public string refd_setl_flag { get; set; }//退费统计标志 字符型 3 1：退费时负的结算数据计入统计0:退费时负的结算数据不计入统计
    }
}
