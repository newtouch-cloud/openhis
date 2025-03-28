using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysWardRepo : IRepositoryBase<SysWardVEntity>
    {
        /// <summary>
        /// 获取有效
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysWardVEntity> GetbqList(string orgId);
    }
}
