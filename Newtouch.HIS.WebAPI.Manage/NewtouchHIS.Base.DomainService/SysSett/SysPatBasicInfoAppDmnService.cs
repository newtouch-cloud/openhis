using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.Organize;

namespace NewtouchHIS.Base.DomainService
{
    public class SysPatBasicInfoAppDmnService : BaseDmnService<SysPatientNatureEntity>, ISysPatBasicInfoAppDmnService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<SysPatientNatureEntity>> GetBRXZListAsync(string orgId)
        {
            var list = await GetByWhereWithAttr<SysPatientNatureEntity>(p => p.zt == "1" && p.OrganizeId == orgId);
            return list;
        }

    }
}
