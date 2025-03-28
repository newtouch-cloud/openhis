using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 供应商操作
    /// </summary>
    public class GysSupplierRepo : RepositoryBase<GysSupplierEntity>, IGysSupplierRepo
    {
        public GysSupplierRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// get GysSupplierEntity list
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyWord"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public IList<GysSupplierEntity> GetList(Pagination pagination, string organizeId, string keyWord, string zt = "1")
        {
            const string sql = @"
            SELECT sup.[Id],sup.[OrganizeId],sup.[name],sup.[py],sup.[address],sup.[zipCode],sup.[tel],sup.[fax],sup.[supplierType]
            ,sup.[khh],sup.[khhzh],sup.[sh],sup.[jszq],sup.[zt],sup.[CreatorCode],sup.[CreateTime],sup.[LastModifyTime],sup.[LastModifierCode]
            FROM dbo.gys_supplier(NOLOCK) sup
            WHERE sup.OrganizeId=@OrganizeId
            AND (sup.name LIKE '%'+@keyWord+'%' OR sup.py LIKE '%'+@keyWord+'%')
            AND sup.zt=@zt ";
            var param = new DbParameter[]
            {
                new SqlParameter("@keyWord", keyWord??""),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@zt", zt)
            };
            return QueryWithPage<GysSupplierEntity>(sql, pagination, param);
        }

        /// <summary>
        /// get GysSupplierEntity list
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="keyWord"></param>
        /// <param name="supplierType">0：其他     1：生产商      2：经销商</param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public IList<GysSupplierEntity> GetList(string organizeId, string keyWord, int supplierType, string zt = "1")
        {
            const string sql = @"
SELECT sup.[Id],sup.[OrganizeId],sup.[name],sup.[py],sup.[address],sup.[zipCode],sup.[tel],sup.[fax],sup.[supplierType]
,sup.[khh],sup.[khhzh],sup.[sh],sup.[jszq],sup.[zt],sup.[CreatorCode],sup.[CreateTime],sup.[LastModifyTime],sup.[LastModifierCode]
FROM dbo.gys_supplier(NOLOCK) sup
WHERE sup.OrganizeId=@OrganizeId
AND (sup.name LIKE '%'+@keyWord+'%' OR sup.py LIKE '%'+@keyWord+'%')
AND sup.zt=@zt 
AND sup.supplierType=@supplierType 
";
            var param = new DbParameter[]
            {
                new SqlParameter("@keyWord", keyWord??""),
                new SqlParameter("@OrganizeId", organizeId??""),
                new SqlParameter("@supplierType", supplierType),
                new SqlParameter("@zt", zt)
            };
            return FindList<GysSupplierEntity>(sql, param);
        }
    }
}
