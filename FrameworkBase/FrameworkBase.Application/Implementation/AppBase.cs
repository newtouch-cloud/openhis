using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;

namespace FrameworkBase.Application
{
    /// <summary>
    /// App基类
    /// </summary>
    public class AppBase
    {
        /// <summary>
        /// 是否为private readonly I 解析实例
        /// </summary>
        private static bool? _PRFAutoResolve = null;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static AppBase()
        {
            _PRFAutoResolve = ConfigurationHelper.GetAppConfigBoolValue("PRFAutoResolve_App");
            if (_PRFAutoResolve == null)
            {
                _PRFAutoResolve = ConfigurationHelper.GetAppConfigBoolValue("PRFAutoResolve");
            }

        }

        private OperatorModel _userIdentity;
        /// <summary>
        /// 登录用户身份
        /// </summary>
        protected internal OperatorModel UserIdentity
        {
            get
            {
                if (_userIdentity == null)
                {
                    _userIdentity = OperatorProvider.GetCurrent();
                }
                return _userIdentity;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AppBase()
        {
            if (_PRFAutoResolve ?? true)
            {
                //利用IOC为private readonly field赋值
                this.PRFAutoResolve();
            }
        }
    }
}
