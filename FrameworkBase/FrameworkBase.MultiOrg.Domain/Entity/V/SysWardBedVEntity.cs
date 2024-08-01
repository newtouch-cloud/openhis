using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 系统床位
    /// </summary>
    [Table("V_S_xt_cw")]
    public class SysWardBedVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int cwId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string cwCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string cwmc { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 所属病区
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 关联病房
        /// </summary>
        public string bfCode { get; set; }

        /// <summary>
        /// 床位类型 男女混
        /// </summary>
        public string cwlx { get; set; }
    }
}
