using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.DO;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.DomainServices;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.ExException;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.Zyypyz;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 医嘱执行
    /// </summary>
    public class YzzxApp : ProcessorFun<ZyypyzzxRequest>
    {
        private readonly IZyYpyzxxRepo _zyYpyzzxRepo;
        private List<ZyYpyzxxEntity> _yzEntities;
        private readonly List<BookItemDo> _sucessList;
        private string zxId = Guid.NewGuid().ToString();//医嘱执行ID

        public YzzxApp(ZyypyzzxRequest request) : base(request)
        {
            Tags = new Dictionary<string, string>
            {
                {Constants.ClientNo, request.ClientNo}
            };
            _sucessList = new List<BookItemDo>();
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (Request == null) throw new FailedException("请求报文不能我空");
            if (Request.yzList == null || Request.yzList.Count <= 0) throw new FailedException("医嘱列表不能为空");
            if (string.IsNullOrWhiteSpace(Request.OrganizeId)) throw new FailedException("组织机构ID不能为空");
            try
            {
                Parallel.ForEach(Request.yzList, detail =>
                {
                    if (string.IsNullOrWhiteSpace(detail.ypCode)) throw new Exception("请传入有效的药品代码");
                    if (string.IsNullOrWhiteSpace(detail.yzId)) throw new Exception("请传入有效的医嘱ID");
                    if (string.IsNullOrWhiteSpace(detail.bqCode)) throw new Exception("请传入有效的病区编码");
                    if (string.IsNullOrWhiteSpace(detail.fyyf)) throw new Exception("请传入有效的发药药房编码");
                    if (detail.sl <= 0) throw new Exception("请输入有效的医嘱执行数量");
                    var ypxx = new SysMedicineExDmnService(new DefaultDatabaseFactory(), false).GetYpDetails(Request.OrganizeId, detail.ypCode);
                    if (ypxx == null) throw new Exception("未找到药品【" + detail.ypCode + "】。");
                    detail.ypmc = ypxx.ypmc;
                    if (detail.zhyz <= 0)
                    {
                        if (ypxx.zycls == null) throw new Exception("药品【" + ypxx.ypmc + "】住院拆零数为空。");
                        detail.zhyz = (int)ypxx.zycls;
                    }

                    detail.dj = ypxx.lsj / ypxx.bzs * detail.zhyz;
                    detail.je = detail.dj * detail.sl;
                });
            }
            catch (Exception e)
            {
                throw new FailedException("", e.Message + (e.InnerException != null ? e.InnerException.Message : ""));
            }
            return new ActResult();
        }

        /// <summary>
        /// 主处理
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            SinglethreadAction(actResult);
        }

        /// <summary>
        /// 单线程执行
        /// </summary>
        private void SinglethreadAction(ActResult actResult)
        {
            var phResult = new DispensingDmnService(new DefaultDatabaseFactory(), false).HospitalizationBook(zxId, Request.yzList, Request.OrganizeId);
            if (string.IsNullOrWhiteSpace(phResult)) return;
            actResult.IsSucceed = false;
            actResult.ResultMsg = phResult;
        }

        /// <summary>
        /// 多线程执行
        /// </summary>
        private void MultithreadingAction()
        {
            var cts = new CancellationTokenSource();
            try
            {
                var errorMsg = "";
                var parent = new Task(() =>
                {
                    var cLocker = new object();
                    var childFactory = new TaskFactory<bool>(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
                    Request.yzList.ForEach(p =>
                    {
                        var bookItem = new BookItemDo
                        {
                            YpCode = p.ypCode,
                            Sl = (int)p.sl * p.zhyz,
                            Yfbm = string.IsNullOrWhiteSpace(p.fyyf) ? Constants.CurrentYfbm.yfbmCode : p.fyyf,
                            OrganizeId = Request.OrganizeId ?? "",
                            CreatorCode = p.yzzxsqr ?? "",
                            zxId = zxId
                        };
                        var childTask = childFactory.StartNew(() =>
                        {
                            if (cts.IsCancellationRequested)
                            {
                                return false;
                            }

                            var phResult = new DispensingDmnService(new DefaultDatabaseFactory(), false).HospitalizationBook(bookItem);
                            if (string.IsNullOrWhiteSpace(phResult))
                            {
                                lock (cLocker)
                                {
                                    _sucessList.Add(bookItem);
                                }
                                return true;
                            }

                            errorMsg = phResult;
                            cts.Cancel();
                            return false;
                        }, cts.Token);
                    });
                });
                parent.Start();
                parent.Wait();
                if (!cts.IsCancellationRequested && string.IsNullOrWhiteSpace(errorMsg)) return;
                throw new ProcessException(errorMsg); ;
            }
            catch (Exception e)
            {
                CancelForzenKc();
                throw new ProcessException(e.Message + (e.InnerException == null ? "" : e.InnerException.Message)); ;
            }
        }

        /// <summary>
        /// 取消冻结
        /// </summary>
        private void CancelForzenKc()
        {
            if (_sucessList.Count > 0)
            {
                Parallel.ForEach(_sucessList, p =>
                    {
                        new DispensingDmnService(new DefaultDatabaseFactory(), false).HospitalizationCancelBook(zxId, p);
                    });
            }
        }

        /// <summary>
        /// 排药
        /// </summary>
        /// <param name="actResult"></param>
        protected override void AfterAction(ActResult actResult)
        {
            AssembleZyYpyzzxEntity();
            if (_yzEntities == null || _yzEntities.Count <= 0) throw new ProcessException("保存住院药品医嘱执行数据失败");
            _zyYpyzzxRepo.Insert(_yzEntities);
            actResult.Data = Request.yzList;//由于事务控制，所以要么全部失败，否则全部成功，Data返回成功队列
        }

        /// <summary>
        /// 组装住院药品医嘱执行表数据
        /// </summary>
        /// <returns></returns>
        private void AssembleZyYpyzzxEntity()
        {
            _yzEntities = new List<ZyYpyzxxEntity>();
            var locker = new object();
            Parallel.ForEach(Request.yzList, p =>
            {
                var item = new ZyYpyzxxEntity
                {
                    bqCode = p.bqCode,
                    CreatorCode = p.yzzxsqr,
                    CreateTime = DateTime.Now,
                    cw = p.cw,
                    dj = p.dj,
                    dl = p.dl,
                    fybz = "1",
                    fyyf = p.fyyf,
                    je = p.je,
                    jsrq = p.jsrq < Convert.ToDateTime("1971-01-01") ? Constants.MinDateTime : p.jsrq,
                    ksCode = p.ksCode,
                    ksrq = p.ksrq < Convert.ToDateTime("1971-01-01") ? Constants.MinDateTime : p.ksrq,
                    lyxh = p.lyxh,
                    OrganizeId = Request.OrganizeId,
                    pcmc = p.pcmc,
                    sjap = p.sjap,
                    sl = p.sl,
                    zhyz = p.zhyz,
                    yl = p.yl,
                    yldw = p.yldw,
                    ypCode = p.ypCode,
                    ysgh = p.ysgh,
                    yzbz = p.yzbz,
                    yzxz = p.yzxz,
                    yzzxsqr = p.yzzxsqr,
                    zfbl = p.zfbl,
                    zfxz = p.zfxz,
                    zlff = p.zlff,
                    zxsl = p.zxsl,
                    zyh = p.zyh,
                    patientName = p.patientName,
                    zxrq = p.zxrq < Convert.ToDateTime("1971-01-01") ? Constants.MinDateTime : p.zxrq,
                    zh = p.zh,
                    zxId = zxId,
                    yzId = p.yzId
                };
                lock (locker)
                {
                    _yzEntities.Add(item);
                }
            });
        }

    }
}
