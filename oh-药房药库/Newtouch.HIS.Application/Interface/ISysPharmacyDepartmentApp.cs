using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.PDS.Requset.PharmacyDepartment;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysPharmacyDepartmentApp
    {

        /// <summary>
        /// 根据药房代码获取药库List（药房可向目标药库申领）
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentVEntity> GetTheUpperYkbmCodeList(string keyword, string yfbmCode, string organizeId);

        /// <summary>
        /// 根据药库代码获取药房List（药库可向目标药房发药）
        /// </summary>
        /// <param name="ykbmCode"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentVEntity> GetTheLowerYfbmCodeList(string ykbmCode);

        /// <summary>
        /// 根据药房代码获取科室List（药房可向目标科室发药）
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        IList<SysDepartmentVEntity> GetTheLowerKsCodeList(string yfbmCode);

        /// <summary>
        /// 药品授权药房部门
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        string EmpowermentPharmacyDepartment(EmpowermentPharmacyDepartmentRequestDto req);

        /// <summary>
        /// 药品授权药房部门 与EmpowermentPharmacyDepartment不同，该方法会先取消该药品所有药房的授权，在重新赋予权限
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        string EmpowermentPharmacyDepartmentAndRemoveOld(EmpowermentPharmacyDepartmentAndRemoveOldRequestDto req);
    }
}
