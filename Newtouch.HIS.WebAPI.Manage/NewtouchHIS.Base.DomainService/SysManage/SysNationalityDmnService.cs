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
    public class SysNationalityDmnService : BaseDmnService<SysNationalityVEntity>, ISysNationalityDmnService
    {
        public async Task<List<SysNationalityVEntity>> GetgjList()
        {
            var result = await GetListBySqlQuery<SysNationalityVEntity>(DBEnum.BaseDb.ToString(),
                @"select gjId, gjCode,py,gjmc from [NewtouchHIS_Base]..V_S_xt_gj width(nolock) where zt = '1'", new List<DbParameter>() {});
            return result;
        }
        
    }
}
