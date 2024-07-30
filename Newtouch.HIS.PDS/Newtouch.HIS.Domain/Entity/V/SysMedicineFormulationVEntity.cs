using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("V_S_xt_ypjx")]
    public class SysMedicineFormulationVEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int jxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jxCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jxmc { get; set; }

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
