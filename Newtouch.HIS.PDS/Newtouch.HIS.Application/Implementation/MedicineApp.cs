using System;
using Newtouch.Core.Common;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Application;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.DO;
using Newtouch.HIS.Domain.IDomainServices;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class MedicineApp : AppBase, IMedicineApp
    {
        private readonly ISysMedicineProfitLossReasonRepo _sysMedicineProfitLossReasonRepo;
        private readonly IMedicineDmnService _medicineDmnService;
        private readonly IPyDmnService _pyDmnService;

        #region 报损报溢
        /// <summary>
        /// 根据损益类型获取损益原因
        /// </summary>
        /// <param name="sylx"></param>
        /// <returns></returns>
        public List<SysMedicineProfitLossReasonEntity> GetLossProfitReasonListByType(string sylx)
        {
            return _sysMedicineProfitLossReasonRepo.GetLossProfitReasonListByType(sylx);
        }

        /// <summary>
        /// 查询损益药品list
        /// </summary>
        /// <param name="inputCode">关键字</param>
        /// <returns></returns>
        public List<ReportLossAndProfitMedicineInfoVO> SelectLossAndProfitMedicineList(string inputCode)
        {
            return _medicineDmnService.SelectLossAndProfitMedicineList(inputCode);
        }

        /// <summary>
        /// 报损报溢 保存
        /// </summary>
        /// <param name="profitLossEntityList"></param>
        public void SaveReportLossAndProfit(List<SysMedicineProfitLossEntity> profitLossEntityList)
        {
            foreach (var item in profitLossEntityList)
            {
                int kcsl = 0;
                decimal jj = 0;
                //1.查出kcsl和jj
                _medicineDmnService.SelectKcslAndJj(item.Ypdm, item.Ph, item.pc, item.Yxq, out kcsl, out jj);
                if (item.Sysl <= 0)  //报损
                {
                    if (kcsl + item.Sysl >= 0)
                    {
                        //保存（update库存 insert损益表）
                        _medicineDmnService.SaveReportLossAndProfit(item, kcsl, jj);
                    }
                    else
                    {
                        throw new FailedCodeException("INSUFFICIENT_STOCK_NOT_ENOUGH_TO_REPORT_LOSS");
                    }
                }
                else  //报溢
                {
                    //保存（update库存 insert损益表）
                    _medicineDmnService.SaveReportLossAndProfit(item, kcsl, jj);
                }
            }

        }

        #endregion

        #region 报损报溢查询
        /// <summary>
        /// 报损报溢查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="inputCode"></param>
        /// <param name="syyy"></param>
        /// <param name="syqk"></param>
        /// <returns></returns>
        public IList<LossAndProditInfoVO> SelectLossAndProditInfoList(Pagination pagination, string startTime, string endTime, string inputCode, string syyy, int syqk)
        {
            return _medicineDmnService.SelectLossAndProditInfoList(pagination, startTime, endTime, inputCode, syyy, syqk);
        }

        /// <summary>
        /// 批发价总金额、零售价总金额查询
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="syyy">损益原因</param>
        /// <param name="inputCode">关键字</param>
        /// <param name="syqk">损益情况</param>
        /// <returns></returns>
        public LossAndProditInfoJeVo ComputePjzeAndLjze(string startTime, string endTime, string syyy, string inputCode, int syqk)
        {
            return _medicineDmnService.ComputePjzeAndLjze(startTime, endTime, syyy, inputCode, syqk);
        }
        #endregion

        #region 首页统计

        /// <summary>
        /// 获取住院月处方发药次
        /// </summary>
        /// <returns></returns>
        public MonthlySummaryDO GetFyCountVoByYfbm(List<FyCountVoByYfbm> val)
        {
            var monthlySummary = new MonthlySummaryDO();
            if (val != null && val.Count > 0)
            {
                val.ForEach(p =>
                {
                    if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-01"))
                    {
                        monthlySummary.January = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-02"))
                    {
                        monthlySummary.February = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-03"))
                    {
                        monthlySummary.March = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-04"))
                    {
                        monthlySummary.April = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-05"))
                    {
                        monthlySummary.May = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-06"))
                    {
                        monthlySummary.June = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-07"))
                    {
                        monthlySummary.July = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-08"))
                    {
                        monthlySummary.August = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-09"))
                    {
                        monthlySummary.September = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-10"))
                    {
                        monthlySummary.October = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-11"))
                    {
                        monthlySummary.November = p.fyCount;
                    }
                    else if (p.fysj.Equals(DateTime.Now.Year.ToString() + "-12"))
                    {
                        monthlySummary.December = p.fyCount;
                    }
                });
            }
            return monthlySummary;
        }

        /// <summary>
        /// 获取今年医嘱次|处方药品次
        /// </summary>
        /// <returns></returns>
        public List<FyClassificationStatistics> GetFyCountBydl()
        {
            var fyCount = _pyDmnService.GetFyCountBydl();
            if (fyCount == null || fyCount.Count <= 0) return null;
            return (from item in fyCount
                    select new FyClassificationStatistics
                    {
                        name = item.dlmc,
                        y = item.ypCount
                    }).ToList();
        }
        #endregion


        /// <summary>
        /// 盘点明细查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="inputCode"></param>
        /// <param name="syyy"></param>
        /// <param name="syqk"></param>
        /// <returns></returns>
        public IList<InventoryQureyinfoVO> InventoryQureyInfo(Pagination pagination, string startTime, string endTime, string inputCode, string syyy, int syqk)
        {
            return _medicineDmnService.GetInventoryQureyInfo(pagination, startTime, endTime, inputCode, syyy, syqk);
        }
    }
}
