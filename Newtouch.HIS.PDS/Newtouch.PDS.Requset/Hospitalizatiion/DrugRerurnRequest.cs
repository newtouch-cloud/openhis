using Microsoft.Build.Framework;
using System;

namespace Newtouch.PDS.Requset.Hospitalizatiion
{
    /// <summary>
    /// 药品退药
    /// </summary>
    public class DrugRerurnRequest
    {

        /// <summary>
        /// 医嘱ID
        /// </summary>
        [Required]
        public string yzId { get; set; }

        /// <summary>
        /// 药品code
        /// </summary>
        [Required]
        public string ypCode { get; set; }

        /// <summary>
        /// 退药申请人
        /// </summary>
        [Required]
        public string tysqr { get; set; }

        /// <summary>
        /// 组号
        /// </summary>
        [Required]
        public string zh { get; set; }

        /// <summary>
        /// 执行日期
        /// </summary>
        [Required]
        public DateTime? zxrq { get; set; }
    }
}
