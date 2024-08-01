using Newtouch.Common;
using System.Collections.Generic;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using Newtouch.Core.Common;
using FrameworkBase.MultiOrg.Domain.Entity;
using System.Linq;

namespace FrameworkBase.MultiOrg.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 组织机构（医疗机构）
    /// </summary>
    [AutoResolveIgnore]
    public class OrganizeController : OrgControllerBase
    {
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="sysOrganizeDmnService"></param>
        public OrganizeController(ISysOrganizeDmnService sysOrganizeDmnService)
        {
            this._sysOrganizeDmnService = sysOrganizeDmnService;
        }

        /// <summary>
        /// 下拉组织机构 数据源
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetChildTreeSelectJson(string organizeId, bool? containsSelf = true)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                organizeId = base.GetAuthOrganizeId();
            }
            var data = _sysOrganizeDmnService.GetValidListByParentOrg(organizeId, containsSelf);
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.Name;
                treeModel.parentId = item.Id == organizeId ? null : item.ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }

    }
}
