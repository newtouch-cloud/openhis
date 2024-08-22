/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 

*********************************************************************************/

using System.Collections.Generic;
using System.Data.Common;
using Newtouch.Core.Common.Interface;
using Newtouch.HIS.Domain.IRepository;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using FrameworkBase.MultiOrg.Domain.Entity;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 收费大类
    /// </summary>
    public class SysChargeCategoryRepo : RepositoryBase<SysChargeCategoryVEntity>, ISysChargeCategoryRepo
    {
        private readonly ICache _cache;

        public SysChargeCategoryRepo(IDefaultDatabaseFactory databaseFactory, ICache cache) : base(databaseFactory)
        {
            this._cache = cache;
        }

        /// <summary>
        /// 收取所有收费大类（带缓存）
        /// </summary>
        /// <returns></returns>
        public IList<SysChargeCategoryVEntity> GetLazyList(string orgId)
        {
            return _cache.Get(string.Format(Infrastructure.CacheKey.ValidSystemChargeMajorClassListSetKey, orgId), () =>
            {
                const string sql = "select DISTINCT dlCode, dlmc, py, mzprintbillcode from [NewtouchHIS_Base]..V_S_xt_sfdl width(nolock) WHERE mzprintbillcode IS NOT NULL AND mzprintbillcode<>'' and OrganizeId = @orgId";
                return FindList<SysChargeCategoryVEntity>(sql, new DbParameter[] { new SqlParameter("@orgId", orgId) });
            });
        }

        /// <summary>
        /// 获取收费大类列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dllbs"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public IList<SysChargeCategoryVEntity> GetList(string orgId, string dllbs = "", string zt = "")
        {
            var sql = new StringBuilder(@"
SELECT dlId, ParentId, dlCode, dlmc, py 
FROM [NewtouchHIS_Base]..V_S_xt_sfdl(nolock) 
WHERE OrganizeId = @OrganizeId
");
            if (!string.IsNullOrWhiteSpace(dllbs))
            {
                var dllbArr = dllbs.Split(',').Select(p => { var i = 0; int.TryParse(p, out i); return i; }).Where(p => p > 0).Distinct().ToArray();
                if (dllbArr.Length > 0)
                {
                    sql.AppendLine("AND dllb in (" + string.Join(",", dllbArr) + ")");
                }
            }
            var pars = new List<DbParameter>
            {
                new SqlParameter("@OrganizeId", orgId)
            };
            if (!string.IsNullOrWhiteSpace(zt))
            {
                sql.AppendLine("AND zt = @zt");
                pars.Add(new SqlParameter("@zt", zt));
            }
            return FindList<SysChargeCategoryVEntity>(sql.ToString(), pars.ToArray());
        }
    }
}
