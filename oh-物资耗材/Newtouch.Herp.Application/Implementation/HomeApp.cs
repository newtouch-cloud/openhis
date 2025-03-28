using System;
using System.Collections.Generic;
using System.Text;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.ValueObjects;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Tools;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// home app
    /// </summary>
    public class HomeApp : AppBase, IHomeApp
    {
        private readonly IWzProductDmnService _wzProductDmnService;

        /// <summary>
        /// 获取代办HTML
        /// </summary>
        /// <returns></returns>
        public string AssembleNeedDealHtml()
        {
            var result = "";
            switch (Constants.CurrentKf.currentKfLevel)
            {
                case (int)EnumWarehouseLevel.OneLevel:
                    result = AssembleZkfNeedDelaHtml();
                    break;
                default:
                    result = AssembleKskfNeedDelaHtml();
                    break;
            }
            return result;
        }

        /// <summary>
        /// 总库房
        /// </summary>
        /// <returns></returns>
        private string AssembleZkfNeedDelaHtml()
        {
            return new StringBuilder(@"
                    <div class='dashboard-stats' menuName='调价审核' menuUrl='/ProductManage/PriceAdjustment/PriceAdjustmentApproval'>
                        <div class='dashboard-stats-item' style='background-color: #fc8675;'>
                            <h2 class='m-top-none' id='h_tjshCount'></h2>
                            <h5>调价审核</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-cny fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-stats' menuName='出库待审批' menuUrl='/BillManage/OutOrInStorageBill/Approval?djlx=").Append((int)EnumOutOrInStorageBillType.Wbck + @"'>
                        <div class='dashboard-stats-item' style='background-color: #65cea7;'>
                            <h2 class='m-top-none' id='h_ckdshCount'></h2>
                            <h5>出库待审批</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-truck fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-stats' menuName='外部入库待审核' menuUrl='/BillManage/OutOrInStorageBill/Approval?djlx=").Append((int)EnumOutOrInStorageBillType.Wbrk + @"'>
                        <div class='dashboard-stats-item' style='background-color: #65cea7;'>
                            <h2 class='m-top-none' id='h_wbrkCount'></h2>
                            <h5>外部入库待审核</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-truck fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-stats' menuName='内部退货审核' menuUrl='/BillManage/OutOrInStorageBill/Approval?djlx=").Append((int)EnumOutOrInStorageBillType.Nbth + @"'>
                        <div class='dashboard-stats-item' style='background-color: #65cea7;'>
                            <h2 class='m-top-none' id='h_nbthCount'></h2>
                            <h5>内部退货审核</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-truck fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-stats' menuName='过期物资' menuUrl='/StorageManage/Storage/ExpiredStorageQuery'>
                        <div class='dashboard-stats-item' style='background-color: #fc8675;'>
                            <h2 class='m-top-none' id='h_expriedWzCount'></h2>
                            <h5>过期物资</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-clock-o fa-3x'></i>
                            </div>
                        </div>
                    </div> ").ToString();
        }

        /// <summary>
        /// 科室库房
        /// </summary>
        /// <returns></returns>
        private string AssembleKskfNeedDelaHtml()
        {
            return new StringBuilder(@"
                    <div class='dashboard-stats' menuName='调价物资' menuUrl='/ProductManage/PriceAdjustment/PriceAdjustmentApproval'>
                        <div class='dashboard-stats-item' style='background-color: #fc8675;'>
                            <h2 class='m-top-none' id='h_tjwzCount'></h2>
                            <h5>调价物资</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-exchange fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-stats' menuName='入库审核' menuUrl='/BillManage/OutOrInStorageBill/Approval?djlx=").Append((int)EnumOutOrInStorageBillType.Zjck + @"'>
                        <div class='dashboard-stats-item' style='background-color: #65cea7;'>
                            <h2 class='m-top-none' id='h_rkdshCount'></h2>
                            <h5>入库审核</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-truck fa-3x'></i>
                            </div>
                        </div>
                    </div>
                    <div class='dashboard-stats' menuName='过期物资' menuUrl='/StorageManage/Storage/WarehouseStorageQuery?ygq=true'>
                        <div class='dashboard-stats-item' style='background-color: #fc8675;'>
                            <h2 class='m-top-none' id='h_expriedWzCount'></h2>
                            <h5>过期物资</h5>
                            <div class='stat-icon'>
                                <i class='fa fa-clock-o fa-3x'></i>
                            </div>
                        </div>
                    </div> ").ToString();
        }

        /// <summary>
        /// 获取代办总数
        /// </summary>
        /// <returns></returns>
        public NeedDealCountVO GetPendingCount()
        {
            NeedDealCountVO result;
            switch (Constants.CurrentKf.currentKfLevel)
            {
                case (int)EnumWarehouseLevel.OneLevel:
                    result = _wzProductDmnService.GetNeedDealCountByZkf(Constants.CurrentKf.currentKfId, OrganizeId);
                    break;
                default:
                    result = _wzProductDmnService.GetNeedDealCountByKskf(Constants.CurrentKf.currentKfId, OrganizeId);
                    break;
            }
            return result;
        }

        /// <summary>
        /// 获取住院月处方发药次
        /// </summary>
        /// <returns></returns>
        public MonthlySummaryDTO GetPsiCountVoByKf(List<VPsiStatisticsByDateEntity> val)
        {
            var monthlySummary = new MonthlySummaryDTO();
            if (val != null && val.Count > 0)
            {
                val.ForEach(p =>
                {
                    monthlySummary.ItemName = p.itemName;
                    if (p.statisticsDate.Equals(DateTime.Now.Year.ToString() + "-01"))
                    {
                        monthlySummary.January = p.sl;
                    }
                    else if (p.statisticsDate.Equals(DateTime.Now.Year.ToString() + "-02"))
                    {
                        monthlySummary.February = p.sl;
                    }
                    else if (p.statisticsDate.Equals(DateTime.Now.Year.ToString() + "-03"))
                    {
                        monthlySummary.March = p.sl;
                    }
                    else if (p.statisticsDate.Equals(DateTime.Now.Year.ToString() + "-04"))
                    {
                        monthlySummary.April = p.sl;
                    }
                    else if (p.statisticsDate.Equals(DateTime.Now.Year.ToString() + "-05"))
                    {
                        monthlySummary.May = p.sl;
                    }
                    else if (p.statisticsDate.Equals(DateTime.Now.Year.ToString() + "-06"))
                    {
                        monthlySummary.June = p.sl;
                    }
                    else if (p.statisticsDate.Equals(DateTime.Now.Year.ToString() + "-07"))
                    {
                        monthlySummary.July = p.sl;
                    }
                    else if (p.statisticsDate.Equals(DateTime.Now.Year.ToString() + "-08"))
                    {
                        monthlySummary.August = p.sl;
                    }
                    else if (p.statisticsDate.Equals(DateTime.Now.Year.ToString() + "-09"))
                    {
                        monthlySummary.September = p.sl;
                    }
                    else if (p.statisticsDate.Equals(DateTime.Now.Year.ToString() + "-10"))
                    {
                        monthlySummary.October = p.sl;
                    }
                    else if (p.statisticsDate.Equals(DateTime.Now.Year.ToString() + "-11"))
                    {
                        monthlySummary.November = p.sl;
                    }
                    else if (p.statisticsDate.Equals(DateTime.Now.Year.ToString() + "-12"))
                    {
                        monthlySummary.December = p.sl;
                    }
                });
            }
            return monthlySummary;
        }

        /// <summary>
        /// 转换类型名称
        /// </summary>
        /// <param name="source"></param>
        public void TransformRkCount(List<ClassificationStatisticsEntity> source)
        {
            if (source == null || source.Count <= 0) return;
            EnumOutOrInStorageBillType t;
            source.ForEach(p =>
            {
                Enum.TryParse(p.name, out t);
                switch (t)
                {
                    case EnumOutOrInStorageBillType.Bsby:
                        p.name = EnumOutOrInStorageBillType.Bsby.GetDescription();
                        break;
                    case EnumOutOrInStorageBillType.Nbth:
                        p.name = EnumOutOrInStorageBillType.Nbth.GetDescription();
                        break;
                    case EnumOutOrInStorageBillType.Slck:
                        p.name = EnumOutOrInStorageBillType.Slck.GetDescription();
                        break;
                    case EnumOutOrInStorageBillType.Wbck:
                        p.name = EnumOutOrInStorageBillType.Wbck.GetDescription();
                        break;
                    case EnumOutOrInStorageBillType.Wbrk:
                        p.name = EnumOutOrInStorageBillType.Wbrk.GetDescription();
                        break;
                    case EnumOutOrInStorageBillType.Zjck:
                        p.name = EnumOutOrInStorageBillType.Zjck.GetDescription();
                        break;
                }
            });
        }
    }
}
