using System.ComponentModel;

namespace NewtouchHIS.Lib.Base.Model
{
    public class JWTTokenRequest
    {
        public AuthType authType { get; set; }
        public AuthIdentity? authIdentity { get; set; }
    }
    /// <summary>
    /// 授权类型
    /// </summary>
    public enum AuthType
    {
        [Description("Web")]
        Web = 1,
        [Description("WebAPI")]
        WebApi = 2
    }

    public class AuthIdentity : UserIdentity
    {
        /// <summary>
        /// 访问的应用信息
        /// </summary>
        public string? AppAuthKey { get; set; }
        /// <summary>
        /// AppId by 颁发授权token应用
        /// </summary>
        public string? KeyLicense { get; set; }
    }


    public class JWTToken
    {
        public string? Token { get; set; }
    }

    /// <summary>
    /// 鉴权身份认证
    /// </summary>
    public class SSOAuthIdentity : SSOAuthAppItem
    {
        /// <summary>
        /// 签发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 接收者
        /// </summary>
        public string Audience { get; set; }
    }
    /// <summary>
    /// 授权应用信息
    /// </summary>
    public class SSOAuthAppItem
    {
        /// <summary>
        /// 系统域名
        /// </summary>
        public string? Domain { get; set; }
        /// <summary>
        /// 系统Id
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 系统密钥
        /// </summary>
        public string AppSecret { get; set; }
    }
}
