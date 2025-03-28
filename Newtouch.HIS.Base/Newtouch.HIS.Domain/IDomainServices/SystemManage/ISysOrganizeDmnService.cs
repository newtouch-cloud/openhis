using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 组织机构DmnService
    /// </summary>
    public interface ISysOrganizeDmnService
    {
        /// <summary>
        /// 获取组织机构已授权的应用列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysApplicationEntity> GetAuthedAppListByTopOrgId(string topOrgId);

        /// <summary>
        /// 是否含有下级机构，有下级机构返回true
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool IsHasLowerOrganize(string orgId);

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
        /// 获取Org名称
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetNameByOrgId(string orgId);

        /// <summary>
        /// 获取 UserId 对应的 医疗机构 List
        /// </summary>
        /// <returns></returns>
        IList<SysOrganizeEntity> GetMedicalOrganizeListByUserId(string userId);

        #region 科室

        /// <summary>
        /// 获取科室列表
        /// </summary>
        /// <returns></returns>
        List<SysDepartmentVO> GetListByOrg(string organizeId);

        /// <summary>
        /// 更新科室关联病区
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="wardList">病区Code List</param>
        /// <param name="OrganizeId"></param>
        void UpdateDepartmentWard(string deptId, string[] wardList, string OrganizeId);

        #endregion
    }
}
