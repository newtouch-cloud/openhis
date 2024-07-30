using Mapster;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.Domain.ValueObjects;
using SqlSugar;

namespace NewtouchHIS.Base.DomainService.SysManage
{
    public class SysOrgDmnService : BaseDmnService<SysOrganizeEntity>, ISysOrgDmnService
    {
        public async Task<List<SysOrgVo>> GetOrganizeList(string? topOrgId = null, bool showTopOrg = false)
        {
            string where = string.Empty;
            if (string.IsNullOrWhiteSpace(topOrgId))
            {
                topOrgId = sysConfig.Top_OrganizeId;
            }
            if (!showTopOrg)
            {
                return (await GetByWhereWithAttr<SysOrganizeEntity>(p => p.TopOrganizeId == topOrgId && p.TopOrganizeId != p.Id && p.zt == "1")).Adapt<List<SysOrgVo>>();
            }
            return (await GetByWhereWithAttr<SysOrganizeEntity>(p => p.TopOrganizeId == topOrgId && p.zt == "1")).Adapt<List<SysOrgVo>>();
        }
        /// <summary>
        /// 组织机构（树形）
        /// </summary>
        /// <param name="parentOrgId"></param>
        /// <returns></returns>
        public async Task<List<SysOrgVo>?> GetOrganizeTree(string parentOrgId)
        {
            var orgAll = await GetByWhere(p => p.zt == "1");
            List<SysOrgVo> orgList = new List<SysOrgVo>();
            var orgThis = orgAll.FirstOrDefault(p => !string.IsNullOrWhiteSpace(parentOrgId) && p.Id == parentOrgId);
            if (orgThis == null)
            {
                return default;
            }
            SysOrgVo parent = orgThis.Adapt<SysOrgVo>();
            parent.OrganizeId = orgThis.Id;
            orgList.Add(parent);
            List<SysOrgVo>  orgChild = OrganizeTreeItem(parentOrgId, orgAll.Select(p => new SysOrgVo
            {
                OrganizeId = p.Id,
                TopOrganizeId = p.TopOrganizeId,
                ParentId = p.ParentId,
                Code = p.Code,
                Name = p.Name,
                gjjgdm = p.gjjgdm
            }).ToList());
            if(orgChild.Count > 0)
            {
                orgList.AddRange(orgChild);
            }
            return orgList;
        }

        private List<SysOrgVo> OrganizeTreeItem(string parentOrgId, List<SysOrgVo> orgList)
        {
            List<SysOrgVo> result = new List<SysOrgVo>();
            var childs = orgList.Where(p => p.ParentId == parentOrgId).ToList();
            result.AddRange(childs);
            childs.ForEach(a =>
            {
                var thisChilds = orgList.Where(p => p.ParentId == a.OrganizeId).ToList();
                if (thisChilds.Any())
                {
                    var childnodes = OrganizeTreeItem(a.OrganizeId, orgList);
                    if (childnodes != null && childnodes.Count > 0)
                    {
                        result.AddRange(childnodes);
                    }
                }
            });
            return result;
        }
    }
}
