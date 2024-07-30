using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.DomainServices;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.ResourcesOperate;

namespace Newtouch.HIS.Application.Implementation.Process
{
    /// <summary>
    /// 住院退药申请流程
    /// </summary>
    public class HospitalizatiionReturnDispensingMedicineProcess : ProcessorFun<HospitalizatiionReturnDispensingMedicineRequestDTO>
    {
        private readonly IDispenseIndexInfoDmnService _dispenseIndexInfoDmnService;
        private readonly List<ZyReturnDrugApplyBillDetailEntity> _drugApplyBillDetail;
        private readonly List<ZyReturnDrugApplyBillEntity> _drugApplyBill;

        public HospitalizatiionReturnDispensingMedicineProcess(HospitalizatiionReturnDispensingMedicineRequestDTO request) : base(request)
        {
            _drugApplyBillDetail = new List<ZyReturnDrugApplyBillDetailEntity>();
            _drugApplyBill = new List<ZyReturnDrugApplyBillEntity>();
        }

        protected override ActResult Validata()
        {
            if (Request == null) throw new FailedException("请求报文不能为空");
            if (Request.ReturnDrugDetail == null || Request.ReturnDrugDetail.Count == 0) throw new FailedException("退药明细（ReturnDrugDetail）不能为空");
            if (string.IsNullOrWhiteSpace(Request.OrganizeId)) throw new FailedException("组织机构ID（OrganizeId）不能为空");
            try
            {
                Parallel.ForEach(Request.ReturnDrugDetail, p =>
                {
                    if (string.IsNullOrWhiteSpace(p.yzId)) throw new Exception("医嘱ID（zxId）不能为空");
                    if (string.IsNullOrWhiteSpace(p.ypCode)) throw new Exception("药品代码（ypCode）不能为空");
                    if (p.tysl < 0) throw new Exception("退药数量（tysl）不能小于零");
                    if (string.IsNullOrWhiteSpace(p.tysqr)) throw new Exception("退药申请人（tysqr）不能为空");
                    var ypxx = new SysMedicineExDmnService(new DefaultDatabaseFactory(), false).GetYpDetails(Request.OrganizeId, p.ypCode);
                    if (ypxx == null) throw new Exception("未找到药品代码为【" + p.ypCode + "】的药品。");
                    if ((p.zhyz ?? 0) > 0) return;
                    if (ypxx.zycls == null) throw new Exception("药品代码为【" + p.ypCode + "】的药品住院拆零数为空。");
                    p.zhyz = (int)ypxx.zycls;
                });
            }
            catch (Exception e)
            {
                throw new FailedException(e.Message + (e.InnerException != null ? e.InnerException.Message : ""));
            }
            return base.Validata();
        }


        protected override void BeforeAction(ActResult actResult)
        {
            var cts = new CancellationTokenSource();
            var hrdLocker = new object();
            var tmpCount = 0;
            var random = new Random();

            string zytyapplyno = DateTime.Now.ToString("yyyyMMddHHmmssfff") + random.Next(10, 100);

            var parent = new Task(p =>
            {
                var childFactory = new TaskFactory(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
                Request.ReturnDrugDetail.Select(o => o.yzId).Distinct().ToList().ForEach(y =>
                {
                    childFactory.StartNew(() =>
                    {
                        var tmpData = Request.ReturnDrugDetail.FindAll(d => d.yzId == y);
                        if (tmpData.Count == 0) return;
                        var rabId = Guid.NewGuid().ToString();
                        var createTime = DateTime.Now;
                        tmpData.ForEach(t =>
                        {
                            var item = new ZyReturnDrugApplyBillDetailEntity
                            {
                                CreateTime = createTime,
                                CreatorCode = t.tysqr,
                                Id = Guid.NewGuid().ToString(),
                                LastModifierCode = "",
                                LastModifyTime = null,
                                rabId = rabId,
                                tysl = t.tysl,
                                ypCode = t.ypCode,
                                zh = t.zh,
                                zhyz = t.zhyz,
                                zt = "1",
                                lyxh = t.lyxh,
                                zytyapplyno = zytyapplyno
                            };
                            lock (hrdLocker)
                            {
                                _drugApplyBillDetail.Add(item);
                            }
                        });

                        lock (hrdLocker)
                        {
                            _drugApplyBill.Add(new ZyReturnDrugApplyBillEntity
                            {
                                applyNo = DateTime.Now.ToString("yyyyMMddHHmmssfff") + random.Next(10, 100) + tmpCount,
                                bqCode = tmpData[0].bqCode,
                                CreateTime = DateTime.Now,
                                CreatorCode = tmpData[0].tysqr,
                                fyyf = tmpData[0].fyyf,
                                Id = rabId,
                                ksCode = tmpData[0].ksCode,
                                LastModifierCode = "",
                                LastModifyTime = null,
                                OrganizeId = Request.OrganizeId,
                                zt = "1",
                                zyh = tmpData[0].zyh,
                                ProcessState = 0,
                                yzId = y,
                                zytyapplyno = zytyapplyno
                            });
                            ++tmpCount;
                        }
                    });
                });
            }, cts.Token);
            parent.Start();
            parent.Wait(cts.Token);
        }

        protected override void Action(ActResult actResult)
        {
            var insertResult =
                _dispenseIndexInfoDmnService.InsertReturnDispensingMedicine(_drugApplyBillDetail, _drugApplyBill);
            if (string.IsNullOrWhiteSpace(insertResult))
            {
                actResult.ResultMsg = _drugApplyBill[0].zytyapplyno;
                return;
            }
            actResult.IsSucceed = false;
            actResult.ResultMsg = insertResult;
        }
    }
}