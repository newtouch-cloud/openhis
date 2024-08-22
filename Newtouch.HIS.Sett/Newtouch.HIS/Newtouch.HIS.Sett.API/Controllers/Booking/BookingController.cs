using Autofac;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Common.Operator;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.API;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Sett.Request;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using FrameworkBase.MultiOrg.Domain.DTO;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using Newtonsoft.Json;
using Newtouch.Core.Common.Extensions;
using Newtouch.Core.Redis;
using Newtouch.HIS.API.Common.Filter;
using Newtouch.HIS.API.Common.Models;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.API;

namespace Newtouch.HIS.Sett.API.Controllers
{
    [RoutePrefix("api/Booking")]
    public class BookingController : ApiControllerBase<BookingController>
    {
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly IBookingDmnService _BookingDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly ISysCardRepo _SysCardRepo;
        private readonly IPatientBasicInfoDmnService _PatientBasicInfoDmnService;
        private readonly ISysPatientNatureRepo _SysPatientNatureRepo;
        private readonly ISysPatientBasicInfoRepo _SysPatientBasicInfoRepo;
        private readonly IBaseDataDmnService _baseDataDmnService;
        private readonly IMzghbookRepo _MzghbookRepo;

        private string user;

        public BookingController(IComponentContext com)
            : base(com)
        {
        }

        private string GetAuth(string orgId)
        {
            var identity = new UserIdentity
            {
                AppId = "API",
                TokenType = "",
                UserId = "APIAdmin",
                Account = "APIAdmin",
                OrganizeId = orgId,
                TopOrganizeId = orgId,
            };
            var key = Guid.NewGuid().ToString();
            RedisHelper.StringSet(key, JsonConvert.SerializeObject((object)identity), new TimeSpan(0, 20, 0));
            HttpContext.Current.Items[(object)"API_UserIdentity_Account"] = (object)identity.Account;
            return identity.Account;
        }


