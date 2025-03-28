using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Framework.Web.Controllers.SysManage;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base.Extension;

namespace HIS.SSO.Areas.SysManage.Controllers
{
    [Area("SysManage")]
    public class CommonController : SystemBaseController
    {
        private readonly ISysOrgDmnService _sysOrgDmn;
        public CommonController(ISystemAppService systemApp, ISysOrgDmnService sysOrgDmn) : base(systemApp)
        {
            _sysOrgDmn = sysOrgDmn;
        }

        public async Task<ActionResult> GetOrgTreeSelectJson(string orgId)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                orgId = GetAuthOrganizeId();
            }
            ///获取角色组织机构
            var data = await _sysOrgDmn.GetOrganizeTree(orgId);
            List<TreeSelectCModel> treeList = data.Where(p => string.IsNullOrWhiteSpace(p.ParentId)).Select(p => new TreeSelectCModel
            {
                id = p.OrganizeId,
                text = p.Name
            }).ToList();
            if (treeList == null || treeList.Count == 0)
            {
                treeList = data.Select(p => new TreeSelectCModel
                {
                    id = p.OrganizeId,
                    text = p.Name
                }).ToList();
            }
            else
            {
                foreach (var item in treeList)
                {
                    item.children = data.Where(p => p.ParentId == item.id).Select(p => new TreeSelectCModel
                    {
                        id = p.OrganizeId,
                        text = p.Name
                    }).ToList();
                }
            }
            return Content(treeList.ToJson());
        }
    }
}
