using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class OrderInfoVO : OrderInfoBaseVO
    {
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal? PayFee { get; set; }
        /// <summary>
        /// 支付流水号
        /// </summary>
        public string PayLsh { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public int? PayWay { get; set; }
        /// <summary>
        /// 支付者身份信息
        /// </summary>
        public string PayerId { get; set; }
        /// <summary>
        /// 订单支付时间
        /// </summary>
        public DateTime? PayTime { get; set; }
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

        ///// <summary>
        ///// 
        ///// </summary>
        //public string zt { get; set; }
        public int patId { get; set; }
        public string dzId { get; set; }
    }

    public class OrderInfoBaseVO
    {
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
        public int? OrderStu { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal? OrderAmt { get; set; }
        /// <summary>
        /// 锁定超时时间
        /// </summary>
        public DateTime? LockExpTime { get; set; }
    }
    /// <summary>
    /// 完整订单信息
    /// </summary>
    public class OutpOrderVO
    {
        /// <summary>
        /// 基本信息
        /// </summary>
        public OrderInfoVO info { get; set; }
        /// <summary>
        /// 订单明细
        /// </summary>
        public List<OrderMzVO> mx { get; set; }
        /// <summary>
        /// 处方类型 0 全部 EnumYiZhuXZ
        /// </summary>
        public int? cflx { get; set; } 
    }

    public class InHosOrderVO
    {
        /// <summary>
        /// 基本信息
        /// </summary>
        public OrderInfoVO info { get; set; }
        public HosPatientVo patient { get; set; }
        /// <summary>
        /// 出院日期（默认同出区时间）
        /// </summary>
        public DateTime cyrq { get; set; }
    }

    public class OrderDetailVO
    {
        /// <summary>
        /// 门诊订单明细
        /// </summary>
        public List<OrderMzVO> mzList { get; set; }
        /// <summary>
        /// 住院订单明细
        /// </summary>
        public List<OrderZyBaseVO> zyList { get; set; }
    }
}
