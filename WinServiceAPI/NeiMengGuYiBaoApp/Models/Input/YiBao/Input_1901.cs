using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_1901 : InputBase
    {
        public data1901 data { get; set; }

    }
    public class data1901
    {
        //        1  type 字典类型  字符型  30  Y 
        //2  parentValue 父字典键值  字符型  10 
        //3  admdvs 行政区划  字符型  6  Y Y 
        //4  date 查询日期  日期型 Y 
        //5  valiFlag 有效标志  字符型

        public string type { get; set; }
        public string parentValue { get; set; }
        public string admdvs { get; set; }
        public string date { get; set; }
        public string vali_Flag { get; set; }
        public string valiFlag { get; set; }
        public string tradiNumber { get; set; }
        public string operatorId { get; set; }
        public string operatorName { get; set; }
    }
}
