using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Common.Web;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.DTO.OutputDto.Outpatient;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.PDS.Requset.Zyypyz;
using Newtouch.Tools;
using Newtouch.Domain.ValueObjects.Apply;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    /// <summary>
    /// 医嘱执行
    /// </summary>
    public class OrderExecutionController : OrgControllerBase
    {

        private readonly IOrderExecutionDmnService _OrderExecutionDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IInpatientPatientInfoRepo _inpatientPatientInfoRepo;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly IDoctorserviceDmnService _doctorserviceDmnService;
        int lyxh = 0;
        private string IsRehabAuthtoNurse;
        private bool isNurse;
        private bool isRehabDoctor;

        /// <summary>
        /// 文字医嘱需要执行
        /// </summary>
        private bool wnes;//true-文字医嘱需要执行  false-文字医嘱无需执行
        private string medicalInsurance;
        private string iskfyzjf;//康复医嘱是否计费

        public OrderExecutionController(IOrderExecutionDmnService OrderExecutionDmnService)
        {
            this._OrderExecutionDmnService = OrderExecutionDmnService;
            var wnesV = _sysConfigRepo.GetValueByCode("wordsNeedExecuteSwitch", OrganizeId);
            if (string.IsNullOrWhiteSpace(wnesV)) wnes = false;
            if ("true".Equals(wnesV.ToLower().Trim()) || "t".Equals(wnesV.ToLower().Trim())) wnes = true;
            IsRehabAuthtoNurse = _sysConfigRepo.GetValueByCode("IsRehabAuthtoNurse", this.OrganizeId);
            isNurse = _sysUserDmnService.CheckStaffIsBelongDuty(UserIdentity.StaffId, "Nurse");
            isRehabDoctor = _sysUserDmnService.CheckStaffIsBelongDuty(UserIdentity.StaffId, "RehabDoctor");
            medicalInsurance = _sysConfigRepo.GetValueByCode("medicalInsurance", OrganizeId);
            iskfyzjf = _sysConfigRepo.GetValueByCode("iskfyzjf", OrganizeId);
            ViewBag.isqfswith = _sysConfigRepo.GetValueByCode("accountqfexecute_switch", OrganizeId);//欠费医嘱开立、执行开关
        }

        /// <summary>
        /// 获取待执行医嘱列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="patList"></param>
        /// <param name="organizeId"></param>
        /// <param name="zxsj"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(Pagination pagination, string patList, string organizeId, string zxsj)
        {
            IList<OrderExecutionVO> rowData = new List<OrderExecutionVO>();
            if (!string.IsNullOrWhiteSpace(patList))
            {
                if (!string.IsNullOrWhiteSpace(IsRehabAuthtoNurse) && IsRehabAuthtoNurse == "0")
                {
                    if (isNurse && isRehabDoctor)
                    {
                        rowData = _OrderExecutionDmnService.GetOrderExecutionYZList(ref pagination, patList, OrganizeId, zxsj, wnes);
                    }
                    else if (isRehabDoctor) 
                    {
                        rowData = _OrderExecutionDmnService.GetOrderExecutionYZList(ref pagination, patList, OrganizeId, zxsj, wnes, IsRehabAuthtoNurse, true,this.UserIdentity.DepartmentCode);
                    }
                    else if(isNurse)
                    {
                        rowData = _OrderExecutionDmnService.GetOrderExecutionYZList(ref pagination, patList, OrganizeId, zxsj, wnes, IsRehabAuthtoNurse, false);
                    }
                }
                else {
                    rowData = _OrderExecutionDmnService.GetOrderExecutionYZList(ref pagination, patList, OrganizeId, zxsj, wnes);
                }
            }
            var data = new
            {
                rows = rowData,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        // GET: NurseManage/OrderExecution
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取病区患者待执行医嘱树
        /// </summary>
        /// <param name="aa"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetPatWardTree(string aa, DateTime zxsj)
        {
            var staffId = UserIdentity.StaffId;
            var wardTree = _OrderExecutionDmnService.GetWardTree(staffId);
            var patTree = _OrderExecutionDmnService.GetPatTree(staffId, zxsj, wnes, OrganizeId);//wnes ? _OrderExecutionDmnService.GetPatTreeIncludeWzyz(staffId, zxsj)
                                                                                                //: _OrderExecutionDmnService.GetPatTree(staffId, zxsj);
            string[] aasz = new string[200];
            if (aa != "")
            {
                aasz = aa.Split(',');
            }

            var treeList = new List<TreeViewModel>();
            foreach (var item in wardTree)
            {
                var patInfo = patTree.Where(p => p.bqCode == item.bqCode).ToList();

                foreach (var itempat in patInfo)
                {
                    var treepat = new TreeViewModel
                    {
                        id = itempat.zyh,
                        text = itempat.zyh + "-"+itempat.BedNo +"-"+ itempat.hzxm,
                        value = itempat.zyh,
                        parentId = item.bqCode,
                        isexpand = false,
                        complete = true,
                        showcheck = true,
                        //checkstate = 0,
                        checkstate=0,
                        hasChildren = false,
                        Ex1 = "c"
                    };
                    if (((IList)aasz).Contains(itempat.zyh))
                    {
                        treepat.checkstate = 1;
                    }
                    treeList.Add(treepat);
                }

                var tree = new TreeViewModel
                {
                    id = item.bqCode,
                    text = item.bqmc,
                    value = item.bqCode,
                    parentId = null,
                    isexpand = true,
                    complete = true,
                    showcheck = true,
                    checkstate = 0,
                    hasChildren = patInfo.Count != 0,
                    Ex1 = "p"
                };
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }
        #region 执行当前
        /// <summary>
        /// 执行当前医嘱
        /// </summary>
        /// <param name="orderList">yzid,yzxh,zyh,yzlx</param>
        /// <param name="Vzxsj">执行时间</param>
        /// <returns></returns>
        public ActionResult submitOrderExecutionList(string patlist, IList<ApiResponseVO> orderList, DateTime Vzxsj)
        {
            Validatepatryrq(patlist, Vzxsj);
            //调用接口返回
            var result = doOrderExecution(orderList, Vzxsj);
            if (result.Split('|')[0] != "T") return Error(result.Split('|')[1]);
            var cnt = orderList.Where(a => a.yzlx == Convert.ToInt32(EnumYzlx.Yp) || a.yzlx == Convert.ToInt32(EnumYzlx.Cydy) || a.yzlx == Convert.ToInt32(EnumYzlx.zcy)).ToList().Count;
            var data = new { cnt = cnt, lyxh = lyxh };
            return Success(result.Split('|')[1], data.ToJson());
        }

        /// <summary>
        /// 构造并调用接口
        /// </summary>
        /// <param name="orderListAll">Apilist</param>
        /// <param name="Vzxsj">执行时间</param>
        /// <returns></returns>
        public string doOrderExecution(IList<ApiResponseVO> orderListAll, DateTime Vzxsj, int? yzxz=null)
        {
            try
            {
                var user = UserIdentity;
                //可以执行的医嘱
                var isOkOrderExecutionresult = _OrderExecutionDmnService.IsOKOrderExecution(orderListAll, Vzxsj,UserIdentity.rygh);
                if (isOkOrderExecutionresult.Split('|')[0] != "T") return isOkOrderExecutionresult;
                //药品医嘱(推送药房)
                IList<ApiResponseVO> orderYpList = orderListAll.Where(a => (a.yzlx == Convert.ToInt32(EnumYzlx.Yp) || a.yzlx == Convert.ToInt32(EnumYzlx.Cydy) || a.yzlx == Convert.ToInt32(EnumYzlx.zcy)) 
                && a.isjf != EnumSF.f.GetDescription()&& a.yply!=EnumYply.ksby.GetDescription()).ToList();
                //项目医嘱
                IList<ApiResponseVO> orderXmList = orderListAll.Where(a => (a.yzlx != Convert.ToInt32(EnumYzlx.Yp) && a.yzlx != Convert.ToInt32(EnumYzlx.Cydy) && a.yzlx != Convert.ToInt32(EnumYzlx.zcy)) ||a.yfztbs!=null).ToList();
                //不计费医嘱、科室备药医嘱 
                IList<ApiResponseVO> nofeeorderYpList = orderListAll.Where(a => (a.yzlx == Convert.ToInt32(EnumYzlx.Yp) || a.yzlx == Convert.ToInt32(EnumYzlx.Cydy) || a.yzlx == Convert.ToInt32(EnumYzlx.zcy)) 
                && (a.isjf == EnumSF.f.GetDescription()||a.yply== EnumYply.ksby.GetDescription())).ToList();
                //领药序号
                lyxh = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueIntValue("fyqqk_lyxh", OrganizeId);

                if (orderYpList.Count > 0)
                {
                    //构造api接口 RequestJson
                    var orderList = _OrderExecutionDmnService.GetapiList(user, orderYpList, Vzxsj, lyxh);
                    var orderExecution = new
                    {
                        OrganizeId = this.OrganizeId,
                        yzList = orderList,
                        ClientNo = Guid.NewGuid(),
                        TimeStamp = DateTime.Now.ToString(CultureInfo.InvariantCulture)
                    };
                    var apiOrderExecution = SiteYfykAPIHelper.Request<APIRequestHelper.DefaultResponse>("/api/Zyypyz/Yzzx", orderExecution);
                    if (apiOrderExecution.code == APIRequestHelper.ResponseResultCode.SUCCESS && apiOrderExecution.data != null)
                    {
                        var successDoOrder = apiOrderExecution.data.ToString().ToObject<RequestOrderExecMsgDto>(); //接口返回数据 
                        if (successDoOrder.Data != null && successDoOrder.IsSucceed && successDoOrder.Data.Count > 0)
                        {
                            string resultMsg = "";
                            List<YzDetail> successDoOrderYp = Tools.Json.ToList<YzDetail>(successDoOrder.Data.ToString());
                            if (successDoOrderYp.Count >= 30)
                            {
                                AppLogger.Info("药房执行成功："+successDoOrderYp.ToArray().ToJson());
                                //防止截断 
                                var zyhlist = successDoOrderYp.Select(p=>p.zyh).Distinct();
                                foreach (string zyh in zyhlist)
                                {
                                    var zyhorder = successDoOrderYp.FindAll(p => p.zyh == zyh);
                                    if (zyhorder.Count > 0)
                                    {
                                        resultMsg += _OrderExecutionDmnService.OrderExecutionSubmit(user, zyhorder, lyxh, Vzxsj);
                                        resultMsg += "【" + zyh + "】";
                                        AppLogger.Info(resultMsg);
                                    }                                    
                                }
                            }
                            else {
                                resultMsg = _OrderExecutionDmnService.OrderExecutionSubmit(user, successDoOrderYp, lyxh, Vzxsj);
                                if (resultMsg.Split('|')[0] != "T")
                                {
                                    return resultMsg;
                                }
                            }
                        }
                        else
                        {
                            return "F|" + successDoOrder.ResultMsg;
                        }

                    }
                    else
                    {
                        return "F|" + apiOrderExecution.sub_msg;
                    }
                }
                if (nofeeorderYpList.Count > 0)
                {
                    var resultMsg = _OrderExecutionDmnService.NoFeeOrderExecutionSubmit(user, nofeeorderYpList, lyxh, Vzxsj);
                    if (resultMsg.Split('|')[0] != "T")
                    {
                        return resultMsg;
                    }
                }
                if (orderXmList.Count <= 0) return "T|执行成功";
                //项目执行
                var xmMsg = wnes ? _OrderExecutionDmnService.OrderExecutionXmWithWzyz(user.rygh, orderXmList, lyxh, Vzxsj,this.OrganizeId,yzxz)
                    : _OrderExecutionDmnService.OrderExecutionXM(user, orderXmList, lyxh, Vzxsj,this.OrganizeId, medicalInsurance,yzxz);
                return xmMsg.Split('|')[0] != "T" ? xmMsg : "T|执行成功";

            }
            catch (Exception ex)
            {
                return "F|" + ex.InnerException;
            }
        }
        #endregion

        #region 执行临时、 长期、全部
        /// <summary>
        /// 执行临时，长期，全部医嘱
        /// </summary>
        /// <param name="patlist">住院号</param>
        /// <param name="yzxz">临时，长期，全部</param>
        /// <param name="Vzxsj"></param>
        /// <returns></returns>
        public ActionResult submitOrderExecutionListbyPat(string patlist, int yzxz, DateTime Vzxsj)
        {
            List<ApiResponseVO> apiList = new List<ApiResponseVO>();
            Validatepatryrq(patlist, Vzxsj);
            if (!string.IsNullOrWhiteSpace(IsRehabAuthtoNurse) && IsRehabAuthtoNurse == "0")
            {
                if (isNurse && isRehabDoctor)
                {
                    apiList = wnes ? _OrderExecutionDmnService.GetAllYZWithWzYz(OrganizeId, patlist, yzxz, Vzxsj) :
                             _OrderExecutionDmnService.GetAllYZ(patlist, yzxz, Vzxsj,IsRehabAuthtoNurse);//获取执行全部医嘱
                }
                else if (isRehabDoctor)
                {
                    apiList = _OrderExecutionDmnService.GetkfYz(OrganizeId, patlist, yzxz, Vzxsj, this.UserIdentity.DepartmentCode);
                }
                else if (isNurse)
                {
                    apiList = wnes ? _OrderExecutionDmnService.GetAllYZWithWzYz(OrganizeId, patlist, yzxz, Vzxsj,IsRehabAuthtoNurse) :
                             _OrderExecutionDmnService.GetAllYZ(patlist, yzxz, Vzxsj, IsRehabAuthtoNurse);//获取执行全部医嘱
                }
            }
            else
            {
                apiList = wnes ? _OrderExecutionDmnService.GetAllYZWithWzYz(OrganizeId, patlist, yzxz, Vzxsj) :
                        _OrderExecutionDmnService.GetAllYZ(patlist, yzxz, Vzxsj);//获取执行全部医嘱
            }

            //接口返回 list 
            var result = doOrderExecution(apiList, Vzxsj,yzxz);
            if (result.Split('|')[0] != "T") return Error(result.Split('|')[1]);
            var cnt = apiList.Where(a => a.yzlx == Convert.ToInt32(EnumYzlx.Yp) || a.yzlx == Convert.ToInt32(EnumYzlx.Cydy) || a.yzlx == Convert.ToInt32(EnumYzlx.zcy)).ToList().Count;
            var data = new { cnt = cnt, lyxh = lyxh };
            return Success(result.Split('|')[1], data.ToJson());

        }

        /// <summary>
        /// 执行医嘱时，验证入院日期
        /// </summary>
        /// <param name="patlist"></param>
        public void Validatepatryrq(string patlist, DateTime Vzxsj)
        {
            if (string.IsNullOrWhiteSpace(patlist))
            {
                throw new FailedException("缺少病人住院号");
            }
            string[] patarr = patlist.Split(',');
            if (patarr != null && patarr.Count() > 0)
            {
                for (int i = 0; i < patarr.Count(); i++)
                {
                    var patzyh = patarr[i];
                    if (patzyh == null || string.IsNullOrWhiteSpace(patzyh))
                    {
                        continue;
                    }
                    var parentity = _inpatientPatientInfoRepo.IQueryable().Where(p => p.zyh == patzyh && p.OrganizeId == OrganizeId && p.zt == "1").ToList();

                    if (parentity.Count() < 0)
                    {
                        throw new FailedException("住院号为" + patarr[i] + "的病人缺少住院信息");
                    }
                    if (parentity.Count() > 1)
                    {
                        throw new FailedException("住院号为" + patarr[i] + "的病人存在多条住院信息");
                    }

                    if (DateTimeManger.DateDiff(DateInterval.Day, DateTime.Parse(parentity[0].ryrq.ToShortDateString()), DateTime.Parse(Vzxsj.ToShortDateString())) < 0)
                    {
                        throw new FailedException("住院号" + patarr[i] + "的病人入院日期为" + parentity[0].ryrq.ToShortDateString() + "。无法在此之前执行医嘱，请核实");
                    }
                }
            }
        }
        #endregion
        #region pacs 接口
        public ActionResult pushApplicationform(IList<ApiResponseVO> orderList)
        {

			var uri = ConfigurationHelper.GetAppConfigValue("pacsUrl");
			List<CheckApplicationfromDTO> datalist = new List<CheckApplicationfromDTO>();
            foreach (var item in orderList)
            {
                if (item != null)
                {
                    var data = _OrderExecutionDmnService.pushApplicationform(item, this.OrganizeId, "Y");
                    if (data != null)
                    {
                        datalist.Add(data);
                    }

                }
            }
            var url = uri+"URISService/services/interface/requestorder";
            if (datalist == null)
            {
                return Success("无数据");
            }
            var msagss = "";
            foreach (var data in datalist)
            {
                string datajson = Tools.Json.ToJson(data);
				try
                {
                    System.Net.HttpWebRequest request = null;
                    System.Net.WebResponse response = null;

                    request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
                    request.ProtocolVersion = System.Net.HttpVersion.Version10;
                    request.Method = "POST";

                    request.ContentType = "application/json";
                    request.CookieContainer = null;//获取验证码时候获取到的cookie会附加在这个容器里面
                    request.AllowAutoRedirect = true;
                    request.KeepAlive = true;//建立持久性连接
                    //request.ContentLength = cs.Length;
                    request.Host = "192.168.0.101";
                    request.UserAgent = "PostmanRuntime/7.29.2";
                    request.Accept = "*/*";
                    byte[] datas = System.Text.Encoding.UTF8.GetBytes(datajson);
                    using (System.IO.Stream stream = request.GetRequestStream())
                    {
                        stream.Write(datas, 0, datas.Length);
                    }

                    response = (System.Net.HttpWebResponse)request.GetResponse();
                    string outputText = string.Empty;
                    using (System.IO.Stream responseStm = response.GetResponseStream())
                    {
                        System.IO.StreamReader redStm = new System.IO.StreamReader(responseStm, System.Text.Encoding.UTF8);
                        outputText = redStm.ReadToEnd();
                    }

                    var apiresp = JavaScriptJsonSerializerHelper.Deserialize<RefJson>(outputText);

					AppLogger.Info(string.Format("Pacs检查申请单入参：{0}，出参结果：{1}", datajson, outputText));

					if (apiresp == null||apiresp.status== "Failure")
                    {
                        msagss = apiresp.errorCode;
                        AppLogger.Info(string.Format("Pacs检查申请单入参：{0}，出参结果：{1}", datajson, outputText));
                    }
                    
                }
                catch (Exception e)
                {

                    return Success("失败", e.Message);
                }
            }

            return Success("");


        }
        #endregion


        #region 医技执行
        public ActionResult MedicalSkillExecution()
        {
            return View();
        }
        public ActionResult MedicalSkillQuery()
        {
            return View();
        }
        public ActionResult GetJyjcExecGridJson(Pagination pagination,DateTime kssj,DateTime jssj, string fylx, string hzlx,
            string sqdlx, string zxzt,string keyword=null)
        {
            var data = new
            {
                rows = _OrderExecutionDmnService.GetJyjcSqd(pagination,OrganizeId,kssj,jssj,zxzt,hzlx,fylx,sqdlx,keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// 批量执行
        /// </summary>
        /// <param name="jyjclist"></param>
        /// <returns></returns>
        public ActionResult jyjcExec(List<jyjcExecReq> jyjclist)
        {
            _doctorserviceDmnService.jyjcExec(jyjclist,OrganizeId,UserIdentity.rygh);
            return Success();
        }
        /// <summary>
        /// 取消执行
        /// </summary>
        /// <param name="jyjclist"></param>
        /// <returns></returns>
        public ActionResult CancaljyjcExec(List<string> jyjclist)
        {
            _doctorserviceDmnService.CancaljyjcExec(jyjclist, OrganizeId, UserIdentity.rygh);
            return Success();
        }
        
        public ActionResult GetJyjcExecRecordJson(Pagination pagination, DateTime kssj, DateTime jssj, string fylx, string hzlx,
           string sqdlx, string keyword = null)
        {
            var data = new
            {
                rows = _OrderExecutionDmnService.GetJyjcSqdRecord(pagination, OrganizeId, kssj, jssj, hzlx, fylx, sqdlx, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        #endregion
    }
}