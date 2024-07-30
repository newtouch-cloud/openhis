using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// 门诊预约查询请求报文
    /// </summary>
    public class MzBespeakRegisterQueryRequestDTO : RequestBase
    {
        /// <summary>
        /// 证件类型
        /// </summary>
        public int? zjlx { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>
        [Required]
        public string zjh { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        [Required]
        public string blh { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }
    }
}
