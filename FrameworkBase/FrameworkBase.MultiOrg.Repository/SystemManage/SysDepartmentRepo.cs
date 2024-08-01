using System;
using System.Collections.Generic;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace FrameworkBase.MultiOrg.Repository.SystemManage
{
    /// <summary>
    /// 系统科室
    /// </summary>
    public sealed class SysDepartmentRepo : RepositoryBase<SysDepartmentVEntity>, ISysDepartmentRepo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysDepartmentRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
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
            var sql = "select * from [NewtouchHIS_Base]..V_S_Sys_Department(nolock) where OrganizeId = @orgId";
            IList<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            if (!string.IsNullOrWhiteSpace(zt))
            {
                sql += " and zt = @zt";
                pars.Add(new SqlParameter("@zt", zt));
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and ( Code like @keyword or Name like @keyword )";
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }

            return this.FindList<SysDepartmentVEntity>(sql, pars == null ? null : pars.ToArray());
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
            var sql = "select Name from [NewtouchHIS_Base]..V_S_Sys_Department(nolock) where Code = @code and OrganizeId = @orgId";
            return this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@code", code)});
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
            var sql = "select * from [NewtouchHIS_Base]..V_S_Sys_Department(nolock) where Code = @code and OrganizeId = @orgId";
            return this.FirstOrDefault<SysDepartmentVEntity>(sql, new[] { new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@code", code)});
        }

        /// <summary>
        /// 根据Name获取Code
        /// </summary>
        /// <param name="name"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetCodeByNameFirstOrDefault(string name, string orgId)
        {
            var sql = "select Code from [NewtouchHIS_Base]..V_S_Sys_Department(nolock) where Name = @name and OrganizeId = @orgId";
            return this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@name", name)});
        }

    }
}
