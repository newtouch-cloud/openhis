using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 药房部门
    /// </summary>
    [Table("V_S_xt_yp_jxyfdy")]
    public class SysMedicineFormulationUsageVEntity
    {
        /// <summary>
        /// 药品剂型Code
        /// </summary>
        public string jxCode { get; set; }

        /// <summary>
        /// 药品用法Code
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
    }
}
