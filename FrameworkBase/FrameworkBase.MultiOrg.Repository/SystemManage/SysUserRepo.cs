using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using System.Data.SqlClient;

namespace FrameworkBase.MultiOrg.Repository.SystemManage
{
    /// <summary>
    /// 系统用户
    /// </summary>
    public sealed class SysUserRepo : RepositoryBase<SysUserVEntity>, ISysUserRepo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysUserRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据用户名 获取Entity
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public SysUserVEntity GetEntityByUserName(string account)
        {
            string sql = "select * from NewtouchHIS_Base..V_C_Sys_User(nolock) where zt='1' and TopOrganizeId= @topOrgId and Account=@account";
            return this.FirstOrDefault<SysUserVEntity>(sql, new[] {
                new SqlParameter("@topOrgId", ConstantsBase.TopOrganizeId),
                new SqlParameter("@account", account) });
        }

    }
}
