using System.Collections.Generic;
using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Common.Model;

namespace FrameworkBase.MultiOrg.Domain.IDomainServices
{
    /// <summary>
    /// 第三方目录对照
    /// </summary>
    public interface ITTCataloguesComparisonDmnService
    {
        /// <summary>
        /// 获取第三方对照项
        /// </summary>
        /// <param name="orgId">组织机构Id</param>
        /// <param name="code">分类Code</param>
        /// <param name="itemCode">小项Code</param>
        /// <param name="TTMark">第三方标示 如“yibao”</param>
        /// <returns>TTCode TTName TTExplain</returns>
        FirstSecondThird GetTTItem(string orgId, string code, string itemCode, string TTMark);

        /// <summary>
        /// 获取第三方对照项
        /// </summary>
        /// <param name="orgId">组织机构Id</param>
        /// <param name="code">分类Code</param>
        /// <param name="TTMark">第三方标示 如“yibao”</param>
        /// <returns>TTCode TTName TTExplain</returns>
        List<TTCataloguesComparisonDetailEntity> GetTTItem(string orgId, string code, string TTMark);

        /// <summary>
        /// 获取所有有效对照目录
        /// </summary>
        /// <param name="orgId">组织机构Id</param>
        /// <returns></returns>
        IList<TTCataloguesComparisonMainEntity> GetValidMainList(string orgId);

        /// <summary>
        /// 获取所有有效对照项
        /// </summary>
        /// <param name="orgId">组织机构Id</param>
        IList<TTCataloguesComparisonDetailEntity> GetDetailListByOrgId(string orgId);


    }
}
