using System.Security.Claims;

namespace NewtouchHIS.Lib.Base.Model.Authentication
{
    public class IdentityClaimTypes
    {

        public const string AppId = "AppId";
        public const string OrganizeId = "OrganizeId";
        public const string TopOrganizeId = "TopOrganizeId";
        public const string Account = "Account";
        public const string UserCode = "UserCode";
        public const string UserName = "UserName";
        public const string UserId = "UserId";
        public const string TokenType = "TokenType";
        public const string Token = "Token";
        public const string AuthType = "AuthType";
        public const string AppAuthKey = "AppAuthKey";

        public const string Role = ClaimTypes.Role;
    }
}
