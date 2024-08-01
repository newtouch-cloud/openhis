using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;

namespace FrameworkBase.MultiOrg.Application
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

        /// <summary>
        /// 组织机构Id
        /// </summary>
        protected string OrganizeId
        {
            get
            {
                if (this.UserIdentity == null)
                {
                    return null;
                }
                return this.UserIdentity.OrganizeId;
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
