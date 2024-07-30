using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public class SysApplicationDmnService : DmnServiceBase, ISysApplicationDmnService
    {
        public SysApplicationDmnService(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取应用已授权的组织机构列表
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public IList<SysOrganizeEntity> GetAuthedOrgListByAppId(string appId)
        {
            var sql = @"if exists (select 1 from Sys_OrganizeApplication where ApplicationId = @appId and OrganizeId = '*' and zt = '1')
begin
    --所有顶级组织机构
	select * from Sys_Organize where zt = '1'
end
else
	select distinct b.* from Sys_OrganizeApplication a
	left join Sys_Organize b
	on a.OrganizeId = b.Id
	where (a.ApplicationId = @appId or a.ApplicationId = '*') and a.zt = '1' and b.zt = '1'";

            return this.FindList<SysOrganizeEntity>(sql, new SqlParameter[] {
                new SqlParameter("@appId", appId),
            });
        }

        /// <summary>
        /// 将应用授权给指定机构
        /// </summary>
        /// <param name="appId"></param>
        public void UpdateAuthOrganizeList(string appId, string orgList, string curUserCode)
        {
            var orgIdArr = orgList.Trim(',');
            if (string.IsNullOrWhiteSpace(orgIdArr))
            {
                throw new FailedException("参数无效。orgList");
            }
            var sql = @"
delete from Sys_OrganizeApplication where ApplicationId = @appId
insert into Sys_OrganizeApplication(Id, OrganizeId, ApplicationId, CreateTime, CreatorCode,zt)
select newid(), Id, @appId, GETDATE(), @creatorCode, '1'
from Sys_Organize where Id in (
    select value from dbo.SplitToTable(@orgIdArr, ',')
)";
            _dataContext.Database.ExecuteSqlCommand(sql, new SqlParameter[] {
                new SqlParameter("@appId", appId),
                new SqlParameter("@creatorCode", curUserCode),
                new SqlParameter("@orgIdArr", SqlDbType.VarChar, -1) { Value = orgIdArr }
            });
        }

        /// <summary>
        /// 将应用授权给所有机构
        /// </summary>
        /// <param name="appId"></param>
        public void AuthAllOrganize(string appId, string curUserCode)
        {
            var sql = @"
if exists (select 1 from Sys_Application where Id = @appId)
begin
    delete from Sys_OrganizeApplication where ApplicationId = @appId
    insert into Sys_OrganizeApplication(Id, OrganizeId, ApplicationId, CreateTime, CreatorCode,zt)
    values(newid(), '*', @appId, GETDATE(), @creatorCode, '1')
end";
            _dataContext.Database.ExecuteSqlCommand(sql, new SqlParameter[] {
                new SqlParameter("@appId", appId),
                new SqlParameter("@creatorCode", curUserCode)
            });
        }

        /// <summary>
        /// 撤销全部授权（组织机构）
        /// </summary>
        /// <param name="appId"></param>
        public void AuthCancelAllOrganize(string appId)
        {
            var sql = @"delete from Sys_OrganizeApplication where ApplicationId = @appId";
            _dataContext.Database.ExecuteSqlCommand(sql, new SqlParameter[] {
                new SqlParameter("@appId", appId)
            });
        }

    }
}
