using Autofac;
using Newtouch.CIS.APIRequest.Inpatient;
using Newtouch.Domain.IDomainServices.Inpatient;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.API.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace NewtouchCIS.API.Controllers
{
    [RoutePrefix("api/Hospitalized")]
    [DefaultAuthorize]
    public class HospitalizedBusinessController : ApiControllerBase<HospitalizedBusinessController>
    {

        private readonly IDeptMedicineApplyNoDmnService _deptMedicineApplyNoDmnService;

        public HospitalizedBusinessController(IComponentContext com)
           : base(com)
        {
        }

        [HttpPost]
        [Route("UpdateApplyNoStatus")]
        public HttpResponseMessage UpdateApplyNoStatus(DeptApplyNoRequest dto)
        {
            Action<DeptApplyNoRequest, DefaultResponse> ac = (req, resp) =>
            {
                var valdata = _deptMedicineApplyNoDmnService.SelectApplyNoStatus(req.OrganizeId, req.SqdArray, req.UserCode);
                if (valdata.Count > 0)
                {
                    valdata = valdata.Where(p => p.sqzt != "1").ToList();
                    if (valdata.Count == 0)
                    {
                        var cnt = _deptMedicineApplyNoDmnService.UpdateApplyNoStatus(req.OrganizeId, req.SqdArray, req.UserCode);
                        resp.code = ResponseResultCode.SUCCESS;
                        if (cnt == 0)
                        {
                            resp.code = ResponseResultCode.FAIL;
                            resp.msg = "更新申请单状态失败,申请单无效";
                        }
                    }
                    else
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "更新申请单状态失败,只能退回申请状态为'已申请'的单据!";
                    }
                }
                else {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未找到有效申请单号!";
                }
            };

            var response = base.CommonExecute(ac, dto);

            return base.CreateResponse(response);
        }
        [HttpPost]
        [Route("UpdateReturnApplyNoStatus")]
        public HttpResponseMessage UpdateReturnApplyNoStatus(DeptApplyNoRequest dto)
        {
            Action<DeptApplyNoRequest, DefaultResponse> ac = (req, resp) =>
            {
                var valdata = _deptMedicineApplyNoDmnService.SelectKcthApplyNoStatus(req.OrganizeId, req.SqdArray, req.UserCode);
                if (valdata != null)
                {
                    if (valdata != "1")
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "更新申请单状态失败,只能退回申请状态为'已申请'的单据!";
                    }
                    else
                    {
                        var cnt = _deptMedicineApplyNoDmnService.UpdateReturnApplyNoStatus(req.OrganizeId, req.SqdArray, req.UserCode);
                        resp.code = ResponseResultCode.SUCCESS;
                        if (cnt == 0)
                        {
                            resp.code = ResponseResultCode.FAIL;
                            resp.msg = "更新申请单状态失败,申请单无效";
                        }
                    }
                }
                else {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未找到该申请单号，申请单无效!";
                }
            };

            var response = base.CommonExecute(ac, dto);

            return base.CreateResponse(response);
        }
    }
}