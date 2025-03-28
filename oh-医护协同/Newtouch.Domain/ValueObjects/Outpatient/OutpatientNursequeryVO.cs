using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    public class OutpatientNursequeryVO
    {
        /// <summary>
        /// 处方明细id
        /// </summary>
        public string cfmxId { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// 患者名称
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 患者性别 1:男 2：女
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypcode { get; set; }
        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// 组号
        /// </summary>
        public string zh { get; set; }

        /// <summary>
        /// 开立时间
        /// </summary>
        public DateTime? kssj { get; set; }

        /// <summary>
        /// 处方明细内容
        /// </summary>
        public string cfmxnr { get; set; }

        /// <summary>
        /// 用法代码
        /// </summary>
        public string yfcode { get; set; }
        /// <summary>
        /// 用法名称
        /// </summary>
        public string ypyfmc { get; set; }
        public string lrjg { get; set; }
        public string cfid { get; set; }
        public string cfh { get; set; }
        public int? cflx { get; set; }
        public string barcode { get; set; }
        public string yscode { get; set; }
        public string ysmc { get; set; }
        public string xb { get; set; }
        public string CreatorName { get; set; }
        public DateTime? CreateTime { get; set; }
        public string LastModifierName { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string cancel { get; set; }
        public string iscancel { get; set; }
        public string gmxxid { get; set; }
        public decimal je { get; set; }
    }
}
