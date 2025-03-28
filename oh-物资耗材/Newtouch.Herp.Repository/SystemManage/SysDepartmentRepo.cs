using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 系统科室
    /// </summary>
    public class SysDepartmentRepo : RepositoryBase<SysDepartmentVEntity>, ISysDepartmentRepo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysDepartmentRepo(IBaseDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取科室列表
        /// </summary>
        /// <param name="orgId">医疗机构</param>
        /// <param name="zt">有效标志。0无效1有效null、Empty所有</param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysDepartmentVEntity> GetList(string orgId, string zt = null, string keyword = null)
        {
            var sql = new StringBuilder("select Id, OrganizeId, ParentId, Name, Code, yjbz, mzzybz, py, zt from dbo.V_S_Sys_Department(nolock) where OrganizeId = @orgId ");
            var pars = new List<SqlParameter>
            {
                new SqlParameter("@orgId", orgId)
            };
            if (!string.IsNullOrWhiteSpace(zt))
            {
                sql.AppendLine(" and zt = @zt");
                pars.Add(new SqlParameter("@zt", zt));
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.AppendLine(" and ( Code like @keyword or Name like @keyword )");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }

            return FindList<SysDepartmentVEntity>(sql.ToString(), pars.ToArray());
        }

        /// <summary>
        /// 根据Code获取科室名称
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetNameByCode(string code, string orgId)
        {
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(orgId))
            {
                return null;
            }
            const string sql = @"select Name from dbo.V_S_Sys_Department(nolock) where Code = @code and OrganizeId = @orgId";
            return FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@code", code) });
        }

        /// <summary>
        /// 根据Code获取科室实体
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public SysDepartmentVEntity GetEntityByCode(string code, string orgId)
        {
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(orgId))
            {
                return null;
            }
            const string sql = "select Id, OrganizeId, ParentId, Name, Code, yjbz, mzzybz, py, zt from dbo.V_S_Sys_Department(nolock) where Code = @code and OrganizeId = @orgId";
            return FirstOrDefault<SysDepartmentVEntity>(sql, new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@code", code) });
        }

        /// <summary>
        /// 根据Name获取Code
        /// </summary>
        /// <param name="name"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetCodeByNameFirstOrDefault(string name, string orgId)
        {
            const string sql = "select Code from dbo.V_S_Sys_Department(nolock) where Name = @name and OrganizeId = @orgId";
            return FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@name", name) });
        }
    }
}
