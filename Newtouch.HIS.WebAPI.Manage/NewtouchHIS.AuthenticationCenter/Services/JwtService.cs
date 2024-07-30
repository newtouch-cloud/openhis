using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Model.Authentication;
using NewtouchHIS.Lib.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NewtouchHIS.Lib.Services
{
    /// <summary>
    /// jwt服务
    /// </summary>
    public abstract class JWTBaseService 
    {
        protected readonly JWTOptions _jWTOptions;
        public JWTBaseService(IOptionsMonitor<JWTOptions> jwtOptions)
        {
            _jWTOptions = jwtOptions.CurrentValue; 
        }

        /// <summary>
        /// 获取授权码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public BusResult<string> GetCode(string clientId, string userName, string password)
        {
            BusResult<string> result = new BusResult<string>();

            string code = string.Empty;
            SSOAuthAppItem appItem = _jWTOptions.SSOAuthApp?.Where(s => s.AppId == clientId).FirstOrDefault();
            if (appItem == null)
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "应用未授权" };
            }

            //用户信息
            UserIdentity UserIdentity = new UserIdentity
            {
                UserId = "101",
                Account = "admin",
                UserName = "张三"
            };

            //生成授权码
            code = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            string key = $"AuthCode:{code}";
            string appCachekey = $"AuthCodeClientId:{code}";
            //缓存授权码
            _cachelper.SetCache<UserIdentity>(key, UserIdentity, TimeSpan.FromMinutes(10));
            //缓存授权码是哪个应用的
            _cachelper.SetCache(appCachekey, appItem.AppId, TimeSpan.FromMinutes(10));
            //创建全局会话
            string sessionCode = $"SessionCode:{code}";
            //SessionCodeUser sessionCodeUser = new SessionCodeUser
            //{
            //    expiresTime = DateTime.Now.AddHours(1),
            //    currentUser = UserIdentity
            //};
            //_cachelper.SetCache<UserIdentity>(sessionCode, UserIdentity, TimeSpan.FromDays(1));
            //全局会话过期时间
            string sessionExpiryKey = $"SessionExpiryKey:{code}";
            DateTime sessionExpirTime = DateTime.Now.AddDays(1);
            _cachelper.SetCache<DateTime>(sessionExpiryKey, sessionExpirTime, TimeSpan.FromDays(1));
            Console.WriteLine($"登录成功，全局会话code:{code}");
            //缓存授权码取token时最长的有效时间
            _cachelper.SetCache<DateTime>($"AuthCodeSessionTime:{code}", sessionExpirTime, TimeSpan.FromDays(1));

            //result.SetSuccess(code);
            return result;
        }
        /// <summary>
        /// 根据会话code获取授权码
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="sessionCode"></param>
        /// <returns></returns>
        public async Task<BusResult<string>> GetCodeBySessionCode(string clientId, string sessionCode)
        {
            BusResult<string> result = new BusResult<string>();
            string code = string.Empty;
            SSOAuthAppItem appItem = _jWTOptions.SSOAuthApp.Where(s => s.AppId == clientId).FirstOrDefault();
            if (appItem == null)
            {
                //result.SetFail("应用不存在");
                return result;
            }
            string codeKey = $"SessionCode:{sessionCode}";
            UserIdentity UserIdentity =await _cachelper.GetCache<UserIdentity>(codeKey);
            if (UserIdentity == null)
            {
                //return result.SetFail("会话不存在或已过期", string.Empty);
            }

            //生成授权码
            code = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            string key = $"AuthCode:{code}";
            string appCachekey = $"AuthCodeClientId:{code}";
            //缓存授权码
            _cachelper.SetCache<UserIdentity>(key, UserIdentity, TimeSpan.FromMinutes(10));
            //缓存授权码是哪个应用的
            _cachelper.SetCache<string>(appCachekey, appItem.AppId, TimeSpan.FromMinutes(10));

            //缓存授权码取token时最长的有效时间
            DateTime expirTime = await _cachelper.GetCache<DateTime>($"SessionExpiryKey:{sessionCode}");
            _cachelper.SetCache<DateTime>($"AuthCodeSessionTime:{code}", expirTime, expirTime - DateTime.Now);

          
            return result;

        }

        /// <summary>
        /// 根据刷新Token获取Token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<string> GetTokenByRefresh(string refreshToken, string clientId)
        {
            //刷新Token是否在缓存
            UserIdentity UserIdentity =await _cachelper.GetCache<UserIdentity>($"RefreshToken:{refreshToken}");
            if (UserIdentity == null)
            {
                return String.Empty;
            }
            //刷新token过期时间
            DateTime refreshTokenExpiry =await _cachelper.GetCache<DateTime>($"RefreshTokenExpiry:{refreshToken}");
            //token默认时间为600s
            double tokenExpiry = 600;
            //如果刷新token的过期时间不到600s了，token过期时间为刷新token的过期时间
            if (refreshTokenExpiry > DateTime.Now && refreshTokenExpiry < DateTime.Now.AddSeconds(600))
            {
                tokenExpiry = (refreshTokenExpiry - DateTime.Now).TotalSeconds;
            }

            //从新生成Token
            string token = IssueToken(new JWTTokenRequest(), clientId, tokenExpiry);
            return token;

        }

        /// <summary>
        /// 根据授权码,获取Token
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="appHSSetting"></param>
        /// <returns></returns>
        public async Task<BusResult<JWTToken>> GetTokenWithRefresh(string authCode)
        {
            BusResult<JWTToken> result = new BusResult<JWTToken>();

            string key = $"AuthCode:{authCode}";
            string clientIdCachekey = $"AuthCodeClientId:{authCode}";
            string AuthCodeSessionTimeKey = $"AuthCodeSessionTime:{authCode}";

            //根据授权码获取用户信息
            UserIdentity UserIdentity =await _cachelper.GetCache<UserIdentity>(key);
            if (UserIdentity == null)
            {
                throw new Exception("code无效");
            }
            //清除authCode，只能用一次
            _cachelper.RemoveCache(key);

            //获取应用配置
            string clientId =await _cachelper.GetCache<string>(clientIdCachekey);
            //刷新token过期时间
            DateTime sessionExpiryTime =await _cachelper.GetCache<DateTime>(AuthCodeSessionTimeKey);
            DateTime tokenExpiryTime = DateTime.Now.AddMinutes(10);//token过期时间10分钟
                                                                   //如果刷新token有过期期比token默认时间短，把token过期时间设成和刷新token一样
            if (sessionExpiryTime > DateTime.Now && sessionExpiryTime < tokenExpiryTime)
            {
                tokenExpiryTime = sessionExpiryTime;
            }
            //获取访问token
            string token = this.IssueToken(new JWTTokenRequest(), clientId, (tokenExpiryTime - DateTime.Now).TotalSeconds);


            TimeSpan refreshTokenExpiry;
            if (sessionExpiryTime != default(DateTime))
            {
                refreshTokenExpiry = sessionExpiryTime - DateTime.Now;
            }
            else
            {
                refreshTokenExpiry = TimeSpan.FromSeconds(60 * 60 * 24);//默认24小时
            }
            //获取刷新token
            string refreshToken = this.IssueToken(new JWTTokenRequest(), clientId, refreshTokenExpiry.TotalSeconds);
            //缓存刷新token
            _cachelper.SetCache($"RefreshToken:{refreshToken}", UserIdentity, refreshTokenExpiry);
            //缓存刷新token过期时间
            _cachelper.SetCache($"RefreshTokenExpiry:{refreshToken}", DateTime.Now.AddSeconds(refreshTokenExpiry.TotalSeconds), refreshTokenExpiry);
            //result.SetSuccess(new JWTToken() { Token = token });
            Console.WriteLine($"client_id:{clientId}获取token,有效期:{sessionExpiryTime.ToString("yyyy-MM-dd HH:mm:ss")},token:{token}");
            return result;
        }

        #region private
        /// <summary>
        /// 签发token
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="clientId"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private string IssueToken(JWTTokenRequest userModel, string clientId, double second = 600)
        {
            var identity = userModel.authIdentity;
            var claims = new List<Claim>
            {
                    new Claim(IdentityClaimTypes.AppId, identity.AppId),
                    new Claim(IdentityClaimTypes.OrganizeId, identity.OrganizeId??""),
                    new Claim(IdentityClaimTypes.TopOrganizeId, identity.TopOrganizeId ?? ""),
                    new Claim(IdentityClaimTypes.Account, identity.Account??""),
                    new Claim(IdentityClaimTypes.UserCode, identity.UserCode??""),
                    new Claim(IdentityClaimTypes.UserName, identity.UserName??""),
                    new Claim(IdentityClaimTypes.UserId, identity.UserId??""),
                    new Claim(IdentityClaimTypes.TokenType, identity.TokenType??""),
                    new Claim(IdentityClaimTypes.Token, identity.Token??""),
                };
            //var appHSSetting = getAppInfoByAppKey(clientId);
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appHSSetting.clientSecret));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var creds = GetCreds(clientId);
            /**
             * Claims (Payload)
                Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT 标准规定了一些字段，下面节选一些字段:
                iss: The issuer of the token，签发主体，谁给的
                sub: The subject of the token，token 主题
                aud: 接收对象，给谁的
                exp: Expiration Time。 token 过期时间，Unix 时间戳格式
                iat: Issued At。 token 创建时间， Unix 时间戳格式
                jti: JWT ID。针对当前 token 的唯一标识
                除了规定的字段外，可以包含其他任何 JSON 兼容的字段。
             * */
            var token = new JwtSecurityToken(
                issuer: "SSOCenter", //谁给的
                audience: clientId, //给谁的
                claims: claims,
                expires: DateTime.Now.AddSeconds(second),//token有效期
                notBefore: null,//立即生效  DateTime.Now.AddMilliseconds(30),//30s后有效
                signingCredentials: creds);
            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            return returnToken;
        }

        /// <summary>
        /// 根据appKey获取应用信息
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        private SSOAuthAppItem getAppInfoByAppKey(string clientId)
        {
            return _jWTOptions.SSOAuthApp.Where(s => s.AppId == clientId).FirstOrDefault();
        }
        /// <summary>
        /// 获取加密方式
        /// </summary>
        /// <returns></returns>
        protected abstract SigningCredentials GetCreds(string clientId);

        #endregion
    }
}
