using System;
using FrameworkBase.MultiOrg.Application;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.OutpatientManage;
using Newtouch.HIS.Sett.Request;
using Newtouch.HIS.Sett.Request.OutPatientPharmacy;
using Newtouch.HIS.Sett.Request.Patient;
using Newtouch.Infrastructure;
//using Newtouch.PDS.Requset;
using Newtouch.Tools;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 内部接口调用业务
    /// </summary>
    public class InsideGeneralApp : AppBase, IInsideGeneralApp
    {
        private readonly IGuiAnOutpatientXnhApp _guiAnOutpatientXnhApp;
        private readonly IPatientBasicInfoDmnService _patientBasic;
        private readonly IOutpatientPharmacyAPIDmnService _outpatientPharmacyAPIDmnService;

        /// <summary>
        /// 公共入口
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public void PublicPortal(MqGeneralTaskRequestDto requestDto)
        {
            if (requestDto == null)
            {
                throw new FailedException("请求报文并不能为空");
            }
            if (string.IsNullOrWhiteSpace(requestDto.body))
            {
                throw new FailedException("报文body不能为空");
            }
            var o = requestDto.body.ToObject<MqGeneralTaskBody>();
            if (o == null)
            {
                throw new FailedException("body反序列化失败，结果为null");
            }

            switch (o.OperationType)
            {
                //case "xnhS27"://查询并落地S27（结算查询）结果
                //    var pr = _guiAnOutpatientXnhApp.QueryAndRecordSettDetail(o.Content);
                //    if (!string.IsNullOrWhiteSpace(pr)) throw new FailedException(pr);
                //    break;
                //case "supplementPatientBaseInfo"://补充患者基本信息
                //    var r = SupplementPatientBaseInfo(o.Content);
                //    if (!string.IsNullOrWhiteSpace(r)) throw new FailedException(r);
                //    break;
                case "OutpatientDrugWithdrawalNotify"://门诊退药
                    var rd = OutpatientDrugWithdrawalNotify(o.Content);
                    if (!string.IsNullOrWhiteSpace(rd)) throw new FailedException(rd);
                    break;
                default:
                    throw new FailedException("未匹配到响应的处理流程，请查看OperationType赋值是否正确");
            }
        }

        /// <summary>
        /// 门诊退药
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string OutpatientDrugWithdrawalNotify(string request)
        {
            var po = request.ToObject<OutpatientDrugWithdrawalNotifyRequest>();
            if (po == null) throw new Exception("请求参数序列化后为空，原始报文：" + request);
            _outpatientPharmacyAPIDmnService.OutpatientDrugWithdrawalNotify(po.OrganizeId, po.cfnm, po.yp, po.sl, po.czh);
            return "";
        }

        /// <summary>
        /// 补充患者基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //private string SupplementPatientBaseInfo(string request)
        //{
        //    var po = request.ToObject<PatientBaseInfoQueryRequest>();
        //    if (po == null) throw new Exception("请求参数序列化后为空，原始报文：" + request);
        //    var brjbxx = _patientBasic.SelectPatientBasicInfoByCfh(po.cfh, po.OrganizeId);
        //    if (brjbxx == null) return string.Format("根据处方号【{0}】和机构Id【{1}】未找到系统病人基本信息！", po.cfh, po.OrganizeId);
        //    var rp = new SupplementPatientBaseInfoRequest();
        //    brjbxx.MapperTo(rp);
        //    rp.cfh = po.cfh;
        //    rp.Timestamp = DateTime.Now;
        //    AppLogger.Instance.Info("SupplementPatientBaseInfo requestXml", rp);
        //    var rs = SitePDSAPIHelper.Request<APIRequestHelper.DefaultResponse>("api/Outpatient/SupplementPatientBaseInfo", rp, autoAppendToken: false);

        //    return rs.code != APIRequestHelper.ResponseResultCode.SUCCESS ? string.Format("更新门诊处方【{0}】患者信息失败。失败原因：{1} {2}", po.cfh, rs.msg, rs.sub_msg) : "";
        //}
    }
}