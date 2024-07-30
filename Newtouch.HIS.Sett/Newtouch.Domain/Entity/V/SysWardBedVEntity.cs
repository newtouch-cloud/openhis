using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("V_S_xt_cw")]
    public class SysWardBedVEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int cwId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cwCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cwmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
    }
}
