using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using Newtouch.Application.Interface;
using Newtouch.Core.Common;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;
using Newtouch.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Newtouch.Application.Implementation
{
    public class PrepareMedicineApp : AppBase, IPrepareMedicineApp
    {

        private readonly ISysMedicineProfitLossReasonRepo _sysMedicineProfitLossReasonRepo;

        private readonly IPrepareMedicineDmnService _preparemMedicineDmnService;

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
            var list= _preparemMedicineDmnService.SelectLossAndProfitMedicineList(inputCode);
            return list;
        }

        /// <summary>
        /// 提交报损报溢
        /// </summary>
        /// <param name="syxx"></param>
        /// <returns></returns>
        public string SubmitReportLossAndProfit(SysMedicineProfitLossEntity[] syxx)
        {
            if (syxx == null || syxx.Length <= 0) return "损益明细不能为空";
            var errorMsg = "";
            var cts = new CancellationTokenSource();
            try
            {
                var parent = new Task(() =>
                {
                    var taskFactory = new TaskFactory<bool>(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
                    foreach (var item in syxx)
                    {
                        taskFactory.StartNew(() =>
                        {
                            if (cts.IsCancellationRequested)
                            {
                                return false;
                            }

                            var tmpResult = _preparemMedicineDmnService.SubmitReportLossAndProfit(item);
                            if (string.IsNullOrWhiteSpace(tmpResult)) return true;
                            cts.Cancel();
                            errorMsg = tmpResult;
                            return false;

                        }, cts.Token);
                    }
                });
                parent.Start();
                parent.Wait();
                if (cts.IsCancellationRequested || !string.IsNullOrWhiteSpace(errorMsg))
                {
                    throw new Exception(errorMsg);
                }

                return "";
            }
            catch (Exception e)
            {
                return e.Message + (e.InnerException == null ? "" : e.InnerException.Message);
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
            return _preparemMedicineDmnService.SelectLossAndProditInfoList(pagination, startTime, endTime, inputCode, syyy, syqk);
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
            return _preparemMedicineDmnService.ComputePjzeAndLjze(startTime, endTime, syyy, inputCode, syqk);
        }

        /// <summary>
        /// 报损报溢 保存
        /// </summary>
        /// <param name="profitLossEntityList"></param>
        //public void SaveReportLossAndProfit(List<SysMedicineProfitLossEntity> profitLossEntityList)
        //{
        //    foreach (var item in profitLossEntityList)
        //    {
        //        int kcsl = 0;
        //        decimal jj = 0;
        //        //1.查出kcsl和jj
        //        _medicineDmnService.SelectKcslAndJj(item.Ypdm, item.Ph, item.pc, item.Yxq, out kcsl, out jj);
        //        if (item.Sysl <= 0)  //报损
        //        {
        //            if (kcsl + item.Sysl >= 0)
        //            {
        //                //保存（update库存 insert损益表）
        //                _medicineDmnService.SaveReportLossAndProfit(item, kcsl, jj);
        //            }
        //            else
        //            {
        //                throw new FailedCodeException("INSUFFICIENT_STOCK_NOT_ENOUGH_TO_REPORT_LOSS");
        //            }
        //        }
        //        else  //报溢
        //        {
        //            //保存（update库存 insert损益表）
        //            _medicineDmnService.SaveReportLossAndProfit(item, kcsl, jj);
        //        }
        //    }

        //}

        #endregion
    }
}
