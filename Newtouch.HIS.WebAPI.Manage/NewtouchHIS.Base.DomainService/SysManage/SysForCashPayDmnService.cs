using Mapster;
using Microsoft.Data.SqlClient;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Lib.Base.EnumExtend;
using System.Data.Common;
using System.Text;

namespace NewtouchHIS.Base.DomainService
{
    public class SysForCashPayDmnService : BaseDmnService<SysCashPaymentModelEntity>, ISysForCashPayDmnService
    {
        public async Task<List<SysCashPaymentModelEntity>> GetList()
        {
            return await GetByWhereWithAttr<SysCashPaymentModelEntity>(p => p.zt == "1");
        }
        
    }
}
