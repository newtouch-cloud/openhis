using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 门诊新农合结算
    /// </summary>
    public interface IOutpatientXnhSettlementResultRepo : IRepositoryBase<OutpatientXnhSettlementResultEntity>
    {

        /// <summary>
        /// 根据outpId获取数据
        /// </summary>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<OutpatientXnhSettlementResultEntity> SelectData(string outpId, string organizeId);

        /// <summary>
        /// 修改结算标志
        /// </summary>
        /// <param name="jszt">结算状态 1-已结 2-已退   默认已结</param>
        /// <param name="outpId"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int UpdateJszt(int jszt, string outpId, string userCode, string organizeId);
    }
}