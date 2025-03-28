using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.WebAPI.Manage.Models
{
    /// <summary>
    /// 参与会议者申请会议
    /// </summary>
    public class TreatedUserRequest
    {
        [Required(ErrorMessage = "职工工号不可为空")]
        public string usercode { get; set; }
        public string username { get; set; }
        /// <summary>
        /// 远程诊疗申请流水号
        /// </summary>
        [Required(ErrorMessage = "诊疗申请流水号不可为空")]
        public string applyId { get; set; }
        /// <summary>
        /// 是否会议重置
        /// 会议超时时，重新申请会议
        /// </summary>
        public bool roomReset { get; set; } = false;
        public string? roomid { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string? mzh { get; set; }
    }
}
