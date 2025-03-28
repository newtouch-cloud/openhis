using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("V_S_xt_mz")]
    public class SysNationVEntity : IEntity<SysNationVEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        public int mzId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzmc { get; set; }

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