        [HttpPost]
        [IgnoreTokenDecrypt]
        [Route("BookingESB")]
        public HttpResponseMessage BookingEsb(BookingReqBase dto)
        {
            HttpResponseMessage httpresp=new HttpResponseMessage();
            DefaultResponse resp = new DefaultResponse();
            if (dto != null)
            {
                //参数规范性校验
                resp = dto.FormatCheck(dto.methodcode);
                if (resp.code == ResponseResultCode.SUCCESS)
                {
                    //身份设置
                    dto.user = GetAuth(dto.OrganizeId);
                    switch (dto.methodcode)
                    {
                        case "Y002"://系统科室列表
                            //para.ks = dto.paradata.ContainsKey("ks") == true ? dto.paradata["ks"] : null; //dto.FormatMedRequest(dto.paradata, "ksdm");
                            //para.ksmc = dto.paradata.ContainsKey("ksmc") == true ? dto.paradata["ksmc"] : null; //dto.FormatMedRequest(dto.paradata, "ksmc");
                            //resp = SysDeptartment(para.ks, resp);
                            httpresp = SysDepartmentInfo(dto);
                            break;  
                        case "Y003": //医生人员信息
                            httpresp = SysStaffInfo(dto);
                            break;
                        case "Y004"://患者信息 optype=1 添加新患者 optype=0 患者查询
                            #region 患者查询与登记
                            if (dto.optype == "1")
                            {
                                if (dto.paradata!=null && dto.paradata.ContainsKey("zjh") 
                                    && dto.paradata.ContainsKey("xm") && dto.paradata.ContainsKey("xb")&& dto.paradata.ContainsKey("csrq"))
                                {
                                    httpresp = SysPatInfoSet(dto);
                                }
                                else
                                {
                                    resp.code = ResponseResultCode.FAIL;
                                    resp.msg = "关键参数不可为空";
                                    httpresp = CreateResponse(resp);
                                }
                            }
                            else if(dto.optype=="3") // 2020-2-19 与his登记同步 不强制要求证件号非空
                            {
                                if (dto.paradata != null && dto.paradata.ContainsKey("xm") && dto.paradata.ContainsKey("xb") && dto.paradata.ContainsKey("csrq"))
                                {
                                    httpresp = SysPatInfoSetPub(dto);
                                }
                                else
                                {
                                    resp.code = ResponseResultCode.FAIL;
                                    resp.msg = "关键参数不可为空";
                                    httpresp = CreateResponse(resp);
                                }
                            }
                            else if (dto.paradata == null || !dto.paradata.ContainsKey("xm") || string.IsNullOrWhiteSpace(dto.paradata["xm"]))
                            {
                                resp.code = ResponseResultCode.FAIL;
                                resp.msg = "关键参数不可为空";
                                httpresp = CreateResponse(resp);
                            }
                            else if ((dto.paradata.ContainsKey("zjh") && !string.IsNullOrWhiteSpace(dto.paradata["zjh"])) ||
                                     (dto.paradata.ContainsKey("kh") && !string.IsNullOrWhiteSpace(dto.paradata["kh"])))
                            {
                                httpresp = SysPatInfo(dto);

                            }
                            else
                            {
                                resp.code = ResponseResultCode.FAIL;
                                resp.msg = "关键参数不可为空";
                                httpresp = CreateResponse(resp);
                            }
                            #endregion
                            break;
                        case "Y005"://医院药品信息
                            httpresp = SysDrugInfo(dto);
                            break;
                        case "Y006"://医疗项目信息
                            httpresp = SysMedicalInfo(dto);
                            break;
                        case "Y007"://门诊疾病信息
                            httpresp = SysDiseaseInfo(dto);
                            break;
                        case "Y008"://挂号项目类别
                            httpresp = SysRegType(dto);
                            break;
                        case "Y009"://门诊排班
                            httpresp = GetMzpbSchedule(dto);
                            break;
                        case "Y010": //门诊排班详细号源信息
                            httpresp = GetMzpbScheduleDetail(dto);
                            break;
                        case "Y011":
                            httpresp = GetRecipeInfo(dto);
                            break;
                        case "Y012":
                            httpresp = GetRecipeDrugInfo(dto);
                            break;
                        case "Y021"://预约记录
                            httpresp = OutBookRecord(dto);
                            break;
                        case "Y022"://预约
                            #region 门诊预约
                            if (dto.paradata != null && dto.paradata.ContainsKey("ghxz")&& dto.paradata.ContainsKey("scheduId")&& dto.paradata.ContainsKey("kh"))
                            {
                                httpresp = OutBookApply(dto);
                            }
                            else
                            {
                                resp.code = ResponseResultCode.FAIL;
                                resp.msg = "关键信息不可为空";
                                httpresp = CreateResponse(resp);
                            }
                            #endregion
                            break;
                        case "Y023"://取消预约
                            if (dto.paradata != null && dto.paradata.ContainsKey("bookId") && dto.paradata.ContainsKey("kh") &&
                                dto.paradata.ContainsKey("lxdh"))
                            {
                                httpresp = OutBookCancel(dto);
                            }
                            else
                            {
                                resp.code = ResponseResultCode.FAIL;
                                resp.msg = "关键信息不可为空";
                                httpresp = CreateResponse(resp);
                            }
                            break;
                        case "Y024"://挂号支付
                            if (dto.paradata != null && dto.paradata.ContainsKey("bookId") && dto.paradata.ContainsKey("kh") &&
                                dto.paradata.ContainsKey("PayFee") && dto.paradata.ContainsKey("PayLsh"))
                            {
                                httpresp = OutBookPay(dto);
                            }
                            else
                            {
                                resp.code = ResponseResultCode.FAIL;
                                resp.msg = "关键信息不可为空";
                                httpresp = CreateResponse(resp);
                            }
                            break;
                        case "Y025"://预约退号
                            if (dto.paradata != null && dto.paradata.ContainsKey("mzh") &&
                                dto.paradata.ContainsKey("kh"))
                            {
                                httpresp = OutBookRegistCancel(dto);
                            }
                            else
                            {
                                resp.code = ResponseResultCode.FAIL;
                                resp.msg = "关键信息不可为空";
                                httpresp = CreateResponse(resp);
                            }
                            break;
                        case "Y026"://无预约挂号
                            if (dto.paradata != null && dto.paradata.ContainsKey("ks") && dto.paradata.ContainsKey("kh") 
                                && dto.paradata.ContainsKey("ghxz"))
                            {
                                httpresp = CommonRegist(dto);
                            }
                            else
                            {
                                resp.code = ResponseResultCode.FAIL;
                                resp.msg = "关键信息不可为空";
                                httpresp = CreateResponse(resp);
                            }
                            break;
                        case "Y030"://门诊收费信息查询
                            if (dto.paradata != null && dto.paradata.ContainsKey("mzh") && dto.paradata.ContainsKey("kh"))
                            {
                                httpresp = OutpatChargeInfo(dto);
                            }
                            else
                            {
                                resp.code = ResponseResultCode.FAIL;
                                resp.msg = "关键信息不可为空";
                                httpresp = CreateResponse(resp);
                            }
                            break;
                    }
                }
                else
                {
                    httpresp = CreateResponse(resp);
                }
            }
            else
            {
                resp.code = ResponseResultCode.FAIL;
                resp.msg = "请校验参数格式是否正确";
                httpresp = CreateResponse(resp);
            }

            return httpresp;
        }


        private DefaultResponse SysDeptartment(string ksdm, DefaultResponse resp)
        {
            return resp;
        }

        /// <summary>
        /// Y002 科室信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private HttpResponseMessage SysDepartmentInfo(BookingReqBase dto) {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            if (dto.paradata != null)
            {
                para.ks = dto.paradata.ContainsKey("ks") == true ? dto.paradata["ks"] : null;
                para.ksmc = dto.paradata.ContainsKey("ksmc") == true ? dto.paradata["ksmc"] : null;
            }
            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                var list = _BookingDmnService.GetDepartmentInfo(para.OrgId,para.ks, para.ksmc);
                if (list.Count > 0)
                {
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未找到相关记录";
                }
            };

