using System.Collections.Generic;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.ValueObjects;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// home app
    /// </summary>
    public interface IHomeApp
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

        /// <summary>
        /// 获取住院月处方发药次
        /// </summary>
        /// <returns></returns>
        MonthlySummaryDTO GetPsiCountVoByKf(List<VPsiStatisticsByDateEntity> val);

        /// <summary>
        /// 转换类型名称
        /// </summary>
        /// <param name="source"></param>
        void TransformRkCount(List<ClassificationStatisticsEntity> source);
    }
}