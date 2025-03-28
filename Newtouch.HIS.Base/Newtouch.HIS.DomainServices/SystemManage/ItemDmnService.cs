using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Data.SqlClient;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 字典、字典项 DmnService
    /// </summary>
    public class ItemDmnService : DmnServiceBase, IItemDmnService
    {
        private readonly ISysItemsDetailRepository _sysItemsDetailRepository;

        public ItemDmnService(IBaseDatabaseFactory databaseFactory
            , ISysItemsDetailRepository sysItemsDetailRepository) : base(databaseFactory)
        {
            this._sysItemsDetailRepository = sysItemsDetailRepository;
        }

        /// <summary>
        /// 获取有效字典项 列表
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId">医疗机构，null仅查共享的</param>
        /// <returns></returns>
        public IList<SysItemsDetailEntity> GetValidListByItemCode(string code,string  keyword, string orgId = null)
        {
            var sql = @"select * from Sys_ItemsDetail
where zt = '1'
and (isnull(@orgId,'') = '' or OrganizeId = '*' or OrganizeId = @orgId)
and ItemId in (
select Id from Sys_Items where zt = '1' and Code = @code
)
and (Name like @keyword or Code like @keyword)
";
            return this.FindList<SysItemsDetailEntity>(sql, new SqlParameter[] {
                new SqlParameter("@code", code),
                new SqlParameter("@orgId", orgId ?? ""),
                new SqlParameter("@keyword",'%'+ (keyword==null?"":keyword) +'%')
            });
        }

    }
}
