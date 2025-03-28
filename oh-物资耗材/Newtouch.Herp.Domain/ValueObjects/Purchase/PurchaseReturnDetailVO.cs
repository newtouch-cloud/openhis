using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.ValueObjects.Purchase
{
    public class PurchaseReturnDetailVO
    {

        public string productmc { get; set; }
        public string cglxmc { get; set; }
        public string thlxmc { get; set; }
        public string psmxtmlxmc { get; set; }
        public string qymc { get; set; }

        public string thmxId { get; set; }
        /// <summary>
        /// 采购编号
        /// </summary>
        public string thId { get; set; }
        public string OrganizeId { get; set; }
        public string SXH { get; set; }
        public string CGLX { get; set; }
        public string THLX { get; set; }
        public string HCTBDM { get; set; }
        public string HCXFDM { get; set; }
        public string YYBDDM { get; set; }
        public string CGGGXH { get; set; }
        public string SCPH { get; set; }
        public string SCRQ { get; set; }
        public string YXRQ { get; set; }
        public string PSMXTMLX { get; set; }
        public string PSMXTM { get; set; }
        public decimal THSL { get; set; }
        public decimal THDJ { get; set; }
        public string QYBM { get; set; }
        public string THYY { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
    }
}
