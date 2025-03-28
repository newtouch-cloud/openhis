using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Base.DomainService;
using NewtouchHIS.Domain.Entity.LIS;
using NewtouchHIS.Domain.IDomainService;
using SqlSugar;

namespace NewtouchHIS.DomainService.Organize
{
    public class OrgBaseDmnService : BaseDmnService<SysDepartmentVEntity>, IOrgBaseDmnService
    {
        public async Task<List<HisDeptVO>> HisDeptQuery(string orgId, string? keyword)
        {
            var dept = await GetByWhereWithAttr<SysDepartmentVEntity>(p => p.yjbz && p.zt == "1" && p.OrganizeId == orgId);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                dept = dept.Where(p => p.Code == keyword || p.Name.Contains(keyword)).ToList();
            }
            return dept.Select(a => new HisDeptVO
            {
                OrganizeId = a.OrganizeId,
                ks = a.Code,
                ksmc = a.Name,
                mzzybz = a.mzzybz,
                yjbz = a.yjbz,
                zt = a.zt,
            }).ToList();
        }
        public async Task<List<HisStaffVO>> HisDocQuery(string orgId, string? keyword)
        {
            var docData = await GetJoinListWithAttr<SysStaffEntity, SysStaffDutyComplexVEntity, HisStaffVO>(
                (a, b) => new JoinQueryInfos(JoinType.Inner, a.Id == b.StaffId),
                (a, b) => new HisStaffVO
                {
                    OrganizeId = a.OrganizeId,
                    name = a.Name,
                    code = a.gh,
                    dutyCode = b.DutyCode,
                    dutyName = b.DutyName
                }, true, (a, b) => a.OrganizeId == orgId && a.zt == "1" && b.zt == "1" && b.DutyCode == "Doctor");

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                docData = docData.Where(p => p.code == keyword || p.name!.Contains(keyword) || p.dutyCode == keyword).ToList();
            }
            return docData;
        }


    }
}
