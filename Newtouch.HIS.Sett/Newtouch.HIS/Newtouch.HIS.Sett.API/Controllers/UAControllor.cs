using Autofac;
using FrameworkBase.API;
using Newtonsoft.Json.Linq;
using Newtouch.Core.Common;
using Newtouch.Core.Redis;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Sett.Request.UA;
using System;
using System.Net.Http;
using System.Web.Http;
using Newtouch.HIS.API.Common.Filter;

namespace Newtouch.HIS.Base.HOSP.API.Controllers
{
    /// <summary>
    /// 统一授权认证
    /// </summary>
    [RoutePrefix("api/UA")]
    public class UAController : ApiControllerBase<UAController>
    {
        private readonly IPatientBasicInfoDmnService _patientBasicInfoDmnService;

        public UAController(IComponentContext com)
       : base(com)
        {

        }

        /// <summary>
        /// 以访问令牌换取用户身份信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetUserInfo")]
        [HttpPost]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage GetUserInfo(UAGetUserInfoRequest request)
        {
            Action<UAGetUserInfoRequest, DefaultResponse> ac = (req, resp) =>
            {
                if (string.IsNullOrWhiteSpace(request.access_token)
                    || string.IsNullOrWhiteSpace(request.AppId))
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = "AUTH_FAILED";
                    resp.sub_msg = "";
                    return;
                }

                string account = "", organizeId = "";

                string encryptedResult = null;
                if (!string.IsNullOrWhiteSpace(req.access_token))
                {
                    encryptedResult = RedisHelper.StringGet(req.access_token);
                }
                if (!string.IsNullOrWhiteSpace(encryptedResult) && encryptedResult != "SIDELINED")
                {
                    var jObj = Tools.Json.ToJObject(encryptedResult);
                    foreach (JProperty prop in jObj.Properties())
                    {
                        if (prop.Name == "UserCode")
                        {
                            account = prop.Value.ToString();
                        }
                        else if (prop.Name == "OrganizeId")
                        {
                            organizeId = prop.Value.ToString();
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(account))
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = "AUTH_FAILED";
                    resp.sub_msg = "";
                    return;
                }
                else
                {
                    //更新过期时间 120?
                    RedisHelper.KeyExpire(req.access_token, new TimeSpan(0, 120, 0));
                    //
                    resp.data = new
                    {
                        Account = account,
                        OrganizeId = organizeId,
                    };
                    resp.code = ResponseResultCode.SUCCESS;
                }
            };

            var response = base.CommonExecute(ac, request);

            return base.CreateResponse(response);
        }

        /// <summary>
        /// 门诊患者查询接口预热
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("OutPatientRegistrationQueryPre")]
        [HttpGet]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage OutPatientRegistrationQueryPre(string organizeId)
        {
            string flag = null;
            string msg = null;
            var pagination = new Pagination()
            {
                page = 1,
                rows = 100,
                sidx = "blh"
            };
            var list = _patientBasicInfoDmnService.OutPatientRegistrationQuery(organizeId
                , ref flag, ref msg, Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01")), null
                , null, null
                , null
                , null
                , null
                , pagination);

            return CreateResponse("");
        }

    }
}