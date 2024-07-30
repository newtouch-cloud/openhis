using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 字典、字典项 DmnService
    /// </summary>
    public class ItemDmnService : DmnServiceBase, IItemDmnService
    {

        public ItemDmnService(IDefaultDatabaseFactory databaseFactory): base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取有效字典项 列表 指定组织机构
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IList<SysItemsDetailVEntity> GetValidItemsDetailListByTopOrgAndItemCode(string topOrgId, string code)
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_S_Sys_ItemsDetail
where 1 = 1
and(TopOrganizeId = @topOrgId or TopOrganizeId = '*')
and ItemId in (
select Id from [NewtouchHIS_Base]..V_S_Sys_Items where Code = @code
)";
            return this.FindList<SysItemsDetailVEntity>(sql, new SqlParameter[] {
                new SqlParameter("@code", code),
                new SqlParameter("@topOrgId", topOrgId)
            });
        }

        /// <summary>
        /// 获取所有字典分类
        /// </summary>
        /// <param name="topOrgId"></param>
        /// <returns></returns>
        public IList<SysItemsVEntity> GetValidItemsList()
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_S_Sys_Items";
            return this.FindList<SysItemsVEntity>(sql);
        }

        /// <summary>
        /// 获取所有字典项
        /// </summary>
        /// <param name="topOrgId"></param>
        /// <returns></returns>
        public IList<SysItemsDetailVEntity> GetValidItemsDetailListByTopOrg(string topOrgId)
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_S_Sys_ItemsDetail
where 1 = 1
and(TopOrganizeId = @topOrgId or TopOrganizeId = '*')";
            return this.FindList<SysItemsDetailVEntity>(sql, new SqlParameter[] {
                new SqlParameter("@topOrgId", topOrgId)
            });
        }

    }
}