using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkBase.MultiOrg.Repository
{
    /// <summary>
    /// 目录对照主表
    /// </summary>
    public interface ITTCataloguesComparisonMainRepo : IRepositoryBase<TTCataloguesComparisonMainEntity>
    {
        /// <summary>
        /// 获取所有对照目录
        /// </summary>
        /// <param name="orgId">组织机构Id</param>
        /// <returns></returns>
        IList<TTCataloguesComparisonMainEntity> GetList(string orgId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        void SubmitForm(string keyValue, TTCataloguesComparisonMainEntity entity);
    }
}
