using NewtouchHIS.Base.Domain;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;
using SqlSugar;

namespace NewtouchHIS.Base.DomainService
{
    /// <summary>
    /// 系统用户
    /// （base库）
    /// </summary>
    public class SysUserDmnService : BaseDmnService<SysUserEntity>, ISysUserDmnService
    {
        #region 用户查询相关
        /// <summary>
        /// 用户分页查询
        /// </summary>
        /// <param name="request"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<PageResponseRow<List<SysUserVO>>> GetPagintionUserList(OLPagination<QueryParamsBase> request, string orgId)
        {
            string? orgName = string.Empty;
            var whereExp = Expressionable.Create<SysUserVEntity, SysUserStaffEntity, SysStaffEntity, SysDepartmentEntity>();
            var orgList = await GetByWhereWithAttr<SysOrganizeEntity>(p => p.zt == "1" && p.TopOrganizeId == sysConfig.Top_OrganizeId);
            if (string.IsNullOrWhiteSpace(orgId))
            {
                //显示所有用户
                orgName = orgList.FirstOrDefault(p => p.Id == sysConfig.Top_OrganizeId)?.Name;
                whereExp.And((a, b, c, d) => a.TopOrganizeId == sysConfig.Top_OrganizeId && a.Account != "admin");
            }
            else
            {
                //仅显示该机构的用户
                orgName = orgList.FirstOrDefault(p => p.Id == orgId)?.Name;
                whereExp.And((a, b, c, d) => c.OrganizeId == orgId && !string.IsNullOrWhiteSpace(c.Id));

            }
            if (!string.IsNullOrWhiteSpace(request.queryParams?.keyword))
            {
                whereExp.And((a, b, c, d) => a.Account.Contains(request.queryParams.keyword) || c.Name.Contains(request.queryParams.keyword));
            }
            string orderby = "";
            OrderByType type = OrderByType.Asc;
            if (!string.IsNullOrWhiteSpace(request.sort))
            {
                orderby = $"{request.sort}";
                if (request.order != "asc")
                {
                    type = OrderByType.Desc;
                }
            }
            var pageData = await GetJoinOrderbyPageList(request.offset, request.limit, orderby, type,
                (a, b, c, d) => new JoinQueryInfos(JoinType.Left, a.Id == b.UserId, JoinType.Left, b.StaffId == c.Id, JoinType.Left, c.DepartmentCode == d.Code && c.OrganizeId == d.OrganizeId),
                (a, b, c, d) => new SysUserVO
                {
                    Account = a.Account,
                    Id = a.Id,
                    TopOrganizeId = a.TopOrganizeId,
                    OrganizeId = c.OrganizeId,
                    LanguageType = a.LanguageType,
                    UserPassword = null,
                    UserSecretkey = null,
                    Locked = a.Locked,
                    zt = a.zt,
                    gh = c.gh,
                    Name = c.Name,
                    DepartmentCode = c.DepartmentCode,
                    DepartmentName = d.Name,
                }, true, whereExp.ToExpression());
            if (pageData != null && pageData.rows != null)
            {
                pageData.rows.ForEach(a => a.OrganizeName = orgName);
            }
            return pageData;
        }

