using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;

namespace Newtouch.Repository
{
    /// <summary>
    /// 中医馆推送患者信息
    /// </summary>
    public class CmmHis01Repo : RepositoryBase<CmmHis01Entity>, ICmmHis01Repo
    {
        public CmmHis01Repo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public CmmHis01Entity SelectData(string id, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.cmm_his_01(NOLOCK) 
WHERE Id=@id AND OrganizeId=@OrganizeId AND zt='1'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return FirstOrDefault<CmmHis01Entity>(sql, param);
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public CmmHis01Entity SelectDataByMzh(string mzh, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.cmm_his_01(NOLOCK) 
WHERE outpatientNo=@mzh AND OrganizeId=@OrganizeId AND zt='1'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@mzh", mzh),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return FirstOrDefault<CmmHis01Entity>(sql, param);
        }
    }
}