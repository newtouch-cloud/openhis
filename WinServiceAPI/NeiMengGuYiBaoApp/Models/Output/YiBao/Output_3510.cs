using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_3510 : OutputBase
    {
        public Output3510 output { get; set; }
    }

    public class Output3510
    {
        /// <summary>
        /// 1|有效期止|日期型|yyyy-MM-dd|
        /// </summary>
        public string expyEnd { get; set; }

        /// <summary>
        /// 2|医疗目录编码|字符型|50|
        /// </summary>
        public string medListCodg { get; set; }

        /// <summary>
        /// 3|统筹区编号|字符型|6|
        /// </summary>
        public string poolareaNo { get; set; }

        /// <summary>
        /// 4|创建机构编号|字符型|20|
        /// </summary>
        public string crteOptinsNo { get; set; }

        /// <summary>
        /// 5|医药机构目录编码|字符型|150|
        /// </summary>
        public string medinsListCodg { get; set; }

        /// <summary>
        /// 6|备注|字符型|500|
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// 7|商品退货标志|字符型|6|Y|
        /// </summary>
        public string prodRetnFlag { get; set; }

        /// <summary>
        /// 8|数据更新时间|日期时间型|yyyy-MM-dd HH:mm:ss|
        /// </summary>
        public string updtTime { get; set; }

        /// <summary>
        /// 9|生产日期|日期型|yyyy-MM-dd|
        /// </summary>
        public string manuDate { get; set; }

        /// <summary>
        /// 10|经办人姓名|字符型|50|
        /// </summary>
        public string opterName { get; set; }

        /// <summary>
        /// 11|数据唯一记录号|字符型|40|
        /// </summary>
        public string rid { get; set; }

        /// <summary>
        /// 12|数据创建时间|日期时间型|yyyy-MM-dd HH:mm:ss|
        /// </summary>
        public string crteTime { get; set; }

        /// <summary>
        /// 13|生产企业名称|字符型|200|
        /// </summary>
        public string prodentpName { get; set; }

        /// <summary>
        /// 14|有效标志|字符型|3|Y|
        /// </summary>
        public string valiFlag { get; set; }

        /// <summary>
        /// 15|批准文号|字符型|100|
        /// </summary>
        public string aprvno { get; set; }

        /// <summary>
        /// 16|定点医药机构编号|字符型|30|
        /// </summary>
        public string fixmedinsCode { get; set; }

        /// <summary>
        /// 17|定点医药机构商品采购流水号|字符型|30|
        /// </summary>
        public string medinsProdPurcNo { get; set; }

        /// <summary>
        /// 18|处方药标志|字符型|3|Y|
        /// </summary>
        public string rxFlag { get; set; }

        /// <summary>
        /// 19|目录特项标志|字符型|3|Y|
        /// </summary>
        public string listSpItemFlag { get; set; }

        /// <summary>
        /// 20|采购发票号|字符型|50|
        /// </summary>
        public string purcInvoNo { get; set; }

        /// <summary>
        /// 21|定点医药机构批次流水号|字符型|30|
        /// </summary>
        public string fixmedinsBchno { get; set; }

        /// <summary>
        /// 22|经办时间|日期时间型|yyyy-MM-dd HH:mm:ss|
        /// </summary>
        public string optTime { get; set; }

        /// <summary>
        /// 23|经办人 ID|字符型|20|
        /// </summary>
        public string opterId { get; set; }

        /// <summary>
        /// 24|采购/退货入库时间|日期时间型|yyyy-MM-dd HH:mm:ss|
        /// </summary>
        public string purcRetnStoinTime { get; set; }

        /// <summary>
        /// 25|生产批号|字符型|30|
        /// </summary>
        public string manuLotnum { get; set; }

        /// <summary>
        /// 26|医药机构目录名称|字符型|100|
        /// </summary>
        public string medinsListName { get; set; }

        /// <summary>
        /// 27|供货商名称|字符型|200|
        /// </summary>
        public string splerName { get; set; }

        /// <summary>
        /// 28|商品赠送标志|字符型|3|Y|
        /// </summary>
        public string prodGeayFlag { get; set; }

        /// <summary>
        /// 29|采购/退货经办人姓名|字符型|50|
        /// </summary>
        public string purcRetnOpterName { get; set; }

        /// <summary>
        /// 30|采购发票编码|字符型|50|
        /// </summary>
        public string purcInvoCodg { get; set; }

        /// <summary>
        /// 31|创建人姓名|字符型|50|
        /// </summary>
        public string crterName { get; set; }

        /// <summary>
        /// 32|创建人 ID|字符型|20|
        /// </summary>
        public string crterId { get; set; }

        /// <summary>
        /// 33|经办机构编号|字符型|20|
        /// </summary>
        public string optinsNo { get; set; }

        /// <summary>
        /// 34|供应商许可证号|字符型|50|
        /// </summary>
        public string splerPmtno { get; set; }

        /// <summary>
        /// 35|最终成交单价|数值型|16,6|
        /// </summary>
        public decimal finlTrnsPric { get; set; }

        /// <summary>
        /// 36|采购/退货数量|数值型|16,2|
        /// </summary>
        public decimal purcRetnCnt { get; set; }

        /// <summary>
        /// 37|随货单号|字符型|50|
        /// </summary>
        public string dyntNo { get; set; }
    }


}
