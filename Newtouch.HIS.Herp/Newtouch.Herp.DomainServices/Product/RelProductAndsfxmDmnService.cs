using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.ValueObjects;

namespace Newtouch.Herp.DomainServices
{
    /// <summary>
    /// 物资收费项目对照
    /// </summary>
    public class RelProductAndsfxmDmnService : DmnServiceBase, IRelProductAndsfxmDmnService
    {
        public RelProductAndsfxmDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 查询物资收费项目对照管理
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sfdlCode"></param>
        /// <param name="productTypeId"></param>
        /// <param name="organizeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public IList<RelProductAndsfxmVo> SelectProductAndsfxm(Pagination pagination, string sfdlCode, string productTypeId, string organizeId, string zt)
        {
            var sql = new StringBuilder(@"
SELECT relps.Id, relps.productId, wz.name productName, wzlb.Id productTypeId, wzlb.name productTypeName, relps.sfdlCode, relps.sfdlmc, relps.OrganizeId
,relps.sfxmCode, relps.sfxmmc, relps.zt, relps.CreatorCode, relps.CreateTime, relps.LastModifyTime, relps.LastModifierCode
FROM dbo.rel_productAndsfxm(NOLOCK) relps
INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=relps.productId AND wz.OrganizeId=relps.OrganizeId AND wz.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.wz_type(NOLOCK) wzlb ON wzlb.Id=wz.typeId AND wzlb.zt='1'
WHERE relps.OrganizeId=@OrganizeId ");
            var param = new List<DbParameter>
            {
                new  SqlParameter("@OrganizeId", organizeId)
            };
            if (!string.IsNullOrWhiteSpace(zt))
            {
                sql.AppendLine("AND relps.zt=@zt ");
                param.Add(new SqlParameter("@zt", zt));
            }
            if (!string.IsNullOrWhiteSpace(sfdlCode))
            {
                sql.AppendLine("AND relps.sfdlCode=@sfdlCode ");
                param.Add(new SqlParameter("@sfdlCode", sfdlCode));
            }
            if (!string.IsNullOrWhiteSpace(productTypeId))
            {
                sql.AppendLine("AND wzlb.Id=@wzlb ");
                param.Add(new SqlParameter("@wzlb", productTypeId));
            }
            return QueryWithPage<RelProductAndsfxmVo>(sql.ToString(), pagination, param.ToArray());
        }

        /// <summary>
        /// 查询物资收费项目对照管理
        /// </summary>
        /// <param name="relId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public RelProductAndsfxmVo SelectProductAndsfxm(string relId, string organizeId)
        {
            const string sql = @"
SELECT relps.Id, relps.productId, wz.name productName, wzlb.Id productTypeId, wzlb.name productTypeName, relps.sfdlCode, relps.sfdlmc, relps.OrganizeId
,relps.sfxmCode, relps.sfxmmc, relps.zt, relps.CreatorCode, relps.CreateTime, relps.LastModifyTime, relps.LastModifierCode
FROM dbo.rel_productAndsfxm(NOLOCK) relps
INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=relps.productId AND wz.OrganizeId=relps.OrganizeId AND wz.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.wz_type(NOLOCK) wzlb ON wzlb.Id=wz.typeId AND wzlb.zt='1'
WHERE relps.OrganizeId=@OrganizeId
AND relps.Id=@id ";
            var param = new DbParameter[]
            {
                new  SqlParameter("@OrganizeId", organizeId),
                new  SqlParameter("@id", relId)
            };
            return FirstOrDefault<RelProductAndsfxmVo>(sql, param.ToArray());
        }
    }
}
