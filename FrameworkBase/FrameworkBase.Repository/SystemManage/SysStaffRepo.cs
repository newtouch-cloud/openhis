using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.IRepository;
using FrameworkBase.Infrastructure;

namespace FrameworkBase.Repository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:15
    /// 描 述：系统人员
    /// </summary>
    public sealed class SysStaffRepo : RepositoryBase<SysStaffEntity>, ISysStaffRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public SysStaffRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// 获取有效实体列表
        /// </summary>
        /// <param name="keyword">筛选关键字</param>
        public IList<SysStaffEntity> GetValidList(string keyword = null)
        {
            var sql = new StringBuilder();
            sql.Append("select * from Sys_Staff(nolock) where 1 = 1 and zt = '1'");
            List<SqlParameter> pars = null;
            return this.FindList<SysStaffEntity>(sql.ToString(), pars == null ? null : pars.ToArray());
        }

    }
}