using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 收费项目多语言配置表
    /// </summary>
    [Table("V_S_xt_sfxm_dyy")]
    public class SysChargeItemMultiLanguageVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int sfxmdyyId { get; set; }

        /// <summary>
        /// 收费项目编码（关联收费项目表）
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 收费项目名称
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 繁体
        /// </summary>
        public string sfxmmcFanti { get; set; }

        /// <summary>
        /// 英语
        /// </summary>
        public string sfxmmcEnglish { get; set; }

        /// <summary>
        /// 日语
        /// </summary>
        public string sfxmmcJpan { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        public string zt { get; set; }

    }
}
