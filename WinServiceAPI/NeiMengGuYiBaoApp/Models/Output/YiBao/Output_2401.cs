using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_2401 : OutputBase
    {
        public result2401 result { get; set; }
    }

    public class result2401
    {
        /// <summary>
        /// 1 | 就诊ID |字符型 |30|Y |医保返回唯一流水 
        /// </summary>
        public string mdtrt_id { get; set; }
    }
}
