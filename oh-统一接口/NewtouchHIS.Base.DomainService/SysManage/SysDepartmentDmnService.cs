using Microsoft.Data.SqlClient;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.Data.Common;
using System.Text;

namespace NewtouchHIS.Base.DomainService
{
    public class SysDepartmentDmnService : BaseDmnService<SysDepartmentEntity>, ISysDepartmentDmnService
    {
        public async Task<string> GetNameByCode(string code, string orgId)
        {
            var result = await GetFirstOrDefaultWithAttr<SysDepartmentEntity>(p=>p.Code == code);
            return result.Name;
        }
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="keyword">筛选关键字</param>
        public async Task<List<SysDepartmentEntity>> GetList(string keyword = null)
        {
            return await FindAllWithAttr<SysDepartmentEntity>();
        }
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="keyword">筛选关键字</param>
        public async Task<List<SysDepartmentEntity>> GetListByOrg(string organizeId)
        {
            return await GetByWhere<SysDepartmentEntity>(p=>p.OrganizeId== organizeId,true,p=>p.CreateTime,OrderByType.Desc);
        }
    }
}
