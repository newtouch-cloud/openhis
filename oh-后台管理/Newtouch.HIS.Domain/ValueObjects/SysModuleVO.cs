using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    [NotMapped]
    public class SysModuleVO : SysModuleEntity
    {
        /// <summary>
        /// 是否对Org开放
        /// </summary>
        public bool IsOpenToOrg { get; set; }

    }

}
