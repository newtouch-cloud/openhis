using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Base.HOSP.Request
{
    /// <summary>
    /// 更新床位占用情况
    /// </summary>
    public class UpdateBedOccupyRequest : RequestBase
    {
        /// <summary>
        /// 床位Code
        /// </summary>
        [Required]
        public string cwCode { get; set; }

        /// <summary>
        /// 是否占用
        /// </summary>
        [Required]
        public bool isOccupy { get; set; }

    }
}

