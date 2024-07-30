using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity.Inpatient
{
    [Table("zy_tyjl")]
    public class Drug_withdrawalzy_tyjlEntity : IEntity<Drug_withdrawalzy_tyjlEntity>
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public System.Decimal tyxh { get; set; }
        public string yzxh { get; set; }
        public string zyh { get; set; }
        public string hzxm { get; set; }
        public string WardCode { get; set; }
        public System.Decimal fyqqxh { get; set; }
        public string ypdm { get; set; }
        public int tysl { get; set; }
        public int ktypsl { get; set; }
        public string ypgg { get; set; }
        public string ypdw { get; set; }
        public System.Decimal dwxs { get; set; }
        public System.Decimal ykxs { get; set; }
        public System.Decimal ypdj { get; set; }
        public int tyqrbz { get; set; }
        public System.Decimal tyqrxh { get; set; }
        public string tyczydm { get; set; }
        public string yftyczydm { get; set; }
        public DateTime yftyrq { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public string LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public string fyId { get; set; }
        public int fzxh { get; set; }
    }
}
