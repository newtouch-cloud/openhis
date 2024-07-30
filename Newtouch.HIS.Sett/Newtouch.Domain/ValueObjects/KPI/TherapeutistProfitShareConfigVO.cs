using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects.KPI
{
    /// <summary>
    /// 
    /// </summary>
    [NotMapped]
    public class TherapeutistProfitShareConfigVO : TherapeutistMonthProfitShareConfigEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string sfdlmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zlsId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zlsxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }


    }
}
