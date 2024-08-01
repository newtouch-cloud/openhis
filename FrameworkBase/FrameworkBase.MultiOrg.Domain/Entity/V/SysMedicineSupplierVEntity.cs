using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 药品提供商
    /// </summary>
    [Table("V_S_xt_ypgys")]
    public class SysMedicineSupplierVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int gysId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string gysCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string gysmc { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 首拼
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

    }
}
