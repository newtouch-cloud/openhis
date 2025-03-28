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
    /// 结构化明细表
    /// </summary>
    [Table("bl_ElementDomain_Detail")]
    public class bl_ElementDomain_DetailEntity : IEntity<bl_ElementDomain_DetailEntity>
    {
        [Key]
        public int Id { get; set; }
        public string OrganizeId { get; set; }

        public int ElementId { get; set; }
        public string Table_Name { get; set; }
        public int Table_Column_No { get; set; }

        public string Table_Column_Code { get; set; }
        public string Table_Colunn_Name { get; set; }
       
        public string Table_Column_Type { get; set; }

        public string Element_ID { get; set; }
        public string Element_Name { get; set; }

        public int Element_Type { get; set; }
        public string Element_Type_Name { get; set; }
        public int Sybz { get; set; }
        public string ysmxId { get; set; }
        public string AreValue { get; set; }
        public int Px { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public int Zt { get; set; }

    }
}
