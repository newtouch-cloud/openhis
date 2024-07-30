using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Application.Interface.Inpatient;
using Newtouch.Common;
using Newtouch.Common.Web;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Utils;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.DTO.OutputDto.Inpatient.API;
using Newtouch.Domain.DTO.OutputDto.Outpatient;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository.Inpatient;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Domain.ViewModels;
using Newtouch.Domain.ViewModels.Outpatient;
using Newtouch.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.CIS.Web.Areas.DoctorManage
{
    public class MedicineController : OrgControllerBase
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IDoctorserviceApp _doctorserviceApp;
        private readonly IDoctorserviceDmnService _doctorserviceDmnService;
        private readonly IQhdZnshSqtxRepo _qhdznshsqtxRepo;



        public ActionResult SubmitdoctorService(List<DoctorServiceRequestDto> reqdoctorservices, List<string> deldata)
        {
            string yzh="";
            _doctorserviceDmnService.SubmitdoctorServiceV2(OrganizeId, reqdoctorservices, deldata, out yzh);
            return Success("", yzh);
        }

        public ActionResult ValidateRepeat(List<DSrepeatRequestVO> req, string zyh)
        {
            var predata = _doctorserviceDmnService.DSTransferCL(req, OrganizeId);
            var data = _doctorserviceDmnService.DoctorserviceValidate(predata, zyh, OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 修改医嘱时，根据医嘱Id获取详情
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="yzlx"></param>
        /// <returns></returns>
        public ActionResult GetYZDetail(string zyh, string yzId, string yzlx)
        {
            //string conflinktoOR = _sysConfigRepo.GetValueByCode("EnableLinkToOR", OrganizeId);
            //string b = _sysConfigRepo.GetByCode("EnableLinkToOR", OrganizeId).ToString();
            String conflinktoOR = ConfigurationManager.AppSettings["EnableLinkToOR"];

            if (!string.IsNullOrWhiteSpace(conflinktoOR) && conflinktoOR == "true")
            {
                var datass = _doctorserviceDmnService.Ssupdate(yzId, zyh, OrganizeId);
                if (datass.Count > 0)
                {
                    throw new FailedException("手术医嘱不能修改");
                }
            }
            var data = _doctorserviceApp.GetYZDetail(zyh, yzId, yzlx, OrganizeId);

            if (data.DoctorServiceUIRequestDto == null || data.DoctorServiceUIRequestDto.Count <= 0) return Content(data.ToJson());
            var d = new List<DocservicekcslRequestDto>();
            foreach (var item in data.DoctorServiceUIRequestDto)
            {
                var e = new DocservicekcslRequestDto { ypCode = item.xmdm, lyyf = item.zxksdm };
                d.Add(e);
            }

            data.DrugStockInfo = Getcurrentkcsl(d);
            return Content(data.ToJson());
            
        }
        /// <summary>
        /// 医嘱诊断
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult OrderDiagnosisForm(string zyh, string brxz)
        {
            ViewBag.brxz = brxz;
            ViewBag.zyh = zyh;
            return View();
        }
        /// <summary>
        /// 构造并调用接口 获取最新库存数量
        /// </summary>
        /// <param name="ypcode"></param>
        /// <returns></returns>
        public string Getcurrentkcsl(List<DocservicekcslRequestDto> ypcodeList)
        {
            try
            {

                var request = new
                {
                    OrganizeId = this.OrganizeId,
                    yplist = ypcodeList,
                    ClientNo = Guid.NewGuid(),
                    TimeStamp = DateTime.Now.ToString()
                };
                var apires = SiteYfykAPIHelper.Request<APIRequestHelper.DefaultResponse>("/api/Stock/query", request, autoAppendToken: false);
                if (apires.code == APIRequestHelper.ResponseResultCode.SUCCESS && apires.data != null)
                {
                    StockQueryResponseDTO successDoOrder = Tools.Json.ToObject<StockQueryResponseDTO>(apires.data.ToString()); //接口返回数据 
                    if (successDoOrder != null && successDoOrder.drugStockInfos.Count > 0)
                    {
                        //List<DrugStockInfo> successDoOrderYp = Tools.Json.ToList<DrugStockInfo>();
                        return successDoOrder.drugStockInfos.ToJson();
                    }

                }
                else
                {
                    return "F|调用药房药库接口失败";
                }

                return "T|执行成功";

            }
            catch (Exception ex)
            {
                return "F|" + ex.InnerException.ToString();
            }
        }

        public ActionResult TCMMedicine() {

            return View();
        }

        /// <summary>
        /// 事前提醒
        /// </summary>
        /// <param name="reqdoctorservices"></param>
        /// <returns></returns>
        public ActionResult GetqhdyzSqtxData(List<DoctorServiceRequestDto> reqdoctorservices , InpatientInfo brxx,string yzcfh)
        {
            var jlId = "";
            if (reqdoctorservices[0].yzlx == (int)EnumYzlx.Wz || reqdoctorservices[0].yzlx == (int)EnumYzlx.oper)
            {
                return Content(null);
            }
            var responsexml = _doctorserviceDmnService.GetqhdyzSqtxData(OrganizeId, reqdoctorservices, brxx, this.UserIdentity.rygh,this.UserIdentity.UserName,yzcfh,out jlId);
            var data = new
            {
                jlId= jlId,
                jydm = "5100",
                xmldata = responsexml
            };
            return Content(data.ToJson());
        }

        public ActionResult SaveLog(string logId, RESPONSEDATA responsedata)
        {
            var jlId = "";
            string responsexml = responsedata.XmlSerialize();
            var entity = new QhdZnshSqtxEntity
            {
                XmlResponse = responsexml
            };
            _qhdznshsqtxRepo.SubmitForm(entity, out jlId, logId);
            return Success();
        }
        /// <summary>
        /// 事前审核接口
        /// </summary>
        /// <param name="reqdoctorservices"></param>
        /// <returns></returns>
        public ActionResult GetPriorReviewData(List<DoctorServiceRequestDto> reqdoctorservices, InpatientInfo brxx,string yzcfh, string GetMAC)
        {
            if (reqdoctorservices[0].yzlx == (int)EnumYzlx.Wz || reqdoctorservices[0].yzlx == (int)EnumYzlx.oper)
            {
                return Content(null);
            }
            string HospitalCode = ConfigurationManager.AppSettings["OrganizeCodeSd"];
            string HospitalName = ConfigurationManager.AppSettings["HospitalName"];
            var response = _doctorserviceDmnService.GetPriorReviewData(OrganizeId, reqdoctorservices, brxx, this.UserIdentity.rygh, this.UserIdentity.UserName, HospitalCode, HospitalName,yzcfh,GetMAC);
            return Success(response);
        }
        /// <summary>
        /// 审核单据删除
        /// </summary>
        /// <returns></returns>
        public ActionResult DeletePriorReview(string zyh,string yzid,string yzlx,string GetMAC)
        {
            var response = _doctorserviceDmnService.DeletePriorReview(zyh, yzid, yzlx,OrganizeId, GetMAC);
            return Success(response);
        }
        /// <summary>
        /// 诊断查询服务接口
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDiagnoseData()
        {
            var response = _doctorserviceDmnService.GetDiagnoseData();
            return Success(response);
        }
        /// <summary>
        /// 病案审核服务
        /// </summary>
        /// <param name="reqdoctorservices"></param>
        /// <returns></returns>
        public ActionResult GetBashData(string zyh, string GetMAC)
        {
            var response = _doctorserviceDmnService.GetBashData(OrganizeId, zyh, this.UserIdentity.rygh, this.UserIdentity.UserName, GetMAC);
            return Success(response);
        }
        /// <summary>
        /// DRG服务
        /// </summary>
        /// <param name="reqdoctorservices"></param>
        /// <returns></returns>
        public ActionResult GetDrgData(string zyh, string GetMAC)
        {
            var response = _doctorserviceDmnService.GetDrgData(OrganizeId, zyh, this.UserIdentity.rygh, this.UserIdentity.UserName, GetMAC);
            return Success(response);
        }
        
    }
}