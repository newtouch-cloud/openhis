using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 首页统计
    /// </summary>
    public interface IHomePageStatisticsApp
    {
        /// <summary>
        /// 获取代办HTML
        /// </summary>
        /// <returns></returns>
        string AssembleNeedDealHtml();

        /// <summary>
        /// 获取代办总数
        /// </summary>
        /// <returns></returns>
        NeedDealCountVO GetPendingCount();
    }
}