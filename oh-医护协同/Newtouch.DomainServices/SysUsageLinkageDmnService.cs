using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ViewModels;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Newtouch.DomainServices
{
    /// <summary>
    /// 用法联动
    /// </summary>
    public class SysUsageLinkageDmnService : DmnServiceBase, ISysUsageLinkageDmnService
    {
        public SysUsageLinkageDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// Select SysUsageLinkage
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="yfCode"></param>
        /// <param name="xmCode"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<SysUsageLinkageVO> SelectSysUsageLinkage(Pagination pagination, string keyword, string yfCode, string xmCode, string OrganizeId)
        {
            var sql = new StringBuilder(@"
SELECT ul.Id, yf.yfmc, yf.yfCode, sfxm.sfxmmc, sfxm.sfxmCode, sfdl.dlmc, sfdl.dlCode, ul.zt, ul.CreateTime, ul.CreatorCode, ul.LastModifyTime, ul.LastModifierCode
FROM dbo.xt_usageLinkage(NOLOCK) ul
INNER JOIN NewtouchHIS_Base.dbo.xt_sfxm(NOLOCK) sfxm ON sfxm.sfxmCode=ul.sfxmCode AND sfxm.zt='1' AND sfxm.OrganizeId=ul.OrganizeId
LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode=ul.dlCode AND sfdl.OrganizeId=ul.OrganizeId AND sfdl.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.xt_ypyf(NOLOCK) yf ON yf.yfCode=ul.yfCode AND yf.zt='1' 
WHERE ul.OrganizeId=@OrganizeId ");
            var param = new List<SqlParameter> {
                new SqlParameter("@OrganizeId", OrganizeId)
            };
            if (!string.IsNullOrWhiteSpace(yfCode))
            {
                sql.AppendLine("AND ul.yfCode=@yfCode ");
                param.Add(new SqlParameter("@yfCode", yfCode));
            }
            if (!string.IsNullOrWhiteSpace(xmCode))
            {
                sql.AppendLine("AND ul.sfxmCode=@sfxmCode ");
                param.Add(new SqlParameter("@sfxmCode", xmCode));
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.AppendLine("AND (sfxm.sfxmmc LIKE '%'+@keywork+'%' OR yf.yfmc LIKE ''+@keywork+'') ");
                param.Add(new SqlParameter("@keywork", keyword));
            }
            return QueryWithPage<SysUsageLinkageVO>(sql.ToString(), pagination, param.ToArray());
        }

        /// <summary>
        /// Select SysUsageLinkage by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysUsageLinkageVO SelectSysUsageLinkage(string id)
        {
            var sql = new StringBuilder(@"
SELECT ul.Id, yf.yfmc, yf.yfCode, sfxm.sfxmmc, sfxm.sfxmCode, sfdl.dlmc, sfdl.dlCode, ul.zt, ul.CreateTime, ul.CreatorCode, ul.LastModifyTime, ul.LastModifierCode
FROM dbo.xt_usageLinkage(NOLOCK) ul
INNER JOIN NewtouchHIS_Base.dbo.xt_sfxm(NOLOCK) sfxm ON sfxm.sfxmCode=ul.sfxmCode AND sfxm.zt='1' AND sfxm.OrganizeId=ul.OrganizeId
LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode=ul.dlCode AND sfdl.OrganizeId=ul.OrganizeId AND sfdl.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.xt_ypyf(NOLOCK) yf ON yf.yfCode=ul.yfCode AND yf.zt='1' 
WHERE ul.Id=@id ");
            var param = new SqlParameter[] {
                new SqlParameter("@id", id)
            };
            return FirstOrDefault<SysUsageLinkageVO>(sql.ToString(), param);
        }
    }
}
