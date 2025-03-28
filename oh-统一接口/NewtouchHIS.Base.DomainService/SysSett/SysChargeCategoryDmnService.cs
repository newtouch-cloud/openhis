using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Organize;

namespace NewtouchHIS.Base.DomainService
{
    public class SysChargeCategoryDmnService : BaseDmnService<SysChargeCategoryVEntity>, ISysChargeCategoryDmnService
    {
        /// 获取收费大类列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dllbs"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public async Task<List<SysChargeCategoryVEntity>> GetList(string orgId, string dllbs = "", string zt = "")
        {
            var result = await GetByWhereWithAttr<SysChargeCategoryVEntity>(p => p.zt == "1" && p.OrganizeId == orgId);
            return result;
        }
    }
}
