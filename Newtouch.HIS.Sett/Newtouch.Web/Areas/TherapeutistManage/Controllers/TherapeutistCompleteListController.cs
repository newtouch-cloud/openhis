using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects.TherapeutistCompleteManage;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.TherapeutistManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class TherapeutistCompleteListController : ControllerBase
    {
        private readonly ITherapeutistCompleteDmnService _therapeutistCompleteDmnService;

        #region 页面初始化
        public ActionResult AddItem()
        {
            return View();
        }

        public ActionResult ArrangeWork()
        {
            return View();
        }

        public ActionResult TatisticWorkload()
        {
            return View();
        }

        public ActionResult AddArrange()
        {
            return View();
        }
        public ActionResult EditArrange()
        {
            return View();
        }

        public ActionResult WorkEfficiency()
        {
            return View();
        }
        #endregion

        #region 治疗师工作列表
        /// <summary>
        /// 查询治疗师工作列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetGridJson(Pagination pagination, string BeginDate, string EndDate, string hzxm, string zls, string dl)
        {
            var rows = _therapeutistCompleteDmnService.GetTherapeutistListWorkedList(pagination, BeginDate, EndDate, hzxm, zls, dl, OrganizeId);
            var list = new
            {
                rows = rows,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }
        #endregion

        #region 治疗师工作时间
        public ActionResult GetpbGridJson(Pagination pagination, int? year, int? month, string ysgh)
        {
            var rows = _therapeutistCompleteDmnService.GetTherapeutistWorkPlanList(pagination, year, month, ysgh, OrganizeId);
            var list = new
            {
                rows = rows,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        public ActionResult GetAllRehabDoctor()
        {
            var list = _therapeutistCompleteDmnService.GetAllRehabDoctor(OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 新增排班时，提交到数据库
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="orgId"></param>
        public ActionResult AddRehabDoctorRange(Dictionary<string, List<AddStaffPlanVO>> vo)
        {
            _therapeutistCompleteDmnService.SubmitPlan(vo, OrganizeId);
            return Success();
        }

        /// <summary>
        ///获取单个治疗师单个月份排班
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="orgId"></param>
        public ActionResult GetFormJson(string keyvalue)
        {
            var data = _therapeutistCompleteDmnService.GetFormJson(keyvalue, OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 编辑单个治疗师排班
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="orgId"></param>
        public ActionResult EditRehabDoctorRange(DoctorWorkingDaysPlanEntity entity, string keyvalue)
        {
            _therapeutistCompleteDmnService.EditRehabDoctorRange(entity, keyvalue, OrganizeId);
            return Success("操作成功。");
        }

        /// <summary>
        /// 删除排班
        /// </summary>
        /// <param name="keyvalue"></param>
        /// <returns></returns>
        public ActionResult DelArrange(string keyvalue)
        {
            _therapeutistCompleteDmnService.DelArrange(keyvalue);
            return Success("删除成功。");
        }
        #endregion

        #region 治疗师工作统计
        public ActionResult GetStaffReport(Pagination pagination, string ysgh, string year, string month)
        {
            var rows = _therapeutistCompleteDmnService.GetStaffReport(pagination, OrganizeId, ysgh, year, month);
            return Content(rows.ToJson());
        }
        #endregion

        /// <summary>
        /// 治疗师工作效率柱状图
        /// </summary>
        public ActionResult GetVisitSC()
        {
            SCNumBO BO = null;
            var userCode = OperatorProvider.GetCurrent().UserCode;
            var orgId = OrganizeId;
            if (string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            BO = _therapeutistCompleteDmnService.GetVisitSC(orgId);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = BO }.ToJson());
        }

        public ActionResult GetVisitDetailSC(Pagination pagination, string time, string type)
        {
            if (string.IsNullOrWhiteSpace(time) || string.IsNullOrWhiteSpace(type))
            {
                return Content("");
                //throw FailedException("类型或时间为空！");
            }
            time = (int.Parse(time) + 1).ToString();
            var data = _therapeutistCompleteDmnService.GetVisitDetailSC(pagination, OrganizeId, type, time);
            var list = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
    }
}