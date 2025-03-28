using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHospSettlementRepo : IRepositoryBase<HospSettlementEntity>
    {
        /// <summary>
        /// 根据住院号查询所有的计算记录，把已结和已冲销的记录进行对冲，得到未冲销的数据
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        List<HospSettlementEntity> GetValidList(string zyh, string orgId);
    }
}
