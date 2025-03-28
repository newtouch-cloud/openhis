using Microsoft.Data.SqlClient;
using NewtouchHIS.Base.Domain;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.Domain.ValueObjects.SysManage;
using NewtouchHIS.Lib.Base.EnumExtend;
using System.Data.Common;

namespace NewtouchHIS.Base.DomainService
{
    public class SysStaffDmnService : BaseDmnService<SysStaffEntity>, ISysStaffDmnService
    {
        public async Task<SysStaffEntity> FindKey(string Id)
        {
            return await FindKey(Id);
        }

        public async Task<List<SysStaffVEntity>> GetStaffListByUserId(string userId)
        {
            string sql = @"select distinct b.* from V_S_Sys_UserStaff(nolock) a
left join V_S_Sys_Staff(nolock) b
on a.StaffId = b.Id
where a.zt = '1' and a.UserId = @userId and b.zt = '1'";
            return await GetListBySqlQuery<SysStaffVEntity>(DBEnum.BaseDb.ToString(), sql, new List<DbParameter>()
            {
                new SqlParameter("@userId",userId)
            });
        }

        public async Task<List<SysStaffDeptVO>> GetStaffDeptByGh(string rygh, string orgId)
        {
            string sql = @"SELECT a.gh,a.Name AS xm,b.Name AS ksmc,b.code AS kscode  
FROM [dbo].[Sys_Staff] AS a INNER JOIN [dbo].[Sys_Department] AS b ON a.DepartmentCode=b.Code and a.OrganizeId=b.OrganizeId
where a.zt = '1' and a.gh = @rygh and a.OrganizeId=@orgId and b.zt = '1'";
            return await GetListBySqlQuery<SysStaffDeptVO>(DBEnum.BaseDb.ToString(), sql, new List<DbParameter>()
            {
                new SqlParameter("@rygh",rygh??""),
                new SqlParameter("@orgId",orgId??"")
            });
        }
    }
}
