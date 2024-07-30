using Autofac;
using FrameworkBase.MultiOrg.Domain.IRepository;
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
using System.Web;
using System.Web.Http;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using Newtonsoft.Json;
using Newtouch.Core.Redis;
using Newtouch.HIS.API.Common.Filter;
using Newtouch.HIS.API.Common.Models;
using Newtouch.HIS.Domain.ValueObjects.API;
using Newtouch.HIS.Domain.IDomainServices.OutpatientManage;
using Newtouch.HIS.Domain.IRepository.OutpatientManage;
using Newtouch.Core.Common;
using Newtouch.HIS.Sett.Request.Booking.Request;

namespace Newtouch.HIS.Sett.API.Controllers
{
    [RoutePrefix("api/OutpBook")]
    public class BookingApiController : ApiControllerBase<BookingApiController>
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
        #region 新排班
        private readonly IOutBookDmnService _outBookDmnService;
        private readonly IOutBookScheduleRepo _outBookScheduleRepo;
        private readonly IBookingRegisterDmnService _bookingRegisterDmn;
        private readonly IOutPatientDmnService _outPatientDmn;
        #endregion

        private string user;

        public BookingApiController(IComponentContext com)
            : base(com)
        {
        }

        private string GetAuth(string orgId, string AppId = "API")
        {
            var identity = new UserIdentity
            {
                AppId = AppId,
                TokenType = "",
                UserId = GetBookTerminal(AppId),
                Account = "API." + GetBookTerminal(AppId),
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
        public ResponseBase BookingEsb(BookingReqBase dto)
        {
            ResponseBase httpresp = new ResponseBase();
            DefaultResponse resp = new DefaultResponse();
            if (dto != null)
            {
                //参数规范性校验
                resp = dto.FormatCheck(dto.methodcode);
                if (resp.code == ResponseResultCode.SUCCESS)
                {
                    //身份设置
                    dto.user = GetAuth(dto.OrganizeId, dto.AppId);

                    switch (dto.methodcode)
                    {
                        case "Y002"://系统科室列表
                            httpresp = SysDepartmentInfo(dto);
                            break;
                        case "Y003": //医生人员信息
                            httpresp = SysStaffInfo(dto);
                            break;
                        case "Y004"://患者信息 optype=1 添加新患者 optype=0 患者查询
                            #region 患者查询与登记
                            if (dto.optype == "1")
                            {
                                if (dto.paradata != null && dto.paradata.ContainsKey("zjh")
                                    && dto.paradata.ContainsKey("xm") && dto.paradata.ContainsKey("xb") && dto.paradata.ContainsKey("csrq"))
                                {
                                    httpresp = SysPatInfoSet(dto);
                                }
                                else
                                {
                                    resp.code = ResponseResultCode.FAIL;
                                    resp.msg = "关键参数不可为空";

                                }
                            }
                            else if (dto.optype == "3") // 2020-2-19 与his登记同步 不强制要求证件号非空
                            {
                                if (dto.paradata != null && dto.paradata.ContainsKey("xm") && dto.paradata.ContainsKey("xb") && dto.paradata.ContainsKey("csrq"))
                                {
                                    httpresp = SysPatInfoSetPub(dto);
                                }
                                else
                                {
                                    resp.code = ResponseResultCode.FAIL;
                                    resp.msg = "关键参数不可为空";

                                }
                            }
                            else if (dto.paradata == null || !dto.paradata.ContainsKey("xm") || string.IsNullOrWhiteSpace(dto.paradata["xm"]))
                            {
                                resp.code = ResponseResultCode.FAIL;
                                resp.msg = "关键参数不可为空";

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

                            }
                            #endregion
                            break;
                        case "Y005"://医院药品信息
                            //httpresp = SysDrugInfo(dto);
                            break;
                        case "Y006"://医疗项目信息
                                    // httpresp = SysMedicalInfo(dto);
                            break;
                        case "Y007"://门诊疾病信息
                            //httpresp = SysDiseaseInfo(dto);
                            break;
                        case "Y008"://挂号项目类别 
                            break;
                        case "Y009"://门诊排班New                            
                            httpresp = dto.optype == "1" ? GetScheduleWithPage(dto) : GetSchedule(dto);
                            break;
                        case "Y010": //门诊排班详细号源信息
                            //httpresp = GetMzpbScheduleDetail(dto);
                            break;
                        case "Y011":
                            break;
                        case "Y012":
                            break;
                        case "Y021"://预约记录
                            httpresp = OutBookRecord(dto);
                            break;
                        case "Y022"://预约
                            #region 门诊预约
                            if (dto.paradata != null && dto.paradata.ContainsKey("ghxz") && dto.paradata.ContainsKey("ScheduId") && dto.paradata.ContainsKey("kh"))
                            {
                                httpresp = OutBookApply(dto);
                            }
                            else
                            {
                                resp.code = ResponseResultCode.FAIL;
                                resp.msg = "关键信息不可为空";

                            }
                            #endregion
                            break;
                        case "Y023"://取消预约
                            if (dto.paradata != null && dto.paradata.ContainsKey("BookId") && dto.paradata.ContainsKey("kh"))
                            {
                                httpresp = OutBookCancel(dto);
                            }
                            else
                            {
                                resp.code = ResponseResultCode.FAIL;
                                resp.msg = "关键信息不可为空";

                            }
                            break;
                        case "Y024"://挂号支付
                            if (dto.paradata != null && dto.paradata.ContainsKey("BookId") && dto.paradata.ContainsKey("kh") &&
                                dto.paradata.ContainsKey("PayFee") && dto.paradata.ContainsKey("PayLsh"))
                            {
                                httpresp = OutBookPay(dto);
                            }
                            else
                            {
                                resp.code = ResponseResultCode.FAIL;
                                resp.msg = "关键信息不可为空";

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

                            }
                            break;
                        case "Y026"://无预约挂号
                            if (dto.paradata != null && dto.paradata.ContainsKey("ScheduId") && dto.paradata.ContainsKey("kh")
                                && dto.paradata.ContainsKey("ghxz"))
                            {
                                httpresp = CommonRegist(dto);
                            }
                            else
                            {
                                resp.code = ResponseResultCode.FAIL;
                                resp.msg = "关键信息不可为空";

                            }
                            break;
                        case "Y027"://当日挂号记录
                            if (dto.paradata != null && dto.paradata.ContainsKey("mzh") && dto.paradata.ContainsKey("kh"))
                            {
                                httpresp = OutpatRegRecord(dto, true);
                            }
                            else
                            {
                                resp.code = ResponseResultCode.FAIL;
                                resp.msg = "门诊号/卡号信息不可为空";
                            }
                            break;
                        case "Y028": //获取待支付账单
                            if (dto.paradata != null && dto.paradata.ContainsKey("mzh") && dto.paradata.ContainsKey("kh"))
                            {
                                httpresp = PresUnpayQuery(dto);
                            }
                            else
                            {
                                resp.code = ResponseResultCode.FAIL;
                                resp.msg = "门诊号及卡号信息不可为空";
                            }
                            break;
                        case "Y029": //获取待支付账单
                            if (dto.paradata != null && dto.paradata.ContainsKey("PresId") && dto.paradata.ContainsKey("mzh"))
                            {
                                httpresp =PresDetailQuery(dto);
                            }
                            else
                            {
                                resp.code = ResponseResultCode.FAIL;
                                resp.msg = "门诊号及卡号信息不可为空";
                            }
                            break;
                        case "Y030"://门诊收费信息查询 作废
                            if (dto.paradata != null && dto.paradata.ContainsKey("mzh") && dto.paradata.ContainsKey("kh"))
                            {
                                //httpresp = OutpatChargeInfo(dto);
                            }
                            else
                            {
                                resp.code = ResponseResultCode.FAIL;
                                resp.msg = "关键信息不可为空";

                            }
                            break;
                        default:
                            resp.code = ResponseResultCode.FAIL;
                            resp.msg = "关键信息不可为空";

                            break;

                    }
                }
                else
                {

                }
            }
            else
            {
                resp.code = ResponseResultCode.FAIL;
                resp.msg = "请校验参数格式是否正确";

            }
            if (httpresp != null && (httpresp.data != null || !string.IsNullOrWhiteSpace(httpresp.msg) || !string.IsNullOrWhiteSpace(httpresp.sub_msg))) return httpresp;
            return resp;
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
        private ResponseBase SysDepartmentInfo(BookingReqBase dto)
        {
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
                var list = _BookingDmnService.GetDepartmentInfo(para.OrgId, para.ks, para.ksmc);
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

            return base.CommonExecute(ac, para);
        }

        /// <summary>
        /// Y003 医生人员信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private ResponseBase SysStaffInfo(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            if (dto.paradata != null)
            {
                para.ys = dto.paradata.ContainsKey("ysgh") == true ? dto.paradata["ysgh"] : null;
                para.xm = dto.paradata.ContainsKey("ysxm") == true ? dto.paradata["ysxm"] : null;
                para.ks = dto.paradata.ContainsKey("ks") == true ? dto.paradata["ks"] : null;
            }
            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                var staffList = _BookingDmnService.GetStaffInfo(para.OrgId, para.ys, para.xm);
                var list = new List<SysDoctorVO>();
                foreach (var obj in staffList)
                {
                    if (string.IsNullOrWhiteSpace(para.ks) || (!string.IsNullOrWhiteSpace(para.ks) && obj.departmentCode == para.ks))
                    {
                        SysDoctorVO staff = new SysDoctorVO();
                        if (obj.Gender == true)
                        {
                            string xb = ((EnumSex)(Convert.ToInt32(obj.Gender))).GetDescription();
                            //properties
                            staff.ks = obj.departmentCode;
                            staff.ksmc = obj.departmentName;
                            staff.DutyId = obj.DutyId;
                            staff.dutyName = obj.dutyName;
                            staff.xb = xb ?? "";
                            staff.ysgh = obj.gh;
                            staff.organizeId = obj.organizeId;
                            staff.ysxm = obj.staffName;
                            list.Add(staff);
                        }
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

            return base.CommonExecute(ac, para);
        }


        /// <summary>
        /// Y004 获取基本患者信息
        /// </summary>
        /// <param name="xm"></param>
        /// <param name="kh"></param>
        /// <param name="zjh"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        private ResponseBase SysPatInfo(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            para.zjh = dto.paradata.ContainsKey("zjh") == true ? dto.paradata["zjh"] : null;
            para.kh = dto.paradata.ContainsKey("kh") == true ? dto.paradata["kh"] : null;
            para.xm = dto.paradata.ContainsKey("xm") == true ? dto.paradata["xm"] : null;
            para.dh = dto.paradata.ContainsKey("dh") == true ? dto.paradata["dh"] : null;
            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
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

            return base.CommonExecute(ac, para);
        }
        /// <summary>
        /// Y004 添加新患者信息
        /// </summary>
        /// <param name="xm"></param>
        /// <param name="kh"></param>
        /// <param name="zjh"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        private ResponseBase SysPatInfoSet(BookingReqBase dto)
        {
            BookingRequestDto bookdto = new BookingRequestDto();
            bookdto.OrgId = dto.OrganizeId;
            bookdto.Timestamp = dto.Timestamp;
            if (dto.paradata != null)
            {
                bookdto.zjh = dto.paradata.ContainsKey("zjh") == true ? dto.paradata["zjh"] : null;
                //bookdto.kh = dto.paradata.ContainsKey("kh") == true ? dto.paradata["kh"] : null;
                bookdto.xm = dto.paradata.ContainsKey("xm") == true ? dto.paradata["xm"] : null;
                bookdto.lxdh = dto.paradata.ContainsKey("lxdh") == true ? dto.paradata["lxdh"] : null;
                if (dto.paradata.ContainsKey("csrq"))
                {
                    bookdto.csrq = Convert.ToDateTime(dto.paradata["csrq"]);
                }
                bookdto.xb = dto.paradata.ContainsKey("xb") == true ? dto.paradata["xb"] : null;
            }
            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                var list = _bookingRegisterDmn.SysPatInfoSet(new RegisterReq
                {
                    IDCard = bookdto.zjh,
                    Name = bookdto.xm,
                    Phone = bookdto.dh,
                    HospitalID = "His",
                    PatType = ((int)EnumBrxz.zf).ToString(),
                    Gender = bookdto.xb
                });
                resp.data = list;
                resp.code = ResponseResultCode.SUCCESS;
                if (list == null)
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "建卡信息登记失败,请重试";
                }

            };
            return CommonExecute(ac, bookdto);

        }

        private ResponseBase SysPatInfoSetPub(BookingReqBase dto)
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
                    p.xm == bookdto.xm && p.csny == bookdto.csrq && p.xb == bookdto.xb && p.OrganizeId == bookdto.OrgId && p.zt == "1");
                if (validatepat.Count() > 0)
                {
                    resp.code = ResponseResultCode.FAIL;
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
                            _PatientBasicInfoDmnService.SavePatBasicCardInfo(vo, bookdto.OrgId, "");
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
            return CommonExecute(ac, bookdto);

        }


