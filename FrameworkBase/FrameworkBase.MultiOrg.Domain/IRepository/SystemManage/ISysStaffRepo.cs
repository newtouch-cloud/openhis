using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace FrameworkBase.MultiOrg.Domain.IRepository
{
    /// <summary>
    /// 系统人员
    /// </summary>
    public interface ISysStaffRepo : IRepositoryBase<SysStaffVEntity>
    {
        /// <summary>
        /// 获取组织机构 有效员工列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysStaffVEntity> GetValidStaffListByOrganizeId(string orgId);

        /// <summary>
        /// 根据工号 获取组织机构 有效员工列表
        /// </summary>
        /// <param name="gh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SysStaffVEntity GetValidStaffByGh(string gh, string orgId);

        /// <summary>
        /// 根据工号 获取 人员姓名
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        string GetNameByGh(string gh, string orgId);
    }
}
