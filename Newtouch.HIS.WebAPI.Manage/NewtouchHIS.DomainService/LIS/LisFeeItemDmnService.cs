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
    public class LisFeeItemDmnService : BaseServices<LisFeeItemEntity>, ILisFeeItemDmnService
    {
        public async Task<List<LisFeeItemEntity>> LisFeeItemQuery()
        {
            var result = await baseDal.FindAll();
            return result;
        }
    }
}
