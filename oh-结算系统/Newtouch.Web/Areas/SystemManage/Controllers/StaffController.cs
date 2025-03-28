using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Core.Common;
using Newtouch.Tools;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 系统人员 扩展
    /// </summary>
    [AutoResolveIgnore]
    public class StaffController : FrameworkBase.MultiOrg.Web.Areas.SystemManage.Controllers.SysStaffController
    {
        private readonly ISysUserDmnService _sysUserDmnService;

        public StaffController(ISysOrganizeDmnService sysOrganizeDmnService, ISysDepartmentRepo sysDepartmentRepo, ISysUserDmnService sysUserDmnService)
            : base(sysOrganizeDmnService, sysDepartmentRepo, sysUserDmnService)
        {
            this._sysUserDmnService = sysUserDmnService;
        }

        /// <summary>
        /// 获取岗位人员
        /// </summary>
        /// <param name="orgId">组织机构Id</param>
        /// <param name="dutyCode">岗位Code</param>
        /// <param name="ks">科室</param>
        /// <param name="keyword">关键字 匹配人员名称或工号</param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetStaffByDutyCode(string orgId, string dutyCode, string ks, string keyword)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                orgId = this.OrganizeId;
            }
            if (string.IsNullOrWhiteSpace(orgId) || string.IsNullOrWhiteSpace(dutyCode))
            {
                return null;
            }
            var list = this._sysUserDmnService.GetStaffByDutyCode(orgId, dutyCode);
            if (!string.IsNullOrWhiteSpace(ks))
            {
                list = list.Where(a => a.ks == ks).ToList();
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                list = list.Where(a => a.StaffName.Contains(keyword.Trim()) || a.StaffGh.Contains(keyword.Trim())).ToList();
            }
            return Content(list.ToJson());
        }

    }
}