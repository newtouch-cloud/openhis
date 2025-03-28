using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Common;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Tools;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// 申领单管理
    /// </summary>
    public class ApplyBillApp : AppBase, IApplyBillApp
    {
        private readonly IWzCrkfsRepo _crkFs;
        private readonly IKfApplyOrderRepo _kfApplyOrderRepo;
        private readonly IKfApplyOrderDetailRepo _kfApplyOrderDetailRepo;
        private readonly IWzProductDmnService _wzProductDmnService;
        private readonly IKfKcxxRepo _kfKcxxRepo;

        /// <summary>
        /// 提交科室申领单
        /// </summary>
        /// <param name="sld"></param>
        /// <param name="sldmx"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string SubmitDepartmentApply(KfApplyOrderEntity sld, List<KfApplyOrderDetailEntity> sldmx, string organizeId)
        {
            if (sld == null) return "申领单主信息不能为空";
            if (sldmx == null || sldmx.Count == 0) return "申领单明细不能为空";
            sld.OrganizeId = organizeId;
            sld.applyProcess = (int)EnumApplyProcess.AuditApproved;
            sld.applyType = (int)EnumApplyType.DepartmentApply;
            sld.Create();
            if (_kfApplyOrderRepo.Insert(sld) <= 0) return "保存申领单主信息失败";
            sldmx.ForEach(p =>
            {
                p.sldId = sld.Id;
                p.Create();
            });
            if (_kfApplyOrderDetailRepo.Insert(sldmx) > 0) return "";
            _kfApplyOrderRepo.Delete(sld);
            return "保存申领单明细失败";
        }

        /// <summary>
        /// 提交申领发货
        /// </summary>
        /// <param name="ckmx"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string SubmitApplyOutStock(List<VApplyBillDetailEntity> ckmx, string organizeId, string userCode)
        {
            var result = "";
            try
            {
                if (ckmx == null || ckmx.Count == 0) return "出库明细不能为空！";
                var crkfs = _crkFs.SelectData("1").FirstOrDefault();
                foreach (var rkbm in ckmx.Select(p => p.rkbm).Distinct())
                {
                    var partCkmx = ckmx.FindAll(p => p.rkbm == rkbm && p.bmdwsysl >= p.xfsl && p.sl >= p.bmdwsysl);
                    if (partCkmx.Count == 0) continue;
                    var djInfDto = new DjInfDTO
                    {
                        crkdj = new KfCrkdjEntity
                        {
                            auditState = ((int)EnumAuditState.Adopt).ToString(), //无需人工审核
                            Pdh = ReceiptNoManage.GetNewReceiptNo(EnumOutOrInStorageBillType.chukuzhikeshi.GetDescription()),
                            deliveryNo = "",
                            crkfs = crkfs == null ? "" : crkfs.Id,
                            rkbm = rkbm,
                            OrganizeId = organizeId,
                            ckczy = userCode,
                            shczy = userCode,
                            ckbm = Constants.CurrentKf.currentKfId,
                            rksj = DateTime.Now,
                            rkczy = userCode,
                            cksj = DateTime.Now,
                            zt = ((int)Enumzt.Enable).ToString(),
                            djlx = (int)EnumOutOrInStorageBillType.chukuzhikeshi,
                            CreateTime = DateTime.Now,
                            CreatorCode = userCode,
                        },
                        crkdjmx = new List<KfCrkmxEntity>(),
                        applyBillDetail = partCkmx
                    };
                    djInfDto.crkdj.Create();
                    foreach (var item in partCkmx)
                    {
                        var kcxx = _kfKcxxRepo.SelectData(djInfDto.crkdj.ckbm, item.productId);
                        if (kcxx == null || kcxx.Count == 0) throw new Exception(string.Format("{0}未找到有效库存", item.wzmc));
                        if (kcxx.Sum(p => p.kcsl - p.djsl) < item.xfsl * item.zhyz) throw new Exception(string.Format("{0}库存不足", item.wzmc));
                        var wzxx = _wzProductDmnService.GetProductInfo(item.productId, organizeId);
                        if (wzxx == null) throw new Exception(string.Format("{0}已禁用", item.wzmc));
                        var sysl = item.xfsl * item.zhyz;
                        foreach (var kcmx in kcxx.OrderBy(p => p.yxq))
                        {
                            if (sysl <= 0) break;
                            var curkykc = kcmx.kcsl - kcmx.djsl;
                            var crkmx = new KfCrkmxEntity
                            {
                                productId = item.productId,
                                lsj = wzxx.lsj * item.zhyz,
                                jj = wzxx.jj * item.zhyz,
                                unitId = item.bmdwId,
                                unitName = item.bmdwmc,
                                zhyz = item.zhyz,
                                yxq = kcmx.yxq,
                                scrq = null,
                                fph = "",
                                pc = kcmx.pc,
                                ph = kcmx.ph,
                                remark = "",
                                ckbmkc = kcmx.kcsl,
                                rkbmkc = 0,
                                zt = ((int)Enumzt.Enable).ToString(),
                                CreateTime = DateTime.Now,
                                CreatorCode = userCode
                            };
                            crkmx.Create();
                            djInfDto.crkdjmx.Add(crkmx);
                            if (curkykc >= sysl)
                            {
                                crkmx.sl = sysl;
                                sysl = 0;
                            }
                            else
                            {
                                crkmx.sl = curkykc;
                                sysl -= curkykc;
                            }
                        }
                    }

                    var partResult = new DeptApplyOutStockProcess(djInfDto).Process();
                    if (!partResult.IsSucceed) throw new Exception(partResult.ResultMsg);
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }
    }
}