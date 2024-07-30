using System.ComponentModel;

namespace Newtouch.Herp.Infrastructure.Enum
{
    /// <summary>
    /// 库房等级
    /// </summary>
    public enum EnumWarehouseLevel
    {
        /// <summary>
        /// 一级
        /// </summary>
        [Description("一级")]
        OneLevel = 1,

        /// <summary>
        /// 二级
        /// </summary>
        [Description("二级")]
        TwoLevel = 2,

        /// <summary>
        /// 三级
        /// </summary>
        [Description("三级")]
        ThreeLevel = 3,

        /// <summary>
        /// 四级
        /// </summary>
        [Description("四级")]
        FourLevel = 4,

        /// <summary>
        /// 五级
        /// </summary>
        [Description("五级")]
        FiveLevel = 5,

        /// <summary>
        /// 六级
        /// </summary>
        [Description("六级")]
        SixLevel = 6,

    }

    /// <summary>
    /// 有效/无效
    /// </summary>
    public enum Enumzt
    {
        /// <summary>
        /// 无效
        /// </summary>
        [Description("无效")]
        Disable = 0,

        /// <summary>
        /// 有效
        /// </summary>
        [Description("有效")]
        Enable = 1

    }

    /// <summary>
    /// 审核状态
    /// </summary>
    public enum EnumAuditState
    {

        /// <summary>
        /// 待处理
        /// </summary>
        [Description("待处理")]
        Waiting = 0,

        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("审核通过")]
        Adopt = 1,

        /// <summary>
        /// 审核不通过
        /// </summary>
        [Description("审核不通过")]
        Refuse = 2,

        /// <summary>
        /// 已作废
        /// </summary>
        [Description("已作废")]
        Cancelled = 3,

