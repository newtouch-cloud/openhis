using Newtouch.HIS.API.Common;
using System;

namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// 已预约门诊挂号信息查询请求报文
    /// </summary>
    public class MzAlreadyBespeakRegisterCountQueryRequestDTO : RequestBase
    {
        /// <summary>
        /// 科室代码
        /// </summary>
        public string ksCode { get; set; }
        
        /// <summary>
        /// 医生工号
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        public string regTime { get; set; }

    }
}
