using System.ComponentModel;

namespace NewtouchHIS.Base.Domain.EnumExtend
{
    public class BusEnum
    {
        /// <summary>
        /// 订单业务类型
        /// </summary>
        public enum EnumBusType
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

        public enum EnumReportStu
        {
            /// <summary>
            /// 已申请
            /// </summary>
            [Description("已申请")]
            ysq = 0,
            /// <summary>
            /// 已接收
            /// </summary>
            [Description("已接收")]
            yjs = 1,
            /// <summary>
            /// 已完成（报告已出）
            /// </summary>
            [Description("已完成")]
            ywc = 2
        }
    }
}
