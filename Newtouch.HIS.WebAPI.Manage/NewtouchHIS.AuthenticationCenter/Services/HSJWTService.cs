using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Model.Authentication;
using NewtouchHIS.Lib.Base.Utilities;
using System.DirectoryServices.ActiveDirectory;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewtouchHIS.AuthenticationCenter.Services
{

    /// <summary>
    /// 对称可逆加密
    /// </summary>
    public class HSJWTService : IJWTService
    {
        private readonly JWTOptions _jWTOptions;
        private string _MsgPrefix = "[鉴权消息]：";
        public HSJWTService(IOptionsMonitor<JWTOptions> jwtOptions)
        {
            _jWTOptions = jwtOptions.CurrentValue;
        }
        /// <summary>
        /// 仅应用信息token （配置认证）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BusResult<JWTToken>> GetTokenAsync(JWTTokenRequest request)
        {
            var appAuth = OptionValid(request);
            string cacheKey = GetCacheKey(request);
            string findCacheKey = RedisHelper.GetString(cacheKey) ?? "";
            var Clims = new List<Claim>();
            if (!string.IsNullOrWhiteSpace(findCacheKey) && ValidateToken(findCacheKey, request.authIdentity.KeyLicense, out Clims))
            {
                return new BusResult<JWTToken> { code = ResponseResultCode.SUCCESS, Data = new JWTToken { Token = findCacheKey } };
            }
            string jwtToken = await TokenGen(request.authIdentity, appAuth);
            RedisHelper.SetString(cacheKey, jwtToken, (HourOfDay)_jWTOptions.ExpireHours);
            return new BusResult<JWTToken> { code = ResponseResultCode.SUCCESS, Data = new JWTToken { Token = jwtToken } };
        }

        public async Task<BusResult<JWTToken>> TokenRefreshAsync(JWTTokenRequest request)
        {
            var cachekey = GetCacheKey(request);
            var appAuth = OptionValid(request);
            string jwtToken = await TokenGen(request.authIdentity, appAuth);
            RedisHelper.SetString(cachekey, jwtToken, (HourOfDay)_jWTOptions.ExpireHours);
            return new BusResult<JWTToken> { code = ResponseResultCode.SUCCESS, Data = new JWTToken { Token = jwtToken } };
        }

        public bool ValidateToken(string Token, string keyLicense, out List<Claim> Clims)
        {
            Clims = new List<Claim>();
            ClaimsPrincipal principal = null;
            if (string.IsNullOrWhiteSpace(Token))
            {
                return false;
            }
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var jwt = handler.ReadJwtToken(Token);
                if (jwt == null)
                {
                    return false;
                }
                var authApp = _jWTOptions.SSOAuthApp?.FirstOrDefault(p => p.AppId == keyLicense);
                if (authApp == null)
                {
                    throw new FailedException($"{_MsgPrefix}token无效！授权应用无效！");
                }
                var secretBytes = Encoding.UTF8.GetBytes(authApp.AppSecret);
                var validationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = _jWTOptions.ValidateLifetime ?? true,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidAudience = authApp.AppId,
                    ValidIssuer = _jWTOptions.Issuer
                };
                SecurityToken securityToken;
                principal = handler.ValidateToken(Token, validationParameters, out securityToken);
                foreach (var item in principal.Claims)
                {
                    Clims.Add(item);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        #region private
        private async Task<string> TokenGen(AuthIdentity identity, SSOAuthAppItem appAuth)
        {
            string jwtToken = "";
            await Task.Run(() =>
            {
                var expTime = DateTime.Now.AddHours(_jWTOptions.ExpireHours);
                var claims = new List<Claim>
                {
                    new Claim(IdentityClaimTypes.AppId, identity!.AppId),
                    new Claim(IdentityClaimTypes.OrganizeId, identity.OrganizeId??""),
                    new Claim(IdentityClaimTypes.TopOrganizeId, identity.TopOrganizeId ?? ""),
                    new Claim(IdentityClaimTypes.Account, identity.Account??""),
                    new Claim(IdentityClaimTypes.UserCode, identity.UserCode??""),
                    new Claim(IdentityClaimTypes.UserName, identity.UserName??""),
                    new Claim(IdentityClaimTypes.UserId, identity.UserId??""),
                    new Claim(IdentityClaimTypes.TokenType, identity.TokenType??""),
                    new Claim(IdentityClaimTypes.Token, identity.Token??""),
                    new Claim(IdentityClaimTypes.AppAuthKey, identity.AppAuthKey??""),
                    new Claim(ClaimTypes.NameIdentifier, identity.UserCode??""),
                };
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appAuth.AppSecret));
                SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _jWTOptions.Issuer,
                    Audience = appAuth.AppId,
                    Subject = new ClaimsIdentity(claims),
                    NotBefore = DateTime.Now,
                    Expires = DateTime.Now.AddHours(this._jWTOptions.ExpireHours),
                    SigningCredentials = creds    //new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature)
                    //SigningCredentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature)
                };
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                jwtToken = tokenHandler.WriteToken(securityToken);
                return;
            });
            return jwtToken;
        }


        /// <summary>
        /// 身份验证 权限验证
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="FailedException"></exception>
        private SSOAuthAppItem OptionValid(JWTTokenRequest request)
        {
            if (request == null || request.authIdentity == null)
            {
                throw new FailedException($"{_MsgPrefix}请补充身份信息");
            }
            var authApp = _jWTOptions.SSOAuthApp?.FirstOrDefault(p => p.AppId == (request.authIdentity.KeyLicense ?? request.authIdentity.AppId));
            if (authApp == null)
            {
                throw new FailedException($"{_MsgPrefix}AppId未授权");
            }
            switch (request.authType)
            {
                case AuthType.Web:
                    if (string.IsNullOrWhiteSpace(request.authIdentity.Account) || string.IsNullOrWhiteSpace(request.authIdentity.UserCode))
                    {
                        throw new FailedException($"{_MsgPrefix}账户信息不可为空");
                    }
                    if (string.IsNullOrWhiteSpace(request.authIdentity.TopOrganizeId) && string.IsNullOrWhiteSpace(request.authIdentity.OrganizeId))
                    {
                        throw new FailedException($"{_MsgPrefix}组织机构信息不可为空");
                    }
                    break;
                case AuthType.WebApi:
                    //限制访问
                    break;
                default:
                    throw new FailedException($"{_MsgPrefix}不支持该授权方式");
                    //break;
            }
            return authApp;
        }

        private string GetCacheKey(JWTTokenRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.authIdentity?.OrganizeId))
            {
                return $"Org_{request.authIdentity.OrganizeId}:{RedisKey.SsoLoginUserPrefix}{request.authType}_{request.authIdentity!.AppId}_{request.authIdentity!.Account ?? "App"}";
            }
            return $"Org*:{RedisKey.SsoLoginUserPrefix}{request.authType}_{request.authIdentity!.AppId}_{request.authIdentity!.Account ?? "App"}";

        }
        #endregion
    }
}
