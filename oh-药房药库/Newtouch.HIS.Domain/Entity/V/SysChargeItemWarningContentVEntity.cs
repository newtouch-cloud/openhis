using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("V_S_xt_sfxmjsnr")]
    public class SysChargeItemWarningContentVEntity : IEntity<SysChargeItemWarningContentVEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        public int sfxjsnrId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzjsnr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zyjsnr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte mzjsjb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte zyjsjb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
