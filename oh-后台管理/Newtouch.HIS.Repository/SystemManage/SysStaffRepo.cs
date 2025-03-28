using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository.SystemManage
{
    /// <summary>
    /// 
    /// </summary>
    public class SysStaffRepo : RepositoryBase<SysStaffEntity>, ISysStaffRepo
    {
        public SysStaffRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取机构下所以系统人员（不包括子机构）
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<SysStaffEntity> GetsatffListByOrg(string OrganizeId)
        {
            var sql = @"select * from Sys_Staff where OrganizeId=@OrganizeId";
            return this.FindList<SysStaffEntity>(sql, new SqlParameter[] {
                new SqlParameter("@OrganizeId",OrganizeId)
            });
        }

        /// <summary>
        /// 获取管理人员Id列表 根据UserId
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<string> GetCurStaffIdListByUserId(string UserId)
        {
            var sql = "select StaffId from Sys_UserStaff where UserId = @UserId";
            return this.FindList<string>(sql, new SqlParameter[] {
                new SqlParameter("@UserId",UserId)
            });
        }

        public IList<SysStaffEntity> GetStaffList(string orgId, string keyword = null)
        {
            var sql = @"select * from Sys_Staff(nolock) where OrganizeId = @orgId
and (gh like @searchKeyword or name like @searchKeyword) and zt=1
order by CreateTime desc";

            return this.FindList<SysStaffEntity>(sql, new[] {
                new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%") });
        }
    }
}
