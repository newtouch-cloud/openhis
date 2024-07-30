namespace NewtouchHIS.Lib.Base.Model
{
    /// <summary>
    /// 访问配置
    /// </summary>
    public class AccessConfig
    {
        /// <summary>
        /// 页面domain（嵌套其他域页面，并执行js 要设置成同一域）
        /// </summary>
        public string? Document_Domain { get; set; }
        /// <summary>
        /// 访问地址域名限制 （类似百度http会自动重定向到https）
        /// </summary>
        public string? AccessDomain { get; set; }
    }
}
