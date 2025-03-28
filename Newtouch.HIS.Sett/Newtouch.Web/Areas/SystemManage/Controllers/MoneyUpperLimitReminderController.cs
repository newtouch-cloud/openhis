using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class MoneyUpperLimitReminderController : ControllerBase
    {
        private readonly IMoneyUpperLimitReminderDmnService _moneyUpperLimitReminderDmnService;
        private readonly IMoneyUpperLimitReminderRepo _moneyUpperLimitReminderRepo;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysUserDmnService _sysUserDmnService;
        // GET: SystemManage/MoneyUpperLimitReminder
        public override ActionResult Index()
        {
            return View();
        }

        public override ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="reminderType"></param>
        /// <returns></returns>
        public ActionResult GetAllList(Pagination pagination, string keyword, string reminderType)
        {
            var list = new
            {
                rows = _moneyUpperLimitReminderDmnService.GetAllList(pagination, this.OrganizeId, keyword, reminderType),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sxtxId"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(MoneyUpperLimitReminderEntity entity, string sxtxId)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;            
            _moneyUpperLimitReminderRepo.SubmitForm(entity, OperatorProvider.GetCurrent().UserCode, sxtxId);
            return Success("操作成功。");
        }

        /// <summary>
        /// 新增or修改
        /// </summary>
        /// <param name="sxtxId"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string sxtxId)
        {
            Pagination pagination = new Pagination();
            pagination.sidx = "CreateTime desc";
            pagination.rows = 1;
            pagination.page = 1;
            var entity = _moneyUpperLimitReminderDmnService.GetAllList(pagination, this.OrganizeId, null, null, sxtxId).FirstOrDefault();
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sxtxId"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string sxtxId)
        {
            _moneyUpperLimitReminderRepo.DeleteForm(this.OrganizeId, sxtxId);
            return Success("操作成功。");
        }

        /// <summary>
        /// 科室浮层
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult SelectDepartmentList(string keyword)
        {
            var list = _sysDepartmentRepo.GetList(this.OrganizeId, "1", keyword);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 医生浮层
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult SelectTherapistList(string keyword, string ks)
         {
            var list = _sysUserDmnService.GetStaffByDutyCode(this.OrganizeId, "RehabDoctor");
            if (!string.IsNullOrEmpty(keyword))
            {
                list = list.Where(a => a.ks == ks && ( a.StaffName.Contains(keyword.Trim()) || a.StaffGh.Contains(keyword.Trim()))).ToList();
            }
            return Content(list.ToJson());
        }
    }
}