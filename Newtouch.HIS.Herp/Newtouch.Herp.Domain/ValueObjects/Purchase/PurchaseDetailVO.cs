using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.ValueObjects.Purchase
{
    public class PurchaseDetailVO
    {

        public string productmc { get; set; }
        public string cglxmc { get; set; }
        public string dcpsbsmc { get; set; }
        public string sfjjmc { get; set; }
        public string psyqmc { get; set; }
        public string qymc { get; set; }


        public string cgmxId { get; set; }
        /// <summary>
        /// 采购编号
        /// </summary>
        public string cgId { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// 药品编号
        /// </summary>
        public string productCode { get; set; }

        public string sxh { get; set; }
        public string cglx { get; set; }
        public string hctbdm { get; set; }
        public string hcxfdm { get; set; }
        public string yybddm { get; set; }
        public string cgggxh { get; set; }
        public string pssm { get; set; }
        public decimal cgsl { get; set; }
        public decimal cgdj { get; set; }
        public string qybm { get; set; }
        public string sfjj { get; set; }
        public string psyq { get; set; }
        public string dcpsbs { get; set; }
        public string cwxx { get; set; }
        public string bzsm { get; set; }


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
