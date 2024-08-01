using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 系统病区
    /// </summary>
    [Table("V_S_xt_bq")]
    public class SysWardVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int bqId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string bqmc { get; set; }

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
