using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 药品分类
    /// </summary>
    [Table("V_S_xt_ypfl")]
    public class SysMedicineClassificationVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int ypflId { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string ypflmc { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string ypflCode { get; set; }

        /// <summary>
        /// 首拼
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        public string zt { get; set; }

    }
}
