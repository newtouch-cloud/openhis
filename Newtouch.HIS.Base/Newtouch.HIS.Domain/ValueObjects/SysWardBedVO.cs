using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    [NotMapped]
    public class SysWardBedVO: SysWardBedEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string bqmc { get; set; }
        public string bfNo { get; set; }
    }
}
