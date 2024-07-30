using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Common.Web;
using Newtouch.Core.Common;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.PDS.Requset.Zyypyz;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    public class OrderExecutionController : OrgControllerBase
    {

        private readonly IOrderExecutionDmnService _OrderExecutionDmnService;
        int lyxh = 0;
        public OrderExecutionController(IOrderExecutionDmnService OrderExecutionDmnService)
        {
            this._OrderExecutionDmnService = OrderExecutionDmnService;
        }
        public ActionResult GetGridJson(Pagination pagination, string patList, string organizeId,string zxsj)
        {
            var data = new
            {
                rows = _OrderExecutionDmnService.GetOrderExecutionYZList(pagination, patList,this.OrganizeId,zxsj),
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
        public ActionResult GetPatWardTree(string aa,DateTime zxsj)
        {
            string staffId = this.UserIdentity.StaffId;
            var wardTree = _OrderExecutionDmnService.GetWardTree(staffId);
            var patTree = _OrderExecutionDmnService.GetPatTree(staffId,zxsj);

            var treeList = new List<TreeViewModel>();
            foreach (InpWardPatTreeVO item in wardTree)
            {
                var patInfo = patTree.Where(p => p.bqCode == item.bqCode).ToList();

                foreach (InpWardPatTreeVO itempat in patInfo)
                {
                    TreeViewModel treepat = new TreeViewModel();
                    treepat.id = itempat.zyh;
                    treepat.text = itempat.zyh + "-" + itempat.hzxm;
                    treepat.value = itempat.zyh;
                    treepat.parentId = item.bqCode;
                    treepat.isexpand = false;
                    treepat.complete = true;
                    treepat.showcheck = true;
                    treepat.checkstate = 0;
                    treepat.hasChildren = false;
                    treepat.Ex1 = "c";
                    treeList.Add(treepat);
                }

                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = patInfo.Count == 0 ? false : true;
                tree.id = item.bqCode;
                tree.text = item.bqmc;
                tree.value = item.bqCode;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = 0;
                tree.hasChildren = hasChildren;
                tree.Ex1 = "p";
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }
        /// <summary>
        /// 执行当前医嘱
        /// </summary>
        /// <param name="orderList">yzid,yzxh,zyh,yzlx</param>
        /// <param name="zxsj">执行时间</param>
        /// <returns></returns>
        public ActionResult submitOrderExecutionList(IList<ApiResponseVO> orderList,DateTime Vzxsj)
        {
            //调用接口返回
            string result = doOrderExecution(orderList, Vzxsj);
            if (result.Split('|')[0].ToString() == "T")
            {
                var cnt = orderList.Where(a => (a.yzlx == Convert.ToInt32(EnumYzlx.Yp)) || (a.yzlx == Convert.ToInt32(EnumYzlx.Cydy))).ToList().Count();
                var data = new { cnt=cnt, lyxh = lyxh };
                return Success(result.Split('|')[1].ToString(), data.ToJson());
            }
            else
            {
                return Error(result.Split('|')[1].ToString());
            }

        }
        /// <summary>
        /// 构造并调用接口
        /// </summary>
        /// <param name="orderList">Apilist</param>
        /// <param name="zxsj">执行时间</param>
        /// <returns></returns>
        public string doOrderExecution(IList<ApiResponseVO> orderListAll, DateTime Vzxsj)
        {
            try
            {
                OperatorModel user = this.UserIdentity;
                //可以执行的医嘱
                string IsOKOrderExecutionresult = _OrderExecutionDmnService.IsOKOrderExecution(orderListAll, Vzxsj);
                if (IsOKOrderExecutionresult.Split('|')[0].ToString() == "T")
                {
                    IList<ApiResponseVO> orderYPList = orderListAll.Where(a => (a.yzlx == Convert.ToInt32(EnumYzlx.Yp)) || (a.yzlx == Convert.ToInt32(EnumYzlx.Cydy))).ToList();
                    IList<ApiResponseVO> orderXMList = orderListAll.Where(a => (a.yzlx != Convert.ToInt32(EnumYzlx.Yp)) && (a.yzlx != Convert.ToInt32(EnumYzlx.Wz)) && (a.yzlx != Convert.ToInt32(EnumYzlx.Cydy))).ToList();
                    ///领药序号
                    lyxh = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueIntValue("fyqqk_lyxh", this.OrganizeId);
                    
                    if (orderYPList.Count > 0)
                    {
                        ///构造api接口 RequestJson
                        List<YzDetail> orderList = _OrderExecutionDmnService.GetapiList(user, orderYPList, Vzxsj, lyxh);
                        var OrderExecution = new
                        {
                            OrganizeId = this.OrganizeId,
                            yzList = orderList,
                            ClientNo = Guid.NewGuid(),
                            TimeStamp = DateTime.Now.ToString(),
                            Token = SiteYfykAPIHelper.GetToken()
                        };
                        var apiOrderExecution = SiteYfykAPIHelper.Request<APIRequestHelper.DefaultResponse>("/api/Zyypyz/Yzzx", OrderExecution);
                        if (apiOrderExecution.code == APIRequestHelper.ResponseResultCode.SUCCESS && apiOrderExecution.data != null)
                        {
                            RequestOrderExecMsgDto successDoOrder = Tools.Json.ToObject<RequestOrderExecMsgDto>(apiOrderExecution.data.ToString()); //接口返回数据 
                            if (successDoOrder.Data != null && successDoOrder.IsSucceed == true && successDoOrder.Data.Count > 0)
                            {
                                List<YzDetail> successDoOrderYp = Tools.Json.ToList<YzDetail>(successDoOrder.Data.ToString());
                                string resultMsg = _OrderExecutionDmnService.OrderExecutionSubmit(user, successDoOrderYp,lyxh, Vzxsj);
                                if (resultMsg.Split('|')[0].ToString() != "T")
                                {
                                    return resultMsg;
                                }
                            }
                            else
                            {
                                return "F|" + successDoOrder.ResultMsg;
                            }
                            
                        }
                        else
                        {
                            return "F|调用药房药库接口失败";
                        }
                    }
                    if (orderXMList.Count > 0)
                    {
                        //项目执行
                        string xmMsg = _OrderExecutionDmnService.OrderExecutionXM(user, orderXMList,lyxh, Vzxsj);
                        if (xmMsg.Split('|')[0].ToString() != "T")
                        {
                            return xmMsg;
                        }
                    }
                    return "T|执行成功";
                }
                else
                {
                    return IsOKOrderExecutionresult;
                }
            }
            catch (Exception ex)
            {
                return "F|"+ex.InnerException.ToString();
            }
           

        }
        /// <summary>
        /// 执行临时，长期，全部医嘱
        /// </summary>
        /// <param name="patlist">住院号</param>
        /// <param name="yzxz">临时，长期，全部</param>
        /// <param name="zxsj"></param>
        /// <returns></returns>
        public ActionResult submitOrderExecutionListbyPat(string patlist, int yzxz,DateTime Vzxsj)
        {
            //获取执行全部医嘱
            List<ApiResponseVO> apiList = _OrderExecutionDmnService.GetAllYZ(patlist, yzxz,Vzxsj);
            //接口返回 list 
            string result = doOrderExecution(apiList, Vzxsj);
            if (result.Split('|')[0].ToString() == "T")
            {
                var cnt = apiList.Where(a => (a.yzlx == Convert.ToInt32(EnumYzlx.Yp)) || (a.yzlx == Convert.ToInt32(EnumYzlx.Cydy))).ToList().Count();
                var data = new { cnt = cnt, lyxh = lyxh };
                return Success(result.Split('|')[1].ToString(), data.ToJson());
            }
            else
            {
                return Error(result.Split('|')[1].ToString());
            }
        }
    }
}