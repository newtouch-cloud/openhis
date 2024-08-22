using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO.PayDto;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices.PayManage
{
    public interface IPayDmnService
    {
        IList<MicroPayTradeInfoDTO> MicroPayTradeQuery(string ksrq, string jsrq, int? payType, int? payStatus,
            string keywords, string orgId);

        IList<MicroPayTradeInfoDTO> TradePayLsit(Pagination pagination, string ksrq, string jsrq, int? payType, int? payStatus,
            string keywords, string orgId);

        IList<OrderRefundInfoEntity> TradeRefundList(string OutTradeNo, string TradeNo);
        /// <summary>
        /// 订单详情查询
        /// </summary>
        /// <param name="OutTradeNo"></param>
        /// <param name="TradeNo"></param>
        /// <returns></returns>
        MicroPayTradeInfoDTO TradePayInfobyNo(string OutTradeNo, string TradeNo);
    }
}
