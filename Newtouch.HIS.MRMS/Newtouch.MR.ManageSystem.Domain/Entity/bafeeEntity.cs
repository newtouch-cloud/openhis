using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.Entity
{
    [Table("mr_dic_bafeetype")]
    public class bafeeEntity : IEntity<bafeeEntity>
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string py { get; set; }
        public string ShortCode { get; set; }
        public int Lev { get; set; }
        public string ParentCode { get; set; }
        public int px { get; set; }
        public string zt { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
    }
}
