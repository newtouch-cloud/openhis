using Autofac;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.API;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Sett.Request;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using Newtouch.HIS.API.Common.Filter;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Sett.Request.Booking.Request;
using System.Configuration;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.InputDto;
using System.Collections.Generic;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Sett.Request.Booking.Response;
using Newtouch.Common.Web;
using System.Threading.Tasks;
using Newtouch.HIS.Proxy.Log;
using Newtouch.Tools;
using Newtouch.HIS.Sett.API.Attribute;

namespace Newtouch.HIS.Sett.API.Controllers
{
    [RoutePrefix("api/BookingRegister")]
    public class BookingRegisterController : ApiControllerBase<BookingRegisterController>
    {
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly IBookingRegisterDmnService _BookingRegisterDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly ISysCardRepo _SysCardRepo;
        private readonly IPatientBasicInfoDmnService _PatientBasicInfoDmnService;
        private readonly ISysPatientNatureRepo _SysPatientNatureRepo;
        private readonly ISysPatientBasicInfoRepo _SysPatientBasicInfoRepo;
        private readonly IBaseDataDmnService _baseDataDmnService;
        private readonly IMzghbookRepo _MzghbookRepo;
        private readonly IInsideGeneralApp _insideGeneralApp;
        private readonly IOutPatChargeApp _outPatChargeApp;
        private readonly IOutpatientRegistRepo _ioutpatientRegistRepo;
        private readonly IOutPatientDmnService _outPatientDmnService;
        private readonly IOutpatientPrescriptionRepo _outpatientPrescriptionRepo;
        private string user;

