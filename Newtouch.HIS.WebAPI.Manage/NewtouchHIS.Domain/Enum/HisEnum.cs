using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.Enum
{
    /// <summary>
    /// HIS内部枚举
    /// </summary>
    public class HisEnum
    {
        /// <summary>
        /// 病人类型标志
        /// </summary>
        public enum PatTypeEnum
        {
            [Description("在院")]
            zy = 1,
            [Description("出院")]
            cy = 2,
            [Description("门诊")]
            mz = 3,

        }
        /// <summary>
        /// 在院标志
        /// </summary>
        public enum EnumZYBZ
        {
            /// <summary>
            /// 入院登记
            /// </summary>
            Xry = 0,
            /// <summary>
            /// 病区中
            /// </summary>
            Bqz = 1,
            /// <summary>
            /// 病区出院（出病区）（待结账）
            /// </summary>
            Djz = 2,
            /// <summary>
            /// 已出院（出院结算）
            /// </summary>
            Ycy = 3,
            /// <summary>
            /// 转区
            /// </summary>
            Zq = 7,

            /// <summary>
            /// 作废记录/取消入院
            /// </summary>
            Wry = 9,
        }

        public enum EnumYzzt
        {
            /// <summary>
            /// 待审 
            /// </summary>
            [Description("待审")]
            Ds = 0,
            /// <summary>
            /// 审核
            /// </summary>
            [Description("审核")]
            Sh = 1,
            /// <summary>
            /// 执行
            /// </summary>
            [Description("执行")]
            Zx = 2,
            /// <summary>
            /// 撤DC
            /// </summary>
            [Description("DC")]
            DC = 3,
            /// <summary>
            /// 停止或作废
            /// </summary>
            [Description("停止")]
            TZ = 4
        }

        /// <summary>
        /// 病人性质
        /// </summary>
        public enum EnumBrxz
        {
            /// <summary>
            /// 自费
            /// </summary>
            [Description("自费")]
            zf = 0,
            /// <summary>
            /// 职工医保
            /// </summary>
            [Description("职工医保")]
            zg = 1,
            /// <summary>
            /// 居民医保
            /// </summary>
            [Description("居民医保")]
            jm = 2,
            /// <summary>
            /// 离休
            /// </summary>
            [Description("离休")]
            lx = 3,
            /// <summary>
            /// 普通医保
            /// </summary>
            [Description("普通医保")]
            pt = 11,
        }

    }
}
