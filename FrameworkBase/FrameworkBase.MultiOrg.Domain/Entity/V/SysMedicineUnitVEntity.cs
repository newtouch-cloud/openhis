using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 药品单位
    /// </summary>
    [Table("V_S_xt_ypdw")]
    public class SysMedicineUnitVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int ypdwId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string ypdwCode { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string ypdwmc { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        public string zt { get; set; }

    }
}
