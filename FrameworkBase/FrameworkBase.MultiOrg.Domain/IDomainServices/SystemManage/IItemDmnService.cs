using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;

namespace FrameworkBase.MultiOrg.Domain.IDomainServices
{
    /// <summary>
    /// 字典相关
    /// </summary>
    public interface IItemDmnService
    {
        /// <summary>
        /// 获取所有字典项
        /// </summary>
        /// <param name="topOrgId">顶级组织机构</param>
        /// <param name="zt"></param>
        /// <returns></returns>
        [Obsolete("新版本不区分TopOrgId，区分OrgId,instead by GetItemsDetailListByOrgId")]
        IList<SysItemsDetailVEntity> GetItemsDetailListByTopOrg(string topOrgId, string zt = null);

        /// <summary>
        /// 获取分类的字典项
        /// </summary>
        /// <param name="topOrgId">顶级组织机构</param>
        /// <param name="code">字典分类</param>
        /// <param name="zt"></param>
        /// <returns></returns>
        [Obsolete("新版本不区分TopOrgId，区分OrgId,instead by GetItemsDetailListByOrgIdAndItemCode")]
        IList<SysItemsDetailVEntity> GetItemsDetailListByTopOrgAndItemCode(string topOrgId, string code, string zt = null);

        /// <summary>
        /// 获取所有字典项
        /// </summary>
        /// <param name="orgId">组织机构</param>
        /// <param name="zt"></param>
        /// <returns></returns>
        IList<SysItemsDetailVEntity> GetItemsDetailListByOrgId(string orgId, string zt = null);

        /// <summary>
        /// 获取分类的字典项
        /// </summary>
        /// <param name="orgId">组织机构</param>
        /// <param name="code">字典分类</param>
        /// <param name="zt"></param>
        /// <returns></returns>
        IList<SysItemsDetailVEntity> GetItemsDetailListByOrgIdAndItemCode(string orgId, string code, string zt = null);

        /// <summary>
        /// 获取所有有效字典分类
        /// </summary>
        /// <returns></returns>
        IList<SysItemsVEntity> GetValidItemTypeList();
    }
}