        /// <summary>
        /// Y005 医院药品信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private ResponseBase SysDrugInfo(BookingReqBase dto)
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

            return CommonExecute(ac, para);

        }

        /// <summary>
        /// Y006 医疗项目信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private ResponseBase SysMedicalInfo(BookingReqBase dto)
        {

            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            int sfxmId = 0; string sfxmCode = null; string sfxmmc = null;
            if (dto.paradata != null)
            {
                sfxmId = dto.paradata.ContainsKey("sfxmId") == true ? int.Parse(dto.paradata["sfxmId"]) : 0;
                sfxmCode = dto.paradata.ContainsKey("sfxmCode") == true ? dto.paradata["sfxmCode"] : null;
                sfxmmc = dto.paradata.ContainsKey("sfxmmc") == true ? dto.paradata["sfxmmc"] : null;
            }
            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                var MedicalList = _BookingDmnService.GetMedicalInfo(para.OrgId, sfxmId, sfxmCode, sfxmmc);
                var list = new List<SysMedicalVO>();
                foreach (var obj in MedicalList)
                {
                    obj.zfxz = ((EnumZiFuXingZhi)int.Parse(obj.zfxz)).GetDescription();
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

            return CommonExecute(ac, para);

        }

        /// <summary>
        /// Y007 门诊疾病信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private ResponseBase SysDiseaseInfo(BookingReqBase dto)
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

            return CommonExecute(ac, para);

        }      

        
        /// <summary>
        /// Y010 排班号源明细
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private ResponseBase GetMzpbScheduleDetail(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            if (dto.paradata != null)
            {
                para.ScheduId = dto.paradata.ContainsKey("ScheduId") == true ? dto.paradata["ScheduId"] : null;
            }
            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                var list = _BookingDmnService.GetScheduleDetail(para.OrgId, para.ScheduId);
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
            return CommonExecute(ac, para);

        }

       
        /// <summary>
        /// Y021 预约记录查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private ResponseBase OutBookRecord(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            para.AppId = dto.AppId;
            para.xm = dto.paradata.ContainsKey("xm") == true ? dto.paradata["xm"] : null;
            para.kh = dto.paradata.ContainsKey("kh") == true ? dto.paradata["kh"] : null;
            para.BookId = dto.paradata.ContainsKey("BookId") == true ? dto.paradata["BookId"] : null;
            para.ks = dto.paradata.ContainsKey("ks") == true ? dto.paradata["ks"] : null;
            para.ys = dto.paradata.ContainsKey("ysgh") == true ? dto.paradata["ysgh"] : null;
            para.outDate = dto.paradata.ContainsKey("OutDate") == true ? dto.paradata["OutDate"] : null;
            para.yyzt = dto.paradata.ContainsKey("yyzt") == true ? dto.paradata["yyzt"] : null;
            if (dto.paradata.ContainsKey("ksrq") && !string.IsNullOrWhiteSpace(dto.paradata["ksrq"]))
            {
                para.ksrq = Convert.ToDateTime(dto.paradata["ksrq"]).ToString("yyyy-MM-dd");
            }

            if (dto.paradata.ContainsKey("jsrq") && !string.IsNullOrWhiteSpace(dto.paradata["jsrq"]))
            {
                para.jsrq = Convert.ToDateTime(dto.paradata["jsrq"]).ToString("yyyy-MM-dd");
            }

            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                if (!string.IsNullOrWhiteSpace(para.BookId))
                {
                    var list = _bookingRegisterDmn.QueryAppRecord(new MzAppointmentRecordReq
                    {
                        BookId = para.BookId,
                        HospitalID = "His"
                    });
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list == null)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "未找到预约记录";
                    }
                }
                else
                {
                    var requestModel = new MzAppointmentRecordListReq
                    {
                        CardNo = para.kh,
                        PatName = para.xm,
                        Dept = para.ks,
                        DeptName = para.ksmc,
                        IDCard = para.zjh,
                        RegType = para.RegType,
                        ysgh = para.ys,
                        ksrq = para.ksrq,
                        jsrq = para.jsrq,
                        HospitalID = "His"
                    };
                    if (!string.IsNullOrWhiteSpace(para.yyzt))
                    {
                        requestModel.yyzt = Convert.ToInt32(para.yyzt);
                    }
                    if (!string.IsNullOrWhiteSpace(para.outDate))
                    {
                        requestModel.OutDate = Convert.ToDateTime(para.outDate);
                    }
                    var list = _bookingRegisterDmn.QueryAppRecordList(requestModel);
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list.Count == 0)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "未找到预约记录";
                    }
                }
            };
            return CommonExecute(ac, para);

        }
        /// <summary>
        /// Y022 门诊预约
        /// </summary>
        /// <returns></returns>
        private ResponseBase OutBookApply(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            para.AppId = GetBookTerminal(dto.AppId);
            para.ghxz = dto.paradata.ContainsKey("ghxz") == true ? dto.paradata["ghxz"] : null;
            para.ScheduId = dto.paradata.ContainsKey("ScheduId") == true ? dto.paradata["ScheduId"] : null;
            para.kh = dto.paradata.ContainsKey("kh") == true ? dto.paradata["kh"] : null;

            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {

                var yyresult = _outBookDmnService.PatBookGh(
                    new MzAppointmentReq
                    {
                        CardNo = para.kh,
                        ScheduId = Convert.ToInt32(para.ScheduId),
                        ghxz = para.ghxz,
                        AppID = dto.AppId
                    });
                if (yyresult != null)
                {
                    resp.data = yyresult;
                    resp.code = ResponseResultCode.SUCCESS;
                    resp.msg = "预约成功";
                }
                else
                {
                    resp.data = yyresult;
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "操作异常，系统无返回";
                }
            };


            return CommonExecute(ac, para);

        }
        /// <summary>
        /// Y023 取消预约
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private ResponseBase OutBookCancel(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            para.AppId = dto.AppId;
            para.kh = dto.paradata.ContainsKey("kh") == true ? dto.paradata["kh"] : null;
            para.BookId = dto.paradata.ContainsKey("BookId") == true ? dto.paradata["BookId"] : null;
            para.lxdh = dto.paradata.ContainsKey("lxdh") == true ? dto.paradata["lxdh"] : null;
            para.CancelReason = dto.paradata.ContainsKey("CancelReason") == true ? dto.paradata["CancelReason"] : null;

            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                var result = _outBookDmnService.CancalBook(new MzAppointmentRecordReq
                {
                    BookId = para.BookId,
                    AppID = GetBookTerminal(para.AppId),
                    Lxdh = para.lxdh,
                    CardNo = para.kh
                });
                resp.data = "BookId:" + para.BookId;
                resp.code = result > 0 ? ResponseResultCode.SUCCESS : ResponseResultCode.FAIL;
                resp.msg = (result == 2 ? "预约已取消，请勿重复操作" : (result <= 0 ? "操作异常，请重新查询预约状态" : "取消成功"));
            };
            return CommonExecute(ac, para);

        }
        /// <summary>
        /// Y024 预约挂号/预约签到
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private ResponseBase OutBookPay(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            para.AppId = dto.AppId;
            para.kh = dto.paradata.ContainsKey("kh") == true ? dto.paradata["kh"] : null;
            para.BookId = dto.paradata.ContainsKey("BookId") == true ? dto.paradata["BookId"] : null;
            para.PayFee = dto.paradata.ContainsKey("PayFee") == true ? dto.paradata["PayFee"] : null;
            para.PayLsh = dto.paradata.ContainsKey("PayLsh") == true ? dto.paradata["PayLsh"] : null;
            para.PayWay = dto.paradata.ContainsKey("PayWay") == true ? dto.paradata["PayWay"] : null;
            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                //去挂号
                var data = _bookingRegisterDmn.BookOutpatRegSett(new RegSettReq
                {
                    BookID = Convert.ToInt32(req.BookId),
                    CardNo = req.kh,
                    PayFee = Convert.ToDecimal(req.PayFee),
                    PayLsh = req.PayLsh,
                    PayWay = req.PayWay,
                    HospitalID = "His",
                    AppID = GetBookTerminal(req.AppId)
                });
                if (data != null && !string.IsNullOrWhiteSpace(data.Mzh))
                {
                    resp.code = ResponseResultCode.SUCCESS;
                    resp.msg = "挂号签到成功";
                    resp.data = data;
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "操作失败，HIS未返回结果，请刷新重试";
                }
            };
            return CommonExecute(ac, para);

        }
        /// <summary>
        /// Y025 取消挂号
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private ResponseBase OutBookRegistCancel(BookingReqBase dto)
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
            return CommonExecute(ac, para);

        }
        /// <summary>
        /// Y026 挂号
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private ResponseBase CommonRegist(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            para.AppId = dto.AppId;
            para.kh = dto.paradata.ContainsKey("kh") == true ? dto.paradata["kh"] : null;
            para.ScheduId = dto.paradata.ContainsKey("ScheduId") == true ? int.Parse(dto.paradata["ScheduId"].ToString().Substring(0, dto.paradata["ScheduId"].ToString().IndexOf('.'))).ToString() : null;
            
            para.ghxz = dto.paradata.ContainsKey("ghxz") == true ? dto.paradata["ghxz"] : null;
            para.PayFee = dto.paradata.ContainsKey("PayFee") == true ? dto.paradata["PayFee"] : null;
            para.PayLsh = dto.paradata.ContainsKey("PayLsh") == true ? dto.paradata["PayLsh"] : null;
            para.PayWay = dto.paradata.ContainsKey("PayWay") == true ? dto.paradata["PayWay"] : null;

            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                //去挂号
                var data = _bookingRegisterDmn.OutpatRegSett(new RegSettReq
                {
                    AppID = GetBookTerminal(req.AppId),
                    HospitalID = "His",
                    CardNo = req.kh,
                    ScheduId = Convert.ToDecimal(req.ScheduId),
                    ghxz = req.ghxz,
                    PayFee = Convert.ToDecimal(req.PayFee),
                    PayLsh = req.PayLsh,
                    PayWay = req.PayWay
                });
                if (data != null && !string.IsNullOrWhiteSpace(data.Mzh))
                {
                    resp.code = ResponseResultCode.SUCCESS;
                    resp.msg = "挂号成功";
                    resp.data = data;
                }
                else
                {

                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "挂号失败，HIS未返回挂号结果，请刷新重试";
                }
            };
            return CommonExecute(ac, para);

        }
        /// <summary>
        /// Y027 门诊挂号查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private ResponseBase OutpatRegRecord(BookingReqBase dto, bool todayonly = true)
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
                var data = _bookingRegisterDmn.GetPatRegList(req.OrgId, req.AppId, true, req.kh, req.mzh);
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
        /// <summary>
        /// Y028 门诊待支付处方
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private ResponseBase PresUnpayQuery(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            para.AppId = GetBookTerminal(dto.AppId);
            para.mzh = dto.paradata.ContainsKey("mzh") == true ? dto.paradata["mzh"] : null;
            para.kh = dto.paradata.ContainsKey("kh") == true ? dto.paradata["kh"] : null;

            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                //查询门诊挂号信息
                var patInfo = _bookingRegisterDmn.getPatInfo(para.mzh, para.OrgId);
                if (patInfo == null || patInfo.kh != para.kh)
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "卡信息异常";
                }
                else
                {
                    //去挂号
                    var res = _bookingRegisterDmn.PresUnpayQuery(para.mzh, para.AppId, para.OrgId);
                    if (res == null || res.Count == 0)
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
                }
            };
            return CommonExecute(ac, para);

        }

        /// <summary>
        /// Y029 门诊待支付处方明细
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private ResponseBase PresDetailQuery(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            para.AppId = GetBookTerminal(dto.AppId);
            para.mzh = dto.paradata.ContainsKey("mzh") == true ? dto.paradata["mzh"] : null;
            para.PresId = dto.paradata.ContainsKey("PresId") == true ? dto.paradata["PresId"] : null;

            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                //去挂号
                var res = _bookingRegisterDmn.PresDetailQuery(para.mzh,para.PresId, para.AppId, para.OrgId);
                if (res == null || res.Count == 0)
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
            return CommonExecute(ac, para);

        }


        #region 新排班        
        /// <summary>
        /// Y009 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private ResponseBase GetSchedule(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            para.ks = dto.paradata.ContainsKey("ks") == true ? dto.paradata["ks"] : null;
            para.ScheduId = dto.paradata.ContainsKey("ScheduId") == true ? dto.paradata["ScheduId"] : null;
            para.ys = dto.paradata.ContainsKey("ys") == true ? dto.paradata["ys"] : null;
            para.ksrq = dto.paradata.ContainsKey("ksrq") == true ? dto.paradata["ksrq"] : null;
            para.jsrq = dto.paradata.ContainsKey("jsrq") == true ? dto.paradata["jsrq"] : null;
            para.RegType = dto.paradata.ContainsKey("RegType") == true ? dto.paradata["RegType"] : null;
            para.IsBook = dto.paradata.ContainsKey("IsBook") == true ? dto.paradata["IsBook"] : "1";

            //var doclist = _outBookDmnService.getStaffListByKs(para.ks, para.OrgId);
            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                var res = _outBookScheduleRepo.GetPagintionListTime(para.OrgId, para.ksrq, para.jsrq, para.ys, "", para.ScheduId, para.ks, para.ScheduId, para.IsBook); ;
                if (res == null || res.Count == 0)
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未找到相关排班信息";
                }
                else
                {
                    resp.code = ResponseResultCode.SUCCESS;
                    resp.msg = "";
                    resp.data = res;
                }
            };
            return CommonExecute(ac, para);
        }
        /// <summary>
        /// Y009 分页获取排班
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private ResponseBase GetScheduleWithPage(BookingReqBase dto)
        {
            BookingRequestDto para = new BookingRequestDto();
            para.OrgId = dto.OrganizeId;
            para.Timestamp = dto.Timestamp;
            para.ks = dto.paradata.ContainsKey("ks") == true ? dto.paradata["ks"] : null;
            para.ScheduId = dto.paradata.ContainsKey("ScheduId") == true ? dto.paradata["ScheduId"] : null;
            para.ys = dto.paradata.ContainsKey("ys") == true ? dto.paradata["ys"] : null;
            para.ksrq = dto.paradata.ContainsKey("ksrq") == true ? dto.paradata["ksrq"] : null;
            para.jsrq = dto.paradata.ContainsKey("jsrq") == true ? dto.paradata["jsrq"] : null;
            para.RegType = dto.paradata.ContainsKey("RegType") == true ? dto.paradata["RegType"] : null;
            para.IsBook = dto.paradata.ContainsKey("IsBook") == true ? dto.paradata["IsBook"] : null;
            var pagination = dto.paradata.ContainsKey("pagination") == true && !string.IsNullOrWhiteSpace(dto.paradata["pagination"]) ? JsonConvert.DeserializeObject<Pagination>(dto.paradata["pagination"]) : new Pagination
            {
                page = 1,
                rows = 20,
                sidx = "outdate"
            };
            pagination.sidx = string.IsNullOrWhiteSpace(pagination.sidx) ? "outdate" : pagination.sidx;
            //var doclist = _outBookDmnService.getStaffListByKs(para.ks, para.OrgId);
            Action<BookingRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                Pagination paging = new Pagination();
                var res = _outBookScheduleRepo.GetPagintionListTime(pagination, para.OrgId, para.ksrq, para.jsrq, para.ys, "", para.ScheduId, para.ks, para.RegType, para.IsBook, out paging); ;
                if (res == null || res.Count == 0)
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未找到相关排班信息";
                }
                else
                {
                    resp.code = ResponseResultCode.SUCCESS;
                    resp.msg = "";
                    resp.sub_msg = paging.ToJson();
                    resp.data = res;
                }
            };
            return CommonExecute(ac, para);
        }
        #endregion

        //#region privare method
        ///// <summary>
        ///// 获取预约来源
        ///// </summary>
        ///// <param name="AppID"></param>
        ///// <returns></returns>
        //private string GetBookTerminal(string AppID)
        //{
        //    if (AppID.Contains("SelfTerminal"))
        //    {
        //        return EnumMzghly.SelfTerminal.ToString();
        //    }
        //    return EnumMzghly.His.ToString();
        //}
        //#endregion
    }
}
