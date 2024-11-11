using FrameworkBase.MultiOrg.Web;
using Newtonsoft.Json;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.EMR.Domain.DTO.InputDto;
using Newtouch.EMR.Domain.DTO.OutputDto.MRUpload;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Domain.ValueObjects.MRHomePage;
using Newtouch.EMR.Infrastructure.EnumMR;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.EMR.Web.Areas.MRHomePage.Controllers
{
    public class MRHomePageController : OrgControllerBase
    {
        private readonly IMRHomePageDmnService _MainRecordDmnService;
        private readonly IMrbasyzdRepo _MrbasyzdRepo;
        private readonly IMrbasyRepo _MrbasyRepo;
        private readonly IZybrjbxxDmnService _zybrjbxxDmnService;
        private readonly IZymeddocsrelationRepo _zymeddocsrelationRep;
        private readonly Ibl_bllxRepo _BllxRepo;
        private readonly IYBInterfaceDmnService _yBInterfaceDmnService;
        private readonly ICommonDmnService _CommonDmnService;
        private readonly string YBOrgCode = ConfigurationHelper.GetAppConfigValue("YBOrgCode");


        private static string UploadURL = ConfigurationHelper.GetAppConfigValue("MRUploadURL");

        //// GET: MRHomePage/MainRecord
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Main()
        {
            var url = Request.Url.ToString();
            var cs = HttpUtility.ParseQueryString(url);
            ViewBag.justshow = cs["justshow"];
            ReportingServiceCom();
            return View();
        }

        //public ActionResult MainPreView()
        //{
        //    return View();
        //}
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
		public ActionResult MainFYInfo()
		{
			return View();
		}
		public ActionResult GetGridList(Pagination pagination, string ksrq, string jsrq, string bazt, string keyword, string cyts)
        {
            var list = _MainRecordDmnService.PatMainList(pagination, this.OrganizeId, ksrq, jsrq, bazt, null, keyword, cyts);
            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        public ActionResult GetEMRPatGridList(Pagination pagination, string keyword, string ksrq, string jsrq, string cyts, string blzt)
        {
            var cybz = Convert.ToInt32(EnumZYBZ.Ycy);
            var list = _zybrjbxxDmnService.GetPatList(pagination, keyword, null, cyts, blzt, this.UserIdentity.rygh, this.OrganizeId, cybz,"MRMS");
            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        public ActionResult GetFormJson(string keyValue, string zyh)
        {
            BasyVO entity = new BasyVO();    
             if (!string.IsNullOrWhiteSpace(keyValue))
            {
                entity = _MainRecordDmnService.GetMainRecord(OrganizeId, keyValue);
            }
            else if (!string.IsNullOrWhiteSpace(zyh))
            {
                entity = _MainRecordDmnService.GetMainRecordbybrjbxx(OrganizeId, zyh);
            }

            return Content(entity.ToJson());
        }

        public ActionResult GetFormJsonFee(string keyValue, string zyh)
        {
            BasyVO vo = new BasyVO();
            var entity = _MainRecordDmnService.GetMainRecordZyFeeDetail(vo, OrganizeId, zyh);
            return Content(entity.ToJson());
        }

        public ActionResult GetZdGridList(string bah, string zyh)
        {
            var list = _MainRecordDmnService.GetDiagLsit(OrganizeId, bah, zyh);
            if (list.Count == 0 && !string.IsNullOrWhiteSpace(bah))
            {
                var exists = _MrbasyzdRepo.FindEntity(p => p.OrganizeId == OrganizeId && p.BAH == bah && p.zt == "1");
                if (exists == null)
                {
                    list = _MainRecordDmnService.GetPatHisZDInfo(bah, zyh, OrganizeId, 2);
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
            if (list.Count == 0 && !string.IsNullOrWhiteSpace(bah))
            {
                var exists = _MrbasyRepo.FindEntity(p => p.OrganizeId == OrganizeId && p.BAH == bah && p.zt == "1");
                if (exists == null)
                {
                    list = _MainRecordDmnService.GetPatHisOperInfo(bah, zyh, OrganizeId);
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
            try {
                if (dto != null && dto.Count > 0)
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
                            //return Success("已结算病人不能添加诊断信息");
                            data = "已结算病人不能添加诊断信息";
                        }
                        //判断结算时间小于参数的修改或创建时间
                        else if ((jsbrxx[0].CreateTime < uptime) && (dtcsxx[0].Value == "ON" || dtcsxx[0].Value == "on"))
                        {
                            //保存诊断
                            _MainRecordDmnService.ZdListSubmit(dto, OrganizeId);
                            data = "诊断信息保存成功";
                        }
                        else {
                            //return Success("不能添加诊断信息");
                            data = "不能添加诊断信息";
                        }
                    }
                    else {
                        //没有结算并且参数开启时同步和保存诊断
                        if (dtcsxx[0].Value == "ON" || dtcsxx[0].Value == "on")
                        {
                            //保存诊断
                            _MainRecordDmnService.ZdListSubmit(dto, OrganizeId);
                            //同步诊断信息
                            _MainRecordDmnService.SynchronizationZD(zyh, this.OrganizeId);
                            data = "诊断信息保存成功";
                        }
                        else if (dtcsxx[0].Value == "OFF" || dtcsxx[0].Value == "off")
                        {
                            //保存诊断
                            _MainRecordDmnService.ZdListSubmit(dto, OrganizeId);
                            data = "诊断信息保存成功";
                        }
                    }


                    //保存诊断
                    //_MainRecordDmnService.ZdListSubmit(dto, OrganizeId);
                    //同步诊断信息
                    //_MainRecordDmnService.SynchronizationZD(zyh, this.OrganizeId);

                }
            }
            catch (Exception e) {
                data = e.Message;
            }
            data = "{\"data\":\""+ data + "\"}";
            return Content(data.ToJson().ToString());
        }

        public ActionResult SubmitOpList(List<BasyOpDto> dto)
        {
            if (dto != null && dto.Count > 0)
            {
                _MainRecordDmnService.OpListSubmit(dto, OrganizeId);
            }
            return Success("手术保存成功");
        }

        public ActionResult SubmitPatBasic(BasyVO dto, BasyRelVO reldto, string keyValue,string mbbh)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                if (!string.IsNullOrWhiteSpace(dto.Id) && dto.Id != keyValue)
                {
                    return Error("病案信息异常，请刷新重试");
                }
                else
                {
                    dto.Id = keyValue;
                }
            }
            _MainRecordDmnService.PatBasicSubmit(dto, reldto,mbbh, OrganizeId, this.UserIdentity.rygh,this.UserIdentity.UserName);
            return Success();
        }

        public ActionResult SubmitMR(string keyValue, List<BasyOpDto> opdto, List<BasyZdDto> zddto,string zyh,string Code)
        {
            try
            {
                //_MainRecordDmnService.PatBasicSubmit(dto, reldto, OrganizeId, this.UserIdentity.rygh);
                if (zddto != null && zddto.Count > 0)
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
                            //return Success("已结算病人不能添加诊断信息");
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
                    else
                    {
                        //没有结算并且参数开启时同步和保存诊断
                        if (dtcsxx[0].Value == "ON" || dtcsxx[0].Value == "on")
                        {
                            //保存诊断
                            _MainRecordDmnService.ZdListSubmit(zddto, OrganizeId);
                            //同步诊断信息
                            _MainRecordDmnService.SynchronizationZD(zyh, this.OrganizeId);
                            //data = "诊断信息保存成功";
                        }
                        else if (dtcsxx[0].Value == "OFF" || dtcsxx[0].Value == "off")
                        {
                            //保存诊断
                            _MainRecordDmnService.ZdListSubmit(zddto, OrganizeId);
                            //data = "诊断信息保存成功";
                        }
                    }
                }
                if (opdto != null && opdto.Count > 0)
                {
                    _MainRecordDmnService.OpListSubmit(opdto, OrganizeId);
                }
                return Success();
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }

        }
        public ActionResult HomePageUploadJS(string zyh, string keyValue,string jydm)
        {
            QHDSmartCheckInput data = new QHDSmartCheckInput();
            var ety = _MainRecordDmnService.GetHomePageforYB(OrganizeId, zyh).FirstOrDefault();
            if (ety == null)
            {
                return Error("请填写完整病历保存后上传");

            }
            string msg = DocumentAuditYB<YB_7600>(ety);
            if (!string.IsNullOrWhiteSpace(msg))
            {
                return Error(msg);
            }
            else {
                QHDSmartCheckInput rootinput = new QHDSmartCheckInput();
                if (jydm != "7610")
                {
                    YBRequestBase<YB_7600> YBJKDATA = new YBRequestBase<YB_7600>();
                    YBJKDATA.REQUESTDATA = new YBRequestSingle<YB_7600>
                    {
                        MSGNO = "7600",
                        AKB020 = YBOrgCode,
                        OPERID = this.UserIdentity.rygh,
                        OPERNAME = UserIdentity.UserName,
                        PERIODNO = "",
                        MSGID = YBOrgCode + DateTime.Now.ToString("yyyymmddhh24miss"),
                        GRANTID = "",
                        INPUT = ety
                    };
                    rootinput.jsondata = JsonConvert.SerializeObject(YBJKDATA);
                    rootinput.jydm = "7600";
                }
                else
                {
                    var zdinfo = _yBInterfaceDmnService.GetBasyZdInfo(OrganizeId, zyh);
                    if (zdinfo != null && zdinfo.Count > 0) {
                        YBRequestBase<YB_7610> YBJKDATA_ZD = new YBRequestBase<YB_7610>();
                        YBJKDATA_ZD.REQUESTDATA = new YBRequestRows<YB_7610>
                        {
                            MSGNO = "7610",
                            AKB020 = YBOrgCode,
                            OPERID = this.UserIdentity.rygh,
                            OPERNAME = UserIdentity.UserName,
                            PERIODNO = "",
                            MSGID = YBOrgCode + DateTime.Now.ToString("yyyyMMddHHmmss"),
                            GRANTID = "",
                            INPUT = zdinfo.ToList()
                        };
                        rootinput.jsondata = JsonConvert.SerializeObject(YBJKDATA_ZD);
                        rootinput.jydm = "7610";
                    }
                    
                }
                data = rootinput;                
            }
            return Content(data.ToJson());
        }
        public ActionResult HomePageUpload(string zyh,string keyValue)
        {
            var ety = _MainRecordDmnService.GetHomePageforYB(OrganizeId, zyh).FirstOrDefault();
            if (ety == null)
            {
                return Error("请填写完整病历保存后上传");

            }
            string msg = DocumentAuditYB<YB_7600>(ety);
            if (!string.IsNullOrWhiteSpace(msg))
            {
                return Error(msg);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(UploadURL))
                {
                    QHDSmartCheckInput rootinput = new QHDSmartCheckInput();
                    YBRequestBase<YB_7600> YBJKDATA = new YBRequestBase<YB_7600>();
                    YBJKDATA.REQUESTDATA = new YBRequestSingle<YB_7600>
                    {
                        MSGNO = "7600",
                        AKB020 = YBOrgCode,
                        OPERID = this.UserIdentity.rygh,
                        OPERNAME = UserIdentity.UserName,
                        PERIODNO = "",
                        MSGID = YBOrgCode + DateTime.Now.ToString("yyyymmddhh24miss"),
                        GRANTID = "",
                        INPUT = ety
                    };
                    rootinput.jsondata = JsonConvert.SerializeObject(YBJKDATA);
                    rootinput.jydm = "7600";
                    var respstr = Tools.Net.HttpClientHelper.HttpPostStringAndRead<string>(UploadURL + "api/QHDSmartCheck/MedicalRecordUpload", JsonConvert.SerializeObject(rootinput), contentType: HttpClientHelper.EnumContentType.json);
                    var resp = JsonConvert.DeserializeObject<ResponseBase>(respstr);
                    if (resp.code == "1")
                    {
                        string bllx = _BllxRepo.FindEntity(p => p.OrganizeId == OrganizeId && p.zt == "1" && p.bllxcode == "basy").bllx;
                        var rel = _zymeddocsrelationRep.FindEntity(p => p.zyh == zyh && p.zt == "1" && p.OrganizeId == OrganizeId && p.bllx == bllx);
                        if (rel != null)
                        {
                            rel.YbUploadFlag = "1";
                            _zymeddocsrelationRep.Update(rel);
                        }
                        var zdinfo = _yBInterfaceDmnService.GetBasyZdInfo(OrganizeId, zyh);
                        if (zdinfo != null && zdinfo.Count > 0)
                        {
                            YBRequestBase<YB_7610> YBJKDATA_ZD = new YBRequestBase<YB_7610>();
                            YBJKDATA_ZD.REQUESTDATA = new YBRequestRows<YB_7610>
                            {
                                MSGNO = "7610",
                                AKB020 = YBOrgCode,
                                OPERID = this.UserIdentity.rygh,
                                OPERNAME = UserIdentity.UserName,
                                PERIODNO = "",
                                MSGID = YBOrgCode + DateTime.Now.ToString("yyyyMMddHHmmss"),
                                GRANTID = "",
                                INPUT = zdinfo.ToList()
                            };
                            rootinput.jsondata = JsonConvert.SerializeObject(YBJKDATA_ZD);
                            rootinput.jydm = "7610";
                            var respstrzd = Tools.Net.HttpClientHelper.HttpPostStringAndRead<string>(UploadURL + "api/QHDSmartCheck/MedicalRecordUpload", JsonConvert.SerializeObject(rootinput), contentType: HttpClientHelper.EnumContentType.json);
                            var respzd = JsonConvert.DeserializeObject<ResponseBase>(respstr);
                            if (respzd.code == "1")
                            {
                                return Success("上传成功");
                            }
                            else
                            {
                                return Error(resp.message);
                            }
                        }
                        return Success("上传成功"); 
                    }
                    else
                    {
                        msg = resp.message;
                    }

                    return Error(msg);
                }
                else
                {
                    return Error("无法获取接口地址") ;
                }
            }
        }
        public ActionResult UpdateUploadStu(string zyh)
        {
            var bllxety = _CommonDmnService.GetBllxList(OrganizeId, "basy").FirstOrDefault();
            var rel = _zymeddocsrelationRep.FindEntity(p => p.zyh == zyh && p.zt == "1" && p.OrganizeId == OrganizeId && p.bllx == bllxety.Code);
            if (rel != null)
            {
                rel.YbUploadFlag = "1";
                _zymeddocsrelationRep.Update(rel);
            }
            return Success();
        }
        #region 医保相关
        public string DocumentAuditYB<T>(T entity)
        {
            string errmsg = "";
            PropertyInfo[] properties = entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo p in properties)
            {
                var required = (RequiredAttribute[])p.GetCustomAttributes(typeof(RequiredAttribute), false);
                var desc = (DescriptionAttribute[])p.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (required != null && required.Length > 0 && desc != null && desc.Length > 0)
                {
                    var thisval = p.GetValue(entity, null);
                    if (thisval == null || string.IsNullOrWhiteSpace(thisval.ToString()))
                    {
                        if (string.IsNullOrWhiteSpace(errmsg))
                        {
                            errmsg = "上传医保关键数据！";
                        }
                        errmsg += "[" + desc[0].Description + "] 不可为空;";
                    }
                }
            }
            return errmsg;
        }

        #endregion

        #region private methods

        /// <summary>
        /// 
        /// </summary>
        private void ReportingServiceCom()
        {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.IsHospAdministrator = this.UserIdentity.IsHospAdministrator.ToString().ToLower();  //是否是医院管理员
            ViewBag.CurUserCode = this.UserIdentity.UserCode;
            ViewBag.curUsergh = UserIdentity.rygh;
        }

        #endregion

        public ActionResult SettlementQuery(string zyh)
        {
            var data = _MainRecordDmnService.SettlementQuery(zyh, this.OrganizeId);
            return Content(data.ToJson());
        }

        public ActionResult DiagnosticSave(string Code)
        {
            var data = _MainRecordDmnService.DiagnosticSave(Code, this.OrganizeId);
            return Content(data.ToJson());
        }
    }
}