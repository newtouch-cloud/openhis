using System.Collections.Generic;
using Newtouch.Infrastructure.EF;
using FrameworkBase.Domain.Entity;

namespace FrameworkBase.Domain.IRepository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:15
    /// 描 述：系统人员
    /// </summary>
    public interface ISysStaffRepo : IRepositoryBase<SysStaffEntity>
    {
        /// <summary>
        /// 获取有效实体列表
        /// </summary>
        /// <param name="keyword">筛选关键字</param>
        IList<SysStaffEntity> GetValidList(string keyword = null);




    }
}