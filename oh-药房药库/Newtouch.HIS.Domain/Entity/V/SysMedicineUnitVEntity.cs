using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("V_S_xt_ypdw")]
    public class SysMedicineUnitVEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int ypdwId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypdwCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypdwmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
