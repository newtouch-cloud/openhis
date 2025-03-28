using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Proxy.guian.DO;
using Newtouch.HIS.Proxy.guian.DTO.S25;

namespace Newtouch.HIS.Application.Implementation.OutpatientManage
{
    /// <summary>
    /// 门诊模拟结算完整流程
    /// </summary>
    public class SettlementOverallProcess : ProcessorFun<SettlementOverallProcessDo>
    {
        private S25RequestDTO _s25RequestDto;

        public SettlementOverallProcess(SettlementOverallProcessDo request) : base(request)
        {
        }

        protected override ActResult Validata()
        {

            return base.Validata();
        }

        protected override void BeforeAction(ActResult actResult)
        {
        }

        protected override void Action(ActResult actResult)
        {
        }
    }
}