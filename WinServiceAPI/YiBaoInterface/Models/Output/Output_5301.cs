using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Output
{
    public class Output_5301 : OutputBase
    {
        public feedetail feedetail { get; set; }
    }

    public class feedetail
    {
        //        1  opsp_dise_code 门慢门特病种目录代码  字符型  30  Y
        //2  opsp_dise_name 门慢门特病种名称  字符型  300  Y
        //3  begndate 开始日期  日期型 Y yyyy-MM-dd
        //4  enddate 结束日期  日期型 yyyy-MM-dd

        public string opsp_dise_code { get; set; }
        public string opsp_dise_name { get; set; }
        public string begndate { get; set; }
        public string enddate { get; set; }
    }
}
