using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Model;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Interface;

namespace FrameworkBase.MultiOrg.DmnService
{
    /// <summary>
    /// 第三方目录对照
    /// </summary>
    public class TTCataloguesComparisonDmnService : DmnServiceBase, ITTCataloguesComparisonDmnService
    {
        private readonly ICache _cache;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public TTCataloguesComparisonDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取第三方对照项
        /// </summary>
        /// <param name="orgId">组织机构Id</param>
        /// <param name="code">分类Code</param>
        /// <param name="itemCode">小项Code</param>
        /// <param name="TTMark">第三方标示 如“yibao”</param>
        /// <returns>TTCode TTName TTExplain</returns>
        public FirstSecondThird GetTTItem(string orgId, string code, string itemCode, string TTMark)
        {
            if (string.IsNullOrWhiteSpace(orgId) || string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(itemCode) || string.IsNullOrWhiteSpace(TTMark))
            {
                throw new FailedException("查询异常，缺少参数");
            }
            var sql = @"select detail.TTCode First, detail.TTName Second, detail.TTExplain Third  
from TTCataloguesComparisonDetail(nolock) detail
left join TTCataloguesComparisonMain main
on detail.MainId = main.Id and main.zt = '1'
where main.OrganizeId = @orgId and detail.OrganizeId = @orgId and detail.zt = '1' and main.Code = @code and detail.Code = @itemCode and main.TTMark = @TTMark";
            var list = this.FindList<FirstSecondThird>(sql, new[] {
                new SqlParameter("@orgId", orgId)
            ,new SqlParameter("@code", code)
            ,new SqlParameter("@itemCode", itemCode)
            ,new SqlParameter("@TTMark", TTMark)});
            if (list.Count == 1)
            {
                return list.First();
            }
            else if (list.Count == 0)
            {
                throw new FailedException("三方对照异常，" + code + "." + itemCode + "未对照");
            }
            else
            {
                throw new FailedException("三方对照异常，" + code + "." + itemCode + "一对多");
            }
        }

        /// <summary>
        /// 获取第三方对照项
        /// </summary>
        /// <param name="orgId">组织机构Id</param>
        /// <param name="code">分类Code</param>
        /// <param name="TTMark">第三方标示 如“yibao”</param>
        /// <returns>TTCode TTName TTExplain</returns>
        public List<TTCataloguesComparisonDetailEntity> GetTTItem(string orgId, string code, string TTMark)
        {
            if (string.IsNullOrWhiteSpace(orgId) || string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(TTMark))
            {
                throw new FailedException("查询异常，缺少参数");
            }
            var sql = @"
SELECT detail.*  
FROM TTCataloguesComparisonDetail(NOLOCK) detail
LEFT JOIN TTCataloguesComparisonMain main ON detail.MainId = main.Id and main.zt = '1' AND main.OrganizeId = detail.OrganizeId 
WHERE detail.OrganizeId = @orgId 
AND detail.zt = '1' 
AND main.Code = @code  
AND main.TTMark = @TTMark ";
            var param = new DbParameter[]
            {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@code", code),
                new SqlParameter("@TTMark", TTMark)
            };
            return FindList<TTCataloguesComparisonDetailEntity>(sql, param);
        }

        /// <summary>
        /// 获取所有有效对照目录
        /// </summary>
        /// <param name="orgId">组织机构Id</param>
        /// <returns></returns>
        public IList<TTCataloguesComparisonMainEntity> GetValidMainList(string orgId)
        {
            var sql = @"select * from TTCataloguesComparisonMain(nolock) where OrganizeId = @orgId and zt = '1'";
            return this.FindList<TTCataloguesComparisonMainEntity>(sql, new[] {
                new SqlParameter("@orgId", orgId)
            });
        }

        /// <summary>
        /// 获取所有有效对照项
        /// </summary>
        /// <param name="orgId">组织机构Id</param>
        public IList<TTCataloguesComparisonDetailEntity> GetDetailListByOrgId(string orgId)
        {
            var sql = @"select * from TTCataloguesComparisonDetail(nolock) where OrganizeId = @orgId and zt = '1'";
            return this.FindList<TTCataloguesComparisonDetailEntity>(sql, new[] {
                new SqlParameter("@orgId", orgId)
            });
        }

        #region 三方目录对照维护Detail

        #endregion

        #region 三方目录对照维护Main

        #endregion


    }
}
