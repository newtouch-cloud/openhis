using NewtouchHIS.Base.DomainService;
using NewtouchHIS.Domain.Entity.LIS;
using NewtouchHIS.Domain.Entity.PatientCenter;
using NewtouchHIS.Domain.IDomainService.LIS;
using NewtouchHIS.Domain.IDomainService.PatientCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.DomainService.LIS
{
    public class LISDmnService : BaseServices<HisDeptEntity>, ILisHisDeptDmnService
    {
        public async Task<List<HisDeptEntity>> HisDeptQuery()
        {
            var result = await baseDal.FindAll();
            return result;
        }

    }
}
