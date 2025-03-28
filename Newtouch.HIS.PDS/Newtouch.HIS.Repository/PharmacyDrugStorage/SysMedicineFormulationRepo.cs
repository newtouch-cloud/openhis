using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 药品剂型 V_S_xt_ypjx 
    /// </summary>
    public class SysMedicineFormulationRepo : RepositoryBase<SysMedicineFormulationVEntity>, ISysMedicineFormulationRepo
    {
        public SysMedicineFormulationRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 返回药品剂型list
        /// </summary>
        /// <returns></returns>
        public List<MedicineFormulationVO> GetMedicineFormulationList()
        {
            return FindList<MedicineFormulationVO>(@"SELECT DISTINCT jxCode,jxmc from [NewtouchHIS_Base].dbo.V_S_xt_ypjx(nolock) WHERE zt='1' ORDER BY jxmc");
        }
    }
}
