using Newtouch.HIS.API.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request
{
    public class UpdateInpatientOutInfoRequest : RequestBase
    {
        //zyh,bq,cw,cyrq,cyzd,doctor
        [Required]
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// 病区
        /// </summary>
        public string bq { get; set; }
        /// <summary>
        /// 床位
        /// </summary>
        public string cw { get; set; }
        /// <summary>
        ///  出院日期
        /// </summary>
        public DateTime? cyrq { get; set; }
        /// <summary>
        /// 出院诊断
        /// </summary>
        public string cyzd { get; set; }
        ///// <summary>
        ///// 出院病情
        ///// </summary>
        //public string cybq { get; set; }
        /// <summary>
        /// 主治医生
        /// </summary>
        public string doctor { get; set; }
        ///// <summary>
        ///// 入院诊断
        ///// </summary>
        //public short? ryzd { get; set; }
        ///// <summary>
        ///// 入院病情
        ///// </summary>
        //public string rybq { get; set; }
        /// <summary>
        /// 在院标志
        /// </summary>
        public string zybz { get; set; }

    }
}
