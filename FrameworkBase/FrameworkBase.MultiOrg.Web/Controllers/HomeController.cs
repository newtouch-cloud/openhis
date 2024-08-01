using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.HIS.Web.Core.Attributes;
using Newtouch.Tools;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FrameworkBase.MultiOrg.Web.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    public abstract class HomeController : OrgControllerBase
    {
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public override ActionResult Index()
        {
            var _sysUserDmnService = DependencyDefaultInstanceResolver.GetInstance<ISysUserDmnService>();
            var _sysOrganizeDmnService = DependencyDefaultInstanceResolver.GetInstance<ISysOrganizeDmnService>();

            var nLanguageTypeKey = ConstantsBase.AppId + "_" + "Newtouch_language_type";
            var language_type = _sysUserDmnService.GetLanguageTypeByUserId(this.UserIdentity.UserId);   //用户属性
            if (!string.IsNullOrWhiteSpace(language_type))
            {
                WebHelper.WriteCookie(nLanguageTypeKey, language_type);
            }
            else
            {
                WebHelper.RemoveCookie(nLanguageTypeKey);
            }

            if (!string.IsNullOrWhiteSpace(this.UserIdentity.OrganizeId))
            {
                //当前Org名称（关联Staff）
                ViewBag.OrgName = _sysOrganizeDmnService.GetNameByOrgId(this.UserIdentity.OrganizeId);
            }
            else
            {

                //显示顶级组织机构的名称
                ViewBag.OrgName = _sysOrganizeDmnService.GetNameByOrgId(this.UserIdentity.TopOrganizeId);
            }

            if (!this.UserIdentity.IsRoot && !this.UserIdentity.IsAdministrator)    //恒true
            {
                //当前UserId 对应的所有 医疗机构 List
                var mediOrgList = _sysOrganizeDmnService.GetMedicalOrganizeListByUserId(this.UserIdentity.UserId);

                ViewBag.MediOrgList = mediOrgList;

                if (mediOrgList != null && mediOrgList.Count > 1)
                {
                    //是否页面加载时就必须选择Org
                    var needChooseOrg = WebHelper.GetCookie(ConstantsBase.AppId + "_" + "CookieKey_ChooseOrg");
                    ViewBag.NeedChooseOrg = !string.IsNullOrWhiteSpace(needChooseOrg);
                }
            }

            IndexBeforeReturnView();

            return View();
        }

        /// <summary>
        /// 切换医疗机构 视图
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult UserOrgChoose()
        {
            var _sysOrganizeDmnService = DependencyDefaultInstanceResolver.GetInstance<ISysOrganizeDmnService>();

            var mediOrgList = _sysOrganizeDmnService.GetMedicalOrganizeListByUserId(this.UserIdentity.UserId);

            ViewBag.MediOrgList = mediOrgList;

            return View();
        }

        /// <summary>
        /// 切换医疗机构
        /// </summary>
        /// <param name="dstnOrgId"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult SwithOrg(string dstnOrgId)
        {
            var _sysUserDmnService = DependencyDefaultInstanceResolver.GetInstance<ISysUserDmnService>();
            var _sysDepartmentRepo = DependencyDefaultInstanceResolver.GetInstance<ISysDepartmentRepo>();
            var _userRoleAuthDmnService = DependencyDefaultInstanceResolver.GetInstance<IUserRoleAuthDmnService>();

            var opr = OperatorProvider.GetCurrent();

            var staffList = _sysUserDmnService.GetStaffListByUserId(opr.UserId);

            var orgStaff = staffList.Where(p => p.OrganizeId == dstnOrgId).FirstOrDefault();

            if (orgStaff != null)
            {
                opr.UserName = orgStaff.Name;
                opr.DepartmentCode = orgStaff.DepartmentCode;
                opr.OrganizeId = orgStaff.OrganizeId;
                opr.StaffId = orgStaff.Id;
                opr.rygh = orgStaff.gh;
                opr.DepartmentName = _sysDepartmentRepo.GetNameByCode(orgStaff.DepartmentCode, orgStaff.OrganizeId);
                opr.token = null;

                var roleList = _userRoleAuthDmnService.GetUserRoleList(opr.UserId, orgStaff.OrganizeId);
                //170904 Administrator不再可以关联Staff，仅admin
                opr.RoleIdList = roleList.Select(p => p.Id).ToList();
                opr.IsHospAdministrator = roleList.Any(p => p.Code == "HospAdministrator");

                WebHelper.WriteCookie(ConstantsBase.AppId + "_" + "CookieKey_OrganizeId", orgStaff.OrganizeId);

                SwithOrgSuccessOprBeforeSave(opr);

                OperatorProvider.AddCurrent(opr);

                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "切换成功" }.ToJson());
            }
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = "切换失败" }.ToJson());
        }

        /// <summary>
        /// 切换医疗机构成功，登录身份 保存之前再处理
        /// </summary>
        /// <param name="opr"></param>
        public virtual void SwithOrgSuccessOprBeforeSave(OperatorModel opr)
        {

        }

        /// <summary>
        /// 欢迎首页（iframe）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult Default()
        {
            return View();
        }

        /// <summary>
        /// 页面Load 时间记录
        /// </summary>
        /// <param name="url"></param>
        /// <param name="enterTime"></param>
        /// <param name="ms"></param>
        /// <returns></returns>
        [TrackerIgnore]
        public virtual ActionResult PageLoadMoniter(string url, DateTime? enterTime, int ms)
        {
            var str = Newtouch.Tools.Json.ToJson(new
            {
                EnterTime = enterTime ?? DateTime.MinValue,
                CostMSec = ms,
                Path = url,
                UserIdentity = this.UserIdentity.UserCode,
            });

            AppLogger.Instance.Moniter(str);

            return null;
        }

        /// <summary>
        /// Index return View之前
        /// </summary>
        public virtual void IndexBeforeReturnView()
        {

        }

    }
}
