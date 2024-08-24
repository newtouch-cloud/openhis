using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_3513 : OutputBase
    {
        public Output3513 output { get; set; }
    }

    public class Output3513
    {
        /// <summary>
        /// 1|定点医药机构编号|字符型|50
        /// </summary>
        public string fixmedinsCode { get; set; }

        /// <summary>
        /// 2|医疗目录编码|字符型|6
        /// </summary>
        public string medListCodg { get; set; }

        /// <summary>
        /// 3|医药机构目录编码|字符型|30
        /// </summary>
        public string medinsListCodg { get; set; }

        /// <summary>
        /// 4|医药机构目录名称|字符型|20
        /// </summary>
        public string medinsListName { get; set; }

        /// <summary>
        /// 5|定点医药机构批次流水号|字符型|150
        /// </summary>
        public string fixmedinsBchno { get; set; }

        /// <summary>
        /// 6|就诊 ID|字符型|30
        /// </summary>
        public string mdtrtId { get; set; }

        /// <summary>
        /// 7|就诊结算类型|字符型|Y
        /// </summary>
        public string mdtrtSetlType { get; set; }

        /// <summary>
        /// 8|记账流水号|字符型|100
        /// </summary>
        public string bkkpSn { get; set; }

        /// <summary>
        /// 9|药品追溯码|字符型|30
        /// </summary>
        public string drugTracCodg { get; set; }

        /// <summary>
        /// 10|人员编号|字符型|30
        /// </summary>
        public string psnNo { get; set; }

        /// <summary>
        /// 11|人员证件类型|字符型|6|Y
        /// </summary>
        public string psnCertType { get; set; }

        /// <summary>
        /// 12|证件号码|字符型|30
        /// </summary>
        public string certno { get; set; }

        /// <summary>
        /// 13|人员姓名|字符型|50
        /// </summary>
        public string psnName { get; set; }

        /// <summary>
        /// 14|备注|字符型|500
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// 15|拆零标志|字符型|3
        /// </summary>
        public string trdnFlag { get; set; }

        /// <summary>
        /// 16|数据更新时间|日期时间型|yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string updtTime { get; set; }

        /// <summary>
        /// 17|经办人姓名|字符型|50
        /// </summary>
        public string opterName { get; set; }

        /// <summary>
        /// 18|数据唯一记录号|字符型|40
        /// </summary>
        public string rid { get; set; }

        /// <summary>
        /// 19|数据创建时间|日期时间型|yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string crteTime { get; set; }

        /// <summary>
        /// 20|经办时间|日期时间型|yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string optTime { get; set; }

        /// <summary>
        /// 21|经办人 ID|字符型|20
        /// </summary>
        public string opterId { get; set; }

        /// <summary>
        /// 22|创建人姓名|字符型|50
        /// </summary>
        public string crterName { get; set; }

        /// <summary>
        /// 23|创建人 ID|字符型|20
        /// </summary>
        public string crterId { get; set; }

        /// <summary>
        /// 24|经办机构编号|字符型|20
        /// </summary>
        public string optinsNo { get; set; }
    }



}
