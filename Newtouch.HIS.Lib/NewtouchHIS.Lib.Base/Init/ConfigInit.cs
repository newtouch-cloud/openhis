using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Lib.Base
{
    public static class ConfigInitHelper
    {
        /// <summary>
        /// 数据库配置
        /// </summary>
        public static BusinessDB DbConfig { get; set; }
        /// <summary>
        /// 业务系统配置
        /// </summary>
        public static BusinessConfig SysConfig { get; set; }
        /// <summary>
        /// 跨域设置
        /// </summary>
        public static AccessConfig? AccessSetting { get; set; }

        
    }

    public static class CacheConfig
    {
        /// <summary>
        /// 用户缓存-token标识
        /// </summary>
        public static string Cache_UserToken => "HisToken:User_";

    }
}
