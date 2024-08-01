using FrameworkBase.MultiOrg.Domain.Entity;
using System.Collections.Generic;

namespace FrameworkBase.MultiOrg.Domain.IDomainServices
{
    /// <summary>
    /// 组织机构（医疗机构）
    /// </summary>
    public interface ISysOrganizeDmnService
    {
        /// <summary>
        /// 获取组织下的所有有效组织机构（parent）
        /// </summary>
        /// <param name="parentOrgId"></param>
        /// <param name="containsSelf"></param>
        /// <returns></returns>
        List<SysOrganizeVEntity> GetValidListByParentOrg(string parentOrgId, bool? containsSelf = true);

        /// <summary>
        /// 根据Id获取组织机构名称
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetNameByOrgId(string orgId);

        /// <summary>
        /// 获取 UserId 对应的 机构 List（关联了机构内的人员）
        /// </summary>
        /// <returns></returns>
        IList<SysOrganizeVEntity> GetMedicalOrganizeListByUserId(string userId);
        
        /// <summary>
        /// 获取 医疗机构 List
        /// </summary>
        /// <returns></returns>
        IList<SysOrganizeVEntity> GetMedicalOrganizeList();

        /// <summary>
        /// 通过机构类别来判断 是否 是 医院
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsHospital(string orgId);

        /// <summary>
        /// 通过机构类别来判断 是否 是 诊所
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsClinic(string orgId);

        /// <summary>
        /// 通过机构类别来判断 是否 是 医疗机构（有具体的业务）
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsMedicalOrganize(string orgId);

        /// <summary>
        /// 根据Id获取组织机构Code
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetCodeByOrgId(string orgId);

    }
}
