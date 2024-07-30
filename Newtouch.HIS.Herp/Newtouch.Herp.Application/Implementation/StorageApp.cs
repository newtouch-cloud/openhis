using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// 盘点
    /// </summary>
    public class StorageApp : AppBase, IStorageApp
    {
        private readonly IStorageManageDmnService _storageDmnService;

        /// <summary>
        /// 提交外部入库
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns>error message</returns>
        public string InStorageSubmit(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx)
        {
            var dj = new KfCrkdjEntity
            {
                OrganizeId = OperatorProvider.GetCurrent().OrganizeId,
                Pdh = crkdj.Pdh,
                deliveryNo = (crkdj.deliveryNo ?? "").Length > 50 ? crkdj.deliveryNo.Substring(0, 50) : crkdj.deliveryNo,
                rkbm = Constants.CurrentKf.currentKfId,
                rkczy = OperatorProvider.GetCurrent().UserCode,
                LastModifierCode = OperatorProvider.GetCurrent().UserCode,
                ckbm = crkdj.ckbm,
                crkfs = crkdj.crkfs,
                LastModifyTime = DateTime.Now,
                CreateTime = DateTime.Now,
                shczy = null,
                auditState = crkdj.auditState,
                zt = ((int)Enumzt.Enable).ToString(),
                djlx = (int)EnumOutOrInStorageBillType.Wbrk
            };
            dj.Create();

            var crkdjmxList = crkdjmx.ToList();
            var mxList = new List<KfCrkmxEntity>();
            foreach (var item in crkdjmxList)
            {
                var mx = new KfCrkmxEntity
                {
                    productId = item.productId,
                    fph = item.fph,
                    LastModifyTime = DateTime.Now,
                    CreateTime = DateTime.Now,
                    LastModifierCode = OperatorProvider.GetCurrent().UserCode,
                    ph = item.ph,
                    yxq = item.yxq,
                    lsj = item.lsj,
                    jj = item.jj,
                    sl = item.sl,
                    unitId = item.unitId,
                    unitName = item.unitName,
                    rkbmkc = item.rkbmkc,
                    ckbmkc = 0,
                    scrq = item.scrq,
                    pc = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    remark = item.remark,
                    zhyz = item.zhyz,
                    applyDetailId = item.applyDetailId,
                    purchaseId = item.purchaseId,
                    zt = ((int)Enumzt.Enable).ToString()
                };
                mx.Create();
                mxList.Add(mx);
            }
            try
            {
                return _storageDmnService.SaveOutOrInStorageInfo(dj, mxList);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 提交外部出库
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns>error message</returns>
        public string OutStorageSubmit(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx)
        {
            var dj = new KfCrkdjEntity
            {
                deliveryNo = crkdj.deliveryNo,
                OrganizeId = OperatorProvider.GetCurrent().OrganizeId,
                Pdh = crkdj.Pdh,
                rkbm = crkdj.rkbm,
                rkczy = OperatorProvider.GetCurrent().UserCode,
                LastModifierCode = OperatorProvider.GetCurrent().UserCode,
                ckczy = OperatorProvider.GetCurrent().UserCode,
                ckbm = Constants.CurrentKf.currentKfId,
                crkfs = crkdj.crkfs,
                LastModifyTime = DateTime.Now,
                CreateTime = DateTime.Now,
                shczy = null,
                auditState = ((int)EnumAuditState.Waiting).ToString(),
                zt = ((int)Enumzt.Enable).ToString(),
                djlx = (int)EnumOutOrInStorageBillType.Wbck
            };
            dj.Create();

            var crkdjmxList = crkdjmx.ToList();
            var mxList = new List<KfCrkmxEntity>();
            foreach (var item in crkdjmxList)
            {
                var mx = new KfCrkmxEntity
                {
                    productId = item.productId,
                    fph = item.fph,
                    LastModifyTime = DateTime.Now,
                    CreateTime = DateTime.Now,
                    LastModifierCode = OperatorProvider.GetCurrent().UserCode,
                    ph = item.ph,
                    yxq = item.yxq,
                    lsj = item.lsj,
                    jj = item.jj,
                    sl = item.sl,
                    unitId = item.unitId,
                    unitName = item.unitName,
                    rkbmkc = 0,
                    ckbmkc = item.ckbmkc,
                    scrq = item.scrq,
                    pc = item.pc,
                    remark = item.remark,
                    zhyz = item.zhyz,
                    applyDetailId = item.applyDetailId,
                    purchaseId = item.purchaseId,
                    zt = ((int)Enumzt.Enable).ToString()
                };
                mx.Create();
                mxList.Add(mx);
            }
            return _storageDmnService.Wbck(dj, mxList);
        }

        /// <summary>
        /// 提交直接出库
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns></returns>
        public string DirectDeliverySubmit(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx)
        {
            var dj = new KfCrkdjEntity
            {
                OrganizeId = OperatorProvider.GetCurrent().OrganizeId,
                Pdh = crkdj.Pdh,
                rkbm = crkdj.rkbm,
                ckczy = OperatorProvider.GetCurrent().UserCode,
                ckbm = Constants.CurrentKf.currentKfId,
                crkfs = crkdj.crkfs,
                auditState = ((int)EnumAuditState.Waiting).ToString(),
                zt = ((int)Enumzt.Enable).ToString(),
                djlx = (int)EnumOutOrInStorageBillType.Zjck,
                CreateTime = DateTime.Now,
                CreatorCode = OperatorProvider.GetCurrent().UserCode
            };
            dj.Create();

            var crkdjmxList = crkdjmx.ToList();
            var mxList = new List<KfCrkmxEntity>();
            foreach (var item in crkdjmxList)
            {
                var mx = new KfCrkmxEntity
                {
                    productId = item.productId,
                    fph = item.fph,
                    ph = item.ph,
                    yxq = item.yxq,
                    lsj = item.lsj,
                    jj = item.jj,
                    sl = item.sl,
                    unitId = item.unitId,
                    unitName = item.unitName,
                    rkbmkc = 0,
                    ckbmkc = item.ckbmkc,
                    scrq = item.scrq,
                    pc = item.pc,
                    remark = item.remark,
                    zhyz = item.zhyz,
                    applyDetailId = item.applyDetailId,
                    purchaseId = item.purchaseId,
                    zt = ((int)Enumzt.Enable).ToString(),
                    CreateTime = DateTime.Now,
                    CreatorCode = OperatorProvider.GetCurrent().UserCode
                };
                mx.Create();
                mxList.Add(mx);
            }
            return _storageDmnService.Wbck(dj, mxList);
        }

        /// <summary>
        /// 提交内部发货退回
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns></returns>
        public string DeliveryOfReturnSubmit(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx)
        {
            var dj = new KfCrkdjEntity
            {
                OrganizeId = OperatorProvider.GetCurrent().OrganizeId,
                Pdh = crkdj.Pdh,
                rkbm = crkdj.rkbm,
                ckczy = OperatorProvider.GetCurrent().UserCode,
                ckbm = Constants.CurrentKf.currentKfId,
                crkfs = crkdj.crkfs,
                auditState = ((int)EnumAuditState.Waiting).ToString(),
                zt = ((int)Enumzt.Enable).ToString(),
                djlx = (int)EnumOutOrInStorageBillType.Nbth,
                CreateTime = DateTime.Now,
                CreatorCode = OperatorProvider.GetCurrent().UserCode
            };
            dj.Create();

            var crkdjmxList = crkdjmx.ToList();
            var mxList = new List<KfCrkmxEntity>();
            foreach (var item in crkdjmxList)
            {
                var mx = new KfCrkmxEntity
                {
                    productId = item.productId,
                    fph = item.fph,
                    ph = item.ph,
                    yxq = item.yxq,
                    lsj = item.lsj,
                    jj = item.jj,
                    sl = item.sl,
                    unitId = item.unitId,
                    unitName = item.unitName,
                    rkbmkc = 0,
                    ckbmkc = item.ckbmkc,
                    scrq = item.scrq,
                    pc = item.pc,
                    remark = item.remark,
                    zhyz = item.zhyz,
                    applyDetailId = item.applyDetailId,
                    purchaseId = item.purchaseId,
                    zt = ((int)Enumzt.Enable).ToString(),
                    CreateTime = DateTime.Now,
                    CreatorCode = OperatorProvider.GetCurrent().UserCode
                };
                mx.Create();
                mxList.Add(mx);
            }
            return _storageDmnService.Wbck(dj, mxList);
        }

    }
}
