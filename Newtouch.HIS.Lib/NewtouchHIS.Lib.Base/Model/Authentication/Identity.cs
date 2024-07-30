namespace NewtouchHIS.Lib.Base.Model
{
    /// <summary>
    /// API访问者身份
    /// </summary>
    public abstract class Identity
    {
        /// <summary>
        /// 应用Id
        /// </summary>
        public string? AppId { get; set; }

        /// <summary>
        /// 令牌类型（用户类型）
        /// </summary>
        public string? TokenType { get; set; }
        public string? Token { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// 系统用户 Code（登录账号）
        /// </summary>
        public string? UserCode { get; set; }

        /// <summary>
        /// 系统人员 姓名
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 用户账户
        /// </summary>
        public string? Account { get; set; }
        public string? SignalConnId { get; set; }
    }
}
