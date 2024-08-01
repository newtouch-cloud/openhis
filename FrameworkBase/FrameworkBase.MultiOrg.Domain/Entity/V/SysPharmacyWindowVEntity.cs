using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 药房窗口
    /// </summary>
    [Table("V_S_xt_yfck")]
    public class SysPharmacyWindowVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int yfckId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string yfckCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string yfckmc { get; set; }

        /// <summary>
        /// 组织机构Id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 药房部门Code（比如是药房部门的001窗口）
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 开启标志（Working标志）
        /// </summary>
        public string kqbz { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        public string zt { get; set; }

    }
}
