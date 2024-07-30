using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.PDS.Requset.DeptPharmacyDrugs;

namespace Newtouch.PDS.API.Controllers
{
    /// <summary>
    /// 部门药房药品
    /// </summary>
    [RoutePrefix("api/DeptPharmacyDrugs")]
    public class DeptPharmacyDrugsController : ApiControllerBase<DeptPharmacyDrugsController>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="com"></param>
        public DeptPharmacyDrugsController(IComponentContext com) : base(com)
        {
        }

        /// <summary>
        /// 门诊排药合计信息（待排药）
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDeptPharmacyDrugs")]
        public HttpResponseMessage GetDeptPharmacyDrugs(DeptPharmacyDrugsRequset par)
        {
            Action<DeptPharmacyDrugsRequset, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = " GetDeptPharmacyDrugs 123123123";
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }
    }
}