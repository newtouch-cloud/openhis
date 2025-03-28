using Autofac;
using Newtouch.Herp.API.Models;

namespace Newtouch.Herp.API
{
    /// <summary>
    /// API Controll 基类
    /// </summary>
    public class ApiControllerBase<TControllerType> : FrameworkBase.API.ApiControllerBase<TControllerType>
    {
        public ApiControllerBase(IComponentContext com)
            : base(com)
        {
        }

        /// <summary>
        /// 用户身份
        /// </summary>
        public UserIdentity UserIdentity
        {
            get
            {
                return this.Identity as UserIdentity;
            }
        }

    }

}