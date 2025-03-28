using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Common.Operator;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    [HandlerLogin]
    public class HomeController : Controller
    {
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly ICommonDmnService _commonDmnService;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysDepartmentRepository _sysDepartmentRepo;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmnService;

        public HomeController(ICommonDmnService commonDmnService, ISysUserDmnService sysUserDmnService
            , ISysOrganizeDmnService sysOrganizeDmnService
            , ISysDepartmentRepository sysDepartmentRepo
            , IUserRoleAuthDmnService userRoleAuthDmnService)
        {
            this._commonDmnService = commonDmnService;
            this._sysUserDmnService = sysUserDmnService;
            this._sysOrganizeDmnService = sysOrganizeDmnService;
            this._sysDepartmentRepo = sysDepartmentRepo;
            this._userRoleAuthDmnService = userRoleAuthDmnService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var cookieLoginFlag = WebHelper.GetCookie(Constants.AppId + "_" + OperatorProvider.GetCurrent().UserCode + "_" + "LoginFlag");
            ViewBag.cookieLoginFlag = cookieLoginFlag;

            var opr = OperatorProvider.GetCurrent();
            var nLanguageTypeKey = "Newtouch_language_type";
            if (!string.IsNullOrWhiteSpace(Constants.AppId))
            {
                nLanguageTypeKey = Constants.AppId + "_" + nLanguageTypeKey;
            }
            var language_type = WebHelper.GetCookie(nLanguageTypeKey);
            if (string.IsNullOrWhiteSpace(language_type))
            {
                language_type = _sysUserDmnService.GetLanguageTypeByUserId(OperatorProvider.GetCurrent().UserId);   //用户属性
                WebHelper.WriteCookie(nLanguageTypeKey, language_type);
            }
            if (!string.IsNullOrWhiteSpace(opr.OrganizeId))
            {
                //当前Org名称（关联Staff）
                ViewBag.OrgName = _sysOrganizeDmnService.GetNameByOrgId(opr.OrganizeId);
            }
            else
            {

                //显示顶级组织机构的名称
                ViewBag.OrgName = _sysOrganizeDmnService.GetNameByOrgId(opr.TopOrganizeId);
            }

            if (!opr.IsRoot && !opr.IsAdministrator)    //恒true
            {
                //当前UserId 对应的所有 医疗机构 List
                var mediOrgList = _sysOrganizeDmnService.GetMedicalOrganizeListByUserId(opr.UserId);

                ViewBag.MediOrgList = mediOrgList;

                if (mediOrgList != null && mediOrgList.Count > 1)
                {
                    //是否页面加载时就必须选择Org
                    var needChooseOrg = WebHelper.GetCookie(Constants.AppId + "_" + "CookieKey_ChooseOrg");
                    ViewBag.NeedChooseOrg = !string.IsNullOrWhiteSpace(needChooseOrg);
                }
            }
            return View();
        }

        /// <summary>
        /// 切换医疗机构 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult UserOrgChoose()
        {
            var opr = OperatorProvider.GetCurrent();

            var mediOrgList = _sysOrganizeDmnService.GetMedicalOrganizeListByUserId(opr.UserId);

            ViewBag.MediOrgList = mediOrgList;

            return View();
        }

        /// <summary>
        /// 切换医疗机构
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SwithOrg(string dstnOrgId)
        {
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

                var roleList = _userRoleAuthDmnService.GetUserRoleList(opr.UserId, orgStaff.OrganizeId);
                //170904 Administrator不再可以关联Staff，仅admin
                opr.RoleIdList = roleList.Select(p => p.Id).ToList();
                opr.IsHospAdministrator = roleList.Any(p => p.Code == "HospAdministrator");

                OperatorProvider.AddCurrent(opr);

                WebHelper.WriteCookie(Constants.AppId + "_" + "CookieKey_OrganizeId", orgStaff.OrganizeId);

                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "切换成功" }.ToJson());
            }
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = "切换失败" }.ToJson());
        }

        [HttpGet]
        public ActionResult Default()
        {
            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Notice()
        {
            ViewBag.UserCode = OperatorProvider.GetCurrent().UserCode;
            ViewBag.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
            return View();
        }

        [HttpGet]
        /// <summary>
        /// 获取就诊人数（门诊记账、住院记账）
        /// </summary>
        public ActionResult GetVisitNum()
        {
            VisitNumBO BO = null;
            var userCode = OperatorProvider.GetCurrent().UserCode;
            if (userCode == "root")
            {
                BO = null;
            }
            else if (userCode == "admin")
            {
                var topOrgId = Constants.TopOrganizeId;
                if (string.IsNullOrEmpty(topOrgId))
                {
                    throw new FailedCodeException("SYS_GET_TOPORGANIZATIONAL_FAILURE");

                }
                BO = _commonDmnService.GetVisitNum(true, null, topOrgId);
            }
            else
            {
                var orgId = OperatorProvider.GetCurrent().OrganizeId;
                if (string.IsNullOrEmpty(orgId))
                {
                    throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
                }
                BO = _commonDmnService.GetVisitNum(false, orgId);
            }
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = BO }.ToJson());
        }

        /// <summary>
        /// 获取业务字段的随机产生值（自增+Format）
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="orgIdIsStar"></param>
        /// <param name="topOrgIdIsStar"></param>
        /// <param name="initFormat"></param>
        /// <param name="initFieldLength"></param>
        /// <returns></returns>
        public ActionResult GetNewFieldUniqueValue(string fieldName, bool? orgIdIsStar = null, bool? topOrgIdIsStar = null, string orgId = null, string topOrgId = null, string initFormat = "", int initFieldLength = 0)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                if (orgIdIsStar.HasValue)
                {
                    orgId = orgIdIsStar.Value ? "*" : OperatorProvider.GetCurrent().OrganizeId;
                }
            }
            if (string.IsNullOrWhiteSpace(topOrgId))
            {
                if (topOrgIdIsStar.HasValue)
                {
                    topOrgId = topOrgIdIsStar.Value ? "*" : Constants.TopOrganizeId;
                }
            }
            if (string.IsNullOrWhiteSpace(initFormat) && initFieldLength > 0)
            {
                initFormat = "{0:D" + initFieldLength + "}";
            }
            string value = null;
            if (string.IsNullOrWhiteSpace(orgId) || string.IsNullOrWhiteSpace(topOrgId) || initFormat == null)
            {
                value = null;
            }
            //else if(orgId == topOrgId)
            //{
            //    value = null;   //???????????????这样合适么
            //}
            else
            {
                value = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue(fieldName, orgId, topOrgId, initFormat);
            }
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = value }.ToJson());
        }

    }
}