        public async Task<List<BsTreeSelectExtDataModel>> GetOrgUserTreeWithDeptAsync(string? orgId, string? keyword, List<string>? checkIds)
        {
            string? orgName = string.Empty;
            List<BsTreeSelectExtDataModel> list = new List<BsTreeSelectExtDataModel>();
            var orgList = await GetByWhereWithAttr<SysOrganizeEntity>(p => p.zt == "1" && p.TopOrganizeId == sysConfig.Top_OrganizeId);
            if (orgList == null || orgList.Count == 0)
            {
                return default;
            }
            if (string.IsNullOrWhiteSpace(orgId))
            {
                orgName = orgList.FirstOrDefault(p => p.Id == sysConfig.Top_OrganizeId)?.Name;
                list.Add(new BsTreeSelectExtDataModel
                {
                    Id = sysConfig.Top_OrganizeId,
                    text = orgName,
                    href = sysConfig.Top_OrganizeId,
                    nodes = orgList.Where(p => p.Id != sysConfig.Top_OrganizeId).Select(p => new BsTreeSelectExtDataModel
                    {
                        ParentId = sysConfig.Top_OrganizeId,
                        Id = p.Id,
                        text = p.Name,
                        href = p.Id,
                        LevelId = 1,
                        state = new BsTreeNodeStuModel
                        {
                            selected = checkIds?.Contains(p.Id),
                            expanded = true
                        }
                    }).ToList(),
                    LevelId = 0
                });
            }
            else
            {
                orgName = orgList.FirstOrDefault(p => p.Id == orgId)?.Name;
                list.Add(new BsTreeSelectExtDataModel
                {
                    ParentId = sysConfig.Top_OrganizeId,
                    Id = orgId,
                    text = orgName,
                    href = orgId,
                    LevelId = 1,
                    state = new BsTreeNodeStuModel
                    {
                        selected = checkIds?.Contains(orgId),
                        expanded = true
                    }
                });
            }
            foreach (var org in list)
            {
                var deptOfOrg = await GetByWhere<SysDepartmentEntity>(p => p.OrganizeId == org.href && p.zt == "1");
                var staff = await GetSatffVOListByOrg(org.href, null);
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    staff = staff.Where(p => p.Name.Contains(keyword)).ToList();
                }
                if (deptOfOrg != null && deptOfOrg.Count > 0)
                {
                    org.nodes = deptOfOrg.Select(p => new BsTreeSelectExtDataModel
                    {
                        ParentId = org.Id,
                        Id = p.Code,
                        text = p.Name,
                        href = p.Code,
                        nodes = staff.Where(s => s.DepartmentCode == p.Code)
                                    .Select(u => new BsTreeSelectExtDataModel
                                    {
                                        ParentId = p.Code,
                                        Id = u.UserId,
                                        text = u.Name,
                                        href = u.UserId,
                                        ext = new List<BsTreeSelectExtModel> {
                                             new BsTreeSelectExtModel{Key= "StaffId", Value= u.StaffId,},
                                             new BsTreeSelectExtModel{Key= "OrganizeId", Value= org.Id,}
                                        },
                                        LevelId = 3,
                                        state = new BsTreeNodeStuModel
                                        {
                                            selected = checkIds?.Contains(u.UserId),
                                        }
                                    }).ToList(),
                        LevelId = 2,
                        state = new BsTreeNodeStuModel
                        {
                            selected = checkIds?.Contains(p.Code),
                        }
                    }).ToList();
                }
            }
            return list;
        }
        #endregion

        /// <summary>
        /// 根据OrganizeId获取 系统人员VO（且有对应的SysUser）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<SysUserStaffVO>> GetSatffVOListByOrg(string orgId, string? toporgId)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                return null;
            }
            toporgId = toporgId ?? sysConfig.Top_OrganizeId;
            var staffList = await GetJoinList<SysStaffEntity, SysUserStaffEntity, SysUserEntity, SysUserStaffVO>(
                (a, b, c) => new JoinQueryInfos(JoinType.Left, a.Id == b.StaffId, JoinType.Left, b.UserId == c.Id),
                (a, b, c) => new SysUserStaffVO
                {
                    UserId = c.Id,
                    UserCode = c.Account,
                    StaffId = a.Id,
                    gh = a.gh,
                    DepartmentCode = a.DepartmentCode,
                    Name = a.Name
                }, true, (a, b, c) => a.TopOrganizeId == toporgId && a.OrganizeId == orgId && a.zt == "1" && b.zt == "1" && c.zt == "1", false);
            return staffList;
        }
        /// <summary>
        /// 获取人员信息 根据组织机构和关键字
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<SysUserOrgandDmtVO>> GetSatffVOListByOrgorKeyvalue(string orgId, string? keyvalue)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                return null;
            }
            var staffList = await GetJoinList<SysStaffEntity,SysOrganizeEntity,SysDepartmentEntity, SysUserVEntity, SysUserOrgandDmtVO>(
                (a, b, c, d) => new JoinQueryInfos(JoinType.Left, a.OrganizeId == b.Id, JoinType.Left, a.DepartmentCode == c.Code, JoinType.Left, a.Id == d.Id),
                (a, b, c, d) => new SysUserOrgandDmtVO
                {
                    Id = a.Id,
                    zt = a.zt,
                    Account = a.gh,
                    gh = a.gh,
                    Name = a.Name,
                    OrganizeName=b.Name,
                    DepartmentName = c.Name,
                    Locked = d.Locked,
                    OrganizeId = a.OrganizeId
                }, true, (a, b, c,d) => a.OrganizeId == orgId && b.zt == "1" && c.zt == "1"&& a.Name.Contains(keyvalue??""), false);
            return staffList;
        }
    }


}
