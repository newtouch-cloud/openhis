using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
   public class Input_5201:InputBase
    {
        public data5201 data { get; set; }
    }

    public class data5201
    {
        public string psn_no { get; set; }
        public string begntime { get; set; }
        public string endtime { get; set; }
        public string med_type { get; set; }
        public string mdtrt_id { get; set; }
        //        5201【5201】就诊信息查询
        //1  psn_no 人员编号  字符型
        //2  begntime 开始时间 时分秒
        //3  endtime 结束时间 
        //4  med_type 医疗类别
        //5  mdtrt_id 就诊 ID

    }
}
