using System.Collections.Generic;
using System.Linq;
using Newtouch.Core.Common.Interface;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Tools;
using Newtouch.HIS.Domain.IRepository;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysDiagnosisRepo : RepositoryBase<SysDiagnosisVEntity>, ISysDiagnosisRepo
    {
        private readonly ICache _cache;

        public SysDiagnosisRepo(IDefaultDatabaseFactory databaseFactory, ICache cache)
            : base(databaseFactory)
        {
            this._cache = cache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<SysDiagnosisVEntity> GetList(string orgId)
        {
            var sql = "SELECT * FROM NewtouchHIS_Base.[dbo].[V_S_xt_zd] with(nolock) where (OrganizeId = @orgId or OrganizeId = '*')";
            return this.FindList<SysDiagnosisVEntity>(sql, new[] { new SqlParameter("@orgId", orgId) });
        }

        /// <summary>
        /// 根据条件获取诊断下拉框
        /// </summary>
        /// <param name="zd"></param>
        /// <returns></returns>
        public IList<ZDSelect> GetzdSelect(string zd, string orgId)
        {
            var data = GetList(orgId).Where(p =>
                p.zt == "1" &&
            (p.zdmc.Contains(zd.Trim()) || p.py.Contains(zd.Trim()))
            ).ToList();
            List<ZDSelect> zdlist = new List<ZDSelect>();
            foreach (SysDiagnosisVEntity item in data)
            {
                ZDSelect a = new ZDSelect();
                a.zdbh = item.zdCode;
                a.icd10 = item.icd10 == null ? "" : item.icd10;
                a.zdmc = item.zdmc;
                a.py = item.py;
                a.zdnm = item.zdId;
                zdlist.Add(a);
            }
            return zdlist;
        }

    }
}
