using Autofac;
using Newtouch.EMR.API.Models;

namespace Newtouch.EMR.API
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