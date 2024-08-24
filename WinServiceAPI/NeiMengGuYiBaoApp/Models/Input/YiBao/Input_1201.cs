using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_1201:InputBase
    {
    
        public medinsinfo1201 medinsinfo { get; set; }

    }
    public class medinsinfo1201
    {
        /// <summary>
        /// 1|定点医疗服务机构类型|字符型|6|Y
        /// </summary>
        public string fixmedins_type { get; set; }
        /// <summary>
        /// 1|定点医药机构名称|字符型|200
        /// </summary>
        public string fixmedins_name { get; set; }
        /// <summary>
        /// 1|定点医药机构编号|字符型|12|查询定点零售药店时填写定点零售药店代码；查询定点医疗机构时填写定点医疗机构代码。
        /// </summary>
        public string fixmedins_code { get; set; }
    }
}
