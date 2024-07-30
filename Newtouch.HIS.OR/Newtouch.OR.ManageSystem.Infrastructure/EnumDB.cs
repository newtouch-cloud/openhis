using System.ComponentModel;

namespace Newtouch.OR.ManageSystem.Infrastructure
{
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

    /// <summary>
    /// 申请状态
    /// </summary>
    public enum EnumSqzt
    {
        [Description("待审核")]
        /// <summary>
        /// 已申请待审核(排班)
        /// </summary>
        dsh = 1,
        [Description("已审核")]
        /// <summary>
        /// 已审核(排班)待手术
        /// </summary>
        ypb = 2,
        [Description("已取消申请")]
        /// <summary>
        /// 已取消申请
        /// </summary>
        yqx = 3,

    }

    /// <summary>
    /// 手术状态
    /// </summary>
    public enum EnumSSzt
    {
        [Description("待排班")]
        /// <summary>
        /// 已审核(排班)
        /// </summary>
        dpb = 1,
        [Description("已排班")]
        /// <summary>
        /// 已审核(排班)待手术
        /// </summary>
        ypb = 2,
        [Description("已作废")]
        /// <summary>
        /// 已取消申请
        /// </summary>
        yzf = 3,
        [Description("已登记")]
        /// <summary>
        /// 已取消登记
        /// </summary>
        yzx = 4

    }

    /// <summary>
    /// 手术登记状态
    /// </summary>
    public enum EnumSSdjzt
    {
        [Description("待登记")]
        ddj = 1,
        [Description("已登记")]
        ydj = 2,
        [Description("登记作废")]
        yzf = 3
    }
    /// <summary>
    /// 手术级别
    /// </summary>
    public enum EnumSSjb
    {
        [Description("I级")]
        yj = 1,
        [Description("II级")]
        ej = 2,
        [Description("III级")]
        sj = 3,
        [Description("IV级")]
        fj = 4
    }

    /// <summary>
    /// 是否隔离
    /// </summary>
    public enum EnumIsgl
    {
        [Description("否")]
        no=0,
        [Description("是")]
        yes=1
    }

    /// <summary>
    /// 是否有菌
    /// </summary>
    public enum EnumIsjun
    {
        [Description("否")]
        no=0,
        [Description("是")]
        yes=1
    }
}