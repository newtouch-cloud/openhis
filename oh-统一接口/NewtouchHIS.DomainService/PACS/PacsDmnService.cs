using NewtouchHIS.Base.DomainService;
using NewtouchHIS.Domain.Entity.PACS;
using NewtouchHIS.Domain.IDomainService.PACS;
using SqlSugar;

namespace NewtouchHIS.DomainService.PACS
{
    public class PacsDmnService : BaseDmnService<PacsFeeitemVEntity>, IPacsDmnService
    {
        public async Task<List<PacsFeeitemVEntity>> PacsFeeitemQuery(string orgId, string? keyword)
        {
            var dept = await GetByWhere(p => p.OrganizeId == orgId);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                dept = dept.Where(p => p.fydm == keyword || p.fymc.Contains(keyword)).ToList();
            }
            return dept.ToList();
        }


    }
}
