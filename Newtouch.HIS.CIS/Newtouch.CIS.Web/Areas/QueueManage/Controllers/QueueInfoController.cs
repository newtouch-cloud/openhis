using FrameworkBase.MultiOrg.Web;
using Newtouch.Common.Web;
using Newtouch.Core.Common;
using Newtouch.Domain.IDomainServices.Queue;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Queue;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.QueueManage.Controllers
{
    public class QueueInfoController : OrgControllerBase
    {
        private readonly IQueueDmnService _queueDmnService;
        // GET: QueueManage/QueueInfo
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult GetQueueInfo(Pagination pagination, string qdsj, string ks, string ys, string ywbz, string ywlx, int? calledstu, string keyword)
        {
            DateTime kssj = Convert.ToDateTime(qdsj);
            DateTime jssj = Convert.ToDateTime(qdsj).AddDays(1).AddMilliseconds(-1);
            var data = new
            {
                rows = _queueDmnService.GetQueue(pagination,OrganizeId, kssj, jssj, ks, ys, ywbz, ywlx, calledstu, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// 排队状态修改
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="calledstu"></param>
        /// <returns></returns>
        public ActionResult CallNumber(string mzh, string calledstu)
        {
            var reqObj = new
            {
                mzh = mzh,
                calledstu = calledstu,
                yhcode = this.UserIdentity.rygh,
                orgId = this.OrganizeId
            };
            var apiResp = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<SignInStateRequest>>(
                 "api/SignInAppointment/SignInState", reqObj);
            if (apiResp.msg != null) return Success("接口调用失败【" + apiResp.msg + apiResp.sub_msg + "】");
            return Success();
        }

        public ActionResult getNextCall(string qdsj, string ks, string ys, string ywbz, string ywlx, int? calledstu, string keyword, string mzh) {
            //var callData = _queueDmnService.GetQueueByMzh(mzh, OrganizeId);
            DateTime kssj = Convert.ToDateTime(qdsj);
            DateTime jssj = Convert.ToDateTime(qdsj).AddDays(1).AddMilliseconds(-1);
            //var alldata = _inpatientOrderPackageRepo.IQueryable().Where(p => p.tcfw == tcfw && p.tclx == linqyzlx && p.OrganizeId == OrganizeId && p.zt == "1").OrderBy(a => a.CreateTime).ThenBy(a => a.tcmc).ToList();

            var allCallData = _queueDmnService.GetQueue(OrganizeId, kssj, jssj, ks, ys, ywbz, ywlx, calledstu, keyword).Where(a => a.calledstu == 2 && a.zt == "1").OrderByDescending(a => a.LastModifyTime);
            var callData = new QueueInfo();
            if (allCallData.Count() != 0)
            {
                callData = allCallData.FirstOrDefault();
            }
            var queueData = _queueDmnService.GetQueue(OrganizeId, kssj, jssj, ks, ys, ywbz, ywlx, calledstu, keyword).Where(a => a.calledstu == 1 && a.zt == "1").OrderBy(a => a.Period).ThenBy(a => a.queno);
            
            var nextData = new QueueInfo();
            if (queueData.Count() != 0) { 
                nextData = queueData.FirstOrDefault();
            }

            QueueVO vo = new QueueVO();
            //vo.sp_calling_no=callData.

                vo.sp_calling_no = callData.queno;
                vo.sp_calling_xm = callData.xm;
                vo.sp_calling_dept = callData.czksmc;
                vo.sp_calling_doc = callData.czysxm;
            vo.sp_calling_ywlx = callData.ywlx;
            vo.sp_nextcall_no = nextData.queno;
            vo.sp_nextcall_xm = nextData.xm;
            vo.sp_nextcall_dept = nextData.czksmc;
            vo.sp_nextcall_doc = nextData.czysxm;
            vo.sp_nextcall_ywlx = nextData.ywlx;
            vo.sp_waiting_count = queueData.Count();
            return Content(vo.ToJson());
        }
    }
}