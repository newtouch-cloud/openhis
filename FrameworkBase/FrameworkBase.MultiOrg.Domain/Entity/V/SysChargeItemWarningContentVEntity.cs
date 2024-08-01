using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 收费项目警示内容（在使用收费项目时给一些警示）
    /// </summary>
    [Table("V_S_xt_sfxmjsnr")]
    public class SysChargeItemWarningContentVEntity 
    {
        /// <summary>
        /// Id
        /// </summary>
        public int sfxjsnrId { get; set; }

        /// <summary>
        /// 收费项目编码
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 门诊 提示的警示内容
        /// </summary>
        public string mzjsnr { get; set; }

        /// <summary>
        /// 住院 提示的警示内容
        /// </summary>
        public string zyjsnr { get; set; }

        /// <summary>
        /// 门诊 提示级别
        /// </summary>
        public byte mzjsjb { get; set; }

        /// <summary>
        /// 住院 提示级别
        /// </summary>
        public byte zyjsjb { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        public string zt { get; set; }

    }
}
