using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository
{
    public class SysStaffExRepo : RepositoryBase<SysStaffVEntity>, ISysStaffExRepo
    {
        public SysStaffExRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 职位对应员工返回list
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<StaffDutyVO> GetStaffDutyListByOrganizeId(string orgId)
        {
            var sql = @"SELECT  *
                        FROM NewtouchHIS_Base.dbo.V_C_Sys_StaffDuty
                        WHERE OrganizeId = @orgId and zt = '1'";
            return this.FindList<StaffDutyVO>(sql, new[] { new SqlParameter("@orgId", orgId) });
        }

    }
}
