using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects.Rehabilitation
{
    /// <summary>
    /// 
    /// </summary>
    [NotMapped]
    public class RehabChargeItemVO: RehabChargeItemEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string sfflName { get; set; }
    }
}
