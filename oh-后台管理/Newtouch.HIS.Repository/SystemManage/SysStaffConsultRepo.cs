using Newtouch.HIS.Domain.Entity.SystemManage;
using Newtouch.HIS.Domain.IRepository.SystemManage;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.SystemManage
{
    public class SysStaffConsultRepo : RepositoryBase<SysStaffConsultEntity>, ISysStaffConsultRepo
    {
        public SysStaffConsultRepo(IBaseDatabaseFactory databaseFactory)
             : base(databaseFactory)
        {
        }

        public IList<SysStaffConsultEntity> GetStaffConsultList(string staffId)
        {
            if (string.IsNullOrWhiteSpace(staffId))
            {
                return null;
            }
            string sql = @"select * from Sys_StaffConsult with(nolock) 
                            where zt=1 and StaffId= @staffId";

            return this.FindList<SysStaffConsultEntity>(sql, new[] { new SqlParameter("@staffId", staffId) });
        }
    }
}
