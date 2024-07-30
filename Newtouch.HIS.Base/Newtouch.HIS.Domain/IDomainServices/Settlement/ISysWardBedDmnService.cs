using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.Settlement
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysWardBedDmnService
    {
        /// <summary>
        /// 获得所有列表、修改form
        /// </summary>
        List<SysWardBedVO> GetWardBedList(int? cwId, string orgId = null, string keyword = null);

        /// <summary>
        /// 
        /// </summary>
        IList<SysWardBedVO> GetPagintionList(string orgId, Pagination pagination, string keyword = null);

    }
}
