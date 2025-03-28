using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.Base.HOSP.Request;
using System;
using System.Net.Http;
using System.Web.Http;
using Newtouch.HIS.API.Common.Models;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Base.HOSP.API.Controllers
{
    /// <summary>
    /// 授权认证
    /// </summary>
    [RoutePrefix("api/Auth")]
    public class AuthController : ApiControllerBase<AuthController>
    {
        public AuthController(IComponentContext com) : base(com)
        { }
        #region private
        private readonly IAccessAuthDmnService _accessAuthDmnService;
        #endregion
        /// <summary>
        /// 以关键信息（用户名、密码等）换取访问令牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetTokenbyProxy")]
        public HttpResponseMessage GetTokenbyProxy(AuthGetTokenRequest request)
        {
            Action<AuthGetTokenRequest, DefaultResponse> ac = (req, resp) =>
            {
                if (string.IsNullOrWhiteSpace(request.UserId)
                    || string.IsNullOrWhiteSpace(request.AppId)
                    || string.IsNullOrWhiteSpace(request.OrganizeId))
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = "AUTH_FAILED";
                    resp.sub_msg = "";
                    return;
                }
                //then假设身份验证通过了
                AccessIdentity ident= new AccessIdentity();
                ident.AppId = request.AppId;
                ident.OrganizeId = request.OrganizeId;
                ident.Account = request.Account;
                //1、由request验证出用户身份 //需要的时候加上验证
                string message = "";
                bool authed= _accessAuthDmnService.Authorized(ident, out message);
                if (authed)
                {
                    //2、身份存储在Redis（有一定的过期时间）
                    var identity = new UserIdentity
                    {
                        AppId = request.AppId,
                        TokenType = request.TokenType,
                        UserId = request.UserId,
                        Account = request.Account,
                        OrganizeId = request.OrganizeId,
                        TopOrganizeId = request.TopOrganizeId,
                        //Tag = "ProxyAPI_TP"
                    };
                    var token = SaveIdentity(identity);
                    //3、返回key
                    resp.data = new
                    {
                        token = token
                    };
                }
                else
                {
                    resp.msg = message;
                }
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, request);
            return base.CreateResponse(response);
        }

        /// <summary>
        /// 以关键信息（用户名、密码等）换取访问令牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetToken(AuthGetTokenRequest request)
        {
            Action<AuthGetTokenRequest, DefaultResponse> ac = (req, resp) =>
            {
                if (string.IsNullOrWhiteSpace(request.UserId)
                    || string.IsNullOrWhiteSpace(request.Account)
                    || string.IsNullOrWhiteSpace(request.OrganizeId)
                    || string.IsNullOrWhiteSpace(request.TopOrganizeId))
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = "AUTH_FAILED";
                    resp.sub_msg = "";
                    return;
                }
                //then假设身份验证通过了
                //1、由request验证出用户身份 //需要的时候加上验证

                //2、身份存储在Redis（有一定的过期时间）
                var identity = new UserIdentity
                {
                    AppId = request.AppId,
                    TokenType = request.TokenType,
                    UserId = request.UserId,
                    Account = request.Account,
                    OrganizeId = request.OrganizeId,
                    TopOrganizeId = request.TopOrganizeId,
                };
                var token = SaveIdentity(identity);

                //3、返回key
                resp.data = new
                {
                    token = token
                };
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, request);
            return base.CreateResponse(response);
        }

    }
}
