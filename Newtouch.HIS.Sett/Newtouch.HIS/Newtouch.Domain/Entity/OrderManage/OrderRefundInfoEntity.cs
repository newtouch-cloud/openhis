using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    [Table("Order_RefundInfo")]
    public class OrderRefundInfoEntity:IEntity<OrderRefundInfoEntity>
    {
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 请求支付订单号
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 第三方交易号
        /// </summary>
        public string TradeNo { get; set; }
        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal Amount { get; set; }
        public int RefundStatus { get; set; }
        public string Memo { get; set; }
        /// <summary>
        /// 创建用户ID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户ID
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// 退款原因
        /// </summary>
        public string RefundReason { get; set; }
        /// <summary>
        /// 退款平台交易时间
        /// </summary>
        public DateTime? RefundDate { get; set; }
        /// <summary>
        /// 退款请求号
        /// </summary>
        public string OutRequestNo { get; set; }
    }
}
