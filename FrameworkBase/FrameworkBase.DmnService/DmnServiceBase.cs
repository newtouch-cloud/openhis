using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.Infrastructure;

namespace FrameworkBase.DmnService
{
    /// <summary>
    /// DmnService基类
    /// </summary>
    public abstract class DmnServiceBase : EFDBBase
    {
        /// <summary>
        /// 是否为private readonly I 解析实例
        /// </summary>
        private static bool? _PRFAutoResolve = null;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static DmnServiceBase()
        {
            _PRFAutoResolve = ConfigurationHelper.GetAppConfigBoolValue("PRFAutoResolve_DmnService");
            if (_PRFAutoResolve == null)
            {
                _PRFAutoResolve = ConfigurationHelper.GetAppConfigBoolValue("PRFAutoResolve");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseFactory"></param>
        public DmnServiceBase(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            if (_PRFAutoResolve ?? true)
            {
                //利用IOC为private readonly field赋值
                this.PRFAutoResolve();
            }
        }

    }
}
