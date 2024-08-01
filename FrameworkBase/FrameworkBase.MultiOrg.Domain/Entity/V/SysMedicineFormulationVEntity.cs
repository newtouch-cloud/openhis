using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 药品剂型
    /// </summary>
    [Table("V_S_xt_ypjx")]
    public class SysMedicineFormulationVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int jxId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string jxCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string jxmc { get; set; }

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
