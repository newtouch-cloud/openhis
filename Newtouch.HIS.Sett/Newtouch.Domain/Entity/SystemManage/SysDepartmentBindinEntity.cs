using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_DepartmentBinding")]
    public class SysDepartmentBindingEntity : IEntity<SysDepartmentBindingEntity>
    {
        /// <summary>
        /// 绑定编码
        /// </summary>
        [Key]
        public string bddm { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        public string ks { get; set; }

        public string ksmc { get; set; }

        public string gh { get; set; }

        public string xm { get; set; }

        public bool xb { get; set; }

        public string zjlx { get; set; }

        public string zjh { get; set; }


        /// <summary>
        /// 0:无效; 1:有效
        /// </summary>
        public string zt { get; set; }
        
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
        public int? px { get; set; }

    }
}
