using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.PDS.Requset;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Tools;
using Newtouch.HIS.Application.Implementation.Process;
using Newtouch.HIS.Domain.IDomainServices;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 出入库单据审核
    /// </summary>
    public class OutOrInStorageBillApprovalProcess : ProcessorFun<OutOrInStorageBillApprovalDTO>
    {
        private readonly ISysMedicineStorageIOReceiptRepo _crkdj;
        private readonly ISysMedicineStorageIOReceiptDetailRepo _crkdjmx;
        private readonly ISysMedicineReceiptDmnService sysMedicineReceiptDmnService;
        private readonly ISysMedicineRequisitionRepo sysMedicineRequisitionRepo;

        public OutOrInStorageBillApprovalProcess(OutOrInStorageBillApprovalDTO request) : base(request)
        {
            request.djmx = new List<SysMedicineStorageIOReceiptDetailEntity>();
            request.dj = new SysMedicineStorageIOReceiptEntity();
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (Request == null)
            {
                throw new FailedException("审核信息不能为空！");
            }
            if (string.IsNullOrWhiteSpace(Request.crkId))
            {
                throw new FailedException("出入库单据ID不能为空！");
            }
            if (string.IsNullOrWhiteSpace(Request.shzt))
            {
                throw new FailedException("审核状态不能为空！");
            }
            var djmc = "";//单据名称
            switch (Request.djlx)
            {
                case (int)EnumDanJuLX.keshifayao:
                    djmc = EnumDanJuLX.keshifayao.GetDescription();
                    break;
                case (int)EnumDanJuLX.neibufayaotuihui:
                    djmc = EnumDanJuLX.neibufayaotuihui.GetDescription();
                    break;
                case (int)EnumDanJuLX.shenlingfayao:
                    djmc = EnumDanJuLX.shenlingfayao.GetDescription();
                    break;
                case (int)EnumDanJuLX.waibucuku:
                    djmc = EnumDanJuLX.waibucuku.GetDescription();
                    break;
                case (int)EnumDanJuLX.yaopinruku:
                    djmc = EnumDanJuLX.yaopinruku.GetDescription();
                    break;
                case (int)EnumDanJuLX.zhijiefayao:
                    djmc = EnumDanJuLX.zhijiefayao.GetDescription();
                    break;
                case (int)EnumDanJuLX.shenqingdiaobo:
                    djmc = EnumDanJuLX.shenqingdiaobo.GetDescription();
                    var sldInfos = sysMedicineRequisitionRepo.IQueryable(p => p.Sldh == Request.pdh && p.OrganizeId == Request.OrganizeId && p.zt == "1");
                    if (sldInfos != null && sldInfos.Any() && Request.shzt == ((int)EnumDjShzt.Cancelled).ToString())
                    {
                        if (sldInfos.ToList().Exists(p => p.ffzt != (int)EnumSLDDeliveryStatus.None))
                        {
                            throw new FailedException("申领单非`未发`状态，不允许撤销审核！");//非作废操作，应该不存在单据审核记录
                        }
                    }
                    break;
                default:
                    throw new FailedException("未匹配到有效单据类型！");
            }
            Request.dj = _crkdj.FindEntity(p => p.crkId == Request.crkId
            && p.OrganizeId == OrganizeId
            && p.djlx == Request.djlx
            && p.zt == "1"
            && (p.Ckbm == Constants.CurrentYfbm.yfbmCode || p.Rkbm == Constants.CurrentYfbm.yfbmCode));
            if (Request.dj == null)
            {
                throw new FailedException(string.Format("根据单据传入的单据ID和单据类型未找到满足要求的单据信息，单据ID【{0}】,单据类型【{1}】！", Request.crkId, djmc));
            }
            if (Request.shzt == ((int)EnumDjShzt.Cancelled).ToString())
            {
                if (((int)EnumDjShzt.WaitingApprove).ToString() == Request.dj.shzt) throw new FailedException("单据尚未审核，不允许作废！"); //作废操作，应该存在单据审核记录
                if (((int)EnumDjShzt.Cancelled).ToString() == Request.dj.shzt) throw new FailedException("单据已作废，不能重复作废！");
                if (((int)EnumDjShzt.Rejected).ToString() == Request.dj.shzt) throw new FailedException("单据已审核不通过，不能作废！");
            }
            else if (Request.shzt == ((int)EnumDjShzt.Rejected).ToString())
            {
                if (((int)EnumDjShzt.WaitingApprove).ToString() != Request.dj.shzt)
                {
                    throw new FailedException("只能操作待处理状态单据！");
                }
            }
            else
            {
                if (((int)EnumDjShzt.WaitingApprove).ToString() != Request.dj.shzt)
                {
                    throw new FailedException("单据已审核，不能重复审核！");//非作废操作，应该不存在单据审核记录
                }
            }
            Request.djmx = _crkdjmx.IQueryable(p => p.crkId == Request.crkId && p.zt == "1").ToList();
            if (Request.djmx == null || Request.djmx.Count <= 0)
            {
                throw new FailedException(string.Format("根据单据传入的单据ID和单据类型未找到满足要求的单据明细，单据ID【{0}】,单据类型【{1}】！", Request.crkId, djmc));
            }
            //if (_mx.Any(p => p.Sl <= 0))
            //{
            //    throw new FailedException("存在数量为零的明细记录，操作失败");
            //}
            return new ActResult();
        }

        /// <summary>
        /// 主处理
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            switch (Request.djlx)
            {
                case (int)EnumDanJuLX.keshifayao:
                case (int)EnumDanJuLX.neibufayaotuihui:
                case (int)EnumDanJuLX.shenlingfayao:
                case (int)EnumDanJuLX.waibucuku:
                case (int)EnumDanJuLX.zhijiefayao:
                case (int)EnumDanJuLX.shenqingdiaobo:
                    if (!sysMedicineReceiptDmnService.ApprovalStorageIoReceipt(Request.crkId, Request.djlx, Request.shzt))
                    {
                        actResult.IsSucceed = false;
                        actResult.ResultMsg = "审核失败！";
                    }
                    break;
                case (int)EnumDanJuLX.yaopinruku:
                    var processResult = new InStorageBillApprovalProcess(Request).Process();
                    actResult.IsSucceed = processResult.IsSucceed;
                    actResult.ResultMsg = processResult.ResultMsg;
                    break;
            }
        }

    }
}
