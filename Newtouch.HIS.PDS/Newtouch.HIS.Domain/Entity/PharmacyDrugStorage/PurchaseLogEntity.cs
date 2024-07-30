using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.PharmacyDrugStorage
{
    /// <summary>
    /// 药品采购接口返回日志表
    /// </summary>
    [Table("xt_yp_cg_log")]
    public class PurchaseLogEntity : IEntity<PurchaseLogEntity>
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// 交易代码
        /// </summary>
        public string jydm { get; set; }
        /// <summary>
        /// 交易名称
        /// </summary>
        public string jymc { get; set; }
        public string param { get; set; }
        /// <summary>
        /// 入参xml
        /// </summary>
        public string XmlRequest { get; set; }
        /// <summary>
        /// 出参xml
        /// </summary>
        public string XmlResponse { get; set; }
        public string zt { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
    }
}
