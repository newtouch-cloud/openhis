using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 系统诊断
    /// </summary>
    [Table("V_S_xt_zyzh")]
    public class SysTCMSyndromeVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int zhId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string zhCode { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        public string zhmc { get; set; }

        /// <summary>
        /// 首拼
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 五笔
        /// </summary>
        public string wb { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
