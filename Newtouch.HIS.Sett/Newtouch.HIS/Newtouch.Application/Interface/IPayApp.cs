using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Application.Interface
{
    public interface IPayApp
    {
        /// <summary>
        /// 退款（原路退回）
        /// </summary>
        /// <param name="outTradeNo"></param>
        /// <param name="refundAmount">金额</param>
        /// <param name="refundReason">退款原因</param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        int TradeRefund(string outTradeNo, decimal refundAmount, string refundReason,string outRequestNo
            , out string errorMsg);

    }
}
