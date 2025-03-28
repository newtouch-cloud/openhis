using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Application.Interface.Inpatient;
using Newtouch.Common.Web;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.DTO.OutputDto.Inpatient.API;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.CIS.Web.Areas.DoctorManage
{
    public class MedicineController : OrgControllerBase
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IDoctorserviceApp _doctorserviceApp;
        private readonly IDoctorserviceDmnService _doctorserviceDmnService;


        public ActionResult SubmitdoctorService(List<DoctorServiceRequestDto> reqdoctorservices, List<string> deldata)
        {
            string yzh;
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
            var data = _doctorserviceApp.GetYZDetail(zyh, yzId, yzlx, OrganizeId);
            if (data.DoctorServiceUIRequestDto != null && data.DoctorServiceUIRequestDto.Count > 0)
            {
                List<DocservicekcslRequestDto> d = new List<DocservicekcslRequestDto>();
                foreach (var item in data.DoctorServiceUIRequestDto)
                {
                    DocservicekcslRequestDto e = new DocservicekcslRequestDto { ypCode = item.xmdm, lyyf = item.zxksdm };
                    d.Add(e);
                }

                data.DrugStockInfo = Getcurrentkcsl(d);

            }
            return Content(data.ToJson());
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
    }
}