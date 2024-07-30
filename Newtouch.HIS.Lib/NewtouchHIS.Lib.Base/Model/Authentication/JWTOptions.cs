namespace NewtouchHIS.Lib.Base.Model
{
    public class JWTOptions
    {
        /// <summary>
        /// 签发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 接收者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public int ExpireHours { get; set; }
        /// <summary>
        /// 验证过期
        /// </summary>
        public bool? ValidateLifetime { get; set; }
        public List<SSOAuthAppItem> SSOAuthApp { get; set; }
    }
}
