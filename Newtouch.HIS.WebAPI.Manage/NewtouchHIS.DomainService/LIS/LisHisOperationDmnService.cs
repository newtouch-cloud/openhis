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
    public class LisHisOperationDmnService : BaseServices<HisOperationEntity>, ILisHisOperationDmnService
    {
        public async Task<List<HisOperationEntity>> HisOperationQuery()
        {
            var result = await baseDal.FindAll();
            return result;
        }
    }
}
