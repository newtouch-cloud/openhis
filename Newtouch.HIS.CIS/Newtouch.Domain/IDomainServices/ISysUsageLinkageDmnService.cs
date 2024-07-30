using Newtouch.Core.Common;
using Newtouch.Domain.ViewModels;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// 用法联动
    /// </summary>
    public interface ISysUsageLinkageDmnService
    {
        /// <summary>
        /// Select SysUsageLinkage
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="yfCode"></param>
        /// <param name="xmCode"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        IList<SysUsageLinkageVO> SelectSysUsageLinkage(Pagination pagination, string keyword, string yfCode, string xmCode, string OrganizeId);

        /// <summary>
        /// Select SysUsageLinkage by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SysUsageLinkageVO SelectSysUsageLinkage(string id);
    }
}
