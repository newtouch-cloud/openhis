using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysStaffDmnService
    {
        /// <summary>
        /// 获取系统人员 分页列表（不包括子机构）
        /// </summary>
        /// <param name="topOrganizeId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysStaffVO> GetPaginationList(Pagination pagination, string OrganizeId, string keyword = null);

        /// <summary>
        /// 添加、更新 系统人员
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <param name="asLoginUser">作为系统登录用户（仅创建时）</param>
        void SubmitForm(SysStaffEntity entity, string keyValue, bool asLoginUser,out string staffId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="dutyIdList"></param>
        void UpdateStaffDuty(string staffId, string[] dutyIdList);

        void UpdateStaffWard(string staffId, string[] wardList,string OrganizeId);

        SysStaffEntity GetSysStaff(string msEmaile, string organizeId);

        SysUserStaffEntity GetUserStaff(string staffId);

        SysStaffEntity GetUserStaffByUserId(string userId, string organizeId);
        void UpdateStaffConsult(string staffId, string zsCode, string OrganizeId);

    }
}
