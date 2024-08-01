using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 农保收费大类
    /// </summary>
    [Table("V_S_xt_nbsfdl")]
    public class SysAgriInsuranceChargeCategoryVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int dlId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string dlmc { get; set; }

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
