using System.ComponentModel;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 日志记录类型
    /// </summary>
    public enum DbLogType
    {
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 0,

        /// <summary>
        /// 登录
        /// </summary>
        [Description("登录")]
        Login = 1,

        /// <summary>
        /// 退出
        /// </summary>
        [Description("退出")]
        Exit = 2,

        /// <summary>
        /// 访问
        /// </summary>
        [Description("访问")]
        Visit = 3,

        /// <summary>
        /// 新增
        /// </summary>
        [Description("新增")]
        Create = 4,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete = 5,

        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Update = 6,

        /// <summary>
        /// 提交
        /// </summary>
        [Description("提交")]
        Submit = 7,

        /// <summary>
        /// 异常
        /// </summary>
        [Description("异常")]
        Exception = 8,

    }

    /// <summary>
    /// 单据类型
    /// </summary>
    public enum EnumDanJuLX
    {
        /// <summary>
        /// 药品入库
        /// </summary>
        [Description("药品入库")]   //单据号YPRKD111111111111
        yaopinruku = 1,

        /// <summary>
        /// 外部出库
        /// </summary>
        [Description("外部出库")]   //单据号WBCKD111111111111
        waibucuku = 2,

        /// <summary>
        /// 直接发药
        /// </summary>
        [Description("直接出库")]   //单据号NBFYD111111111111
        zhijiefayao = 3,

        /// <summary>
        /// 申领发药
        /// </summary>
        [Description("申领出库")]   //单据号SLCKD111111111111
        shenlingfayao = 4,

        /// <summary>
        /// 内部发药退回
        /// </summary>
        [Description("内部发药退回")] //单据号NBFYTHD111111111111
        neibufayaotuihui = 5,

        /// <summary>
        /// 药房向科室发药
        /// </summary>
        [Description("科室发药")] //单据号KSFYD111111111111
        keshifayao = 6,

        /// <summary>
        /// 基药出库
        /// </summary>
        [Description("基药出库")] // 根据出库方式统计单据信息
        jiyaochuku = 13,

        /// <summary>
        /// 申请调拨
        /// </summary>
        [Description("申请调拨")] // 单据号DBSQD111111111111
        shenqingdiaobo = 14,
    }

    /// <summary>
    /// 库存状态
    /// </summary>
    public enum EnumKCZT
    {
        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Enabled = 1,
        /// <summary>
        /// 停用
        /// </summary>
        [Description("停用")]
        Disabled = 0
    }

    /// <summary>
    /// 本部门状态
    /// </summary>
    public enum BenBuMenZT
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 1,
        /// <summary>
        /// 控制
        /// </summary>
        [Description("控制")]
        Control = 0
    }

    /// <summary>
    /// 药品状态
    /// </summary>
    public enum EnumYPZT
    {
        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Enabled = 1,
        /// <summary>
        /// 停用
        /// </summary>
        [Description("停用")]
        Disabled = 0
    }

    /// <summary>
    /// 库存显示
    /// </summary>
    public enum EnumKCXS
    {
        /// <summary>
        /// None
        /// </summary>
        [Description("全部")]
        None = -1,
        /// <summary>
        /// 显示零库存
        /// </summary>
        [Description("显示零库存")]
        Xslkc = 0,
        /// <summary>
        /// 不显示理论数量为零
        /// </summary>
        [Description("不显示理论数量为零")]
        Bxsllslwl = 1,
        /// <summary>
        /// 不显示实际数量为零
        /// </summary>
        [Description("不显示实际数量为零")]
        Bxssjslwl = 2,
        /// <summary>
        /// 不显示实际数量为零
        /// </summary>
        [Description("不显示两者都为零")]
        Bxslzdwl = 4
    }

    /// <summary>
    /// 申领单发放状态
    /// </summary>
    public enum EnumSLDDeliveryStatus
    {
        /// <summary>
        /// 未入库
        /// </summary>
        [Description("未入库")]
        None = 0,

        /// <summary>
        /// 部分入库
        /// </summary>
        [Description("部分入库")]
        Part,

        /// <summary>
        /// 已入库
        /// </summary>
        [Description("已入库")]
        All,

        /// <summary>
        /// 已终止（某些（甚至全部）明细以后都不会再发了，但这会被视为一张已结束的申领单）
        /// </summary>
        [Description("已终止")]
        Abandon
    }

    /// <summary>
    /// 审核结果
    /// </summary>
    public enum EnumDjShzt
    {
        /// <summary>
        /// 未审核
        /// </summary>
        [Description("未审核")]
        WaitingApprove = 0,
        /// <summary>
        /// 已通过
        /// </summary>
        [Description("已通过")]
        Approved = 1,
        /// <summary>
        /// 未通过（最终状态）
        /// </summary>
        [Description("未通过")]
        Rejected = 2,
        /// <summary>
        /// 已作废（最终状态）（只有审核通过 -> 才可至此状态）
        /// </summary>
        [Description("已作废")]
        Cancelled = 3,
    }

    /// <summary>
    /// 损益标志
    /// </summary>
    public enum EnumProfitLossMark
    {
        /// <summary>
        /// 报损
        /// </summary>
        [Description("报损")]
        Loss = 0,

        /// <summary>
        /// 报溢
        /// </summary>
        [Description("报溢")]
        Profit = 1
    }

    /// <summary>
    /// 药品调价操作类型（撤销 批准 拒绝 执行）
    /// </summary>
    public enum EnumPriceAdjustOperationType
    {
        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]
        None = 0,
        /// <summary>
        /// 批准
        /// </summary>
        [Description("批准")]
        Approval = 1,
        /// <summary>
        /// 拒绝
        /// </summary>
        [Description("拒绝")]
        Refuse = 2,
        /// <summary>
        /// 撤销
        /// </summary>
        [Description("撤销")]
        Withdraw = 3,
        /// <summary>
        /// 执行
        /// </summary>
        [Description("执行")]
        Execute = 4
    }

    /// <summary>
    /// 药品调价执行状态（0：未执行 1：已执行）
    /// </summary>
    public enum EnumPriceAdjustZXStatus
    {
        /// <summary>
        /// 未执行
        /// </summary>
        [Description("未执行")]
        Not = 0,
        /// <summary>
        /// 已执行
        /// </summary>
        [Description("已执行")]
        Executed = 1
    }

    /// <summary>
    /// 收费大类 大类类别
    /// </summary>
    public enum EnumSfdlDllb
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("药品")]
        Yp = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("治疗项目")]
        Zlxm,
        /// <summary>
        /// 
        /// </summary>
        [Description("非治疗项目")]
        Flzxm
    }

    /// <summary>
    /// 发药备注 0：未发；1：已排；2：已发；3：已退
    /// </summary>
    public enum EnumFybz
    {
        /// <summary>
        /// 未排
        /// </summary>
        [Description("未排")]
        Wp = 0,

        /// <summary>
        /// 已排
        /// </summary>
        [Description("已排")]
        Yp = 1,

        /// <summary>
        /// 已发
        /// </summary>
        [Description("已发")]
        Yf = 2,

        /// <summary>
        /// 已退
        /// </summary>
        [Description("已退")]
        Yt = 3
    }

    /// <summary>
    /// 医嘱性质 临时、长期
    /// </summary>
    public enum Yzxz
    {
        /// <summary>
        /// None
        /// </summary>
        [Description("全部")]
        None = 0,

        /// <summary>
        /// 临时
        /// </summary>
        [Description("临时")]
        Ls = 1,

        /// <summary>
        /// 长期
        /// </summary>
        [Description("长期")]
        Cq = 2,
    }
    public enum Yzlx
    {
        [Description("药品")]
        Yp = 2,
        /// <summary>
        /// 中草药
        /// </summary>
        [Description("中草药")]
        zcy = 10,
    }

    /// <summary>
    /// 药品操作记录 zy_ypyzczjl.operateType 和 mz_cfypczjl.operateType
    /// </summary>
    public enum EnumOperateType
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// 发药
        /// </summary>
        [Description("发药")]
        Fy = 1,

        /// <summary>
        /// 退药
        /// </summary>
        [Description("退药")]
        Ty = 2
    }

    /// <summary>
    /// 发药方式
    /// </summary>
    public enum EnumFyfs
    {
        /// <summary>
        /// 正常出库
        /// </summary>
        [Description("正常出库")]
        Zcck = 11,

        /// <summary>
        /// 退货出库
        /// </summary>
        [Description("退货出库")]
        Thck = 5,

        /// <summary>
        /// 赠送出库
        /// </summary>
        [Description("赠送出库")]
        Zsck = 12,

        /// <summary>
        /// 救灾出库
        /// </summary>
        [Description("救灾出库")]
        Jzck = 13,
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum EnumSex
    {
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        male = 1,
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        female = 2,
        /// <summary>
        /// 不详
        /// </summary>
        [Description("不详")]
        other = 3
    }

    public enum EnumSF
    {
        /// <summary>
        /// 否
        /// </summary>
        [Description("否")]
        f = 0,
        /// <summary>
        /// 是
        /// </summary>
        [Description("是")]
        s = 1,
    }
    /// <summary>
    /// 采购模式
    /// </summary>
    public enum EnumCGLX
    {
        [Description("医保范围招标")]
        ybfwzb = 1,
        [Description("医保范围未招标")]
        ybfwwzb = 2,
        [Description("医保范围招标未中标")]
        ybfwzbwzb = 3,
        [Description("医保范围挂网")]
        ybfwgw = 4,
        [Description(" 自费范围挂网")]
        zffwgw = 5,
        [Description("市级量价挂钩集中采购")]
        sjljggjzcg = 6,
        [Description("不允许采购")]
        byxcg = 7,
        [Description("其他")]
        qt = 9,
    }
    /// <summary>
    /// 商品类型
    /// </summary>
    public enum EnumSPLX
    {
        [Description("药品类")]
        ypl = 1,
        [Description("医用耗材器械类")]
        yyhcqxl = 2,
        [Description("其他")]
        qt = 9,
    }
    /// <summary>
    /// 采购计量单位
    /// </summary>
    public enum EnumCGJLDW
    {
        [Description("计价单位")]
        jjdw = 1,
        [Description("最小使用单位")]
        zxsydw = 2,
    }

    /// <summary>
    /// 申领单类型
    /// </summary>
    public enum EnumSldlx
    {

        /// <summary>
        /// 内部申领单
        /// </summary>
        [Description("内部申领单")]
        neibushenlingdan = 0,

        /// <summary>
        /// 申请调拨单
        /// </summary>
        [Description("申请调拨单")]
        shenqingdiaobodan = 1,
    }

    /// <summary>
    /// 远程诊疗状态
    /// </summary>
    public enum Emunzlzt
    {
        [Description("待确认")]
        dqr = 1,
        [Description("就诊中")]
        jzz = 2,
        [Description("已结束")]
        yjs = 3,
        [Description("已退回")]
        yth = 4,
        [Description("已撤销")]
        ycx = 5,
        [Description("已发药")]
        yfy = 6,
    }

    /// <summary>
    /// 自负性质 v2 
    /// 重庆再用
    /// </summary>
    public enum EnumZFXZv2
    {
        /// <summary>
        /// 自费
        /// </summary>
        [Description("自费")]
        ZF = 1,
        /// <summary>
        /// 甲类
        /// </summary>
        [Description("甲类")]
        J = 4,
        /// <summary>
        /// 乙类
        /// </summary>
        [Description("乙类")]
        Y = 5,
        /// <summary>
        /// 丙类
        /// </summary>
        [Description("丙类")]
        B = 6,
    }

    /// <summary>
    /// 病区记账取整类型
    /// </summary>
    public enum EnumBQJZQZ
    {
        /// <summary>
        /// 每天取整
        /// </summary>
        [Description("每天取整")]
        Day = 1,
        /// <summary>
        /// 每次取整
        /// </summary>
        [Description("每次取整")]
        Times
    }

    public enum EnumJybz
    {
        [Description("国基药")]
        Gjy = 1,
        [Description("贵安增补基药")]
        Gazbjy = 2,
        [Description("非基药")]
        Fjy = 3,
        [Description("非前述几种")]
        dbs = 4,
    }


    public enum EnumYpsx
    {
        [Description("麻醉药品")]
        Mzyp = 9,
        [Description("毒性药品")]
        Dxyp = 4,
        [Description("精一药品")]
        Jyyp = 5,
        [Description("精二药品")]
        Jeyp = 6,
    }

}
