using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;

namespace Newtouch.Herp.Application.Implementation.BillApprovalProcess
{
    /// <summary>
    /// 外部出库
    /// </summary>
    public class WbckProcess : ProcessorFun<BillApprovalDTO>
    {
        private readonly IKfCrkdjRepo _kfCrkdjRepo;
        private readonly IKfCrkmxRepo _kfCrkmxRepo;
        private readonly IStorageManageDmnService _storageManageDmnService;
        private KfCrkdjEntity _dj;
        private List<KfCrkmxEntity> _mx;

        public WbckProcess(BillApprovalDTO request) : base(request)
        {
        }

        /// <summary>
        /// 验证Request
        /// </summary>
        protected override ActResult Validata()
        {
            _dj = _kfCrkdjRepo.FindEntity(p => p.Id == Request.djId && p.zt == ((int)Enumzt.Enable).ToString() && p.OrganizeId == Request.OrganizeId);
            if (_dj == null) throw new FailedException("单据不存在");
            if (Request.shzt == ((int)EnumAuditState.Cancelled).ToString())
            {
                if (((int)EnumAuditState.Waiting).ToString() == _dj.auditState)
                    throw new FailedException("未找到单据审核记录，作废失败"); //作废操作，应该存在单据审核记录
                if (((int)EnumAuditState.Cancelled).ToString() == _dj.auditState)
                    throw new FailedException("单据已作废，不能重复作废");
                if (((int)EnumAuditState.Refuse).ToString() == _dj.auditState)
                    throw new FailedException("单据已审核不通过，不能作废");
            }
            else if (Request.shzt == ((int)EnumAuditState.Refuse).ToString())
            {
                if (((int)EnumAuditState.Waiting).ToString() != _dj.auditState)
                    throw new FailedException("只能操作待处理状态单据");
            }
            else
            {
                if (((int)EnumAuditState.Waiting).ToString() != _dj.auditState)
                    throw new FailedException("【审核通过】只针对待处理单据"); //非作废操作，应该不存在单据审核记录
            }

            _mx = _kfCrkmxRepo.IQueryable(p => p.crkId == Request.djId && p.zt == ((int)Enumzt.Enable).ToString())
                .ToList();
            if (_mx == null) throw new FailedException("单据明细不存在");
            return new ActResult();
        }

        /// <summary>
        /// 主处理
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            string result;
            switch (Convert.ToInt32(Request.shzt))
            {
                case (int)EnumAuditState.Adopt:
                    result = _storageManageDmnService.SubtractKc(_dj, _mx, OperatorProvider.GetCurrent().UserCode);
                    break;
                case (int)EnumAuditState.Refuse:
                    result = _storageManageDmnService.Unfreeze(_dj, _mx, OperatorProvider.GetCurrent().UserCode);
                    break;
                case (int)EnumAuditState.Cancelled:
                    result = _storageManageDmnService.WbckCancelled(_dj, _mx, OperatorProvider.GetCurrent().UserCode);
                    break;
                default:
                    result = "未找到匹配的操作";
                    break;
            }
            if (string.IsNullOrWhiteSpace(result)) return;
            actResult.IsSucceed = false;
            actResult.ResultMsg = result;
        }
    }
}