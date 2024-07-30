using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// 更新住院病人在院标志
    /// </summary>
    public class UpdateInpatientStatusRequest : RequestBase
    {
        /// <summary>
        /// 住院号
        /// </summary>
        [Required]
        public string zyh { get; set; }

        /// <summary>
        /// 在院标志
        /// </summary>
        [Required]
        public string zybz { get; set; }
    }
}
