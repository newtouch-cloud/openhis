using Newtouch.HIS.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects.KPI
{
    /// <summary>
    /// 
    /// </summary>
    [NotMapped]
    public class MedicalOrgProfitShareConfigVO : MedicalOrgMonthProfitShareConfigEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string sfdlmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }


    }
}
