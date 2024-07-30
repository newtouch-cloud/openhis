using Microsoft.Extensions.Configuration;

namespace NewtouchHIS.Lib.Base.Utilities
{
    public class ConfigHelper
    {
        private static IConfiguration _config;

        public ConfigHelper(IConfiguration configuration)
        {
            _config = configuration;
        }
        /// <summary>
        /// 读取appsettings.json文件中指定节点信息
        /// </summary>
        /// <param name="sessions"></param>
        /// <returns></returns>
        public static string ReadAppSettings(params string[] sessions)
        {
            try
            {
                if (sessions.Any())
                {
                    return _config[string.Join(":", sessions)];
                }
            }
            catch
            {
                return string.Empty;
            }
            return string.Empty;
        }
        /// <summary>
        /// 读取实体信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <returns></returns>
        public static List<T> ReadAppSettings<T>(params string[] session)
        {
            List<T> list = new List<T>();
            _config.Bind(string.Join(":", session), list);
            return list;
        }
    }
}
