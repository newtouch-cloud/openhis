using System.ComponentModel;

namespace NewtouchHIS.Base.Domain.EnumExtend
{
    public class OrderEnum
    {
        /// <summary>
        /// 订单状态0:待支付 1:支付中  2:已支付 3：已退款 4:已作废
        /// </summary>
        public enum EnumOrderStatus
        {
            /// <summary>
            /// 待支付
            /// </summary>
            [Description("待支付")]
            dzf = 0,
            /// <summary>
            /// 支付中
            /// </summary>
            [Description("支付中")]
            zfz = 1,
            /// <summary>
            /// 已支付
            /// </summary>
            [Description("已支付")]
            yzf = 2,
            /// <summary>
            /// 已退款
            /// </summary>
            [Description("已退款")]
            ytk = 3,
            /// <summary>
            /// 已作废
            /// </summary>
            [Description("已作废")]
            zf = 4,
        }

        /// <summary>
        /// 订单业务类型
        /// </summary>
        public enum EnumOrderType
        {
            /// <summary>
            /// 门诊
            /// </summary>
            [Description("门诊")]
            mz = 1,
            /// <summary>
            /// 住院
            /// </summary>
            [Description("住院")]
            zy = 2
        }
    }
}
