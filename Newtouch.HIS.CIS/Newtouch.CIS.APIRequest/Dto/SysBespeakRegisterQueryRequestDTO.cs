using Newtouch.HIS.API.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// 门诊挂号排班请求报文
    /// </summary>
    public class SysBespeakRegisterQueryRequestDTO : RequestBase
    {
        /// <summary>
        /// 科室代码
        /// </summary>
        [Required]
        public string ksCode { get; set; }

        /// <summary>
        /// 医生工号
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// 挂号日期
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// g挂号时段
        /// </summary>
        public string regTime { get; set; }
    }
}
