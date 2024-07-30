using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.Application.Interface;
using Newtouch.PDS.Requset.Zyypyz;

namespace Newtouch.PDS.API.Controllers
{
    /// <summary>
    /// 住院药品医嘱
    /// </summary>
    [RoutePrefix("api/Zyypyz")]
    public class ZyypyzController : ApiControllerBase<ZyypyzController>
    {
        private readonly IResourcesOperateApp _resourcesOperateApp;

        public ZyypyzController(IComponentContext com) : base(com)
        {
        }

        /// <summary>
        /// 医嘱执行
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [Route("yzzx")]
        [HttpPost]
        public HttpResponseMessage Yzzx(ZyypyzzxRequest par)
        {
            Action<ZyypyzzxRequest, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _resourcesOperateApp.HospitalizatiionBook(par);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }

    }
}
