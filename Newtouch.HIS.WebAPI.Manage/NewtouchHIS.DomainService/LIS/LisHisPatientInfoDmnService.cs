using NewtouchHIS.Base.DomainService;
using NewtouchHIS.Domain.Entity.LIS;
using NewtouchHIS.Domain.IDomainService.LIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.DomainService.LIS
{
    public class LisHisPatientInfoDmnService : BaseServices<HisPatientInfoEntity>, ILisHisPatientInfoDmnService
    {
        public async Task<List<HisPatientInfoEntity>> HisPatientInfoQuery()
        {
            var result = await baseDal.FindAll();
            return result;
        }
    }
}
