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
    public class SysNationDmnService : BaseDmnService<SysNationVEntity>, ISysNationDmnService
    {
        public async Task<List<SysNationVEntity>> GetmzList()
        {
            var result = await GetListBySqlQuery<SysNationVEntity>(DBEnum.BaseDb.ToString(),
                @"select mzCode, mzmc, py from [NewtouchHIS_Base]..V_S_xt_mz width(nolock) where zt = '1'", new List<DbParameter>() { });
            return result;
        }
        
    }
}
