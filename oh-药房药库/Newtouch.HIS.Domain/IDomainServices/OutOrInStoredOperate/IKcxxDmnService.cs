namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 库存操作
    /// </summary>
    public interface IKcxxDmnService
    {
        /// <summary>
        /// 冻结库存  扣库存使用  内部发药退回
        /// </summary>
        /// <param name="ypdm"></param>
        /// <param name="organizeid"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="sl"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="rkbm"></param>
        /// <returns></returns>
        string FrozenStockReduceByReturninward(string ypdm, string organizeid, string pc, string ph, int sl, string yfbmCode, string rkbm);

        /// <summary>
        /// 冻结库存  扣库存 直接出库使用
        /// </summary>
        /// <param name="ypdm"></param>
        /// <param name="organizeid"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="sl"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="rkbm"></param>
        /// <returns></returns>
        string FrozenStockReduceByDeliveryDirect(string ypdm,
           string organizeid,
           string pc,
           string ph,
           int sl,
           string yfbmCode);

        /// <summary>
        /// 扣库存
        /// </summary>
        /// <param name="ypdm">药品代码</param>
        /// <param name="sl">最小单位数量</param>
        /// <param name="pc">批次</param>
        /// <param name="ph">批号</param>
        /// <param name="yfbmCode">需扣库存的药房部门</param>
        /// <param name="organizeid">组织机构</param>
        /// <returns></returns>
        string SubtractStock(string ypdm, int sl, string pc, string ph, string yfbmCode, string organizeid);
    }
}