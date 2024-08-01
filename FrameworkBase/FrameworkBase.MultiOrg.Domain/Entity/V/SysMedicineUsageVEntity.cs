using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 药品用法
    /// </summary>
    [Table("V_S_xt_ypyf")]
    public class SysMedicineUsageVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int yfId { get; set; }

        /// <summary>
        /// 用法编码
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string yfmc { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// 药品类型，区分中药还是西药用法
        /// </summary>
        public string yplx { get; set; }

    }
}
