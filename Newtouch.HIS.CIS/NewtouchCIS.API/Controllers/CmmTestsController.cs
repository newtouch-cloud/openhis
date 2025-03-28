using System.Web.Mvc;
using Autofac;
using Newtouch.Application.Interface;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Tools;

namespace NewtouchCIS.API.Controllers
{
    /// <summary>
    /// 中医馆结构测试
    /// </summary>
    [System.Web.Http.RoutePrefix("api/cmmtests")]
    public class CmmTestsController : ApiControllerBase<CmmTestsController>
    {
        private readonly IOutpatientCmmManagerApp _outpatientCmmManagerApp;

        public CmmTestsController(IComponentContext com) : base(com)
        {
        }

        ///// <summary>
        ///// 当前患者门诊预约查询
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //public string TCM_HIS_01(TcmHis01Request request)
        //{
        //    var result = _outpatientCmmManagerApp.TcmHis01(request.jzId, request.organizeId);
        //    return result.ToJson();
        //}

        /// <summary>
        /// 当前患者门诊预约查询
        /// </summary>
        /// <param name="jzxx"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("TCM_HIS_01")]
        public JsonResult TCM_HIS_01(TreatEntityObj jzxx, string organizeId)
        {
            var result = _outpatientCmmManagerApp.TcmHis01(jzxx, organizeId, "interfacetest");
            return new JsonResult { Data = result.ToJson() }; ;
        }

    }
}