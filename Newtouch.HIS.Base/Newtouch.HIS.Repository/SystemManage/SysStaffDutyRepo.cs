using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysStaffDutyRepo : RepositoryBase<SysStaffDutyEntity>, ISysStaffDutyRepo
    {
        public SysStaffDutyRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public IList<SysStaffDutyEntity> GetListByStaffId(string staffId)
        {
            if (string.IsNullOrWhiteSpace(staffId))
            {
                return null;
            }
            var sql = "select * from sys_StaffDuty(nolock) where StaffId = @staffId and zt = '1'";
            return this.FindList<SysStaffDutyEntity>(sql, new[] { new SqlParameter("@staffId", staffId) });
        }

    }
}
