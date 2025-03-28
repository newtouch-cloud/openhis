/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 

*********************************************************************************/

using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Interface;
using Newtouch.HIS.Domain.IRepository;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 收费项目
    /// </summary>
    public class SysChargeItemRepo : RepositoryBase<SysChargeItemVEntity>, ISysChargeItemRepo
    {
        private readonly ICache _cache;

        public SysChargeItemRepo(IDefaultDatabaseFactory databaseFactory, ICache cache)
            : base(databaseFactory)
        {
            this._cache = cache;
        }

        /// <summary>
        /// 根据sfxm Code获取Entity
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        public SysChargeItemVEntity SelectSysChargeItemBysfxm(string sfxm, string orgId)
        {
            var sql = "select * from [NewtouchHIS_Base]..V_S_xt_sfxm width(nolock) where sfxmCode = @sfxm and OrganizeId = @orgId";
            return this.FirstOrDefault<SysChargeItemVEntity>(sql, new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@sfxm", sfxm) });
        }

        /// <summary>
        /// 根据编码获取dl
        /// </summary>
        /// <param name="sfxm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetdlCodeBysfxm(string sfxm, string orgId)
        {
            var sql = "select sfdlCode from [NewtouchHIS_Base]..V_S_xt_sfxm width(nolock) where sfxmCode = @sfxm and OrganizeId = @orgId";
            return this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@sfxm", sfxm) });
        }

    }
}
