using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.MR.ManageSystem.Domain.DTO.InputDto;
using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using Newtouch.MR.ManageSystem.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.MR.ManageSystem.Web.Areas.MRHomePage.Controllers
{
    public class MainRecordController : OrgControllerBase
    {
#pragma warning disable CS0649 // Field 'MainRecordController._MainRecordDmnService' is never assigned to, and will always have its default value null
        private readonly IMainRecordDmnService _MainRecordDmnService;
#pragma warning restore CS0649 // Field 'MainRecordController._MainRecordDmnService' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'MainRecordController._EMRDmnService' is never assigned to, and will always have its default value null
        private readonly IEMRDmnService _EMRDmnService;
#pragma warning restore CS0649 // Field 'MainRecordController._EMRDmnService' is never assigned to, and will always have its default value null
#pragma warning disable CS0169 // The field 'MainRecordController._MrbasyzdRepo' is never used
        private readonly IMrbasyzdRepo _MrbasyzdRepo;
        private readonly IMrbasyRepo _MrbasyRepo;
#pragma warning restore CS0169 // The field 'MainRecordController._MrbasyzdRepo' is never used
        //GET: MRHomePage/MainRecord
        //public ActionResult Index()
        //{
            
        //    return View();
        //}

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult MainPreView()
        {
            return View();
        }
        public ActionResult MainEdit()
        {
            return View();
        }
        public ActionResult MainPatInfo()
        {
            return View();
        }
        public ActionResult MainDiag()
        {
            return View();
        }
        public ActionResult MainOplist()
        {
            return View();
        }
        public ActionResult MainOpEdit()
        {
            return View();
        }
        public ActionResult OutHosPatQuery()
        {
            return View();
        }
        public ActionResult MainZyInfo()
        {
            return View();
        }
        public ActionResult MainFeeInfo()
        {
            return View();
        }

        public ActionResult GetGridList(Pagination pagination,string ksrq,string jsrq,string bazt,string keyword, string sfzh,string cykb)
        {
            var list = _MainRecordDmnService.PatMainList(pagination, this.OrganizeId,ksrq, jsrq,bazt, null,keyword, sfzh,cykb);
            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        public ActionResult GetEMRPatGridList(Pagination pagination,string keyword,string ksrq,string jsrq,string cyts,string blzt)
        {
            var cybz = Convert.ToInt32(EnumZYBZ.Ycy);
            var list = _EMRDmnService.GetPatList(pagination, keyword, null, cyts,blzt, this.UserIdentity.rygh, this.OrganizeId, cybz);
            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        public ActionResult GetFormJson(string keyValue,string zyh)
        {
            BasyVO entity = new BasyVO();
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                entity = _MainRecordDmnService.GetMainRecord(OrganizeId, keyValue);
            }
            else if(!string.IsNullOrWhiteSpace(zyh))
            {               
                entity = _MainRecordDmnService.GetMainRecordbybrjbxx(OrganizeId, zyh);
            }
            
            return Content(entity.ToJson());
        }

        public ActionResult GetFormJsonFee(string keyValue, string zyh)
        {
            BasyVO entity = new BasyVO();
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                var ety = _MrbasyRepo.FindEntity(p => p.ZYH == zyh && p.OrganizeId == OrganizeId && p.zt == "1");
                if (ety != null)
                {
                    keyValue = ety.Id;
                    entity = _MainRecordDmnService.GetMainRecord(OrganizeId, keyValue);
                }
                else
                {
                    entity = _MainRecordDmnService.GetMainRecordbybrjbxx(OrganizeId, zyh);
                }
            }
            else {
                entity = _MainRecordDmnService.GetMainRecord(OrganizeId, keyValue);
            }

           // var data = _MainRecordDmnService.GetMainRecordZyFeeDetail(entity, OrganizeId, zyh);
            var data = _MainRecordDmnService.GetMainRecordZyFeeDetail(entity, OrganizeId, zyh);
            return Content(data.ToJson());
        }

        public ActionResult GetZdGridList(string bah,string zyh)
        {
            var list = _MainRecordDmnService.GetDiagLsit(OrganizeId,bah,zyh);
            if (list.Count == 0 && !string.IsNullOrWhiteSpace(bah))
            {
                var exists = _MrbasyzdRepo.FindEntity(p => p.OrganizeId == OrganizeId && p.BAH == bah && p.zt == "1");
                if (exists == null)
                {
                    list = _MainRecordDmnService.GetPatHisZDInfo(bah,zyh, OrganizeId,2);
                }
            }
            var data = new
            {
                rows = list,
                page = 1,
                records = 1,
                total = 1
            };
            return Content(data.ToJson());
        }

        public ActionResult GetOpGridList(string bah, string zyh)
        {
            var list = _MainRecordDmnService.GetOPLsit(OrganizeId, bah, zyh);
            if(list.Count==0 && !string.IsNullOrWhiteSpace(bah))
            {
                var exists = _MrbasyRepo.FindEntity(p => p.OrganizeId == OrganizeId && p.BAH == bah && p.zt == "1");
                if (exists == null)
                {
                    list = _MainRecordDmnService.GetPatHisOperInfo(bah,zyh, OrganizeId);
                }
            }
            var data = new
            {
                rows = list,
                page = 1,
                records = 1,
                total = 1
            };
            return Content(data.ToJson());
        }


        public ActionResult SubmitZdList(List<BasyZdDto> dto,string zyh,string Code)
        {
            var data = "";
            if (dto!=null && dto.Count>0)
            {
                //结算病人信息
                var jsbrxx = _MainRecordDmnService.SettlementQuery(zyh, this.OrganizeId);
                //是否同步动态参数信息
                var dtcsxx = _MainRecordDmnService.DiagnosticSave(Code, this.OrganizeId);

                //判断是否结算病人
                if (jsbrxx.Count > 0)
                {
                    //参数修改时间为空时取创建时间
                    var uptime = dtcsxx[0].LastModifyTime.ToString() == null || dtcsxx[0].LastModifyTime.ToString() == "" ? dtcsxx[0].CreateTime : dtcsxx[0].LastModifyTime;
                    //判断结算时间大于参数的修改或创建时间
                    if ((jsbrxx[0].CreateTime > uptime) && (dtcsxx[0].Value == "OFF" || dtcsxx[0].Value == "off"))
                    {
                        data = "已结算病人不能添加诊断信息";
                    }
                    //判断结算时间小于参数的修改或创建时间
                    else if ((jsbrxx[0].CreateTime < uptime) && (dtcsxx[0].Value == "ON" || dtcsxx[0].Value == "on"))
                    {
                        //保存诊断
                        _MainRecordDmnService.ZdListSubmit(dto, OrganizeId);
                        data = "诊断信息保存成功";
                    }
                    else
                    {
                        //return Success("不能添加诊断信息");
                        data = "不能添加诊断信息";
                    }
                }
                //_MainRecordDmnService.ZdListSubmit(dto, OrganizeId);
            }
            data = "{\"data\":\"" + data + "\"}";
            return Content(data.ToJson().ToString());
        }

        public ActionResult SubmitOpList(List<BasyOpDto> dto)
        {
            if(dto!=null&&dto.Count>0)
            {
                _MainRecordDmnService.OpListSubmit(dto, OrganizeId);
            }
            return Success("手术保存成功");
        }

        public ActionResult SubmitPatBasic(BasyVO dto,BasyRelVO reldto, string keyValue) {
            if(!string.IsNullOrWhiteSpace(keyValue))
            {
                if(!string.IsNullOrWhiteSpace(dto.Id) && dto.Id!=keyValue)
                {
                    return Error("病案信息异常，请刷新重试");
                }
                else
                {
                    dto.Id = keyValue;
                }
            }
            _MainRecordDmnService.PatBasicSubmit(dto,reldto, OrganizeId,this.UserIdentity.rygh);
            return Success();
        }

        public ActionResult SubmitMR(string keyValue, List<BasyOpDto> opdto, List<BasyZdDto> zddto,string zyh,string Code)
        {
            //_MainRecordDmnService.PatBasicSubmit(dto, reldto, OrganizeId, this.UserIdentity.rygh);
            if(zddto!=null&&zddto.Count>0)
            {
                //_MainRecordDmnService.ZdListSubmit(zddto, OrganizeId);
                //结算病人信息
                var jsbrxx = _MainRecordDmnService.SettlementQuery(zyh, this.OrganizeId);
                //是否同步动态参数信息
                var dtcsxx = _MainRecordDmnService.DiagnosticSave(Code, this.OrganizeId);

                //判断是否结算病人
                if (jsbrxx.Count > 0)
                {
                    //参数修改时间为空时取创建时间
                    var uptime = dtcsxx[0].LastModifyTime.ToString() == null || dtcsxx[0].LastModifyTime.ToString() == "" ? dtcsxx[0].CreateTime : dtcsxx[0].LastModifyTime;
                    //判断结算时间大于参数的修改或创建时间
                    if ((jsbrxx[0].CreateTime > uptime) && (dtcsxx[0].Value == "OFF" || dtcsxx[0].Value == "off"))
                    {
                        //data = "已结算病人不能添加诊断信息";
                    }
                    //判断结算时间小于参数的修改或创建时间
                    else if ((jsbrxx[0].CreateTime < uptime) && (dtcsxx[0].Value == "ON" || dtcsxx[0].Value == "on"))
                    {
                        //保存诊断
                        _MainRecordDmnService.ZdListSubmit(zddto, OrganizeId);
                        //data = "诊断信息保存成功";
                    }
                    else
                    {
                        //return Success("不能添加诊断信息");
                        //data = "不能添加诊断信息";
                    }
                }
            }
            if (opdto != null && opdto.Count > 0)
            {
                _MainRecordDmnService.OpListSubmit(opdto, OrganizeId);
            }
            return Success();
        }


        //------------------归档按钮
        public ActionResult guidang(string dataId,string ZYH,string XM) {
           var data= _MrbasyRepo.Updatebazt(dataId,ZYH,XM);
            return Content(data.ToJson());
        }

        public ActionResult CXguidang(string dataId, string ZYH, string XM) {
            var LastModifierCode = UserIdentity.UserName;
            var LastModifyTime = DateTime.Now;
            var data1 = _MrbasyRepo.CXUpdatebazt(dataId, ZYH, XM, this.OrganizeId, LastModifierCode, LastModifyTime);
            return Content(data1.ToJson());
        }
        public ActionResult CXGDZT(string dataId, string ZYH, string XM) {
            var LastModifierCode = UserIdentity.UserName;
            var LastModifyTime = DateTime.Now;
            var data1 = _MrbasyRepo.CXUpdatebazt(dataId, ZYH, XM, this.OrganizeId, LastModifierCode, LastModifyTime);
            return Content(data1.ToJson());
        }


        private void EMRBasyPrintReportCom()
        {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.topOrgId = Constants.TopOrganizeId;
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.IsHospAdministrator = this.UserIdentity.IsHospAdministrator.ToString().ToLower();  //是否是医院管理员
            ViewBag.CurUserCode = this.UserIdentity.UserCode;
            ViewBag.curUsergh = UserIdentity.rygh;
        }


        public ActionResult AjaxValueIndex() {
          
            var str = "{\"orgId\":\"" + this.OrganizeId + "\",\"ReportServerHOST\":\""+ ConfigurationHelper.GetAppConfigValue("ReportServer.HOST") + "\"}";
            return Content(str.ToString());
        }


        public ActionResult SettlementQuery(string zyh) {
           var data=_MainRecordDmnService.SettlementQuery(zyh, this.OrganizeId);
            return Content(data.ToJson());
        }
        public ActionResult MedicalRecordExportQuery(string kssj,string jssj)
        {
            var data = _MainRecordDmnService.MedicalRecordExportQuery(kssj, jssj, this.OrganizeId);
            var data2 = _MainRecordDmnService.MedicalRecordExportFYQuery(kssj, jssj, this.OrganizeId);
            return Success(data + data2);
        }
        public ActionResult DiagnosticSave(string Code)
        {
            var data = _MainRecordDmnService.DiagnosticSave(Code, this.OrganizeId);
            return Content(data.ToJson());
        }
    }
}