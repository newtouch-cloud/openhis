using System.Collections.Generic;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 系统药房部门
    /// </summary>
    public interface ISysPharmacyDepartmentApp
    {
        /// <summary>
        /// 获取该药品授权的组织机构
        /// </summary>
        /// <param name="ypId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysPharmacyDepartmentVEntity> EmpowermentPharmacyDepartmentQuery(string ypId, string organizeId);

        /// <summary>
        /// 授权药房部门
        /// </summary>
        /// <param name="ypId"></param>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="epds"></param>
        /// <returns></returns>
        string SubmitEmpowermentPharmacyDepartment(int? ypId, string ypCode, string organizeId, string userCode,List<string> epds);
    }
}