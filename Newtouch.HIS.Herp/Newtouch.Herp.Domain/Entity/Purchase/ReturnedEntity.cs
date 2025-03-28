using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.Entity.Purchase
{
    /// <summary>
    /// 退货
    /// </summary>
    [Table("xt_wz_th")]
    public class ReturnedEntity : IEntity<ReturnedEntity>
    {
        [Key]


        public string thId { get; set; }
        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }
        public string CZLX { get; set; }
        public string YYBM { get; set; }
        public string PSDBM { get; set; }
        public string SJTHRQ { get; set; }
        public string THDBH { get; set; }
        public string CGFS { get; set; }
        public string XTBM { get; set; }
        public string SFHBSFW { get; set; }
        public string DDBH { get; set; }
        public int? JLS { get; set; }
        public int? thdzt { get; set; }
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
