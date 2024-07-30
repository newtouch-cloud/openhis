using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.Purchase
{
    [Table("xt_yp_fpmx")]
    public class PurchaseBillDetailEntity : IEntity<PurchaseBillDetailEntity>
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string fph { get; set; }

        public string fprq { get; set; }
        public decimal fphszje { get; set; }
        public string yqbm { get; set; }
        public string yybm { get; set; }
        public string psdbm { get; set; }
        public string dlsgbz { get; set; }
        public string fpbz { get; set; }
        public string sfwpsfp { get; set; }
        public string wpsfpsm { get; set; }
        public string fpmxbh { get; set; }
        public string splx { get; set; }
        public string sfch { get; set; }
        public string zxspbm { get; set; }
        public string scph { get; set; }
        public string scrq { get; set; }
        public decimal spsl { get; set; }
        public string glmxbh { get; set; }
        public string xsddh { get; set; }
        public int? sxh { get; set; }
        public string yxrq { get; set; }
        public decimal wsdj { get; set; }
        public decimal hsdj { get; set; }
        public decimal sl { get; set; }
        public decimal se { get; set; }
        public decimal hsje { get; set; }
        public decimal pfj { get; set; }
        public decimal lsj { get; set; }
        public string pzwh { get; set; }


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
