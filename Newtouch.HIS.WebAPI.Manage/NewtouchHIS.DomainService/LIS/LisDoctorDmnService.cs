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
    public class LisDoctorDmnService : BaseServices<LisDoctorEntity>, ILisDoctorDmnService
    {
        public async Task<List<LisDoctorEntity>> LisDoctorQuery()
        {
            var result = await baseDal.FindAll();
            return result;
        }
    }
}
