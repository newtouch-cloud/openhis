using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;
using FrameworkBase.Domain.Entity;

namespace FrameworkBase.Domain.IRepository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:13
    /// 描 述：系统日志
    /// </summary>
    public interface ISysLogRepo : IRepositoryBase<SysLogEntity>
    {
        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        List<SysLogEntity> GetPaginationList(Pagination pagination, string queryJson);

    }
}