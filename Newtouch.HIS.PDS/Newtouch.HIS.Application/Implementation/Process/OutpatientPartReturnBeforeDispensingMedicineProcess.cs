using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.DomainServices;
using Newtouch.HIS.DomainServices.OutPatientPharmacy;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.Log;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.ResourcesOperate;

namespace Newtouch.HIS.Application.Implementation.Process
{
    /// <summary>
    /// (未发药前)部分退药
    /// </summary>
    public class OutpatientPartReturnBeforeDispensingMedicineProcess : ProcessorFun<OutpatientPartReturnBeforeDispensingMedicineRequestDTO>
    {
        private readonly IResourcesOperateApp _resourcesOperateApp;

        public OutpatientPartReturnBeforeDispensingMedicineProcess(OutpatientPartReturnBeforeDispensingMedicineRequestDTO request) : base(request)
        {
        }

        protected override ActResult Validata()
        {
            if (Request == null) throw new FailedException("", "请求报文不能为空");
            if (Request.Items == null || Request.Items.Count == 0) throw new FailedException("", "退药明细不能为空");
            try
            {
                Parallel.ForEach(Request.Items.Select(p => p.cfh).Distinct().ToList(), p =>
                {
                    var rpInfo = new tyDmnService(new DefaultDatabaseFactory(), false).SelectRpList(p, Request.OrganizeId);
                    if (rpInfo == null || rpInfo.Count == 0) throw new Exception("未找到有效的处方");
                    if (rpInfo[0].fybz != ((int)EnumFybz.Yp).ToString()) throw new Exception("该退药操作只适用已排未发处方");
                });
            }
            catch (Exception e)
            {
                LogCore.Error("OutpatientPartReturnBeforeDispensingMedicineProcess Validata error", e,
                    "(未发药前)部分退药验证异常");
                throw new FailedException("", e.Message + (e.InnerException != null ? e.InnerException.Message : ""));
            }
            return new ActResult();
        }

        protected override void BeforeAction(ActResult actResult)
        {
            try
            {
                Parallel.ForEach(Request.Items, p =>
                {
                    if (p.zhyz != 0) return;
                    var ypxx = new SysMedicineExDmnService(new DefaultDatabaseFactory(), false).GetYpDetails(Request.OrganizeId, p.ypdm);
                    if (ypxx == null) throw new Exception("未找到代码为【" + p.ypdm + "】的药品。");
                    if (ypxx.mzcls == null) throw new Exception("药品代码为【" + p.ypdm + "】的药品门诊拆零数为空。");
                    p.zhyz = (int)ypxx.mzcls;
                    if (p.sl <= 0) Request.Items.Remove(p);
                });
            }
            catch (Exception e)
            {
                throw new FailedException(e.Message + (e.InnerException != null ? e.InnerException.Message : ""));
            }
        }

        private static object actionThreadLock = new object();

        protected override void Action(ActResult actResult)
        {
            var errorMsg = "";
            try
            {
                var cts = new CancellationTokenSource();
                var parent = new Task(() =>
                {
                    var childFactory = new TaskFactory<bool>(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
                    Request.Items.ForEach(p =>
                    {
                        childFactory.StartNew(() =>
                        {
                            if (cts.IsCancellationRequested)
                            {
                                return default(bool);
                            }
                            var tmpResult = new tyDmnService(new DefaultDatabaseFactory(), false).OutpatientPartReturn(p, Request.OrganizeId);
                            if (string.IsNullOrWhiteSpace(tmpResult)) return true;
                            lock (actionThreadLock)
                            {
                                errorMsg = tmpResult;
                            }

                            cts.Cancel();
                            return false;
                        }, cts.Token);
                    });
                });
                parent.Start();
                parent.Wait(cts.Token);
                if (!string.IsNullOrWhiteSpace(errorMsg)) throw new Exception(errorMsg);
            }
            catch (Exception e)
            {
                throw new FailedException(e.Message + (e.InnerException != null ? e.InnerException.Message : "") + errorMsg);
            }
        }
    }
}