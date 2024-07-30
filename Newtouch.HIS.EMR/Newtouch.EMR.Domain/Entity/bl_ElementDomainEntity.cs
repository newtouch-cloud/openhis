using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// 结构化主表
    /// </summary>
    [Table("bl_ElementDomain")]
    public class bl_ElementDomainEntity : IEntity<bl_ElementDomainEntity>
    {
        [Key]
        public int Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// 病历类型标识
        /// </summary>
        public string Bllx { get; set; }
        public string Table_EngLish_Name { get; set; }
        public string Table_Name { get; set; }
        public string Regex { get; set; }
        public string Px { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public int Zt { get; set; }
    }
}