        /// <summary>
        /// 暂存
        /// </summary>
        [Description("暂存")]
        Temporary = 4,
    }

    /// <summary>
    /// 单据类型
    /// </summary>
    public enum EnumOutOrInStorageBillType
    {
        /// <summary>
        /// 外部入库
        /// </summary>
        [Description("外部入库")]
        Wbrk = 1,

        /// <summary>
        /// 外部出库
        /// </summary>
        [Description("外部出库")]
        Wbck = 2,

        /// <summary>
        /// 直接出库
        /// </summary>
        [Description("直接出库")]
        Zjck = 3,

        /// <summary>
        /// 申领出库
        /// </summary>
        [Description("申领出库")]
        Slck = 4,

        /// <summary>
        /// 内部退货
        /// </summary>
        [Description("内部退货")]
        Nbth = 5,

        /// <summary>
        /// 报损报溢
        /// </summary>
        [Description("报损报溢")]
        Bsby = 6,

        /// <summary>
        /// 配送至科室
        /// </summary>
        [Description("出库至科室")] //单据号ckZKS111111111111
        chukuzhikeshi = 7,

        /// <summary>
        /// 科室申领
        /// </summary>
        [Description("科室申领")]
        kssl = 8,

        /// <summary>
        /// 科室申领出库
        /// </summary>
        [Description("科室申领出库")]
        ksSlck = 9,

        /// <summary>
        /// 采购计划
        /// </summary>
        [Description("采购计划")]
        purchasingPlan = 10,

        /// <summary>
        /// 采购订单
        /// </summary>
        [Description("采购订单")]
        purchasingOrder = 11,
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
    /// 库存锁
    /// </summary>
    public enum EnumKcLock
    {
        /// <summary>
        /// 无锁
        /// </summary>
        [Description("无锁")]
        none = 0,

        /// <summary>
        /// 开始盘点
        /// </summary>
        [Description("开始盘点")]
        InventoryStart = 1,

        /// <summary>
        /// 结束盘点
        /// </summary>
        [Description("结束盘点")]
        InventoryEnd = 2,

        /// <summary>
        /// 损益
        /// </summary>
        [Description("损益")]
        ProfitAndLoss = 3,

        /// <summary>
        /// 出入库单据审核
        /// </summary>
        [Description("出入库单据审核")]
        OutgoingOrWarehousing = 4
    }

    /// <summary>
    /// 损益标志
    /// </summary>
    public enum EnumSybz
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
        Profit = 1,
    }

    #region 调价

    /// <summary>
    /// 调价审核标志 0:未审核 1:已审核 2:已拒绝 3.已撤销
    /// </summary>
    public enum EnumTjShzt
    {
        /// <summary>
        /// 未审核
        /// </summary>
        [Description("未审核")]
        Waiting = 0,

        /// <summary>
        /// 已审核
        /// </summary>
        [Description("已审核")]
        Audited = 1,

        /// <summary>
        /// 已拒绝
        /// </summary>
        [Description("已拒绝")]
        Refuse = 2,

        /// <summary>
        /// 已撤销
        /// </summary>
        [Description("已撤销")]
        Revoke = 3
    }

    /// <summary>
    /// 调价执行标志
    /// </summary>
    public enum EnumTjZxbz
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
        Already = 1
    }
    #endregion

    /// <summary>
    /// 生产商/经销商/其他  公司类型
    /// </summary>
    public enum EnumSupplierType
    {
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 0,

        /// <summary>
        /// 生产商
        /// </summary>
        [Description("生产商")]
        Producer = 1,

        /// <summary>
        /// 经销商
        /// </summary>
        [Description("经销商")]
        Distributor = 2,
    }

    /// <summary>
    /// 处理过程   1:待处理；2:审核通过；3:审核不通过；4:配送；5:部分完成；6:完成；7:拒收
    /// </summary>
    public enum EnumApplyProcess
    {
        /// <summary>
        /// 等待处理
        /// </summary>
        [Description("等待处理")]
        Waiting = 1,

        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("审核通过")]
        AuditApproved = 2,

        /// <summary>
        /// 审核不通过
        /// </summary>
        [Description("审核不通过")]
        AuditFailed = 3,

        /// <summary>
        /// 配送中
        /// </summary>
        [Description("配送中")]
        Distributing = 4,

        /// <summary>
        /// 部分完成
        /// </summary>
        [Description("部分完成")]
        PartialCompletion = 5,

        /// <summary>
        /// 全部完成
        /// </summary>
        [Description("全部完成")]
        Completion = 6,

        /// <summary>
        /// 配送拒收
        /// </summary>
        [Description("配送拒收")]
        Rejection = 7,
    }

    /// <summary>
    /// 单据类型 1-科室申领；2-库房申领
    /// </summary>
    public enum EnumApplyType
    {
        /// <summary>
        /// 待处理
        /// </summary>
        [Description("科室申领")]
        DepartmentApply = 1,

        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("库房申领")]
        WarehouseApply = 2,
    }

    /// <summary>
    /// 采购订单处理流程 -1：拒处理； 0：待处理； 1：备货； 2：配送； 3：签收； 4：完成； 5：拒签 
    /// </summary>
    public enum EnumOrderProcess
    {
        /// <summary>
        /// 拒处理
        /// </summary>
        [Description("拒处理")]
        Refusal =-1,

        /// <summary>
        /// 待处理
        /// </summary>
        [Description("待处理")]
        Waiting = 0,

        /// <summary>
        /// 备货
        /// </summary>
        [Description("备货")]
        PreparingGoods = 1,

        /// <summary>
        /// 配送
        /// </summary>
        [Description("配送")]
        Delivering = 2,

        /// <summary>
        /// 签收
        /// </summary>
        [Description("签收")]
        SignFor = 3,

        /// <summary>
        /// 完成
        /// </summary>
        [Description("完成")]
        Complete = 4,

        /// <summary>
        /// 拒签
        /// </summary>
        [Description("拒签")]
        RefusalSign = 5
    }

    /// <summary>
    /// 订单类型 0：暂存单；1：正式单
    /// </summary>
    public enum EnumOrderTypeHrp
    {

        /// <summary>
        /// 暂存单
        /// </summary>
        [Description("暂存单")]
        TempOrder = 0,

        /// <summary>
        /// 正式单
        /// </summary>
        [Description("正式单")]
        OfficialOrder = 1,

        /// <summary>
        /// 作废单
        /// </summary>
        [Description("作废单")]
        BadOrder = 2,
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


    #region 采购

    //4.1.1 操作类型
    public enum EnumCZLX
    {
        [Description("新增")] XZ=1,
        [Description("作废")] ZF=3,
    }

    //4.1.2 采购单类型
    public enum EnumCGDLX
    {
        [Description("医院自行采购单")] YYZXCGD= 1,
        [Description("托管药库采购单")] TGYKCGD= 2,
    }

    //4.1.3 采购类型
    public enum EnumCGLX
    {
        [Description("招标采购")]
        ZBCG = 1,
        [Description("带量采购")]
        DLCG = 2,
        [Description("挂网采购")]
        GWCG = 3,
        [Description("其他")]
        QT = 9,
    }

    //4.1.4 配送要求
    public enum EnumPSYQ
    {
        [Description("按单配送")] ADPS= 1,
        [Description("按需配送")] AXPS= 2,
    }

    //4.1.5 退货类型

    public enum EnumTHLX
    {
        [Description("正常退货")] ZCTH= 1,
        [Description("补差价退货")] BCJTH= 2,

    }

    //4.1.6 配送验收类型
    public enum EnumPSYSLX
    {
        [Description("预验收")] YYS= 1,
        [Description("实验收")] SYS= 2,

    }

    //4.1.7 单据填写方
    public enum EnumDJTXF
    {
        [Description("采购方填写")]
        CGFTX = 1,
        [Description("销售方代填")]
        XSFDT = 2,
    }

    //4.1.8 配送单状态
    public enum EnumPSDZT
    {
        [Description("待确认")] DQR= 0,
        [Description("作废")] ZF= 1,
        [Description("已确认，未验收")] YQR= 10,

    }
    //4.1.9 采购明细状态
    public enum EnumCGMXZT
    {
        [Description("待提交")] DTJ= 0,
        [Description("已作废")] YZF= 1,
        [Description("待审核")] DSH= 10,
        [Description("审核不通过")] SHBTG= 11,
        [Description("待企业接受确认")] DQYJSQR= 20,
        [Description("企业已接收确认配送")] QYYJSQRPS= 21,
        [Description("企业已接收确认不予配送")] QYYJSQRBYPS= 22,
        [Description("企业已配送完成")] QYYPSWC= 23,
    }

    //4.1.10 企业库存
    public enum EnumQYKC
    {
        [Description("充足")] CZ= 1,
        [Description("有货， 限量")] YHXL= 2,
        [Description("有货， 少量")] YHSL= 3,
        [Description("暂无货")] ZWH= 4,
        [Description("不予披露")] BYPL= 5,
    }

    //4.1.11 发票状态
    public enum EnumFPZT
    {
        [Description("待确认")] DQR= 0,
        [Description("已作废")] YZF= 1,
        [Description("未验收")] WYS= 10,
        [Description("已验收")] YYS= 20,
        [Description("已拒收")] YJS= 21,
        [Description("已支付")] FPYZF= 30,
    }

    //4.1.12 配送明细状态
    public enum EnumPSMXZT
    {
        [Description("待确认")] DQR= 0,
        [Description("已作废")] YZF= 1,
        [Description("已提交未验收")] YTJWYS= 10,
        [Description("验收中")] YSZ= 20,
        [Description("验收入库完成")] YSRKWC= 30,
    }

    //4.1.13 退货明细状态
    public enum EnumTHMXZT
    {
        [Description("待确认")] DQR= 0,
        [Description("已作废")] YZF= 1,
        [Description("已确认待企业接受")] YQRDQYJS= 10,
        [Description("企业已确认退货处理")] QYYQRTHCL= 20,
        [Description("企业已确认不予处理")] QYYQRBYCL= 21,
    }

    //4.1.14 证件类型
    public enum EnumZJLX
    {
        [Description("组织机构代码证")] ZZJGDMZ= 1,
        [Description("营业执照")] YYZZ= 2,
        [Description("经营许可证")] JYXKZ= 3,
        [Description("生产许可证")] SCXKZ= 4,
    }

    //4.1.15 配送明细条码类型
    public enum EnumPSMXTMLX
    {
        [Description("GS1 条码")] GSI= 1,
        [Description("HIBC 条码")] HIBC= 2,
        [Description("其他")] QT= 99,
    }

    //4.1.16 发票验收结果
    public enum EnumFPYSJG
    {
        [Description("验收通过")] YSTG= 1,
        [Description("验收不通过")] YSBTG= 0,
    }

    //4.1.17 采购单状态
    public enum EnumCGDZT
    {
        [Description("待提交")] DTJ= 0,
        [Description("已作废")] YZF= 1,
        [Description("已提交")] YTJ= 20,
    }

    //4.1.18 退货单状态
    public enum EnumTHDZT
    {
        [Description("待确认")] DQR= 0,
        [Description("已作废")] YZF= 1,
        [Description("已确认")] YQR= 10,

    }

    //4.1.19 采购方式

    public enum EnumCGFS
    {
        [Description("非系统采购")]
        FXTCG = 1,
        [Description("按系统采购")]
        AXTCG = 2,
    }

    //4.1.20 是否含伴随服务
    public enum EnumSFHBSFW
    {
        [Description("否")] F= 0,
        [Description("是")] S= 1,
    }

    
    #endregion
}
