using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.IDomainServices.ReportManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.HighchartsManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class InpatientController : ControllerBase
    {
        private readonly IHighchartsDmnService _highchartsDmnService;


        #region 页面初始化
        /// <summary>
        /// 住院就诊人次人数分析
        /// </summary>
        /// <returns></returns>
        public ActionResult NumberofInpatientVisits()
        {
            return View();
        }
        /// <summary>
        /// 住院收入分析
        /// </summary>
        /// <returns></returns>
        public ActionResult NumberofInpatientSalary()
        {
            return View();
        }

        public ActionResult InPatientVisitPerTherapist()
        {
            return View();
        }
        #endregion

        /// <summary>
        ///获取住院就诊人数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetInpatientVisitNum()
        {
            //获取住院新增人数
            List<OutpatientVisitNumVO> outpatientlist = _highchartsDmnService.GetInpatientVisitNum(OrganizeId);
            //获取出院人数
            List<InpatientVisitNumVO> inpatientList = _highchartsDmnService.GetInpatientDischargeNum(OrganizeId);
            //放在一个对象，返回到页面
            var data = new VisitNumBO
            {
                OutpatientList = outpatientlist,
                InpatientList = inpatientList
            };
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = data }.ToJson());
        }

        /// <summary>
        /// 获取住院收入统计
        /// </summary>
        /// <returns></returns>
        public ActionResult GetInpatientSalaryNum()
        {
            var data = _highchartsDmnService.GetInpatientSalaryNum(OrganizeId);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = data }.ToJson());
        }

        #region 住院费用报表
        /// <summary>
        /// 获取就诊费用
        /// </summary>
        public ActionResult GetVisitNum(string year, string datatype, string orgId)
        {
            var PatientVisitPerThe = new PatientVisitPerThe();
            if (string.IsNullOrWhiteSpace(year))
            {
                throw new FailedException("请选择年份！");
            }
            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("请选择组织机构！");
            }
            if (datatype == "avg")
            {
                PatientVisitPerThe.discharge = _highchartsDmnService.GetInTherapistavgDischarge(year, orgId);
                PatientVisitPerThe.visitper = _highchartsDmnService.GetInTherapistavgVisit(year, orgId);
            }
            else
            {
                PatientVisitPerThe.discharge = _highchartsDmnService.GetInTherapistDischarge(year, orgId);
                PatientVisitPerThe.visitper = _highchartsDmnService.GetInTherapistVisit(year, orgId);
            }

            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = PatientVisitPerThe }.ToJson());
        }

        public ActionResult GetMonthVisitNum(string year, string datatype, string orgId, int month)
        {
            var PatientVisitPerThe = new PatientMonthVisitPerThe();
            if (string.IsNullOrWhiteSpace(year))
            {
                throw new FailedException("请选择年份！");
            }
            if (string.IsNullOrWhiteSpace(datatype))
            {
                throw new FailedException("请选择类型！");
            }
            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("请选择组织机构！");
            }
            if (datatype == "avg")
            {
                PatientVisitPerThe.discharge = _highchartsDmnService.GetMonthTherapistavgDischarge(year, orgId, month < 10 ? "0" + month : month + "");
                PatientVisitPerThe.visitper = _highchartsDmnService.GetMonthInTherapistavgVisit(year,month < 10 ? "0" + month : month + "", orgId);
            }
            else
            {
                PatientVisitPerThe.discharge = _highchartsDmnService.GetMonthInTherapistDischarge(year, orgId, month < 10 ? "0" + month : month + "");
                PatientVisitPerThe.visitper = _highchartsDmnService.GetMonthInTherapistVisit(year, month < 10 ? "0" + month : month + "", orgId);
            }

            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = PatientVisitPerThe }.ToJson());
        }
        #endregion
    }
}