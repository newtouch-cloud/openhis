using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.API.Common.Filter;
using Newtouch.HIS.Application.Interface.HospitalizationManage;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.HospitalizationManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Sett.Request;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Newtouch.HIS.Sett.API.Controllers
{
    /// <summary>
    /// 对外接口 患者信息 无需身份信息
    /// </summary>
    [RoutePrefix("api/PatientAnon")]
    [IgnoreTokenDecrypt]
    public class PatientApiController : ApiControllerBase<OrderController>
    {
        private readonly IDischargeSettleApp _dischargeSettleApp;
        private readonly IPatientCenterDmnService _patientCenterDmnService;
        private readonly IPatientBasicInfoDmnService _patientBasicInfoDmn;
        private readonly IDischargeSettleDmnService _dischargeSettleDmnService;

        public PatientApiController(IComponentContext com)
            : base(com)
        {
        }

        #region 门诊
        [HttpPost]
        [Route("OutpatRegRecord")]
        [IgnoreTokenDecrypt]
        public ResponseBase OutpatRegRecord(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            para.AppId = GetBookTerminal(dto.AppId);
            para.kh = dto.paradata.ContainsKey("kh") == true ? dto.paradata["kh"] : null;
            para.mzh = dto.paradata.ContainsKey("mzh") == true ? dto.paradata["mzh"] : null;
            para.xm = dto.paradata.ContainsKey("xm") == true ? dto.paradata["xm"] : null;

            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                //去挂号
                var data = _patientCenterDmnService.GetPatRegList(req.OrgId, req.AppId, false, req.kh, req.mzh);
                if (data.Count > 0)
                {
                    resp.code = ResponseResultCode.SUCCESS;
                    resp.msg = "";
                    resp.data = data;
                }
                else
                {

                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未能查询到有效挂号信息";
                }
            };
            return CommonExecute(ac, para);
        }
        #endregion

        #region 报告
        [HttpPost]
        [Route("PatientLisReport")]
        [IgnoreTokenDecrypt]
        public ResponseBase PatientLisReport(BookingReqBase apireq)
        {
            PatientLisReportRequest para = new PatientLisReportRequest();
            para.Timestamp = apireq.Timestamp;
            para.AppId = GetBookTerminal(apireq.AppId);
            para.kh = apireq.paradata.ContainsKey("kh") == true ? apireq.paradata["kh"] : null;
            para.mzh = apireq.paradata.ContainsKey("mzh") == true ? apireq.paradata["mzh"] : null;
            para.zyh = apireq.paradata.ContainsKey("zyh") == true ? apireq.paradata["zyh"] : null;
            para.sqdzt = apireq.paradata.ContainsKey("sqdzt") == true ? apireq.paradata["sqdzt"] : null;
            para.ywlx = apireq.paradata.ContainsKey("ywlx") == true ? apireq.paradata["ywlx"] : null;

            Action<PatientLisReportRequest, DefaultResponse> ac = (req, resp) =>
            {
                var data =new List<PatientReportIndex>();
                if (req.ywlx=="1")
                {
                    data = _patientCenterDmnService.OutPatReportList(req.kh, req.mzh, req.sqdzt, (int)EnumCflx.InspectionPres, req.AppId, apireq.OrganizeId);
                }
                else if(req.ywlx == "2")
                {
                    data = _patientCenterDmnService.InHosPatReportList(req.kh, req.zyh, req.sqdzt, (int)EnumCflx.InspectionPres, req.AppId, apireq.OrganizeId);
                }
               
                if (data != null && data.Count > 0)
                {
                    resp.code = ResponseResultCode.SUCCESS;
                    resp.msg = "";
                    resp.data = data;
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = string.IsNullOrWhiteSpace(req.mzh) && string.IsNullOrWhiteSpace(req.zyh) ? "未能查询到三天内有效报告信息" : "未能查询到有效报告信息";
                }
            };
            return CommonExecute(ac, para);
        }
        [HttpPost]
        [Route("PatientLisReportDetail")]
        [IgnoreTokenDecrypt]
        public ResponseBase PatLisReportDetail(BookingReqBase apireq)
        {
            PatientLisReportDetailRequest para = new PatientLisReportDetailRequest();
            para.Timestamp = apireq.Timestamp;
            para.AppId = GetBookTerminal(apireq.AppId);
            para.sqdh = apireq.paradata.ContainsKey("sqdh") == true ? apireq.paradata["sqdh"] : null;
            para.xmdm = apireq.paradata.ContainsKey("xmdm") == true ? apireq.paradata["xmdm"] : null;
            para.ywlx = apireq.paradata.ContainsKey("ywlx") == true ? apireq.paradata["ywlx"] : null;

            Action<PatientLisReportDetailRequest, DefaultResponse> ac = (req, resp) =>
            {
                //去挂号
                var data = _patientCenterDmnService.PatLisReportDetail(req.sqdh, req.xmdm, req.ywlx, req.AppId, apireq.OrganizeId);
                if (data != null)
                {
                    resp.code = ResponseResultCode.SUCCESS;
                    resp.msg = "";
                    resp.data = data;
                }
                else
                {

                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未能查询到有效报告信息";
                }
            };
            return CommonExecute(ac, para);
        }
        #endregion

        #region 住院
        /// <summary>
        /// 查询患者
        /// </summary>
        /// <param name="apireq"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InHosPatientQuery")]
        [IgnoreTokenDecrypt]
        public ResponseBase InHosPatientQuery(BookingReqBase apireq)
        {
            InPatientCenterRequest request = new InPatientCenterRequest
            {
                AppId = GetBookTerminal(apireq.AppId),
                Timestamp = apireq.Timestamp,
                zyh = apireq.paradata.ContainsKey("zyh") == true ? apireq.paradata["zyh"] : null,
                kh = apireq.paradata.ContainsKey("kh") == true ? apireq.paradata["kh"] : null,
                zjh = apireq.paradata.ContainsKey("zjh") == true ? apireq.paradata["zjh"] : null
            };
            if (string.IsNullOrWhiteSpace(request.zyh) && string.IsNullOrWhiteSpace(request.zjh) && string.IsNullOrWhiteSpace(request.kh))
            {
                return new ResponseBase
                {
                    code = ResponseResultCode.FAIL,
                    msg = "关键身份信息不可为空"
                };
            }
            Action<InPatientCenterRequest, DefaultResponse> ac = (req, resp) =>
            {
                var data = _patientCenterDmnService.InHosPatientBasic(request.zyh, request.zjh, null, apireq.OrganizeId, request.kh);
                if (data != null)
                {
                    resp.data = data.Select(p => new
                    {
                        zyh = p.zyh,
                        xm = p.xm,
                        xb = p.xb,
                        zybz = p.zybz,
                        brxzmc = p.brxzmc,
                        kh = p.kh,
                        nl = p.nlshow,
                        cw = p.cwmc,
                        ryrq = p.ryrq,
                        cyrq = p.cyrq,
                        bqmc = p.wardname,
                        zyts = p.zyts
                    });
                    //resp.data = data;
                    resp.code = ResponseResultCode.SUCCESS;
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未找到相关记录";
                }
            };
            return base.CommonExecute(ac, request);
        }

        /// <summary>
        /// 查询住院患者账单
        /// </summary>
        /// <param name="apireq"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InHosBillUnpaid")]
        [IgnoreTokenDecrypt]
        public ResponseBase InHosBillUnpaidByZyh(BookingReqBase apireq)
        {
            InPatientCenterRequest request = new InPatientCenterRequest
            {
                AppId = GetBookTerminal(apireq.AppId),
                Timestamp = apireq.Timestamp,
                zyh = apireq.paradata.ContainsKey("zyh") == true ? apireq.paradata["zyh"] : null,
                kh = apireq.paradata.ContainsKey("kh") == true ? apireq.paradata["kh"] : null,
                zjh = apireq.paradata.ContainsKey("zjh") == true ? apireq.paradata["zjh"] : null
            };
            if (string.IsNullOrWhiteSpace(request.zyh) && string.IsNullOrWhiteSpace(request.zjh) && string.IsNullOrWhiteSpace(request.kh))
            {
                return new ResponseBase
                {
                    code = ResponseResultCode.FAIL,
                    msg = "关键身份信息不可为空"
                };
            }
            var patInfo = _patientCenterDmnService.InHosPatientBasic(request.zyh, request.zjh, null, apireq.OrganizeId, request.kh).FirstOrDefault();
            if (patInfo != null && (patInfo.zybz == ((int)EnumZYBZ.Djz).ToString() || patInfo.zybz == ((int)EnumZYBZ.Ycy).ToString()))
            {
                request.zyh = request.zyh ?? patInfo.zyh;
            }
            else
            {
                return new ResponseBase
                {
                    code = ResponseResultCode.FAIL,
                    msg = "请确认患者在院状态，请办理出区后再进行结算"
                };
            }
            if (string.IsNullOrWhiteSpace(request.zyh))
            {
                return new ResponseBase
                {
                    code = ResponseResultCode.FAIL,
                    msg = "未找到该患者住院信息"
                };
            }
            Action<InPatientCenterRequest, DefaultResponse> ac = (req, resp) =>
            {
                var data = _dischargeSettleDmnService.GetHospGroupFeeVOList(request.zyh, apireq.OrganizeId, "2");
                if (data != null)
                {
                    resp.data = data;
                    resp.code = ResponseResultCode.SUCCESS;
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未找到相关记录";
                }
            };
            return base.CommonExecute(ac, request);
        }

        #endregion
    }


}
