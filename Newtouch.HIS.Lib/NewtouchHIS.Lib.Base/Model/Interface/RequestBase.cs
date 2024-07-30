using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Lib.Base.Model
{
    public class RequestBase
    {
        [Required(ErrorMessage = "应用Id（AppId）必传")]
        /// <summary>
        /// 应用Id
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 机构Id
        /// </summary>
        public string? OrganizeId { get; set; }

        [Required(ErrorMessage = "时间戳必传")]
        /// <summary>
        /// 发送请求的时间，格式"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        public DateTime Timestamp { get; set; }

    }

    public class BusRequest : RequestBase
    {
        /// <summary>
        /// 令牌类型
        /// </summary>
        public string? TokenType { get; set; }

        /// <summary>
        /// 访问令牌
        /// </summary>
        public string? Token { get; set; }
    }
}
