using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    [NotMapped]
    public class SysUserVO : SysUserEntity
    {
        public string gh { get; set; }

        public string Name { get; set; }

        public string DepartmentCode { get; set; }

        public string DepartmentName { get; set; }
        
        public string OrganizeId { get; set; }

        public string OrganizeName { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? Locked { get; set; }
    }

}
