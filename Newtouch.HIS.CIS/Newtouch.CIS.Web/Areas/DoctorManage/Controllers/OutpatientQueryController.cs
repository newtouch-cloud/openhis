using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.Domain.IDomainServices.Outpatient;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.DoctorManage.Controllers
{
    public class OutpatientQueryController : OrgControllerBase
    {
        // GET: DoctorManage/OutpatientQuery

        private readonly IOutpatientQueryDmnService _outpatientQueryDmnService;
        #region 页面
        /// <summary>
        /// 门诊就诊记录
        /// </summary>
        /// <returns></returns>
        public ActionResult ConsultRecord()
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrganizeId = this.OrganizeId;
            return View();
        }

        /// <summary>
        /// 门诊就诊详情
        /// </summary>
        /// <returns></returns>
        public ActionResult ConsultDetail()
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrganizeId = this.OrganizeId;
            return View();
        }
        /// <summary>
        /// 查询门诊详细资料
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryOutpatientDetail()
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrganizeId = this.OrganizeId;
            return View();
        }
        /// <summary>
        /// 查询门诊预约记录
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryOutpatientReservation()
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrganizeId = this.OrganizeId;
            return View();
        }
        #endregion

        #region 门诊就诊记录
        public ActionResult GetConsultRecordGridJson(Pagination pagination, string kssj, string jssj)
        {
            var data = new
            {
                rows = _outpatientQueryDmnService.GetConsultRecordGridJson(pagination, OrganizeId, kssj,jssj, this.UserIdentity.UserCode),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        #endregion

        #region 门诊就诊详情
        public ActionResult GetConsultDetailGridJson(Pagination pagination, string kssj, string jssj,string keyword)
        {
            var data = new
            {
                rows = _outpatientQueryDmnService.GetConsultDetailGridJson(pagination, OrganizeId, kssj, jssj, this.UserIdentity.UserCode, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        #endregion
        #region 查询门诊详细资料
        public ActionResult GetOutpatientDetailGridJson(Pagination pagination, string kssj, string jssj)
        {
            var data = new
            {
                rows = _outpatientQueryDmnService.GetOutpatientDetailGridJson(pagination, OrganizeId, kssj, jssj,this.UserIdentity.UserCode),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        public ActionResult GetOutpatientDetailMX(string mzh)
        {
            var rows = _outpatientQueryDmnService.GetOutpatientDetailMXGridJson(mzh, OrganizeId);
            return Content(rows.ToJson());
        }
        #endregion
        #region 查询门诊预约记录
        public ActionResult GetReservationGridJson(Pagination pagination, string kssj, string jssj,string xm)
        {
            var data = new
            {
                rows = _outpatientQueryDmnService.GetReservationGridJson(pagination, OrganizeId, kssj, jssj, this.UserIdentity.UserCode,xm),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        #endregion


		public ActionResult MZcasehistory()
		{
			ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
			ViewBag.OrganizeId = this.OrganizeId;
			return View();
		}
        #region 门诊病历记录
        public ActionResult GetMedicalRecordGridJson(Pagination pagination, string kssj, string jssj)
        {
            var data = new
            {
                rows = _outpatientQueryDmnService.GetConsultRecordGridJson(pagination, OrganizeId, kssj, jssj, null),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        #endregion
        public ActionResult MZBLlsinfo(Pagination pagination, string blh)
		{
			var data = new
			{
				rows = _outpatientQueryDmnService.GetlsblInfoJson(pagination, OrganizeId, blh, this.UserIdentity.UserCode),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records,
			};
			return Content(data.ToJson());
		}
		public ActionResult MZlsjzCfInfo(Pagination pagination, string jzid)
		{
			var data = new
			{
				rows = _outpatientQueryDmnService.GetlsjzcfblInfoJson(pagination, OrganizeId, jzid, this.UserIdentity.UserCode),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records,
			};
			return Content(data.ToJson());
		}
	}
}