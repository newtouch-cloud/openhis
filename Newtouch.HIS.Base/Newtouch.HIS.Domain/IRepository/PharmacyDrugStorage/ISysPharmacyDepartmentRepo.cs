using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysPharmacyDepartmentRepo : IRepositoryBase<SysPharmacyDepartmentEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="yjbmjb"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentEntity> GetList(string orgId, byte? yjbmjb);

        /// <summary>
        /// 获取有效的药房和药库
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="yjbmjb"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentEntity> GetEffectiveList(string orgId, byte? yjbmjb);

        /// <summary>
        /// 获取药房部门信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentEntity> GetPagintionList(Pagination pagination, string organizeId, string keyword = null);

        /// <summary>
        /// 修改和添加药房部门
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void submitForm(SysPharmacyDepartmentEntity entity, int? keyValue);

        /// <summary>
        /// 获取UserId当前已关联的要yfbmCode （不包括子机构）
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IList<string> GetCurPharmacyList(string userId, string orgId);

        /// <summary>
        /// 获取组织机构下的药房/药库 （不包括子机构）
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentEntity> GetPharmacyListByOrg(string OrganizeId);
    }
}
