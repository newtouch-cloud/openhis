using Newtouch.HIS.API.Common.Interface;
using Newtouch.HIS.API.Common.Models;

namespace Newtouch.PDS.API.App_Start
{
    /// <summary>
    /// 
    /// </summary>
    public class HISAPIUserIdentityResolver : IUserIdentityResolver<Identity>
    {

        public Identity GetIdentity(string token)
        {
            var identity = ApiControllerBaseEx.GetIdentity<UserIdentity>(token);
            return identity;
        }

        public Identity GetIdentity(string token, string tokenType)
        {
            var identity = ApiControllerBaseEx.GetIdentity<UserIdentity>(token, tokenType);
            return identity;
        }
    }
}