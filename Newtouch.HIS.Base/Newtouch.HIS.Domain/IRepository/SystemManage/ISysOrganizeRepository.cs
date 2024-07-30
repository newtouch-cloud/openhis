using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysOrganizeRepository : IRepositoryBase<SysOrganizeEntity>
    {
        /// <summary>
        /// 获取组织下的所有组织
        /// </summary>
        /// <returns></returns>
        List<SysOrganizeEntity> GetListByTopOrg(string topOrganizeId);

        /// <summary>
        /// 获取组织下的所有有效组织（top）
        /// </summary>
        /// <returns></returns>
        List<SysOrganizeEntity> GetValidListByTopOrg(string topOrganizeId);

        /// <summary>
        /// 获取组织下的所有有效组织（parent）
        /// </summary>
        /// <returns></returns>
        List<SysOrganizeVO> GetValidListByParentOrg(string parentOrgId);

        /// <summary>
        /// 获取组织下的所有组织（parent）（包括无效的）
        /// </summary>
        /// <returns></returns>
        List<SysOrganizeVO> GetListByParentOrg(string parentOrgId);

        /// <summary>
        /// 获取顶级组织机构列表 包括无效的
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysOrganizeEntity> GetPagintionListByTopOrg(Pagination pagination, string keyword = null);

        /// <summary>
        /// 获取顶级组织机构列表 包括无效的
        /// </summary>
        /// <returns></returns>
        List<SysOrganizeEntity> GetTopOrgList();

        /// <summary>
        /// 获取顶级组织机构列表
        /// </summary>
        /// <returns></returns>
        List<SysOrganizeEntity> GetValidTopOrgList();

        /// <summary>
        /// 提交新建、更新 实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysOrganizeEntity entity, string keyValue);

        /// <summary>
        /// 根据OrgId获取Code
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetCodeById(string orgId);
    }
}
