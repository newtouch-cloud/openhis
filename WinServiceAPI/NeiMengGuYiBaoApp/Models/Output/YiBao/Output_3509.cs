using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_3509 : OutputBase
    {
        public Output3509 output { get; set; }
    }

    public class Output3509
    {
        /// <summary>
        /// 1|库存变更时间|日期时间型|yyyy-MM-dd HH:mm:ss|
        /// </summary>
        public string invChgTime { get; set; }

        /// <summary>
        /// 2|医疗目录编码|字符型|50|
        /// </summary>
        public string medListCodg { get; set; }

        /// <summary>
        /// 3|统筹区编号|字符型|6|
        /// </summary>
        public string poolareaNo { get; set; }

        /// <summary>
        /// 4|库存变更经办人姓名|字符型|50|
        /// </summary>
        public string invChgOpterName { get; set; }

        /// <summary>
        /// 5|创建机构编号|字符型|20|
        /// </summary>
        public string crteOptinsNo { get; set; }

        /// <summary>
        /// 6|医药机构目录编码|字符型|150|
        /// </summary>
        public string medinsListCodg { get; set; }

        /// <summary>
        /// 7|备注|字符型|500|
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// 8|数据更新时间|日期时间型|yyyy-MM-dd HH:mm:ss|
        /// </summary>
        public string updtTime { get; set; }

        /// <summary>
        /// 9|经办人姓名|字符型|50|
        /// </summary>
        public string opterName { get; set; }

        /// <summary>
        /// 10|数据唯一记录号|字符型|40|
        /// </summary>
        public string rid { get; set; }

        /// <summary>
        /// 11|数据创建时间|日期时间型|yyyy-MM-dd HH:mm:ss|
        /// </summary>
        public string crteTime { get; set; }

        /// <summary>
        /// 12|有效标志|字符型|3|Y|
        /// </summary>
        public string valiFlag { get; set; }

        /// <summary>
        /// 13|定点医药机构编号|字符型|30|
        /// </summary>
        public string fixmedinsCode { get; set; }

        /// <summary>
        /// 14|库存变更类型|字符型|6|Y|
        /// </summary>
        public string invChgType { get; set; }

        /// <summary>
        /// 15|处方药标志|字符型|3|Y|
        /// </summary>
        public string rxFlag { get; set; }

        /// <summary>
        /// 16|定点医药机构商品库存变更流水号|字符型|30|
        /// </summary>
        public string medinsProdInvChgNo { get; set; }

        /// <summary>
        /// 17|目录特项标志|字符型|3|Y|
        /// </summary>
        public string listSpItemFlag { get; set; }

        /// <summary>
        /// 18|定点医药机构批次流水号|字符型|30|
        /// </summary>
        public string fixmedinsBchno { get; set; }

        /// <summary>
        /// 19|经办时间|日期时间型|yyyy-MM-dd HH:mm:ss|
        /// </summary>
        public string optTime { get; set; }

        /// <summary>
        /// 20|经办人 ID|字符型|20|
        /// </summary>
        public string opterId { get; set; }

        /// <summary>
        /// 21|医药机构目录名称|字符型|100|
        /// </summary>
        public string medinsListName { get; set; }

        /// <summary>
        /// 22|数量|数值型|16,4|
        /// </summary>
        public decimal cnt { get; set; }

        /// <summary>
        /// 23|单价|数值型|16,6|
        /// </summary>
        public decimal pric { get; set; }

        /// <summary>
        /// 24|创建人姓名|字符型|50|
        /// </summary>
        public string crterName { get; set; }

        /// <summary>
        /// 25|创建人 ID|字符型|20|
        /// </summary>
        public string crterId { get; set; }

        /// <summary>
        /// 26|经办机构编号|字符型|20|
        /// </summary>
        public string optinsNo { get; set; }

        /// <summary>
        /// 27|拆零标志|字符型|3|Y|
        /// </summary>
        public string trdnFlag { get; set; }
    }


}
