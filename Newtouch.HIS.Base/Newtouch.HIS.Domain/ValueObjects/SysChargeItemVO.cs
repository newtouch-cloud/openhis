using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    [NotMapped]
    public class SysChargeItemVO : SysChargeItemEntity
    {
        /// <summary>
        /// 是否允许编辑
        /// </summary>
        public string sfdlmc { get; set; }
        /// <summary>
        /// 是否同步医保
        /// </summary>
        public string isSynch { get; set; }
        public string gjybmc { get; set; }
    }
}
