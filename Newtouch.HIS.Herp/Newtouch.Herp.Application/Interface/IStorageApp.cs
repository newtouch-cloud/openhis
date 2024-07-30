using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// 库存维护
    /// </summary>
    public interface IStorageApp
    {
        /// <summary>
        /// 提交外部入库
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns>error message</returns>
        string InStorageSubmit(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx);

        /// <summary>
        /// 提交外部出库
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns>error message</returns>
        string OutStorageSubmit(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx);

        /// <summary>
        /// 提交直接出库
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns></returns>
        string DirectDeliverySubmit(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx);

        /// <summary>
        /// 提交内部发货退回
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns></returns>
        string DeliveryOfReturnSubmit(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx);
    }
}