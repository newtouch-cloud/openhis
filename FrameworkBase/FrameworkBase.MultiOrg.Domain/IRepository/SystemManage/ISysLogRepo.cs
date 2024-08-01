using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace FrameworkBase.MultiOrg.Domain.IRepository
{
    /// <summary>
    /// SysLog系统日志
    /// </summary>
    public interface ISysLogRepo : IRepositoryBase<SysLogEntity>
    {
        /// <summary>
        /// SysLog日志查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        List<SysLogEntity> GetPaginationList(Pagination pagination, string orgId, string queryJson);

    }
}
