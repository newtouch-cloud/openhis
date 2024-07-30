using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.WebAPI.Manage.Models
{
    public class BookingReqBase
    {
        [Required(ErrorMessage = "应用Id（AppId）必传")]
        public string AppId { get; set; }
        /// <summary>
        /// 组织机构Id（已经定位到了具体医院）
        /// </summary>
        [Required(ErrorMessage = "机构Id（OrganizeId）必传")]
        public string OrganizeId { get; set; }
        /// <summary>
        /// 请求编号
        /// </summary>
        [Required(ErrorMessage = "方法名（methodcode）必传")]
        public string methodcode { get; set; }
        public string? optype { get; set; }
        /// <summary>
        /// 当前用户
        /// </summary>
        public string? user { get; set; }
        /// <summary>
        /// 发送请求的时间，格式"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        [Required(ErrorMessage = "时间戳必传")]
        public DateTime Timestamp { get; set; }

    }

    public class BookingRequest: BookingReqBase
    {
        /// <summary>
        /// 入参
        /// </summary>
        public object? paradata { get; set; }
    }

}
