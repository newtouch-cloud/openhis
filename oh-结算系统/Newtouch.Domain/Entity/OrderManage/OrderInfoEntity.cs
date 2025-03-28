
using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 订单信息表 2023-9-8 chl
    /// </summary>
    [Table("order_info")]
    public class OrderInfoEntity : IEntity<OrderInfoEntity>
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单类型 EnumOrderType
        /// </summary>
        public int? OrderType { get; set; }
        /// <summary>
        /// 就诊卡号
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 本次就诊标识  门诊：mzh；住院：zyh
        /// </summary>
        public string VisitNo { get; set; }
        /// <summary>
        /// 订单状态 EnumOrderStatus
        /// </summary>
        public int OrderStu { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderAmt { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal? PayFee { get; set; }
        /// <summary>
        /// 支付流水号
        /// </summary>
        public string PayLsh { get; set; }
        /// <summary>
        /// 支付者身份信息
        /// </summary>
        public string PayerId { get; set; }        
        /// <summary>
        /// 订单支付时间
        /// </summary>
        public DateTime? PayTime { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public int? PayWay { get; set; }
        /// <summary>
        /// HIS内部结算号
        /// </summary>
        public string SettTradeNo { get; set; }
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
        public int? patid { get; set; }
        public string dzId { get; set; }
    }
}
