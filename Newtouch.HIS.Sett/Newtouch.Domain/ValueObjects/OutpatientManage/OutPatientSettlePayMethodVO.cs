
using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    [NotMapped]
    public class OutPatientSettlePayMethodVO: OutpatientSettlementPaymentModelEntity
    {
        /// <summary>
        /// 现金支付方式名称
        /// </summary>
        public string xjzffsmc { get; set; }
    }
}
