using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.IDomainServices;
using FrameworkBase.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FrameworkBase.DmnService
{
    /// <summary>
    /// 字典相关
    /// </summary>
    public sealed class ItemDmnService : DmnServiceBase, IItemDmnService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public ItemDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据分类Code获取字典项
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IList<SysItemsDataEntity> GetValidListByItemCode(string code)
        {
            var sql = @"select * from Sys_ItemsDetail(nolock)
where zt = '1'
and ItemId in (
select Id from Sys_Items(nolock) where zt = '1' and Code = @code
)";
            return this.FindList<SysItemsDataEntity>(sql, new SqlParameter[] {
                new SqlParameter("@code", code)
            });
        }

    }
}
