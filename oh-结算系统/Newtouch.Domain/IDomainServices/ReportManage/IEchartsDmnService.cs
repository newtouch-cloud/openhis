using Newtouch.HIS.Domain.ValueObjects.ReportManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices.ReportManage
{
    public interface IEchartsDmnService
    {
        /// <summary>
        /// 绩效查询 图标
        /// </summary>
        List<PerformanceIndicatorVO> SelectPerformanceIndicator(string orgId, string year);

        /// <summary>
        /// 门诊人次月份图表
        /// </summary>
        List<PerformanceIndicatorVO> PerformanceMonthIndicator(string orgId, string year, string month);
    }
}
