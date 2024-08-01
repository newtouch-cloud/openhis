using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Newtouch.Core.Common;
using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.IRepository;
using FrameworkBase.Infrastructure;
using Oracle.ManagedDataAccess.Client;

namespace FrameworkBase.Oracle.Repository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:13
    /// 描 述：系统角色
    /// </summary>
    public sealed class SysRoleRepo : RepositoryBase<SysRoleEntity>, ISysRoleRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public SysRoleRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysRoleEntity> GetPaginationList(Pagination pagination, string keyword)
        {
            var sql = new StringBuilder();
            sql.Append("select * from \"Sys_Role\" where 1 = 1");
            List<OracleParameter> pars = null;
            pars = new List<OracleParameter>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.Append(" and (\"Name\" like :keyword or \"Code\" like :keyword)");
                pars.Add(new OracleParameter(":keyword", "%" + keyword.Trim() + "%"));
            }
            return this.QueryWithPage<SysRoleEntity>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
        }

        /// <summary>
        /// 获取有效实体列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysRoleEntity> GetValidList(string keyword = null)
        {
            var sql = new StringBuilder();
            sql.Append("select * from \"Sys_Role\" where 1 = 1 and \"zt\" = '1'");
            List<OracleParameter> pars = null;
            pars = new List<OracleParameter>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.Append(" and (\"Name\" like :keyword or \"Code\" like :keyword)");
                pars.Add(new OracleParameter(":keyword", "%" + keyword.Trim() + "%"));
            }
            return this.FindList<SysRoleEntity>(sql.ToString(), pars == null ? null : pars.ToArray());
        }

    }
}