using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    [NotMapped]
    public class SysStaffVO : SysStaffEntity
    {
        public string DepartmentName { get; set; }
        public string OrganizeName { get; set; }
        public string staffName { get; set; }
        public string staffNames { get; set; }
    }
}
