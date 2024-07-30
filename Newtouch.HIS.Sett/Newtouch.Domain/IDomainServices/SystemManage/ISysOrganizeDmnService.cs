using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 组织机构 DmnService
    /// </summary>
    public interface ISysOrganizeDmnService
    {
        /// <summary>
        /// 获取组织下的所有有效组织（parent）
        /// </summary>
        /// <returns></returns>
        List<SysOrganizeVEntity> GetValidListByParentOrg(string parentOrgId);

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
        /// 获取所有的顶级组织机构
        /// </summary>
        /// <returns></returns>
        IList<SysOrganizeVEntity> GetValidTopOrgList();

        /// <summary>
        /// 获取Org名称
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetNameByOrgId(string orgId);

        /// <summary>
        /// 获取 UserId 对应的 医疗机构 List
        /// </summary>
        /// <returns></returns>
        IList<SysOrganizeVEntity> GetMedicalOrganizeListByUserId(string userId);

    }
}
