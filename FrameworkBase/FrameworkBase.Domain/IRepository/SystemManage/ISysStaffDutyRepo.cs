using System.Collections.Generic;
using Newtouch.Infrastructure.EF;
using FrameworkBase.Domain.Entity;

namespace FrameworkBase.Domain.IRepository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:15
    /// 描 述：人员岗位对照
    /// </summary>
    public interface ISysStaffDutyRepo : IRepositoryBase<SysStaffDutyEntity>
    {
        /// <summary>
        /// 获取人员 已 关联 岗位 关联关系列表
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        IList<SysStaffDutyEntity> GetListByStaffId(string staffId);

    }
}