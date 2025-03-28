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
    public class SysDepartmentVO : SysDepartmentEntity
    {
        public string OrganizeName { get; set; }

		public string gjksmc { get; set; }
	}
}
