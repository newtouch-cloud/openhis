using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 国籍
    /// </summary>
    [Table("V_S_xt_gj")]
    public class SysNationalityVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int gjId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string gjCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string gjmc { get; set; }

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
