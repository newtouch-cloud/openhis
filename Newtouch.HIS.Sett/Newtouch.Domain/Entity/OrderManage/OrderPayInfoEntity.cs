using System;
using FrameworkBase.MultiOrg.Domain.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity
{
    [Table("Order_PayInfo")]
    public class OrderPayInfoEntity: IEntity<OrderPayInfoEntity>
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
        /// 支付金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// 支付来源 支付宝1 微信2
        /// </summary>
        public int PayType { get; set; }
        /// <summary>
        /// 是否支付成功 0 否 1 是
        /// </summary>
        public int PayStatus { get; set; }
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
        /// 支付账户
        /// </summary>
        public string PayAccount { get; set; }
        /// <summary>
        /// 交易描述
        /// </summary>
        public string TradeDesc { get; set; }

        public string OrganizeId { get; set; }

    }
}
