using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_3512 : OutputBase
    {
        public Output3512 output { get; set; }
    }

    public class Output3512
    {
        /// <summary>
        /// 1|定点医药机构编号|字符型|30|
        /// </summary>
        public string fixmedinsCode { get; set; }

        /// <summary>
        /// 2|医药机构目录编码|字符型|150|
        /// </summary>
        public string medinsListCodg { get; set; }

        /// <summary>
        /// 3|定点医药机构批次流水号|字符型|30|
        /// </summary>
        public string fixmedinsBchno { get; set; }

        /// <summary>
        /// 9|医疗目录编码|字符型|50|
        /// </summary>
        public string medListCodg { get; set; }

        /// <summary>
        /// 10|医药机构目录名称|字符型|150|
        /// </summary>
        public string medinsListName { get; set; }

        /// <summary>
        /// 11|药品追溯码|字符型|30|
        /// </summary>
        public string drugTracCodg { get; set; }

        /// <summary>
        /// 12|有效标志|字符型|3|Y|
        /// </summary>
        public string valiFlag { get; set; }

        /// <summary>
        /// 13|经办人 ID|字符型|20|
        /// </summary>
        public string opterId { get; set; }

        /// <summary>
        /// 14|创建人姓名|字符型|50|
        /// </summary>
        public string crterName { get; set; }

        /// <summary>
        /// 15|创建人 ID|字符型|20|
        /// </summary>
        public string crterId { get; set; }

        /// <summary>
        /// 16|经办机构编号|字符型|20|
        /// </summary>
        public string optinsNo { get; set; }
    }



}