        public BookingRegisterController(IComponentContext com)
            : base(com)
        {
        }
        [HttpPost]
        [Route("Y001")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage GetCardInfo(CardInfoReq req)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _BookingRegisterDmnService.GetCardInfo(req);
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list.Count ==0)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        var FirstVisit = ConfigurationManager.AppSettings["FirstVisit"];
                        if (FirstVisit == "Y")
                        {
                            resp.code = ResponseResultCode.ERROR;
                            resp.msg = "未找到相关记录,请新建患者卡信息";
                        }
                        else
                        {
                            resp.msg = "未找到相关记录,初次就诊需要先线下医院就诊";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resp.msg = ex.Message;
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }
        /// <summary>
        /// 出诊科室信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Y002")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage SysDepartmentInfo(DepartmentDTO req)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _BookingRegisterDmnService.GetDepartmentInfo(req);
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list.Count ==0)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "未找到相关记录";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.msg = ex.Message;
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }
        /// <summary>
        /// 一段时间内排班
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Y003")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage GetMzpbSchedule(DepartmentSchedulingDTO req)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _BookingRegisterDmnService.GetMzpbSchedule(req);
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list.Count ==0)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "未找到相关记录";
                    }
                }
            }
            catch (Exception ex) {
                resp.msg = ex.Message;
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }

        /// <summary>
        /// 科室排班信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Y004")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage GetMzpb(MzKsPbDto dto)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _BookingRegisterDmnService.GetMzpb(dto);
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list.Count == 0)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "未找到相关记录";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.msg = ex.Message;
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }

        /// <summary>
        /// 出诊科室排班详细
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Y005")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage GetMzpbDetail(MzKsPbDto dto)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _BookingRegisterDmnService.GetMzpbDetail(dto);
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list.Count == 0)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "未找到相关记录";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.msg = ex.Message;
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }
        /// <summary>
        /// 预约挂号
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Y006")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage OutAppointment(MzAppointmentReq req)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _BookingRegisterDmnService.OutAppointment(req);
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list == null)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "该卡无法移动端预约，请窗口挂号！";
                    }
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(((Newtouch.Core.Common.Exceptions.FailedException)ex).Msg))
                {
                    resp.msg = ((Newtouch.Core.Common.Exceptions.FailedException)ex).Msg;
                }
                else
                {
                    resp.msg = ex.Message;
                }
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }
        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Y007")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage OutpatRegSett(RegSettReq req)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _BookingRegisterDmnService.OutpatRegSett(req);
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list == null)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "结算失败,交易流水号为空,请重试!";
                    }
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(((Newtouch.Core.Common.Exceptions.FailedException)ex).Msg))
                {
                    resp.msg = ((Newtouch.Core.Common.Exceptions.FailedException)ex).Msg;
                }
                else
                {
                    resp.msg = ex.Message;
                }
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }
        /// <summary>
        /// 预约号查询预约信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Y008")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage QueryMzBookIngRecord(MzAppointmentRecordReq req)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _BookingRegisterDmnService.QueryAppRecord(req);
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list == null)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "未找到预约记录";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.msg = ex.Message;
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }

        /// <summary>
        /// 查询预约记录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Y009")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage QueryMzBookIngRecordList(MzAppointmentRecordListReq req)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _BookingRegisterDmnService.QueryAppRecordList(req);
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list.Count == 0)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "未找到预约记录";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.msg = ex.Message;
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }
        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Y010")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage CancelOutApp(MzAppointmentRecordReq req)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    int cnt=_BookingRegisterDmnService.CancelOutApp(req);
                    resp.data = null;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (cnt==0)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "取消预约失败,请重试";
                    }
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(((Newtouch.Core.Common.Exceptions.FailedException)ex).Msg))
                {
                    resp.msg = ((Newtouch.Core.Common.Exceptions.FailedException)ex).Msg;
                }
                else
                {
                    resp.msg = ex.Message;
                }
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }
        /// <summary>
        /// 出诊科室信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Y011")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage SysDoctorDeptInfo(DeptDoctorReq req)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _BookingRegisterDmnService.GetStaffInfo(req);
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list.Count == 0)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "未找到相关记录";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.msg = ex.Message;
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }
        /// <summary>
        /// 建卡
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Y012")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage SysPatInfoSet(RegisterReq req)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _BookingRegisterDmnService.SysPatInfoSet(req);
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list ==null)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "建卡信息登记失败,请重试";
                    }
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(((Newtouch.Core.Common.Exceptions.FailedException)ex).Msg))
                {
                    resp.msg = ((Newtouch.Core.Common.Exceptions.FailedException)ex).Msg;
                }
                else
                {
                    resp.msg = ex.Message;
                }
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }
        /// <summary>
        /// 取消结算
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Y013")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage CancalSett(CancalSettReq req)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _BookingRegisterDmnService.CancalSett(req);
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list == null)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "取消结算失败,请重试";
                    }
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(((Newtouch.Core.Common.Exceptions.FailedException)ex).Msg))
                {
                    resp.msg = ((Newtouch.Core.Common.Exceptions.FailedException)ex).Msg;
                }
                else
                {
                    resp.msg = ex.Message;
                }
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }
        /// <summary>
        /// 待缴费账单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Y014")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage QueryCostOrder(CostOrderReq req) {

            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _BookingRegisterDmnService.QueryCostOrder(req);
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list.Count == 0)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "未找到相关记录";
                    }
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(((Newtouch.Core.Common.Exceptions.FailedException)ex).Msg))
                {
                    resp.msg = ((Newtouch.Core.Common.Exceptions.FailedException)ex).Msg;
                }
                else
                {
                    resp.msg = ex.Message;
                }
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }
        /// <summary>
        /// 待缴费明细
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Y015")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage QueryCostOrderDetail(CostOrderDetailReq req)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _BookingRegisterDmnService.QueryCostOrderDetail(req);
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list==null||list.OrderDetailData.Count==0)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "未找到相关待缴费账单";
                    }
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(((Newtouch.Core.Common.Exceptions.FailedException)ex).Msg))
                {
                    resp.msg = ((Newtouch.Core.Common.Exceptions.FailedException)ex).Msg;
                }
                else
                {
                    resp.msg = ex.Message;
                }
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Y016")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage CancalOrde(CancalOrderReq req)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _BookingRegisterDmnService.CancalOrde(req);
                    resp.data = null;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list == 0)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "取消订单失败或订单不存在";
                    }
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(((Newtouch.Core.Common.Exceptions.FailedException)ex).Msg))
                {
                    resp.msg = ((Newtouch.Core.Common.Exceptions.FailedException)ex).Msg;
                }
                else
                {
                    resp.msg = ex.Message;
                }
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }
        /// <summary>
        /// 挂号/处方预结算
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Y017")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage OutPreSett(PreSettReq req)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _BookingRegisterDmnService.OutPreSett(req);
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list ==null)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "未存在可收费订单";
                    }
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(((Newtouch.Core.Common.Exceptions.FailedException)ex).Msg))
                {
                    resp.msg = ((Newtouch.Core.Common.Exceptions.FailedException)ex).Msg;
                }
                else
                {
                    resp.msg = ex.Message;
                }
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }
        /// <summary>
        /// 处方结算
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Y018")]
        [IgnoreTokenDecrypt]
        [SimulatedUserLogin]
        public HttpResponseMessage OutSett(SettReq req)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var orgId = ConfigurationManager.AppSettings[req.HospitalID];
                    var ghEntity = _ioutpatientRegistRepo.FindEntity(p => p.mzh == req.Mzh && p.OrganizeId == orgId && p.zt == "1");

                    if (ghEntity == null)
                        throw new FailedException("不存在该门诊号的挂号记录");
                    if (!string.IsNullOrWhiteSpace(req.PatType))
                    {
                        if (req.PatType != ((int)EnumBrxz.zf).ToString() && ghEntity.brxz == ((int)EnumBrxz.zf).ToString())
                        {
                            throw new FailedException("入参:PatType 值错误,该人员性质为自费");
                        }
                    }
                    var validCf = _BookingRegisterDmnService.ValidFee(ghEntity.mzh, orgId);
                    if (!string.IsNullOrWhiteSpace(validCf))
                        throw new FailedException("门诊号:" + req.Mzh + "的处方订单已发生改变，请重新拉取费用订单");

                    var amount = _BookingRegisterDmnService.GetOrderAmount(req.Mzh, orgId);
                    if (amount.Count == 0)
                        throw new FailedException("订单中心无未结算订单");
                    if (amount.Count > 1)
                        throw new FailedException("订单中心存在多条未结订单,请取消订单或重新查询代缴费订单");
                    var jsje = amount.FirstOrDefault().TotalAmount;
                    if (jsje != req.TotalAmount)
                        throw new FailedException("收费金额异常，HIS为：【" + jsje + "】传入金额为：【" + req.TotalAmount + "】");

                    if (ghEntity.brxz != ((int)EnumBrxz.zf).ToString() && req.PatType != ((int)EnumBrxz.zf).ToString())
                    {
                        //走医保
                        throw new FailedException("医保收费未开通");
                    }
                    OutpatientSettFeeRelatedDTO feeRelated = new OutpatientSettFeeRelatedDTO()
                    {
                        zje = Convert.ToDecimal(req.TotalAmount),
                        ssk = Convert.ToDecimal(req.PayFee),
                        zffs1 = (req.AppID == "Alipay" ? "10" : "11"),//11微信,10支付宝
                        zfje1 = Convert.ToDecimal(req.PayFee),
                        zhaoling = 0
                    };
                    BasicInfoDto2018 patInfo = _BookingRegisterDmnService.getPatInfo(ghEntity.mzh, orgId);
                    var cfnmlist = _BookingRegisterDmnService.getCfnmList(ghEntity.mzh, orgId);
                    if (!(cfnmlist != null || cfnmlist.Count > 0))
                    {
                        throw new FailedException("该门诊号无可收费数据");
                    }
                    try {
                        patInfo.fph = _outPatientDmnService.GetInvoiceListByEmpNo(req.AppID, orgId);
                    }
                    catch (Exception e) { }
                    string outTradeNo = "";
                    IList<int> jsnmList;
                    CQMzjs05Dto ybfeeRelated = new CQMzjs05Dto();
                    var resultnewjs = _outPatChargeApp.submitOutpatCharge(patInfo, feeRelated, ybfeeRelated, null, null, orgId, cfnmlist, out jsnmList, null, outTradeNo);
                    if (resultnewjs)
                    {
                        _BookingRegisterDmnService.OutSett(req, jsnmList[0],orgId);
                        var res = new
                        {
                            jsnm = jsnmList[0]
                        };
                        if (cfnmlist != null && cfnmlist.Count > 0)
                        {
                            var toCIS = _sysConfigRepo.GetBoolValueByCode("Outpatient_ChargeFee_AutoSyncPrescriptionStatus", orgId) ?? false;
                            if (toCIS)
                            {
                                var cfList = cfnmlist.Distinct()
                                    .Select(p =>
                                    {
                                        var cfzbEntity = _outpatientPrescriptionRepo.GetValidEntityByCfnm(orgId, p);
                                        return cfzbEntity != null && cfzbEntity.cfly == "1" ? cfzbEntity.cfh : null;
                                    })
                                    .Where(p => !string.IsNullOrWhiteSpace(p))
                                    .Select(p => new PrescriptionChargeStatusUpdateRequestDTO
                                    {
                                        cfh = p
                                    }).ToList();
                                if (cfnmlist.Count > 0)
                                {
                                    var reqObj = new
                                    {
                                        cfList = cfList,
                                        sfbz = true,
                                    };
                                    var apiResp = SiteCISAPIHelper.Request<APIRequestHelper.DefaultResponse>(
                                        "api/Prescription/UpdateChargeStatus", reqObj, autoAppendToken: false);
                                    if (apiResp != null)
                                    {
                                        AppLogger.Info(string.Format("处方收费状态更新API同步至CIS，处方号：{0}，结果：{1}、{2}", string.Join(",", cfnmlist), apiResp.code, apiResp.sub_code));
                                    }
                                    else
                                    {
                                        AppLogger.Info(string.Format("处方收费状态更新API同步至CIS，处方号：{0}，结果：未获取到响应，同步失败", string.Join(",", cfnmlist)));
                                    }
                                }
                            }
                        }
                        //异步推送处方收费成功的通知给药房药库(取消mq的用法)
                        //notifyPDS(jsnmList[0], DateTime.Now, cfnmlist, patInfo.fph, orgId, req.AppID);
                        resp.data = new SettResp() { RegId = jsnmList[0].ToString(), xjzf = req.TotalAmount, ybzf = 0.00.ToDecimal(), grzf = req.TotalAmount };
                        resp.msg = "";
                        resp.code = ResponseResultCode.SUCCESS;
                    }
                    else
                    {
                        //CancalOrderReq cancalreq = new CancalOrderReq();
                        //cancalreq.AppID = req.AppID;
                        //cancalreq.HospitalID = req.HospitalID;
                        //cancalreq.Mzh = req.Mzh;
                        //cancalreq.CardNo = req.Mzh;
                        //cancalreq.OrderNo = req.OrderNo;
                        //var i = _BookingRegisterDmnService.ErrOrderCancal(cancalreq);

                        resp.data = null;
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "结算失败,请重试";
                    }

                }
            }
            catch (Exception ex)
            {
                //CancalOrderReq cancalreq = new CancalOrderReq();
                //cancalreq.AppID = req.AppID;
                //cancalreq.HospitalID = req.HospitalID;
                //cancalreq.Mzh = req.Mzh;
                //cancalreq.CardNo = req.Mzh;
                //cancalreq.OrderNo = req.OrderNo;
                //var i = _BookingRegisterDmnService.ErrOrderCancal(cancalreq);

                resp.msg = ex.Message;
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }
        /// <summary>
        /// 对账
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Y019")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage ContrastBill(ContrastBillReq req)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _BookingRegisterDmnService.ContrastBill(req);
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list.Count == 0)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "未找到账单信息";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.msg = ex.Message;
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }

        /// <summary>
        /// 结算成功推送状态至药房
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="sfrq"></param>
        /// <param name="cfnmList"></param>
        /// <param name="fph"></param>
        /// <param name="orgId"></param>
        /// <param name="userCode"></param>
        private void notifyPDS(int jsnm, DateTime sfrq, IList<int> cfnmList, string fph, string orgId, string userCode)
        {
            if (cfnmList == null || cfnmList.Count == 0)
            {
                return;
            }
            try
            {
                //cfnm cfh
                var ypCflx = (int)EnumPrescriptionType.Medicine;
                var ypcfList = _outpatientPrescriptionRepo.IQueryable(p => cfnmList.Contains(p.cfnm) && p.cflx == ypCflx).ToList();

                if (ypcfList.Count > 0)
                {
                    var creatorCode = userCode;
                    var context = System.Web.HttpContext.Current;
                    foreach (var cf in ypcfList)
                    {
                        Task.Run(() =>
                        {
                            var reqObj = new
                            {
                                Jsnm = jsnm,
                                Sfsj = sfrq,
                                Cfh = cf.cfh,
                                Cfnm = cf.cfnm,
                                Fph = fph,
                                OrganizeId = orgId,
                                CreatorCode = creatorCode,
                                TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")//,
                                //Token = token
                            };
                            var apiResp = SitePDSAPIHelper.Request<APIRequestHelper.DefaultResponse>("api/ResourcesOperate/OutpatientCommit", reqObj, autoAppendToken: false, httpContext: context);

                            LogCore.Info("OutpatientCommit response", apiResp.ToJson());
                            AppLogger.Info(apiResp != null
                                ? string.Format("HIS收费后药品推送至药房，处方号：{0}，结果：{1}、{2}", reqObj.Cfh, apiResp.code,
                                    apiResp.sub_code)
                                : string.Format("HIS收费后药品推送至药房，处方号：{0}，结果：未获取到响应，同步失败", reqObj.Cfh));
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                LogCore.Error("HIS结算后推送到药房接口失败", ex);
            }
        }
    }
}
