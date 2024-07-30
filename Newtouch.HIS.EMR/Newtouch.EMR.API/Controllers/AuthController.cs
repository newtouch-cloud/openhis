using Autofac;
using Newtouch.EMR.API.Models;
using Newtouch.EMR.APIRequest;
using Newtouch.HIS.API.Common;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Newtouch.EMR.API.Controllers
{
    /// <summary>
    /// 授权认证
    /// </summary>
    [RoutePrefix("api/Auth")]
    public class AuthController : ApiControllerBase<AuthController>
    {
        public AuthController(IComponentContext com)
       : base(com)
        {

        }

        /// <summary>
        /// 以关键信息（用户名、密码等）换取访问令牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetToken")]
        [HttpPost]
        public HttpResponseMessage GetToken(AuthGetTokenRequest request)
        {
            Action<AuthGetTokenRequest, DefaultResponse> ac = (req, resp) =>
            {
                //1、由request验证出用户身份

                //2、身份存储在Redis（有一定的过期时间）
                var identity = new UserIdentity()
                {
                    AppId = request.AppId,
                    TokenType = request.TokenType,
                    UserId = "?",
                    Account = "?",
                    OrganizeId = "?",
                    TopOrganizeId = "?",
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