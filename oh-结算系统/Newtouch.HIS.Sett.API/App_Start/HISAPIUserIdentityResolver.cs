using FrameworkBase.API;
using Newtouch.HIS.API.Common.Interface;
using Newtouch.HIS.API.Common.Models;

namespace Newtouch.HIS.Sett.API.App_Start
{
    /// <summary>
    /// 
    /// </summary>
    public class HISAPIUserIdentityResolver : IUserIdentityResolver<Identity>
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