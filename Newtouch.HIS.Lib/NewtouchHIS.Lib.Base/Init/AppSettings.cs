using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using SqlSugar.Extensions;

namespace NewtouchHIS.Lib.Base.Utilities
{
    public class AppSettings
    {
        static IConfiguration Configuration { get; set; }

        public AppSettings(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 封装要操作的字符
        /// </summary>
        /// <param name="sections">节点配置</param>
        /// <returns></returns>
        public static string App(params string[] sections)
        {
            try
            {
                if (sections.Any())
                {
                    return Configuration[string.Join(":", sections)];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return "";
        }

        /// <summary>
        /// 递归获取配置信息数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sections"></param>
        /// <returns></returns>
        public static List<T> App<T>(params string[] sections)
        {
            List<T> list = new List<T>();
            // 引用 Microsoft.Extensions.Configuration.Binder 包
            Configuration.Bind(string.Join(":", sections), list);
            return list;
        }
        public static T Bind<T>(string key, T t)
        {
            Configuration.Bind(key, t);

            return t;
        }

        public static T? GetAppConfig<T>(string key, T? defaultValue = default)
        {
            if (Configuration[key] == null)
            {
                return defaultValue;
            }
            T setting = (T)Convert.ChangeType(Configuration[key], typeof(T));
            var value = setting;
            if (setting == null)
                value = defaultValue;
            return value;
        }

        /// <summary>
        /// 获取配置文件 
        /// </summary>
        /// <param name="key">eg: WeChat:Token</param>
        /// <returns></returns>
        public static string GetConfig(string key)
        {
            return Configuration[key];
        }

        /// <summary>
        /// 获取布尔类型配置值 默认false
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool GetAppConfigBoolValue(string key, bool? defaultValue = null)
        {
            string val = Configuration[key];
            if (string.IsNullOrEmpty(val) || !val.ObjToBool())
            {
                return defaultValue ?? false;
            }
            return val.ObjToBool();
        }
        #region 读取AppConfig扩展自定义配置
        /// <summary>
        /// 获取配置文件 
        /// (读取路径：AppConfigExt)
        /// </summary>
        /// <param name="key">eg: WeChat:Token</param>
        /// <returns></returns>
        public static string GetConfigExt(string key)
        {
            key = AssemblyKey(key);
            return Configuration[key];
        }
        /// <summary>
        /// 获取布尔类型配置值 默认false
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool GetAppConfigBoolValueExt(string key, bool? defaultValue = null)
        {
            key = AssemblyKey(key);
            string val = Configuration[key];
            if (string.IsNullOrEmpty(val) || !val.ObjToBool())
            {
                return defaultValue ?? false;
            }
            return val.ObjToBool();
        }

        private static string AssemblyKey(string key)
        {
            return $"AppConfigExt:{key}";
        }
        #endregion
    }
}
