using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 字典、字典项 DmnService
    /// </summary>
    public interface IItemDmnService
    {
        /// <summary>
        /// 获取有效字典项 列表 指定组织机构
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IList<SysItemsDetailVEntity> GetValidItemsDetailListByTopOrgAndItemCode(string organizeId, string code);

        /// <summary>
        /// 获取所有字典分类
        /// </summary>
        /// <param name="topOrgId"></param>
        /// <returns></returns>
        IList<SysItemsVEntity> GetValidItemsList();

        /// <summary>
        /// 获取所有字典项
        /// </summary>
        /// <param name="topOrgId"></param>
        /// <returns></returns>
        IList<SysItemsDetailVEntity> GetValidItemsDetailListByTopOrg(string topOrgId);

    }
}
