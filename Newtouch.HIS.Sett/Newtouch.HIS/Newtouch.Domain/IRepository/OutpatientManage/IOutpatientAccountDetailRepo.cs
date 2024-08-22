using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOutpatientAccountDetailRepo : IRepositoryBase<OutpatientAccountDetailEntity>
    {
        /// <summary>
        /// 根据记账计划明细Id获取list
        /// </summary>
        /// <returns></returns>
        IList<OutpatientAccountDetailEntity> GetListbyjzjhids(string orgId, IList<string> mxIdList);
    }
}
