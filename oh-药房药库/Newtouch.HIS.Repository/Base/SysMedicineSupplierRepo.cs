using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 供应商
    /// </summary>
    public class SysMedicineSupplierRepo : RepositoryBase<SysMedicineSupplierVEntity>, ISysMedicineSupplierRepo
    {
        public SysMedicineSupplierRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// get Suppliers
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<SysMedicineSupplierVEntity> GetGysList(string keyword, string organizeId)
        {
            const string sql = @"
SELECT gysId,gysCode,gysmc,OrganizeId,py,zt
FROM [NewtouchHIS_Base].[dbo].[xt_ypgys](NOLOCK)
WHERE OrganizeId=@organizeId
AND zt='1'
AND (''=@keyword OR gysCode LIKE '%'+@keyword+'%' OR gysmc LIKE '%'+@keyword+'%' OR py LIKE '%'+@keyword+'%')
";
            var param = new DbParameter[]
            {
                new SqlParameter("@keyword", keyword),
                new SqlParameter("@organizeId", organizeId),
            };
            return FindList<SysMedicineSupplierVEntity>(sql, param);
        }
    }
}