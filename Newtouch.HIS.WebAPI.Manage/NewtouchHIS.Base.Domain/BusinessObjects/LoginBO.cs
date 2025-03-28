using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain
{
    public class LoginBO
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage ="用户名不可为空")]
        public string Username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不可为空")]
        public string Password { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public class UALoginBO
    {
        public string? access_token { get; set; }
    }
}
