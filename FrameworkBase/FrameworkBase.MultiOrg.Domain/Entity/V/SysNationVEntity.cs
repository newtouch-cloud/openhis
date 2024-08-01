using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 民族
    /// </summary>
    [Table("V_S_xt_mz")]
    public class SysNationVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int mzId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string mzCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string mzmc { get; set; }

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
