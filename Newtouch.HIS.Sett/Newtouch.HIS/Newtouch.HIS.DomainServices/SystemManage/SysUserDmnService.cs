using Newtouch.HIS.Domain.IDomainServices;
using System.Data.SqlClient;
using System.Collections.Generic;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.DmnService;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 用户相关DmnService
    /// </summary>
    public class SysUserExDmnService : DmnServiceBase, ISysUserExDmnService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysUserExDmnService(IDefaultDatabaseFactory databaseFactory) 
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据UserId获取系统用户可操作的药房部门Code List
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<string> GetYfbmCodeListByUserId(string userId, string orgId)
        {
            var sql = "select distinct yfbmCode from [NewtouchHIS_Base]..V_S_Sys_UserYfbm where UserId = @userId and zt = '1' and OrganizeId = @orgId";
            return this.FindList<string>(sql, new[] { new SqlParameter("@userId", userId), new SqlParameter("@orgId", orgId) });
        }

    }
}
