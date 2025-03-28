using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class HospMultiDiagnosisRepo : RepositoryBase<HospMultiDiagnosisEntity>, IHospMultiDiagnosisRepo
    {
        public HospMultiDiagnosisRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        /// <summary>
        /// 用过住院号获取诊断信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public List<HospMultiDiagnosisEntity> GetDiagnoseByZYH(string zyh)
        {
            return this.IQueryable().Where(p => p.zyh == zyh).ToList();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<HospMultiDiagnosisEntity> SelectData(string zyh, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.zy_rydzd(NOLOCK) 
WHERE zyh=@zyh AND OrganizeId=@OrganizeId AND zt='1'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@zyh", zyh),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return FindList<HospMultiDiagnosisEntity>(sql, param);
        }
    }
}


