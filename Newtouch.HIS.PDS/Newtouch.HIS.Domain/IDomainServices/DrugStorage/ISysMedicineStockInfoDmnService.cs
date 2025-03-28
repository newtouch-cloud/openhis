using Newtouch.HIS.Domain.Entity.V;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.DrugStorage
{
    /// <summary>
    /// 库存操作
    /// </summary>
    public interface ISysMedicineStockInfoDmnService
    {

        /// <summary>
        /// 外部入库 扣库存(作废)
        /// </summary>
        /// <param name="sl">最小单位数量</param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="ypdm"></param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string WbrkKkc(int sl, string pc, string ph, string ypdm, string organizeId, string yfbmCode, string userCode);

        /// <summary>
        /// 冻结库存，并返回被冻结的批次信息
        /// </summary>
        /// <param name="kcsl"></param>
        /// <param name="ypdm"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string FrozenStork(int kcsl, string ypdm, string yfbmCode, string organizeId, string userCode, out List<FrozenBatchesDetailVEntity> frozenInfo);
    }
}