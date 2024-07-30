using Newtouch.HIS.API.Common.Interface;
using Newtouch.HIS.API.Common.Models;

namespace Newtouch.CIS.API.App_Start
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
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        public Identity GetIdentity(string token, string tokenType)
        {
            return null;
        }

    }
}