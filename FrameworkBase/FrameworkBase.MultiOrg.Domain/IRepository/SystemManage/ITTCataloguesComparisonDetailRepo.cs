using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace FrameworkBase.MultiOrg.Repository
{
    /// <summary>
    /// 目录对照子表
    /// </summary>
    public interface ITTCataloguesComparisonDetailRepo : IRepositoryBase<TTCataloguesComparisonDetailEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="mainId"></param>
        /// <returns></returns>
        IList<TTCataloguesComparisonDetailEntity> GetListByMainId(string keyword, string mainId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        void SubmitForm(string keyValue, TTCataloguesComparisonDetailEntity entity);




    }
}