            var response = base.CommonExecute(ac, para);
            return base.CreateResponse(response);
        }

        /// <summary>
        /// Y003 医生人员信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private HttpResponseMessage SysStaffInfo(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            if (dto.paradata != null)
            {
                para.ys = dto.paradata.ContainsKey("ys") == true ? dto.paradata["ys"] : null;
                para.xm = dto.paradata.ContainsKey("xm") == true ? dto.paradata["xm"] : null;
            }
            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                var staffList = _BookingDmnService.GetStaffInfo(para.OrgId, para.ys, para.xm);
                var list = new List<SysVSStaffVO>();
                foreach (var obj in staffList)
                {
                    SysVSStaffVO staff = new SysVSStaffVO();
                    if (obj.Gender == true)
                    {
                        string Gender = ((EnumSex)(Convert.ToInt32(obj.Gender))).GetDescription();
                        //properties
                        staff.departmentCode = obj.departmentCode;
                        staff.departmentName = obj.departmentName;
                        staff.DutyId = obj.DutyId;
                        staff.dutyName = obj.dutyName;
                        staff.Gender = Gender;
                        staff.gh = obj.gh;
                        staff.organizeId = obj.organizeId;
                        staff.staffName = obj.staffName;
                        list.Add(staff);
                    }
                }
                if (list.Count > 0)
                {
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未找到相关记录";
                }
            };
             
            var response = base.CommonExecute(ac, para);
            return base.CreateResponse(response);
        }


        /// <summary>
        /// Y004 获取基本患者信息
        /// </summary>
        /// <param name="xm"></param>
        /// <param name="kh"></param>
        /// <param name="zjh"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        private HttpResponseMessage SysPatInfo(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            para.zjh = dto.paradata.ContainsKey("zjh") == true ? dto.paradata["zjh"] : null;
            para.kh = dto.paradata.ContainsKey("kh") == true ? dto.paradata["kh"] : null;
            para.xm = dto.paradata.ContainsKey("xm") == true ? dto.paradata["xm"] : null;
            para.dh = dto.paradata.ContainsKey("dh") == true ? dto.paradata["dh"] : null;
            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) => {
                var list = _BookingDmnService.GetPatInfo(para.OrgId, para.kh, para.zjh, para.xm);
                if (list != null && list.Count > 0)
                {
                    resp.code = ResponseResultCode.SUCCESS;
                    resp.data = list;
                    resp.msg = "查询成功";
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "患者信息不存在";
                }
            };
             
            var response = base.CommonExecute(ac, para);
            return base.CreateResponse(response);
        }
        /// <summary>
        /// Y004 添加新患者信息
        /// </summary>
        /// <param name="xm"></param>
        /// <param name="kh"></param>
        /// <param name="zjh"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        private HttpResponseMessage SysPatInfoSet(BookingReqBase dto)
        {
            BookingRequestDto bookdto=new BookingRequestDto();
            bookdto.OrgId = dto.OrganizeId;
            bookdto.Timestamp = dto.Timestamp;
            if (dto.paradata != null)
            {
                //bookdto.patid = dto.paradata.ContainsKey("patid") == true ? dto.paradata["patid"] : null;
                bookdto.zjh = dto.paradata.ContainsKey("zjh") == true ? dto.paradata["zjh"] : null;
                bookdto.kh = dto.paradata.ContainsKey("kh") == true ? dto.paradata["kh"] : null;
                bookdto.xm = dto.paradata.ContainsKey("xm") == true ? dto.paradata["xm"] : null;
                bookdto.dh = dto.paradata.ContainsKey("dh") == true ? dto.paradata["dh"] : null;
                if (dto.paradata.ContainsKey("csrq"))
                {
                    bookdto.csrq = Convert.ToDateTime(dto.paradata["csrq"]);
                }
                bookdto.xb = dto.paradata.ContainsKey("xb") == true ? dto.paradata["xb"] : null;
            }
            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                if (CommmHelper.CheckIDCard(bookdto.zjh))
                {
                    var validatepat = _SysPatientBasicInfoRepo.IQueryable().Where(p =>
                        p.zjlx == ((int) EnumZJLX.sfz).ToString() && p.zjh == bookdto.zjh && p.OrganizeId == bookdto.OrgId &&
                        p.zt == "1");
                    if (validatepat.Count() > 0)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.sub_code = "10001";
                        resp.msg = "证件信息已存在";
                    }
                    else
                    {
                        SysHosBasicInfoVO vo = new SysHosBasicInfoVO();
                        //var khpz = _sysConfigRepo.GetBoolValueByCode("xt_kh", OrgId, true).Value ? "ON" : "OFF";
                        //if (khpz == "ON")
                        //{
                        //    vo.kh = _SysCardRepo.GetCardSerialNo(OrgId);
                        //}
                        //var blhpz = _sysConfigRepo.GetBoolValueByCode("xt_blh", OrgId, false).Value ? "ON" : "OFF";
                        //if (blhpz == "ON")
                        //{
                        //    vo.blh = _SysPatientBasicInfoRepo.Getblh(OrgId);
                        //}
                        vo.kh = _SysCardRepo.GetCardSerialNo(bookdto.OrgId);
                        vo.blh = _SysPatientBasicInfoRepo.Getblh(bookdto.OrgId);
                        vo.xm = bookdto.xm;
                        vo.csny = bookdto.csrq.ToDateString();
                        vo.zjlx = ((int) EnumZJLX.sfz).ToString();
                        vo.zjh = bookdto.zjh;
                        vo.dh = bookdto.dh;
                        vo.xb = bookdto.xb;
                        vo.py = CommmHelper.StrConvertToPinyin(bookdto.xm, "1");
                        //vo.patfrom = dto.AppId;
                        
                        if (vo.xb == "1" || vo.xb == "2")
                        {
                            vo.brxz = _SysPatientNatureRepo.GetbxzcBySearch("自费", bookdto.OrgId)[0].brxz;
                            try
                            {
                                _PatientBasicInfoDmnService.SavePatBasicCardInfo(vo, bookdto.OrgId,"");
                                var list = _BookingDmnService.GetPatInfo(bookdto.OrgId, vo.kh, vo.zjh, vo.xm);
                                resp.data = list;
                                resp.code = ResponseResultCode.SUCCESS;
                                resp.msg = "患者信息添加成功";
                            }
                            catch (Exception ex)
                            {
                                resp.code = ResponseResultCode.ERROR;
                                resp.msg = ex.InnerException.InnerException.ToString();
                            }
                        }
                        else
                        {
                            resp.code = ResponseResultCode.FAIL;
                            resp.sub_code = "10003";
                            resp.msg = "参数字典不规范";
                        }
                    }

                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = "10002";
                    resp.msg = "证件格式不规范";
                }
            };
            var response = base.CommonExecute(ac, bookdto);
            return base.CreateResponse(response);
        }

        private HttpResponseMessage SysPatInfoSetPub(BookingReqBase dto)
        {
            BookingRequestDto bookdto = new BookingRequestDto();
            bookdto.OrgId = dto.OrganizeId;
            bookdto.Timestamp = dto.Timestamp;
            if (dto.paradata != null)
            {
                //bookdto.patid = dto.paradata.ContainsKey("patid") == true ? dto.paradata["patid"] : null;
                bookdto.zjh = dto.paradata.ContainsKey("zjh") == true ? dto.paradata["zjh"] : null;
                bookdto.kh = dto.paradata.ContainsKey("kh") == true ? dto.paradata["kh"] : null;
                bookdto.xm = dto.paradata.ContainsKey("xm") == true ? dto.paradata["xm"] : null;
                bookdto.dh = dto.paradata.ContainsKey("dh") == true ? dto.paradata["dh"] : null;
                if (dto.paradata.ContainsKey("csrq"))
                {
                    bookdto.csrq = Convert.ToDateTime(dto.paradata["csrq"]);
                }
                bookdto.xb = dto.paradata.ContainsKey("xb") == true ? dto.paradata["xb"] : null;
            }
            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                var validatepat = _SysPatientBasicInfoRepo.IQueryable().Where(p =>
                    p.xm == bookdto.xm && p.csny == bookdto.csrq && p.xb == bookdto.xb &&p.OrganizeId==bookdto.OrgId && p.zt == "1");
                if (validatepat.Count() > 0)
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = "10001";
                    resp.msg = "患者信息已存在";
                }
                else
                {
                    SysHosBasicInfoVO vo = new SysHosBasicInfoVO();
                    vo.kh = _SysCardRepo.GetCardSerialNo(bookdto.OrgId);
                    vo.blh = _SysPatientBasicInfoRepo.Getblh(bookdto.OrgId);
                    vo.xm = bookdto.xm;
                    vo.csny = bookdto.csrq.ToDateString();
                    vo.zjlx = ((int)EnumZJLX.sfz).ToString();
                    vo.zjh = bookdto.zjh;
                    vo.dh = bookdto.dh;
                    vo.xb = bookdto.xb;
                    vo.py = CommmHelper.StrConvertToPinyin(bookdto.xm, "1");
                    if (vo.xb == "1" || vo.xb == "2")
                    {
                        vo.brxz = _SysPatientNatureRepo.GetbxzcBySearch("自费", bookdto.OrgId)[0].brxz;
                        try
                        {
                            _PatientBasicInfoDmnService.SavePatBasicCardInfo(vo, bookdto.OrgId,"");
                            var list = _BookingDmnService.GetPatInfo(bookdto.OrgId, vo.kh, vo.zjh, vo.xm);
                            resp.data = list;
                            resp.code = ResponseResultCode.SUCCESS;
                            resp.msg = "患者信息添加成功";
                        }
                        catch (Exception ex)
                        {
                            resp.code = ResponseResultCode.ERROR;
                            resp.msg = ex.InnerException.InnerException.ToString();
                        }
                    }
                    else
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.sub_code = "10003";
                        resp.msg = "参数字典不规范";
                    }
                }
            };
            var response = base.CommonExecute(ac, bookdto);
            return base.CreateResponse(response);
        }


        /// <summary>
        /// Y005 医院药品信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private HttpResponseMessage SysDrugInfo(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            int ypId = 0; string ypmc = null; 
            if (dto.paradata != null)
            {
                ypId = dto.paradata.ContainsKey("ypId") == true ? int.Parse(dto.paradata["ypId"]) : 0;
                ypmc = dto.paradata.ContainsKey("ypmc") == true ? dto.paradata["ypmc"] : null;
            }
            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                var DrugList = _BookingDmnService.GetDrugInfo(para.OrgId, ypId, ypmc);
                var list = new List<SysDrugVO>();
                foreach (var obj in DrugList)
                {
                    obj.zfxz = ((EnumZiFuXingZhi)int.Parse(obj.zfxz)).GetDescription();
                    obj.jbywbj = "基本药物";
                    list.Add(obj);
                }
                if (list.Count > 0)
                {
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未找到相关记录";
                }
            };

            var response = base.CommonExecute(ac, para);
            return base.CreateResponse(response);
        }

        /// <summary>
        /// Y006 医疗项目信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private HttpResponseMessage SysMedicalInfo(BookingReqBase dto) {

            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            int sfxmId = 0;string sfxmCode = null; string sfxmmc = null;
            if (dto.paradata != null)
            {
                sfxmId = dto.paradata.ContainsKey("sfxmId") == true ? int.Parse(dto.paradata["sfxmId"]) : 0;
                sfxmCode = dto.paradata.ContainsKey("sfxmCode") == true ? dto.paradata["sfxmCode"] : null;
                sfxmmc = dto.paradata.ContainsKey("sfxmmc") == true ? dto.paradata["sfxmmc"] : null;
            }
            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                var MedicalList = _BookingDmnService.GetMedicalInfo(para.OrgId, sfxmId,sfxmCode,sfxmmc);
                var list= new List<SysMedicalVO>();
                foreach (var obj in MedicalList) {
                    obj.zfxz= ((EnumZiFuXingZhi)int.Parse(obj.zfxz)).GetDescription();
                    list.Add(obj);
                }
                if (list.Count > 0)
                {
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未找到相关记录";
                }
            };

            var response = base.CommonExecute(ac, para);
            return base.CreateResponse(response);
        }

        /// <summary>
        /// Y007 门诊疾病信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private HttpResponseMessage SysDiseaseInfo(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            string zdCode = null; string zdmc = null; string icd10 = null;
            if (dto.paradata != null)
            {
                zdCode = dto.paradata.ContainsKey("zdCode") == true ? dto.paradata["zdCode"] : null;
                zdmc = dto.paradata.ContainsKey("zdmc") == true ? dto.paradata["zdmc"] : null;
                icd10 = dto.paradata.ContainsKey("icd10") == true ? dto.paradata["icd10"] : null;
            }
            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                var list = _BookingDmnService.GetDiseaseInfo(para.OrgId, zdCode, zdmc, icd10);
                if (list.Count > 0)
                {
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未找到相关记录";
                }
            };

            var response = base.CommonExecute(ac, para);
            return base.CreateResponse(response);
        }

        /// <summary>
        /// Y008 ghlx 挂号类型
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        private HttpResponseMessage SysRegType(BookingReqBase dto)
        {
            Action<BookingReqBase, DefaultResponse> ac = (req, resp) =>
            {
                var list = _BookingDmnService.SysItemDetail(dto.OrganizeId, "RegType");
                if (list.Count > 0)
                {
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未找到相关记录";
                }
            };

            var response = base.CommonExecute(ac, dto);
            return base.CreateResponse(response);
        }

        /// <summary>
        /// Y009 门诊排班
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="outDate"></param>
        /// <param name="czks"></param>
        /// <param name="ysgh"></param>
        /// <param name="regType"></param>
        /// <param name="ghpbid"></param>
        /// <param name="scheduId"></param>
        /// <returns></returns>
        private HttpResponseMessage GetMzpbSchedule(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            if (dto.paradata != null)
            {
                para.outDate = dto.paradata.ContainsKey("outDate") == true ? dto.paradata["outDate"] : null;
                para.ks = dto.paradata.ContainsKey("ks") == true ? dto.paradata["ks"] : null;
                para.ys = dto.paradata.ContainsKey("ys") == true ? dto.paradata["ys"] : null;
                para.regType = dto.paradata.ContainsKey("regType") == true ? dto.paradata["regType"] : null;
                para.ghpbId = dto.paradata.ContainsKey("ghpbId") == true ? dto.paradata["ghpbId"] : null;
                para.scheduId = dto.paradata.ContainsKey("scheduId") == true ? dto.paradata["scheduId"] : null;
                para.FromoutDate = dto.paradata.ContainsKey("FromoutDate") == true ? dto.paradata["FromoutDate"] : null;
            }
            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                var pblx = (int) EnumMzpblx.yy;
                var list = _BookingDmnService.GetMzpbSchedule(para.OrgId, para.outDate, para.ks, para.ys, para.regType, para.ghpbId, para.scheduId,null,pblx);
                if (list.Count > 0)
                {
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未找到相关记录";
                }
            };

            var response = base.CommonExecute(ac, para);
            return base.CreateResponse(response);
        }
        /// <summary>
        /// Y010 排班号源明细
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private HttpResponseMessage GetMzpbScheduleDetail(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            if (dto.paradata != null)
            {
                para.scheduId = dto.paradata.ContainsKey("scheduId") == true ? dto.paradata["scheduId"] : null;
            }
            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                var list = _BookingDmnService.GetScheduleDetail(para.OrgId, para.scheduId);
                if (list.Count > 0)
                {
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未找到相关记录";
                }
            };
            var response = base.CommonExecute(ac, para);
            return base.CreateResponse(response);
        }

        /// <summary>
        /// Y011 就诊处方信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private HttpResponseMessage GetRecipeInfo(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            var cfnm = "";
            var mzh = "";
            cfnm = dto.paradata.ContainsKey("cfnm") == true ? dto.paradata["cfnm"] : null;
            mzh = dto.paradata.ContainsKey("mzh") == true ? dto.paradata["mzh"] : null;
            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) => {
                var recipeList = _BookingDmnService.GetRecipeInfo(para.OrgId, cfnm, mzh);
                var list = new List<SysRecipeVO>();
                foreach (var obj in recipeList)
                {
                    obj.cfzt = ((EnumZfzt)(Convert.ToInt32(obj.cfzt))).GetDescription();
                    obj.mjzbz= ((EnumJzzt)(Convert.ToInt32(obj.mjzbz))).GetDescription();
                    list.Add(obj);
                }
                if (list != null && list.Count > 0)
                {
                    resp.code = ResponseResultCode.SUCCESS;
                    resp.data = list;
                    resp.msg = "查询成功";
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "就诊处方信息不存在";
                }
            };

            var response = base.CommonExecute(ac, para);
            return base.CreateResponse(response);
        }

        /// <summary>
        /// Y012 门诊处方明细
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private HttpResponseMessage GetRecipeDrugInfo(BookingReqBase dto) {

            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            var cfmxId = "";
            var cfnm = "";
            cfmxId = dto.paradata.ContainsKey("cfmxId") == true ? dto.paradata["cfmxId"] : null;
            cfnm = dto.paradata.ContainsKey("cfnm") == true ? dto.paradata["cfnm"] : null;
            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) => {
                var recipeDrugList = _BookingDmnService.GetRecipeDrugInfo(para.OrgId, cfmxId, cfnm);
                var list = new List<SysRecipeDrugVO>();
                foreach (var obj in recipeDrugList)
                {
                    obj.zfxz = ((EnumZiFuXingZhi)(Convert.ToInt32(obj.zfxz))).GetDescription();
                    list.Add(obj);
                }
                if (list != null && list.Count > 0)
                {
                    resp.code = ResponseResultCode.SUCCESS;
                    resp.data = list;
                    resp.msg = "查询成功";
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "门诊处方明细信息不存在";
                }
            };

            var response = base.CommonExecute(ac, para);
            return base.CreateResponse(response);
        }
        /// <summary>
        /// Y021 预约记录查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private HttpResponseMessage OutBookRecord(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            para.AppId = dto.AppId;
            para.xm = dto.paradata.ContainsKey("xm") == true ? dto.paradata["xm"] : null;
            para.kh = dto.paradata.ContainsKey("kh") == true ? dto.paradata["kh"] : null;
            para.bookId = dto.paradata.ContainsKey("bookId") == true ? dto.paradata["bookId"] : null;
            para.lxdh = dto.paradata.ContainsKey("lxdh") == true ? dto.paradata["lxdh"] : null;
            para.ks = dto.paradata.ContainsKey("ks") == true ? dto.paradata["ks"] : null;
            para.ys = dto.paradata.ContainsKey("ys") == true ? dto.paradata["ys"] : null;
            if (dto.paradata.ContainsKey("ksrq") && !string.IsNullOrWhiteSpace(dto.paradata["ksrq"]))
            {
                para.ksrq = Convert.ToDateTime(dto.paradata["ksrq"]).ToString("yyyy-MM-dd");
            }

            if (dto.paradata.ContainsKey("jsrq") && !string.IsNullOrWhiteSpace(dto.paradata["jsrq"]))
            {
                para.jsrq = Convert.ToDateTime(dto.paradata["jsrq"]).ToString("yyyy-MM-dd");
            }

            if (dto.paradata.ContainsKey("yyzt") && !string.IsNullOrWhiteSpace(dto.paradata["yyzt"]))
            {
                para.yyzt = Convert.ToInt32(dto.paradata["yyzt"]);
            }
            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                var list=_BookingDmnService.OutbookRecord(para.OrgId, para.kh,para.xm, para.lxdh, para.bookId,
                    para.yyzt, para.ksrq, para.jsrq, para.ks, para.ys,para.AppId);
                if (list.Count > 0)
                {
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未找到预约记录";
                }
            };
            var response = base.CommonExecute(ac, para);
            return base.CreateResponse(response);
        }
        /// <summary>
        /// 门诊预约
        /// </summary>
        /// <returns></returns>
        private HttpResponseMessage OutBookApply(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            para.AppId = dto.AppId;
            para.ghxz = dto.paradata.ContainsKey("ghxz") == true ? dto.paradata["ghxz"] : null;
            para.scheduId = dto.paradata.ContainsKey("scheduId") == true ? dto.paradata["scheduId"] : null;
            para.kh = dto.paradata.ContainsKey("kh") == true ? dto.paradata["kh"] : null;
            para.patid = dto.paradata.ContainsKey("patid") == true ? dto.paradata["patid"] : null;

            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                //患者身份验证
                var pat = _BookingDmnService.GetPatInfo(para.OrgId, para.kh, null, null).FirstOrDefault();
                //排班有效性验证
                if (pat!= null)
                {
                    //仅限预约当天之后的有效预约
                    string nowday = DateTime.Now.ToString("yyyy-MM-dd");
                    
                    var pb = _BookingDmnService.GetMzpbSchedule(para.OrgId, null, null, null, null, null,
                        para.scheduId, nowday,(int)EnumMzpblx.yy).FirstOrDefault();
                    if (pb!=null)
                    {
                        DateTime pbOutdate = Convert.ToDateTime(pb.OutDate);
                        var pbety = _MzghbookRepo.FindEntity(p =>
                            p.OrganizeId == para.OrgId && p.yyzt != ((int) EnumMzyyzt.cancel).ToString() &&
                            p.zt == "1" && p.kh == para.kh && p.ScheduId.ToString() == para.scheduId);
                        var kspbety = _MzghbookRepo.FindEntity(p =>
                            p.OrganizeId == para.OrgId && p.yyzt != ((int) EnumMzyyzt.cancel).ToString() &&
                            p.zt == "1" && p.kh == para.kh && p.ks == pb.czks && p.OutDate == pbOutdate);
                        if (pbety != null)
                        {
                            resp.code = ResponseResultCode.FAIL;
                            resp.msg = "同一号源不可重复预约";
                        }
                        else if (kspbety!=null)
                        {
                            resp.code = ResponseResultCode.FAIL;
                            resp.msg = "同一科室当天不可重复预约";
                        }
                        else
                        {
                            var bookid= _BookingDmnService.OutbookingApply(para, pat,pb).ToString();
                            var bookety = bookid == null
                                ? null
                                : _BookingDmnService.OutbookRecord(para.OrgId, null, null,null, bookid,
                                    (int) EnumMzyyzt.book, null, null, null, null,para.AppId);
                            if (bookety != null)
                            {
                                resp.data = bookety;
                                resp.code = ResponseResultCode.SUCCESS;
                                resp.msg = "预约成功";
                            }
                            else
                            {
                                resp.code = ResponseResultCode.FAIL;
                                if (bookid == "-100")
                                {
                                    resp.msg = "该排班已无可约号源";
                                }
                                else if (bookid == "-200")
                                {
                                    resp.msg = "无法获取号源信息，请重试";
                                }
                                else if (bookid == "-300")
                                {
                                    resp.msg = "关键信息不可为空";
                                }
                                else
                                {
                                    resp.msg = "预约失败";
                                }
                               
                            }
                        }
                    }
                    else
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "当前排班不可约";
                    }
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未找到卡信息";
                }


            };

            var response = base.CommonExecute(ac, para);
            return base.CreateResponse(response);

        }
        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private HttpResponseMessage OutBookCancel(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            para.AppId = dto.AppId;
            para.kh = dto.paradata.ContainsKey("kh") == true ? dto.paradata["kh"] : null;
            para.bookId = dto.paradata.ContainsKey("bookId") == true ? dto.paradata["bookId"] : null;
            para.lxdh = dto.paradata.ContainsKey("lxdh") == true ? dto.paradata["lxdh"] : null;

            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                //校验预约信息是否一致、预约状态
                var record = _MzghbookRepo.FindEntity(p =>
                    p.BookId.ToString() == para.bookId && p.OrganizeId == para.OrgId && p.zt == "1");
                if (record != null)
                {
                    if (record.kh == para.kh)
                    {
                        if (record.yyzt == ((int) EnumMzyyzt.book).ToString())
                        {
                            record.yyzt = ((int) EnumMzyyzt.cancel).ToString();
                            record.CancelReason = para.AppId + ";" + para.lxdh;
                            record.CancelTime=DateTime.Now;
                            record.LastModifyTime=DateTime.Now;
                            var up=_MzghbookRepo.Update(record);
                            if (up > 0)
                            {
                                resp.code = ResponseResultCode.SUCCESS;
                                resp.msg = "取消预约成功";
                                resp.data = new {BookId = record.BookId};
                            }
                            else
                            {
                                resp.code = ResponseResultCode.FAIL;
                                resp.msg = "取消预约失败，当前预约状态不可取消";
                            }
                        }
                        else
                        {
                            resp.code = ResponseResultCode.FAIL;
                            resp.msg = "取消预约失败，当前预约状态不可取消";
                        }
                    }
                    else
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "操作失败，卡信息异常";
                    }
                }
            };
            var response = base.CommonExecute(ac, para);
            return base.CreateResponse(response);
        }

        private HttpResponseMessage OutBookPay(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            para.AppId = dto.AppId;
            para.kh = dto.paradata.ContainsKey("kh") == true ? dto.paradata["kh"] : null;
            para.bookId = dto.paradata.ContainsKey("bookId") == true ? dto.paradata["bookId"] : null;
            //para.lxdh = dto.paradata.ContainsKey("lxdh") == true ? dto.paradata["lxdh"] : null;
            para.PayFee= dto.paradata.ContainsKey("PayFee") == true ? dto.paradata["PayFee"] : null;
            para.PayLsh = dto.paradata.ContainsKey("PayLsh") == true ? dto.paradata["PayLsh"] : null;

            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                //去挂号
                var res = _BookingDmnService.OutpatRegApply(para.bookId, para.kh, para.OrgId, para.PayFee, para.PayLsh,dto.user);
                if (res.code == 0)
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = res.msg;
                }
                else
                {
                    resp.code = ResponseResultCode.SUCCESS;
                    resp.msg = res.msg;
                    resp.data = res.data;
                }
            };
            var response = base.CommonExecute(ac, para);
            return base.CreateResponse(response);
        }

        private HttpResponseMessage OutBookRegistCancel(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto
            {
                OrgId = dto.OrganizeId,
                Timestamp = dto.Timestamp,
                AppId = dto.AppId,
                kh = dto.paradata.ContainsKey("kh") == true ? dto.paradata["kh"] : null,
                mzh = dto.paradata.ContainsKey("mzh") == true ? dto.paradata["mzh"] : null
            };

            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                var res = _BookingDmnService.OutPatRegCancel(para.OrgId, dto.user, para.mzh, para.kh, para.AppId);
                if (res.code == 0)
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = res.sub_code.ToString();
                    resp.msg = res.msg;
                }
                else
                {
                    resp.code = ResponseResultCode.SUCCESS;
                    resp.msg = res.msg;
                    resp.data = res.data;
                }
            };
            var response = base.CommonExecute(ac, para);
            return base.CreateResponse(response);
        }

        private HttpResponseMessage CommonRegist(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            para.AppId = dto.AppId;
            para.kh = dto.paradata.ContainsKey("kh") == true ? dto.paradata["kh"] : null;
            //para.ghrq = dto.paradata.ContainsKey("ghrq") == true ? dto.paradata["ghrq"] : null;
            para.ghrq = DateTime.Now.ToString("yyyy-MM-dd");
            para.ks = dto.paradata.ContainsKey("ks") == true ? dto.paradata["ks"] : null;
            para.ghxz = dto.paradata.ContainsKey("ghxz") == true ? dto.paradata["ghxz"] : null;
            para.ghlybz = dto.paradata.ContainsKey("ghlybz") == true ? dto.paradata["ghlybz"] : null;


            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                //去挂号
                var res = _BookingDmnService.OutpatReg(para.kh, para.OrgId, para.ghrq, para.ghxz, para.ks, dto.user, para.AppId, para.ghlybz);
                if (res.code == 0)
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = res.msg;
                }
                else
                {
                    resp.code = ResponseResultCode.SUCCESS;
                    resp.msg = res.msg;
                    resp.data = res.data;
                }
            };
            var response = base.CommonExecute(ac, para);
            return base.CreateResponse(response);
        }

        private HttpResponseMessage OutpatChargeInfo(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            para.AppId = dto.AppId;
            para.kh = dto.paradata.ContainsKey("kh") == true ? dto.paradata["kh"] : null;
            para.mzh = dto.paradata.ContainsKey("mzh") == true ? dto.paradata["mzh"] : null;
            para.xm = dto.paradata.ContainsKey("xm") == true ? dto.paradata["xm"] : null;

            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                //去挂号
                var res = _BookingDmnService.OutpatChargeInfo(para.OrgId, para.kh, para.mzh,para.AppId);
                if (res==null || res.Count==0)
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未找到相关收费信息";
                }
                else
                {
                    resp.code = ResponseResultCode.SUCCESS;
                    resp.msg = "";
                    resp.data = res;
                }
            };
            var response = base.CommonExecute(ac, para);
            return base.CreateResponse(response);
        }
    }
}
