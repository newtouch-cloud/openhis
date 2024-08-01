using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IDomainServices.SystemManage;
using FrameworkBase.MultiOrg.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkBase.MultiOrg.DmnService.SystemManage
{
    /// <summary>
    /// 
    /// </summary>
    public class SysConfigDmnService: DmnServiceBase, ISysConfigDmnService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysConfigDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysConfigEntity> GetAllConfigs(string keyword, string orgId)
        {
            string sql = @"select Id,OrganizeId,Code,Name,Value,Memo,CreateTime,CreatorCode,LastModifyTime,LastModifierCode,zt,px
                            from sys_config with(nolock)
                            where OrganizeId=@orgId ";

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (Code like @searchKeyword or Name like @searchKeyword) ";
            }

            sql += @"
                    union all
                    select Id,'' OrganizeId,Code,Name,'' Value,Memo,CreateTime,CreatorCode,LastModifyTime,LastModifierCode,zt,px
                    from Sys_ConfigTemplate a with(nolock)
                    where a.zt=1 and not exists(select 1 from sys_config b with(nolock) where a.Code=b.Code and b.OrganizeId=@orgId )       ";

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (Code like @searchKeyword or Name like @searchKeyword) ";
            }

            sql += " order by Code asc ";

            return this.FindList<SysConfigEntity>(sql, new SqlParameter[] {
                    new SqlParameter("@orgId", orgId),
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                });
        }
        /// <summary>
        /// 根据Id获取配置明细
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        public IList<SysConfigEntity> GetConfigForm(string configId)
        {
            string sql = @"select Id,OrganizeId,Code,Name,Value,Memo,CreateTime,CreatorCode,LastModifyTime,LastModifierCode,zt,px
                        from sys_config a with(nolock)
                        where a.Id=@Id ";
            var ety = this.FindList<SysConfigEntity>(sql,new SqlParameter[]{ new SqlParameter("@Id", configId)});
            if (ety.Count <= 0)
            {
                sql = @"select Id,null OrganizeId,Code,Name,null Value,Memo,CreateTime,CreatorCode,LastModifyTime,LastModifierCode,zt,px
                        from Sys_ConfigTemplate a with(nolock)
                        where a.Id=@Id ";

                ety = this.FindList<SysConfigEntity>(sql, new SqlParameter[] { new SqlParameter("@Id", configId) });
            }

            return ety;
        }

    }
}
