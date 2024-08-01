using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 系统药房部门药品
    /// </summary>
    [Table("V_S_xt_yfbm_yp")]
    public class SysPharmacyDepartmentDrugVEntity
    {
        /// <summary>
        /// 药房部门编码
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 大类编码
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 状态 0-无效  1-有效
        /// </summary>
        public string zt { get; set; }
    }
}