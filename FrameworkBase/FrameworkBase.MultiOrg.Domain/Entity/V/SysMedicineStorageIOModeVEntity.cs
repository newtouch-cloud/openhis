using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 药品输入库方式
    /// </summary>
    [Table("V_S_xt_ypcrkfs")]
    public class SysMedicineStorageIOModeVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int crkfsId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string crkfsCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string crkfsmc { get; set; }

        /// <summary>
        /// 出入库标志（入库使用，还是出库使用）
        /// </summary>
        public string crkbz { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        public string zt { get; set; }

    }
}
