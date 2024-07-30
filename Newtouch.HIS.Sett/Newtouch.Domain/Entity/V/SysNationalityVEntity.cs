using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("V_S_xt_gj")]
    public class SysNationalityVEntity : IEntity<SysNationalityVEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        public int gjId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gjCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gjmc { get; set; }

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
