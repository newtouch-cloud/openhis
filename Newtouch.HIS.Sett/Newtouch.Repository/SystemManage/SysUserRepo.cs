using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
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
    public class SysUserRepo : RepositoryBase<SysUserVEntity>, ISysUserRepo
    {
        public SysUserRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 返回一个实体
        /// </summary>
        /// <param name="topOrgId"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public SysUserVEntity GetEntity(string topOrgId, string account)
        {
            string sql = "select * from NewtouchHIS_Base..V_C_Sys_User where zt='1' and TopOrganizeId= @topOrgId and Account=@account";
            return this.FirstOrDefault<SysUserVEntity>(sql, new[] { new SqlParameter("@topOrgId", topOrgId), new SqlParameter("@account", account) });
        }
    }
}
