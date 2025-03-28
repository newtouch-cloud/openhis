using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("V_S_xt_ypcrkfs")]
    public class SysMedicineStorageIOModeVEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int crkfsId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string crkfsCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string crkfsmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string crkbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
