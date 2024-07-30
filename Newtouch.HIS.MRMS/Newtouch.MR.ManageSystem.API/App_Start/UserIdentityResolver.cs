
using FrameworkBase.API;
using Newtouch.HIS.API.Common.Interface;
using Newtouch.HIS.API.Common.Models;

namespace Newtouch.MR.ManageSystem.API.App_Start
{
    /// <summary>
    /// 用户身份解析
    /// </summary>
    public class UserIdentityResolver : IUserIdentityResolver<Identity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Identity GetIdentity(string token)
        {
            var identity = ApiControllerBaseEx.GetIdentity<UserIdentity>(token);
            return identity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        public Identity GetIdentity(string token, string tokenType)
        {
            var identity = ApiControllerBaseEx.GetIdentity<UserIdentity>(token, tokenType);
            return identity;
        }

    }
}