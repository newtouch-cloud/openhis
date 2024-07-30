using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain
{
    /// <summary>
    /// 生成授权key
    /// </summary>
    public class AppFriendAuthKey
    {
        /// <summary>
        /// Desc:授权ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? AuthId { get; set; }

        /// <summary>
        /// Desc:应用通用Id
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? AppId { get; set; }

        /// <summary>
        /// Desc:应用名称
        /// Default:
        /// Nullable:False
        public string? AppName { get; set; }
        public string? FriendId { get; set; }
        public string? FriendAppId { get; set; }
        public string? AuthLevs { get; set; }
        public DateTime? TimeSpanNow { set; get; } = DateTime.Now;

    }
    /// <summary>
    /// 申请Token key
    /// App to Friend
    /// </summary>
    public class AppFriendAuthKeyRequest {
        [Required(ErrorMessage = "申请授权AppId不可为空")]
        public string AppId { get; set; }
        public string? FriendAppId { get; set; }
    }


}
