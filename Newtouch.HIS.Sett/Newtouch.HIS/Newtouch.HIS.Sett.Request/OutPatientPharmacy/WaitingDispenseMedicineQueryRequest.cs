using Newtouch.HIS.API.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// 门诊排药请求
    /// </summary>
    public class WaitingDispenseMedicineQueryRequest : RequestBase
    {
        /// <summary>
        /// 门诊药房 药房部门Code
        /// </summary>
        [Required]
        public string yfbmCode { get; set; }

        [Required]
        public DateTime fysjs { get; set; }

        [Required]
        public int fybz { get; set; }

    }
}
