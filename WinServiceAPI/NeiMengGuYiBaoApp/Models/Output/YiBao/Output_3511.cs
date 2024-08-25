using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_3511 : OutputBase
    {
        public Output3511 output { get; set; }
    }

    public class Output3511
    {
        /// <summary>
        /// 1|有效期止|日期型|yyyy-MM-dd|
        /// </summary>
        public string expyEnd { get; set; }

        /// <summary>
        /// 2|药师证件类型|字符型|3|Y|
        /// </summary>
        public string pharCertType { get; set; }

        /// <summary>
        /// 3|销售/退货经办人姓名|字符型|50|
        /// </summary>
        public string selRetnOpterName { get; set; }

        /// <summary>
        /// 4|医疗目录编码|字符型|50|
        /// </summary>
        public string medListCodg { get; set; }

        /// <summary>
        /// 5|统筹区编号|字符型|6|
        /// </summary>
        public string poolareaNo { get; set; }

        /// <summary>
        /// 6|结算 ID|字符型|30|
        /// </summary>
        public string setlId { get; set; }

        /// <summary>
        /// 7|创建机构编号|字符型|20|
        /// </summary>
        public string crteOptinsNo { get; set; }

        /// <summary>
        /// 8|医药机构目录编码|字符型|150|
        /// </summary>
        public string medinsListCodg { get; set; }

        /// <summary>
        /// 9|备注|字符型|500|
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// 10|数据更新时间|日期时间型|yyyy-MM-dd HH:mm:ss|
        /// </summary>
        public string updtTime { get; set; }

        /// <summary>
        /// 11|医保费用结算类型|字符型|6|Y|
        /// </summary>
        public string hiFeesetlType { get; set; }

        /// <summary>
        /// 12|生产日期|日期型|yyyy-MM-dd|
        /// </summary>
        public string manuDate { get; set; }

        /// <summary>
        /// 13|经办人姓名|字符型|50|
        /// </summary>
        public string opterName { get; set; }

        /// <summary>
        /// 14|人员编号|字符型|30|
        /// </summary>
        public string psnNo { get; set; }

        /// <summary>
        /// 15|数据唯一记录号|字符型|40|
        /// </summary>
        public string rid { get; set; }

        /// <summary>
        /// 16|数据创建时间|日期时间型|yyyy-MM-dd HH:mm:ss|
        /// </summary>
        public string crteTime { get; set; }

        /// <summary>
        /// 17|药师证件号码|字符型|50|
        /// </summary>
        public string pharCertno { get; set; }

        /// <summary>
        /// 18|有效标志|字符型|3|Y|
        /// </summary>
        public string valiFlag { get; set; }

        /// <summary>
        /// 19|证件号码|字符型|600|
        /// </summary>
        public string certno { get; set; }

        /// <summary>
        /// 20|定点医药机构编号|字符型|30|
        /// </summary>
        public string fixmedinsCode { get; set; }

        /// <summary>
        /// 21|处方药标志|字符型|3|Y|
        /// </summary>
        public string rxFlag { get; set; }

        /// <summary>
        /// 22|药师执业资格证号|字符型|50|
        /// </summary>
        public string pharPracCertNo { get; set; }

        /// <summary>
        /// 23|目录特项标志|字符型|3|Y|
        /// </summary>
        public string listSpItemFlag { get; set; }

        /// <summary>
        /// 24|定点医药机构批次流水号|字符型|30|
        /// </summary>
        public string fixmedinsBchno { get; set; }

        /// <summary>
        /// 25|经办时间|日期时间型|yyyy-MM-dd HH:mm:ss|
        /// </summary>
        public string optTime { get; set; }

        /// <summary>
        /// 26|人员姓名|字符型|50|
        /// </summary>
        public string psnName { get; set; }

        /// <summary>
        /// 27|电子监管编码|字符型|20|
        /// </summary>
        public string elecSupnCodg { get; set; }

        /// <summary>
        /// 28|开单医师姓名|字符型|50|
        /// </summary>
        public string bilgDrName { get; set; }

        /// <summary>
        /// 29|经办人 ID|字符型|20|
        /// </summary>
        public string opterId { get; set; }

        /// <summary>
        /// 30|生产批号|字符型|30|
        /// </summary>
        public string manuLotnum { get; set; }

        /// <summary>
        /// 31|医药机构目录名称|字符型|100|
        /// </summary>
        public string medinsListName { get; set; }

        /// <summary>
        /// 32|人员证件类型|字符型|6|Y|
        /// </summary>
        public string psnCertType { get; set; }

        /// <summary>
        /// 33|销售/退货时间|日期时间型|yyyy-MM-dd HH:mm:ss|
        /// </summary>
        public string selRetnTime { get; set; }

        /// <summary>
        /// 34|销售/退货数量|数值型|16,2|
        /// </summary>
        public decimal selRetnCnt { get; set; }

        /// <summary>
        /// 35|创建人姓名|字符型|50|
        /// </summary>
        public string crterName { get; set; }

        /// <summary>
        /// 36|药师姓名|字符型|50|
        /// </summary>
        public string pharName { get; set; }

        /// <summary>
        /// 37|开单医师证件类型|字符型|3|Y|
        /// </summary>
        public string prscDrCertType { get; set; }

        /// <summary>
        /// 38|创建人 ID|字符型|20|
        /// </summary>
        public string crterId { get; set; }

        /// <summary>
        /// 39|开单医师证件号码|字符型|50|
        /// </summary>
        public string prscDrCertno { get; set; }

        /// <summary>
        /// 40|经办机构编号|字符型|20|
        /// </summary>
        public string optinsNo { get; set; }

        /// <summary>
        /// 41|拆零标志|字符型|3|Y|
        /// </summary>
        public string trdnFlag { get; set; }

        /// <summary>
        /// 42|最终成交单价|数值型|16,6|
        /// </summary>
        public decimal finlTrnsPric { get; set; }

        /// <summary>
        /// 43|定点医药机构商品销售流水号|字符型|30|
        /// </summary>
        public string medinsProdSelNo { get; set; }
    }


}
