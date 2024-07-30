using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    [NotMapped]
    public class SysOrganizeVO : SysOrganizeEntity
    {
        /// <summary>
        /// 是否允许编辑
        /// </summary>
        public bool AllowEdit { get; set; }
    }
}
