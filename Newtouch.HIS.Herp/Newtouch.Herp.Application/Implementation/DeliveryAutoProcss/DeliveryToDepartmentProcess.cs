using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Herp.Infrastructure.Log;

namespace Newtouch.Herp.Application.Implementation.DeliveryAutoProcss
{
    /// <summary>
    /// 外部入库
    /// </summary>
    public class DeliveryToDepartmentProcess : ProcessorFun<DjInfDTO>
    {
        private readonly IStorageManageDmnService _storageDmnService;
        private readonly IKfKcxxDmnService iKfKcxxDmnService;
        private readonly IKfCrkdjDmnService iKfCrkdjDmnService;

        public DeliveryToDepartmentProcess(DjInfDTO request) : base(request)
        {
        }

        /// <summary>
        /// 验证Request
        /// </summary>
        protected override ActResult Validata()
        {
            if (Request == null) throw new FailedException("单据内容不能为空");
            if (Request.crkdj == null) throw new FailedException("单据主信息不能为空");
            if (Request.crkdjmx == null || Request.crkdjmx.Count <= 0) throw new FailedException("单据明细不能为空");

            return new ActResult();
        }

        /// <summary>
        /// 主处理
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            //提交单据表
            var djSubmitResult = DeliveryToDepartmentSubmit(Request.crkdj, Request.crkdjmx);
            if (!string.IsNullOrWhiteSpace(djSubmitResult))
            {
                actResult.IsSucceed = false;
                actResult.ResultMsg = djSubmitResult;
                return;
            }
            //库存变更
            var updateKcslParam = AssembleUpdateKcslDTO();
            var result = iKfKcxxDmnService.JustSubtractKcsl(updateKcslParam);
            if (string.IsNullOrWhiteSpace(result)) return;
            RollBackDj();
            actResult.IsSucceed = false;
            actResult.ResultMsg = result;
        }

        /// <summary>
        /// 回滚单据
        /// </summary>
        private void RollBackDj()
        {
            try
            {
                if (iKfCrkdjDmnService.DeleteDjById(Request.crkdj.Id, Request.crkdj.OrganizeId) <= 0)
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                LogCore.Error("自动出库至科室", e, string.Format("回滚单据失败，单据号【{0}】", Request.crkdj.Id), Tags);
            }
        }

        /// <summary>
        /// 组装修改库存DTO
        /// </summary>
        /// <returns></returns>
        private List<UpdateKcslDTO> AssembleUpdateKcslDTO()
        {
            return Request.crkdjmx.Select(p => new UpdateKcslDTO
            {
                productId = p.productId,
                pc = p.pc,
                ph = p.ph,
                warehouseId = Request.crkdj.ckbm,
                sl = p.sl * p.zhyz,
                organizeId = Request.crkdj.OrganizeId,
                userCode = Request.crkdj.CreatorCode
            }).ToList();
        }

        /// <summary>
        /// 提交直接出库
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns></returns>
        private string DeliveryToDepartmentSubmit(KfCrkdjEntity crkdj, List<KfCrkmxEntity> crkdjmx)
        {
            crkdj.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
            crkdj.ckczy = OperatorProvider.GetCurrent().UserCode;
            crkdj.ckbm = Constants.CurrentKf.currentKfId;
            crkdj.auditState = ((int)EnumAuditState.Adopt).ToString(); //无需人工审核
            crkdj.rksj = DateTime.Now;
            crkdj.rkczy = OperatorProvider.GetCurrent().UserCode;
            crkdj.cksj = DateTime.Now;
            crkdj.zt = ((int)Enumzt.Enable).ToString();
            crkdj.djlx = (int)EnumOutOrInStorageBillType.chukuzhikeshi;
            crkdj.CreateTime = DateTime.Now;
            crkdj.CreatorCode = OperatorProvider.GetCurrent().UserCode;
            crkdj.Create();

            var mxList = new List<KfCrkmxEntity>();
            foreach (var item in crkdjmx)
            {
                item.rkbmkc = 0;
                item.zt = ((int)Enumzt.Enable).ToString();
                item.CreateTime = DateTime.Now;
                item.CreatorCode = OperatorProvider.GetCurrent().UserCode;
                item.Create();
                mxList.Add(item);
            }
            return iKfCrkdjDmnService.SaveDj(crkdj, mxList);
        }
    }
}

