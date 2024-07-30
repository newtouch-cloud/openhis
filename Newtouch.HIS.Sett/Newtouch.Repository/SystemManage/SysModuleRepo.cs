using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysModuleExRepository : RepositoryBase<SysModuleEntity>, ISysModuleExRepo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysModuleExRepository(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<SysModuleEntity> GetModuleTreeJsonByName(string name)
        {
            string sql = @"SELECT  *
                            FROM SYS_Module
                            WHERE (parentId = (SELECT   Id
                                                FROM     SYS_Module
                                                WHERE   Name = @name
                                                 )
                                or Name = @name)
and zt = '1'";
            return this.FindList<SysModuleEntity>(sql, new[] { new SqlParameter("@name", name) });
        }

    }
}
