using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("V_S_xt_ypgys")]
    public class SysMedicineSupplierVEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int gysId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gysCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gysmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
