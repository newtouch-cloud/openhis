using System.Collections.Generic;
using Newtouch.Infrastructure.EF;
using FrameworkBase.Domain.Entity;

namespace FrameworkBase.Domain.IRepository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:20
    /// 描 述：用户人员对照表
    /// </summary>
    public interface ISysUserStaffRepo : IRepositoryBase<SysUserStaffEntity>
    {
        /// <summary>
        /// 获取关联人员Id列表 根据UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<string> GetStaffIdListByUserId(string userId);

        /// <summary>
        /// 提交 用户关联人员
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="staffIds"></param>
        void SubmitUserStaff(string userId, string staffIds);

    }
}