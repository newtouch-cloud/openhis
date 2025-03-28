using System;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// 执行调价损益
    /// </summary>
    public class AdjustPriceExecuteProcess : ProcessorFun<string>
    {
        private readonly IWzPriceAdjustmentRepo wzPriceAdjustmentRepo;
        private readonly IWzProductRepo wzProductRepo;
        private readonly IWzPriceAdjustmentDmnService _wzPriceAdjustmentDmnService;

        private readonly WzPriceAdjustmentEntity _tjInfo;

        public AdjustPriceExecuteProcess(string request) : base(request)
        {
            _tjInfo = wzPriceAdjustmentRepo.FindEntity(p => p.wztjId == request);
        }

        /// <summary>
        /// 验证Request
        /// </summary>
        protected override ActResult Validata()
        {
            if (_tjInfo == null)
            {
                throw new FailedException(string.Format("未查到主键为{0}的调价信息！", Request));
            }
            try
            {
                if (_tjInfo.shzt.Equals(((int)EnumTjShzt.Waiting).ToString())
                    || _tjInfo.shzt.Equals(((int)EnumTjShzt.Refuse).ToString())
                    || _tjInfo.shzt.Equals(((int)EnumTjShzt.Revoke).ToString())) //未审核 拒绝 撤销
                {
                    throw new FailedException("1", string.Format("主键为{0}的调价信息状态处于未审核或拒绝或撤销，不可执行！", Request));
                }
                if (((int)EnumTjZxbz.Already).ToString().Equals(_tjInfo.zxbz)) //执行
                {
                    throw new FailedException("2", string.Format("主键为{0}的调价信息已执行，不可重复执行！", Request));
                }
                if (_tjInfo.zxsj <= DateTime.Now)
                {
                    throw new FailedException("3", string.Format("主键为{0}的调价信息已过执行日期！", Request));
                }
            }
            catch (FailedException ex)
            {
                var wzInfo = wzProductRepo.FindEntity(p => p.Id == _tjInfo.productId && p.OrganizeId == OrganizeId);
                if (wzInfo == null || string.IsNullOrWhiteSpace(wzInfo.name)) throw;
                switch (ex.Code)
                {
                    case "1":
                        throw new FailedException(string.Format("调价物资【{0}】状态处于未审核或拒绝或撤销，不可执行！", wzInfo.name));
                    case "2":
                        throw new FailedException(string.Format("调价物资【{0}】已执行，不可重复执行！", wzInfo.name));
                    case "3":
                        throw new FailedException(string.Format("调价物资【{0}】已过执行日期！", wzInfo.name));
                }
            }
            return new ActResult();
        }

        /// <summary>
        /// 主处理
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            var exectResult = _wzPriceAdjustmentDmnService.AdjustPriceExecute(_tjInfo);
            if (string.IsNullOrWhiteSpace(exectResult)) return;
            actResult.IsSucceed = false;
            actResult.ResultMsg = exectResult;
        }
    }
}
