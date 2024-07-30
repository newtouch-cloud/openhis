using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Tools;

namespace Newtouch.Herp.Web.Areas.DepartmentManage.Controllers
{
    /// <summary>
    /// 科室管理
    /// </summary>
    public class DepartmentController : ControllerBase
    {
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysUserDmnService _sysUserDmnService;

        /// <summary>
        /// 获取科室
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult DepartmentQuery(string keyword)
        {
            var depts = _sysDepartmentRepo.GetList(OrganizeId, zt: "1", keyword: keyword);
            return Content(depts.ToJson());
        }

        /// <summary>
        /// 获取科室
        /// </summary>
        /// <returns></returns>
        public ActionResult CurrentUserDepartmentQuery()
        {
            var account = UserIdentity.UserCode;
            var depts = _sysUserDmnService.SelectUserDepartment(account, OrganizeId);
            return Content(depts.ToJson());
        }
    }
}