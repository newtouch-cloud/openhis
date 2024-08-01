using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.ValueObjects;
using Newtouch.Core.Common;
using System.Collections.Generic;

namespace FrameworkBase.Domain.IDomainServices
{
    /// <summary>
    /// 人员相关
    /// </summary>
    public interface ISysStaffDmnService
    {
        /// <summary>
        /// 添加、更新 系统人员
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <param name="asLoginUser">作为系统登录用户（仅创建时）</param>
        void SubmitForm(SysStaffEntity entity, string keyValue, bool asLoginUser);

        /// <summary>
        /// 更新人员岗位
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="dutyIdList"></param>
        void UpdateStaffDuty(string staffId, string[] dutyIdList);

        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysStaffVO> GetPaginationStaffList(Pagination pagination, string keyword);

    }
}
