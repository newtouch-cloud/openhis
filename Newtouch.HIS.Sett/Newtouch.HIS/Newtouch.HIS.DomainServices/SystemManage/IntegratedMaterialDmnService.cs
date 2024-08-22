using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.Entity;
using System.Linq;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public class IntegratedMaterialDmnService : DmnServiceBase, IIntegratedMaterialDmnService
    {
        public IntegratedMaterialDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据收费项目获取 一次性材料 收费材料项目综合
        /// </summary>
        /// <param name="sfxm"></param>
        /// <returns></returns>
        public SysChargeMaterialItemSynthesisEntity GetSfclxmzhEntity(string sfxm, string orgId)
        {
            var ff = (from mapper in this._dataContext.Set<ComprehensiveMaterialChargeItemMappEntity>()
                      join clzhsf in this._dataContext.Set<SysChargeMaterialItemSynthesisEntity>()
                      on mapper.clzhsfxm equals clzhsf.clzhxm
                      where mapper.sfxm == sfxm
                      && clzhsf.zt == "1"
                      && mapper.OrganizeId == orgId && clzhsf.OrganizeId == "1"
                      && clzhsf.qfbs.HasValue
                      && clzhsf.qfbs != 1   //??什么意思
                      select clzhsf).FirstOrDefault();
            return ff;
        }

    }

}
