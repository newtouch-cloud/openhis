using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 药房部门
    /// </summary>
    [Table("V_S_xt_yfbm")]
    public class SysPharmacyDepartmentVEntity : IEntity<SysPharmacyDepartmentVEntity>
    {
        /// <summary>
        /// 药房部门ID
        /// </summary>
        public int yfbmId { get; set; }

        /// <summary>
        /// 药房部门名称
        /// </summary>
        public string yfbmmc { get; set; }

        /// <summary>
        /// 药房部门代码
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 组织机构代码
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 门诊住院标志
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 药剂部门级别
        /// </summary>
        public byte yjbmjb { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// 状态 0：无效  1：有效
        /// </summary>
        public string zt { get; set; }

    }
}
