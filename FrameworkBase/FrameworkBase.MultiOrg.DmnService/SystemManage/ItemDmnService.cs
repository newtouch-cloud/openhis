using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FrameworkBase.MultiOrg.DmnService.SystemManage
{
    /// <summary>
    /// 字典相关
    /// </summary>
    public sealed class ItemDmnService : DmnServiceBase, IItemDmnService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public ItemDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        #region obsolete

        /// <summary>
        /// 获取所有字典项
        /// </summary>
        /// <param name="topOrgId">顶级组织机构</param>
        /// <param name="zt"></param>
        /// <returns></returns>
        [Obsolete("新版本不区分TopOrgId，区分OrgId,instead by GetItemsDetailListByOrgId")]
        public IList<SysItemsDetailVEntity> GetItemsDetailListByTopOrg(string topOrgId, string zt = null)
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_S_Sys_ItemsDetail(nolock)
where 1 = 1
and(TopOrganizeId = @topOrgId or TopOrganizeId = '*')";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@topOrgId", topOrgId));
            if (!string.IsNullOrWhiteSpace(zt))
            {
                sql += " and zt = @zt";
                pars.Add(new SqlParameter("@zt", zt));
            }
            return this.FindList<SysItemsDetailVEntity>(sql, pars.ToArray());
        }

        /// <summary>
        /// 获取分类的字典项
        /// </summary>
        /// <param name="topOrgId">顶级组织机构</param>
        /// <param name="code">字典分类</param>
        /// <param name="zt"></param>
        /// <returns></returns>
        [Obsolete("新版本不区分TopOrgId，区分OrgId,instead by GetItemsDetailListByOrgIdAndItemCode")]
        public IList<SysItemsDetailVEntity> GetItemsDetailListByTopOrgAndItemCode(string topOrgId, string code, string zt = null)
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_S_Sys_ItemsDetail(nolock)
where 1 = 1
and(TopOrganizeId = @topOrgId or TopOrganizeId = '*')
and ItemId in (
select Id from [NewtouchHIS_Base]..V_S_Sys_Items(nolock) where Code = @code and zt = '1'
)";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@code", code));
            pars.Add(new SqlParameter("@topOrgId", topOrgId));
            if (!string.IsNullOrWhiteSpace(zt))
            {
                sql += " and zt = @zt";
                pars.Add(new SqlParameter("@zt", zt));
            }
            return this.FindList<SysItemsDetailVEntity>(sql, pars.ToArray());
        }

        #endregion obsolete

        /// <summary>
        /// 获取所有字典项
        /// </summary>
        /// <param name="orgId">组织机构，未指定时查询common部分</param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public IList<SysItemsDetailVEntity> GetItemsDetailListByOrgId(string orgId, string zt = null)
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_S_Sys_ItemsDetail(nolock)
where 1 = 1
and(OrganizeId = @orgId or OrganizeId = '*')";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId ?? ""));
            if (!string.IsNullOrWhiteSpace(zt))
            {
                sql += " and zt = @zt";
                pars.Add(new SqlParameter("@zt", zt));
            }
            return this.FindList<SysItemsDetailVEntity>(sql, pars.ToArray());
        }

        /// <summary>
        /// 获取分类的字典项
        /// </summary>
        /// <param name="orgId">组织机构，未指定时查询common部分</param>
        /// <param name="code">字典分类</param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public IList<SysItemsDetailVEntity> GetItemsDetailListByOrgIdAndItemCode(string orgId, string code, string zt = null)
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_S_Sys_ItemsDetail(nolock)
where 1 = 1
and(OrganizeId = @orgId or OrganizeId = '*')
and ItemId in (
select Id from [NewtouchHIS_Base]..V_S_Sys_Items(nolock) where Code = @code and zt = '1'
)";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@code", code));
            pars.Add(new SqlParameter("@orgId", orgId ?? ""));
            if (!string.IsNullOrWhiteSpace(zt))
            {
                sql += " and zt = @zt";
                pars.Add(new SqlParameter("@zt", zt));
            }
            return this.FindList<SysItemsDetailVEntity>(sql, pars.ToArray());
        }

        /// <summary>
        /// 获取所有有效字典分类
        /// </summary>
        /// <returns></returns>
        public IList<SysItemsVEntity> GetValidItemTypeList()
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_S_Sys_Items(nolock) where zt= '1'";
            return this.FindList<SysItemsVEntity>(sql);
        }

    }
}
