using System;
using System.ComponentModel;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 日志记录类型 枚举项扩展
    /// Value请从101开始
    /// </summary>
    public enum DbLogType_Ex
    {
        /// <summary>
        /// 出院结算
        /// </summary>
        [Description("出院结算")]
        DischargeSettlement = 101,

    }

    /// <summary>
    /// 在院标志
    /// </summary>
    public enum EnumZYBZ
    {
        /// <summary>
        /// 入院登记
        /// </summary>
        [Description("新入院")]
        Xry = 0,
        /// <summary>
        /// 病区中
        /// </summary>
        [Description("病区中")]
        Bqz = 1,
        /// <summary>
        /// 病区出院（出病区）（待结账）
        /// </summary>
        [Description("待结账")]
        Djz = 2,
        /// <summary>
        /// 已出院（出院结算）
        /// </summary>
        [Description("已出院")]
        Ycy = 3,
        /// <summary>
        /// 转区中
        /// </summary>
        [Description("转区中")]
        Zq = 7,

        /// <summary>
        /// 作废记录/取消入院
        /// </summary>
        [Description("作废记录")]
        Wry = 9,
    }

    /// <summary>
    /// 系统账户类别
    /// </summary>
    public enum EnumXTZHXZ
    {
        /// <summary>
        /// 单位帐户
        /// </summary>
        [Description("单位帐户")]
        DWZH = 0,
        /// <summary>
        /// 个人帐户
        /// </summary>
        [Description("个人帐户")]
        GRZH = 1,
        /// <summary>
        /// 家床账户
        /// </summary>
        [Description("家床账户")]
        JCZH = 2,
        /// <summary>
        /// 住院预缴款账户
        /// </summary>
        [Description("住院预缴款账户")]
        ZYYJKZH = 3,
        /// <summary>
        /// 帮困账户
        /// </summary>
        [Description("帮困账户")]
        BKZH = 4,
        /// <summary>
        /// 门诊登记账户
        /// </summary>
        [Description("门诊登记账户")]
        MZDJZH = 5,
        /// <summary>
        /// 门诊预缴款账户
        /// </summary>
        [Description("门诊预缴款账户")]
        MZYJKZH = 6
    }

    /// <summary>
    /// 医保交易类型  0 不交易 1 普通交易 2 大病交易 3 家床交易 4 工伤交易 5 住院交易 6 农保交易
    /// xt_brxz.ybjylx
    /// </summary>
    public enum EnumYBJYLX
    {
        /// <summary>
        /// 不交易
        /// </summary>
        [Description("不交易")]
        ybjylx0 = 0,
        /// <summary>
        /// 普通交易
        /// </summary>
        [Description("普通交易")]
        ybjylx1 = 1,
        /// <summary>
        /// 大病交易
        /// </summary>
        [Description("大病交易")]
        ybjylx2 = 2,
        /// <summary>
        /// 家床交易
        /// </summary>
        [Description("家床交易")]
        ybjylx3 = 3,
        /// <summary>
        /// 工伤交易
        /// </summary>
        [Description("工伤交易")]
        ybjylx4 = 4,
        /// <summary>
        /// 住院交易
        /// </summary>
        [Description("住院交易")]
        ybjylx5 = 5,
        /// <summary>
        /// 农保交易
        /// </summary>
        [Description("农保交易")]
        ybjylx6 = 6,
    }

    /// <summary>
    /// 自负性质（药品、项目字典的属性）
    /// </summary>
    public enum EnumZiFuXingZhi
    {
        /// <summary>
        /// 可报
        /// </summary>
        [Description("可报")]
        KB = 0,
        /// <summary>
        /// 自费
        /// </summary>
        [Description("自费")]
        ZF = 1,
        /// <summary>
        /// 分类自负
        /// </summary>
        [Description("分类自负")]
        FLZF = 2,
        /// <summary>
        /// 绝对自理
        /// </summary>
        [Description("绝对自理")]
        JueDuiZiLi = 3,
        /// <summary>
        /// 甲类
        /// </summary>
        [Description("甲类")]
        Jia,
        /// <summary>
        /// 乙类
        /// </summary>
        [Description("乙类")]
        Yi,
        /// <summary>
        /// 丙类
        /// </summary>
        [Description("丙类")]
        Bing,
    }

    /// <summary>
    /// 算法 自负性质
    /// 确定按自负比例计算出来的自负部分的性质
    /// </summary>
    public enum EnumSuanFaZFXZ
    {
        /// <summary>
        /// 自负（同医保交易后自负现金）
        /// </summary>
        [Description("自负")]
        ZF = 0,
        /// <summary>
        /// 自理
        /// </summary>
        [Description("自理")]
        ZL = 1,
    }

    /// <summary>
    /// 账户收支性质 
    /// xt_brzhszjl.szxz
    /// </summary>
    public enum AccSzxz
    {
        [Description("门诊账户存取")]
        MZZH = 0,
        [Description("家床记帐")]
        JCZH = 1,
        [Description("门诊结算")]
        MZJS = 2,
        [Description("病区记帐")]
        BQJZ = 3,
        [Description("住院结算")]
        ZYJS = 4,
        [Description("住院账户存取")]
        ZYZH = 5,
        [Description("住院补缴款")]
        ZYBJK = 6,
        [Description("门诊记账登记")]
        MZJZDJ = 7
    }

    /// <summary>
    /// 卡类型/卡身份标识
    /// </summary>
    [Description("卡类型")]
    public enum EnumCardType 
    {
        /// <summary>
        /// 医保磁条卡
        /// </summary>
        [Description("医保磁条卡")]
        CTK = 0,
        /// <summary>
        /// 虚拟卡/院内卡（自动生成时虚拟卡/手动输入时院内卡）
        /// </summary>
        [Description("虚拟卡")]
        XNK = 1,
        /// <summary>
        /// 社保卡
        /// </summary>
        [Description("社保卡")]
        YBJYK = 2,
        /// <summary>
        /// 医保电子凭证
        /// </summary>
        [Description("医保电子凭证")]
        ybdzpz = 3,
        /// <summary>
        /// 身份证医保就诊
        /// </summary>
        [Description("身份证(医保)")]
        sfz = 4,
        /// <summary>
        /// 健康码
        /// </summary>
        [Description("健康码")]
        XNHJYK = 5,
    }

    /// <summary>
    /// 门诊类型 mjzbz
    /// </summary>
    public enum EnumOutPatientType
    {
        /// <summary>
        /// 普通门诊
        /// </summary>
        [Description("普通门诊")]
        generalOutpat = 1,
        /// <summary>
        /// 急诊
        /// </summary>
        [Description("急诊")]
        emerDiagnosis = 2,
        /// <summary>
        /// 专家门诊
        /// </summary>
        [Description("专家门诊")]
        expertOutpat = 3,
            /// <summary>
            /// 特病门诊
            /// </summary>
        [Description("特病门诊")]
        SpecialOutpat = 4,
        /// <summary>
        /// 重大疾病门诊
        /// </summary>
        [Description("重大疾病门诊")]
        MajorDiseases = 5,
        /// <summary>
        /// 慢性病门诊
        /// </summary>
        [Description("慢性病门诊")]
        ChronicDisease = 6,
        /// <summary>
        /// 居民两病
        /// </summary>
        [Description("居民两病")]
        jmlb = 7,
        /// <summary>
        /// 意外伤害门诊
        /// </summary>
        [Description("意外伤害门诊")]
        ywshmz = 8,
        /// <summary>
        /// 生育门诊
        /// </summary>
        [Description("生育门诊")]
        symz = 9,
        /// <summary>
        /// 耐多药结核门诊
        /// </summary>
        [Description("耐多药结核门诊")]
        ndyjhmz = 10,
        /// <summary>
        /// 儿童两病门诊
        /// </summary>
        [Description("儿童两病门诊")]
        etlbmz = 11
    }

    /// <summary>
    /// 门诊住院标志  0 通用 1 仅门诊 2 仅住院 3 系统（门诊住院都不可用）例如：家床
    /// </summary>
    public enum Enummzzybz
    {
        /// <summary>
        /// 通用
        /// </summary>
        [Description("通用")]
        ty = 0,
        /// <summary>
        /// 仅门诊
        /// </summary>
        [Description("仅门诊")]
        mz = 1,
        /// <summary>
        /// 仅住院
        /// </summary>
        [Description("仅住院")]
        zy = 2,
        /// <summary>
        /// 系统
        /// </summary>
        [Description("系统")]
        sys = 3
    }

    /// <summary>
    /// 
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

    /// <summary>
    /// 结算状态
    /// </summary>
    public enum EnumJieSuanZT
    {
        /// <summary>
        /// 已结（未退）
        /// </summary>
        [Description("已结")]
        YJ = 1,
        /// <summary>
        /// 已退
        /// </summary>
        [Description("已退")]
        YT,
    }

    /// <summary>
    /// 医嘱性质
    /// </summary>
    public enum EnumYiZhuXZ
    {
        /// <summary>
        /// 药品
        /// </summary>
        YP = 1,
        /// <summary>
        /// 项目
        /// </summary>
        XM,
    }

    /// <summary>
    /// 财务发票停用标志
    /// </summary>
    public enum EnumCWFPTY
    {
        /// <summary>
        /// 未停用
        /// </summary>
        WTY = 0,

        /// <summary>
        /// 已停用
        /// </summary>
        YTY = 1,
    }
    /// <summary>
    /// 挂号性质 
    /// 0-普通挂号
    /// 1-空挂号
    /// 2-家床挂号
    /// 3-自费转医保挂号
    /// 4-门诊记账挂号
    /// </summary>
    public enum EnumGhxz
    {
        PTGH = 0,
        KGH = 1,
        JCGH = 2,
        ZFZYBGH = 3,
        MZJZGH = 4
    }
    /// <summary>
    /// 结算类型
    /// </summary>
    public enum EnumJslx
    {
        /// <summary>
        /// 挂号
        /// </summary>
        [Description("挂号收费")]
        GH = 0,
        /// <summary>
        /// 
        /// </summary>
        [Description("家床记账")]
        JCJZ = 1,
        /// <summary>
        /// 门诊记账
        /// </summary>
        [Description("门诊记账")]
        MZJZ = 2,
        /// <summary>
        /// 
        /// </summary>
        [Description("住院")]
        ZY = 3,
        /// <summary>
        /// 
        /// </summary>
        [Description("ZHSZ")]
        ZHSZ = 4
    }

    /// <summary>
    ///医嘱性质 1 临时医嘱   2 长期医嘱   3 出院医嘱
    /// </summary>
    public enum EnumYZXZ
    {
        /// <summary>
        /// 临时医嘱
        /// </summary>
        LSYZ = 1,
        /// <summary>
        /// 长期医嘱
        /// </summary>
        CQYZ = 2,
        /// <summary>
        /// 出院医嘱
        /// </summary>
        CYYZ = 3
    }


    /// <summary>
    ///医嘱状态 1 未撤销   2 已撤销
    /// </summary>
    public enum EnumYZZT
    {
        /// <summary>
        /// 未撤销
        /// </summary>
        WCX = 1,
        /// <summary>
        /// 已撤销
        /// </summary>
        YCX = 2
    }

    /// <summary>
    /// 住院记账收费项目类型
    /// </summary>
    public enum EnumxmType
    {
        XM = 2,//药品
        YP = 1 //项目
    }

    /// <summary>
    /// 语言
    /// </summary>
    public enum Enumlan
    {
        Default = 0,
        Fanti = 1,
        English = 2,
        Jpanese = 3,
    }
    /// <summary>
    /// 打印方式 1:补打  2:重打 3:打印发票
    /// </summary>
    public enum Enumdyfs
    {
        /// <summary>
        /// 补打
        /// </summary>
        BD = 1,
        /// <summary>
        /// 重打
        /// </summary>
        CD = 2,
        /// <summary>
        /// 打印发票
        /// </summary>
        Print = 3
    }

    /// <summary>
    /// 挂号类型 特需 特需V
    /// </summary>
    [Obsolete("不同医疗机构的编码不一样")]
    public enum Enumghlx
    {
        TeXu = 7884,
        TeXuV = 7889,
    }

    /// <summary>
    /// 生效标志
    /// </summary>
    public enum EnumSXBZ
    {
        /// <summary>
        /// 即时生效
        /// </summary>
        [Description("即时生效")]
        Jssx = 0,
        /// <summary>
        /// 定点生效
        /// </summary>
        [Description("定点生效")]
        Ddsx = 1

    }

    /// <summary>
    /// 地域标志
    /// </summary>
    public enum EnumDY
    {
        /// <summary>
        /// 本地
        /// </summary>
        [Description("本地")]
        bd = 0,
        /// <summary>
        /// 外地
        /// </summary>
        [Description("外地")]
        wd = 1

    }

    /// <summary>
    /// 婚否
    /// </summary>
    public enum EnumHF
    {
        /// <summary>
        /// 未婚
        /// </summary>
        [Description("未婚")]
        wh = 0,
        /// <summary>
        /// 已婚
        /// </summary>
        [Description("已婚")]
        yh,
        /// <summary>
        /// 不详
        /// </summary>
        [Description("不详")]
        UnKnown = 9,
    }

    /// <summary>
    /// 入院途径
    /// </summary>
    public enum EnumRYTJ
    {
        /// <summary>
        /// 门诊
        /// </summary>
        [Description("普通住院")]
        mz = 21,
        /// <summary>
        /// 急诊
        /// </summary>
        [Description("急诊转住院")]
        jz=24,
        /// <summary>
        /// 其他医疗机构转入
        /// </summary>
        [Description("转入住院")]
        qtyljg=9925,
        /// <summary>
        /// 转外诊治住院
        /// </summary>
        [Description("转外诊治住院")]
        zwzzzy = 23,
        /// <summary>
        /// 重大疾病住院
        /// </summary>
        [Description("重大疾病住院")]
        dbry= 9902,
        /// <summary>
        /// 生育住院
        /// </summary>
        [Description("生育住院")]
        syzy = 52,
        /// <summary>
        /// 儿童两病住院
        /// </summary>
        [Description("儿童两病住院")]
        etlb= 9904,
        /// <summary>
        /// 耐多药结核住院
        /// </summary>
        [Description("耐多药结核住院")]
        ndyzy= 9907,
        /// <summary>
        /// 新生儿随母住院
        /// </summary>
        [Description("新生儿随母住院")]
        xsesmzy = 9921,
        /// <summary>
        /// 120
        /// </summary>
        [Description("120")]
        jj120,
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Else = 9,
    }

    /// <summary>
    /// 证件类型
    /// </summary>
    public enum EnumZJLX
    {
        /// <summary>
        /// 身份证
        /// </summary>
        [Description("身份证")]
        sfz = 1,
        /// <summary>
        /// 护照
        /// </summary>
        [Description("护照")]
        hz,
        /// <summary>
        /// 军官证
        /// </summary>
        [Description("军官证")]
        jgz,
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        qt = 9

    }

    /// <summary>
    /// 医保特殊待遇
    /// </summary>
    public enum EnumYBTSDY
    {
        /// <summary>
        /// 普通
        /// </summary>
        [Description("普通")]
        pt = 1,
        /// <summary>
        /// 离休
        /// </summary>
        [Description("离休")]
        lx,
        /// <summary>
        /// 伤残
        /// </summary>
        [Description("伤残")]
        sc

    }

    /// <summary>
    /// 适用医保办法 0：城保 1：个体医保 2：小城镇 3：小时工
    // A:民政医疗帮困
    // B：老年遗嘱 C:五保老人 D：重残人员
    // E:中小学生和婴幼儿 F： 其他居民
    //G：医疗互助帮困对象
    /// </summary>
    public enum EnumSYYBBF
    {
        /// <summary>
        /// 城保
        /// </summary>
        [Description("城保")]
        cb = 1,
        /// <summary>
        /// 个体医保
        /// </summary>
        [Description("个体医保")]
        gtyb,
        /// <summary>
        /// 小城镇
        /// </summary>
        [Description("小城镇")]
        xcz,
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        qt = 9

    }

    /// <summary>
    /// 挂号排班时间
    /// </summary>
    public enum EnumRegSchedule
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        none = 0,
        /// <summary>
        /// 全天
        /// </summary>
        [Description("全天")]
        allday = 1,
        /// <summary>
        /// 上午
        /// </summary>
        [Description("上午")]
        AM = 2,
        /// <summary>
        /// 下午
        /// </summary>
        [Description("下午")]
        PM = 3,

    }

    /// <summary>
    /// 记账计划执行状态
    /// </summary>
    public enum EnumJzjhZXZT 
    {
        /// <summary>
        /// 未执行
        /// </summary>
        [Description("未执行")]
        None = 0,
        /// <summary>
        /// 执行中
        /// </summary>
        [Description("执行中")]
        Part = 1,
        /// <summary>
        /// 全部
        /// </summary>
        [Description("已完成")]
        Finished = 2,
        /// <summary>
        /// 已停止
        /// </summary>
        [Description("已停止")]
        Stopped = 3,
    }

    /// <summary>
    /// 提醒类型
    /// </summary>
    public enum EnmuReminderType
    {
        /// <summary>
        /// 金额上限提醒
        /// </summary>
        [Description("金额上限提醒")]
        金额上限提醒 = 1,    //比如：治疗师一个月内收费不得超过十万
    }

    /// <summary>
    /// 同步治疗记录 处理状态
    /// </summary>
    public enum EnumTbzljlClztType
    {
        /// <summary>
        /// 未确认
        /// </summary>
        [Description("未确认")]
        WQR = 1,
        /// <summary>
        /// 已确认
        /// </summary>
        [Description("已确认")]
        YQR,
        /// <summary>
        /// 作废
        /// </summary>
        [Description("作废")]
        ZF,
    }

    /// <summary>
    /// 登录标志
    /// </summary>
    public enum EnumLoginFlag
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("单点登录")]
        SSO = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("系统登录")]
        App = 2
    }

    /// <summary>
    /// 收费项目计价策略
    /// </summary>
    public enum EnumSfxmJjcl
    {
        /// <summary>
        /// 按时长
        /// </summary>
        [Description("按时长")]
        Time = 1,
        /// <summary>
        /// 按数量
        /// </summary>
        [Description("按数量")]
        Amount,
        /// <summary>
        /// 按面积
        /// </summary>
        [Description("按面积")]
        Acreage,
    }

    /// <summary>
    /// 门诊病人就诊状态（就诊标志）
    /// </summary>
    public enum EnumOutpatientJzbz
    {
        /// <summary>
        /// 待就诊
        /// </summary>
        [Description("待就诊")]
        Djz = 1,
        /// <summary>
        /// 就诊中
        /// </summary>
        [Description("就诊中")]
        Jzz,
        /// <summary>
        /// 结束就诊
        /// </summary>
        [Description("结束就诊")]
        Jsjz,
    }

    /// <summary>
    /// 枚举类型
    /// </summary>
    public enum EnumPrescriptionType
    {
        /// <summary>
        /// 药品
        /// </summary>
        [Description("药品")]
        Medicine = 1,
        /// <summary>
        /// 治疗项目
        /// </summary>
        [Description("治疗项目")]
        Treament,
    }
    /// <summary>
    /// 处方类型
    /// </summary>
    public enum EnumCflx
    {
        /// <summary>
        /// 西药处方
        /// </summary>
        [Description("西药处方")]
        WMPres = 1,
        /// <summary>
        /// 中药处方
        /// </summary>
        [Description("中药处方")]
        TCMPres = 2,
        /// <summary>
        /// 康复处方
        /// </summary>
        [Description("康复处方")]
        RehabPres = 3,
        /// <summary>
        /// 检验处方
        /// </summary>
        [Description("检验处方")]
        InspectionPres = 4,
        /// <summary>
        /// 检查处方
        /// </summary>
        [Description("检查处方")]
        ExaminationPres = 5,
        /// <summary>
        /// 常规项目处方
        /// </summary>
        [Description("常规项目处方")]
        RegularItemPres = 6,

        [Description("电子处方")]
        Dzcf = 7,

    }
    /// <summary>
    /// 收支性质
    /// </summary>
    public enum EnumSZXZ
    {
        [Description("充值")]
        cz = 1,
        [Description("取款")]
        qk = 2,
        [Description("门诊结算")]
        mzjs = 3,
        [Description("门诊结算退回")]
        mzjsth = 4,
        [Description("住院结算")]
        zyjs = 5,
        [Description("住院结算退回")]
        zyjsth = 6,
        [Description("退余额")]
        tye = 9
    }

    /// <summary>
    /// 危重级别
    /// </summary>
    public enum EnumWzjb
    {
        /// <summary>
        /// 一般
        /// </summary>
        [Description("一般")]
        Normal = 1,
        /// <summary>
        /// 病重
        /// </summary>
        [Description("病重")]
        Serious = 2,
        /// <summary>
        /// 病危
        /// </summary>
        [Description("病危")]
        Bedying = 3
    }

    public enum EnumOrgshzt {
        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("通过")]
        TG = 1,
        /// <summary>
        /// 审核未通过
        /// </summary>
        [Description("未通过")]
        WTG = 2,
        /// <summary>
        /// 待审
        /// </summary>
        [Description("待审")]
        DS = 3,
        /// <summary>
        /// 未提交
        /// </summary>
        [Description("未提交")]
        WTJ = 4,
        /// <summary>
        /// 预提交
        /// </summary>
        [Description("预提交")]
        YTJ = 5
    }

    public enum EnumPayStatus
    {
        [Description("支付成功")]
        Success =1,
        [Description("支付失败")]
        Failed =2
    }

    public enum EnumRefundStatus
    {
        [Description("退款成功")]
        Success = 1,
        [Description("退款失败")]
        Failed = 2,
        [Description("未知")]
        UnKnown = 3
    }

    public enum EnumTradeType
    {

        /// <summary>
        /// 支付宝付款码
        /// </summary>
        [Description("支付宝")]
        Alipay_Bar_Code = 1,
        /// <summary>
        /// 微信付款码
        /// </summary>
        [Description("微信")]
        Wechat_MICROPAY = 2,
    }

    /// <summary>
    /// 枚举-年龄单位
    /// </summary>
    public enum EnumAgeUnit
    {
        Year = 1,
        Month = 2,
        Day = 3,
        Hour = 4,
    }
    /// <summary>
    /// 门诊预约状态
    /// </summary>
    public enum EnumMzyyzt
    {
        [Description("已约")]
        book =1,
        [Description("已挂号")]
        reg =2,
        [Description("预约已取消")]
        cancel =3,
        [Description("当日挂号")]
        bookreg = 4,
        [Description("取消挂号")]
        regcancel =5
    }
    /// <summary>
    /// 门诊排班类型
    /// </summary>
    public enum EnumMzpblx
    {
        [Description("全部")]
        all = 1,
        [Description("开放预约")]
        yy = 2,
        [Description("非预约")]
        fyy = 3
    }
    /// <summary>
    /// 开放预约
    /// </summary>
    public enum EnumMzpbBook
    {
        [Description("是")]
        Y = 1,
        [Description("否")]
        F = 0,
    }
    /// <summary>
    /// 处方状态
    /// </summary>
    public enum EnumZfzt {

        [Description("正常处方")]
        zc = 0,
        [Description("退药或者其他作废处方")]
        ty = 1
    }

    /// <summary>
    /// 急诊状态
    /// </summary>
    public enum EnumJzzt
    {
        [Description("否")]
        zc = 0,
        [Description("是")]
        ty = 1
    }
    /// <summary>
    /// 门诊挂号来源
    /// </summary>
    public enum EnumMzghly
    {
        [Description("His")]
        His = 0,
        [Description("预约")]
        Yy = 1,
        [Description("银医通")]
        Yyt = 3,
        [Description("微信")]
        WeChat = 4,
        [Description("支付宝")]
        Alipay = 5,
        [Description("体检")]
        Tj = 6,
        [Description("自助机")]
        SelfTerminal = 7,
    }
    /// <summary>
    /// 医保属性
    /// </summary>
    public enum EnumCblb
    {
        [Description("全部")]
        all = 0,
        [Description("职工")]
        zg = 1,
        [Description("居民")]
        jm = 2,
        [Description("离休")]
        lx = 3,
        [Description("异地职工")]
        ydzg = 4,
        [Description("异地居民")]
        ydjm = 5,

    }

    public enum EnumRportType
    {
        [Description("职工费用申报表(民政救助)")]
        zgmzjz = 1,
        [Description("居民费用申请表(民政救助)")]
        jmmzjz = 2,
        [Description("普通报账报表(职工)")]
        zgbz = 3,
        [Description("普通报账报表(居民)")]
        jmbz = 4,
        [Description("普通报账报表(离休)")]
        lxbz = 5,
        [Description("普通报账报表(异地职工)")]
        ydzgbz = 6,
        [Description("普通报账报表(异地居民)")]
        ydjmbz = 7,

    }
    /// <summary>
    /// 人员类别
    /// </summary>
    public enum EnumRylb
    {
        [Description("职工在职")]
        zgzz = 1101,
        [Description("公务员在职")]
        gwyzz = 1102,
        [Description("灵活就业人员在职")]
        lhjyryzz = 1103,
        [Description("地方其他扩展人员")]
        qtdfkzry = 1160,
        [Description("职工退休")]
        zgtx = 1201,
        [Description("公务员退休")]
        gwytx = 1202,
        [Description("灵活就业人员退休")]
        lhjyrytx = 1203,
        [Description("地方其他扩展人员")]
        qtdfkzrytx = 1260,
        [Description("离休人员")]
        lxry = 1300,
        [Description("地方其他扩展人员")]
        lxdfqtkzry = 1360,
        [Description("新生儿")]
        xse = 1401,
        [Description("学龄前儿童")]
        xlqet = 1402,
        [Description("中小学生")]
        zxxs = 1403,
        [Description("大学生")]
        dxs = 1404,
        [Description("未成年（未入学）")]
        wcn = 1405,
        [Description("地方其他扩展身份")]
        dfqtgkzf = 1460,
        [Description("普通居民（成年）")]
        ptjm = 1501,
        [Description("地方其他扩展身份")]
        jmdfqtgzsf = 1560,
        [Description("居民（老年）")]
        jmln = 16,
        [Description("居民（成年）")]
        jmcn = 15,
        [Description("居民（未成年）")]
        jmwcn = 14,
        [Description("离休")]
        lx = 13,
        [Description("退休人员")]
        txry = 12,
        [Description("在职")]
        zz = 11

    }
    public enum EnumYbQuery
    {
        [Description("人员待遇享受")]
        rydyxs = 1,
        [Description("就诊信息查询")]
        jzxxcx = 2,
        [Description("诊断信息查询")]
        zdxxcx = 3,
        [Description("结算信息查询")]
        jsxxcx = 4,
        [Description("费用明细查询")]
        fymxcx = 5,
        [Description("人员慢特病用药记录查询")]
        rymtbyyjl = 6,
        [Description("人员累计信息查询")]
        ryljxxcx = 7,
        [Description("人员特殊病备案查询")]
        rytsbbacx = 8,
        [Description("人员定点信息查询")]
        ryddxxcx = 9,
        [Description("在院信息查询")]
        zyxxcx = 10,
        [Description("转院信息查询")]
        zyxx = 11,

    }
    /// <summary>
    /// 待遇检查类型
    /// </summary>
    public enum Enumdyjclx
    {
        [Description("基金项检查")]
        jjxjc = 1,
        [Description("其他检查")]
        qtjc = 99
    }
    /// <summary>
    /// 基金支付类型
    /// </summary>
    public enum Enumjjzflx
    {
        [Description("城镇职工基本医疗保险统筹基金")]
        tcjj = 310100,
        [Description("城镇职工基本医疗保险个人账户基金")]
        grzhjj = 310200,
        [Description("公务员医疗补助基金")]
        gwybzjj = 320100,
        [Description("大额医疗费用补助基金")]
        debzjj = 330100,
        [Description("离休人员医疗保障基金")]
        lxbzjj = 340100,
        [Description("一至六级残疾军人医疗补助基金")]
        cjjrbzjj = 350100,
        [Description("企业补充医疗保险基金")]
        qybxjj = 370100,
        [Description("医院垫付基金")]
        yydfjj = 999996,
        [Description("区级公务员基金")]
        qgwyjj = 999901,
        [Description("城乡居民基本医疗保险基金")]
        cxjmbxjj = 390100,
        [Description("城乡居民大病医疗保险基金")]
        cxjmdbbxjj = 390200,
        [Description("生育基金")]
        syjj = 510100,
        [Description("职工生育基金")]
        zgsyjj = 310103,
        [Description("医疗救助基金")]
        yljzjj = 610100,
        [Description("医疗再救助基金")]
        ylzjzjj = 610102,
        [Description("健康扶贫基金")]
        jkpfjj = 610103,
        [Description("精准脱贫基金")]
        jztpjj = 610104,
        [Description("其他基金")]
        qtjj = 999997
    }
    /// <summary>
    /// 基金款项待遇享受标志
    /// </summary>
    public enum Enumdyxsbz
    {
        [Description("否")]
        s = 0,
        [Description("是")]
        f = 1
    }
    /// <summary>
    /// 诊断类别
    /// </summary>
    public enum Enumzdlx
    {
        [Description("西医诊断")]
        xyzd = 1,
        [Description("中医主病诊断")]
        zyzzd = 2,
        [Description("中医主证诊断")]
        zyzzzd = 3
    }
    /// <summary>
    /// 险种类型
    /// </summary>
    public enum Enumxzlx
    {
        [Description("职工基本医疗保险")]
        zgjbylbx = 310,
        [Description("公务员医疗补助")]
        gwyylbz = 320,
        [Description("大额医疗费用补助")]
        deylfybz = 330,
        [Description("离休人员医疗保障")]
        lxryylbz = 340,
        [Description("一至六级残废军人医疗补助")]
        cfjrylbz = 350,
        [Description("老红军医疗保障")]
        lhjylbz = 360,
        [Description("企业补充医疗保险")]
        qybcylbx = 370,
        [Description("新型农村合作医疗")]
        xxnchzyl = 380,
        [Description("城乡居民基本医疗保险")]
        cxjmjbylbx = 390,
        [Description("城镇居民基本医疗保险")]
        czjmjbylbx = 391,
        [Description("城乡居民大病医疗保险")]
        cxjmdbbx = 392,
        [Description("其他特殊人员医疗保障")]
        qttsryylbz = 399,
        [Description("长期照护保险")]
        cqzhbx = 410,
        [Description("生育保险")]
        sybx = 510
    }
    /// <summary>
    /// 医疗类别
    /// </summary>
    public enum Enumyllb
    {
        [Description("普通门诊")]
        ptmz = 11,
        [Description("门诊挂号")]
        mzgh = 12,
        [Description("急诊")]
        jz = 13,
        [Description("门诊慢特病")]
        mzmtb = 14,
        [Description("意外伤害门诊")]
        ywshmz = 19,
        [Description("普通住院")]
        ptzy = 21,
        [Description("转外诊治住院")]
        zwzzzy = 23,
        [Description("急诊转住院")]
        jzzzy = 24,
        [Description("生育门诊")]
        symz = 51,
        [Description("生育住院")]
        syzy = 52,
        [Description("计划生育手术费")]
        jhsyssf = 53,
        [Description("重大疾病门诊")]
        zdjbmz = 9901,
        [Description("重大疾病住院")]
        zdjbzy = 9902,
        [Description("儿童两病门诊")]
        etlbmz = 9903,
        [Description("儿童两病住院")]
        etlbzy = 9904,
        [Description("耐多药结核门诊")]
        ndyjhmz = 9906,
        [Description("耐多药结核住院")]
        ndyjhzy = 9907,
        [Description("转入住院")]
        zrzy = 9925,
        [Description("新生儿随母住院")]
        xsesmzy = 9921
    }
    /// <summary>
    /// 申报来源
    /// </summary>
    public enum Enumsbly
    {
        [Description("定点医药机构")]
        etlbmz = 01,
        [Description("中心经办系统")]
        etlbzy = 02,
        [Description("网上经办系统")]
        ndyjhmz = 03,
        [Description("APP")]
        ndyjhzy = 04,
        [Description("一体机")]
        zrzy = 05,
        [Description("其他")]
        xsesmzy = 99
    }
    /// <summary>
    /// 生育类别
    /// </summary>
    public enum EnumSylb
    {
        /// <summary>
        /// 正常产
        /// </summary>
        [Description("正常产")]
        Zcc = 1,
        /// <summary>
        /// 助娩产
        /// </summary>
        [Description("助娩产")]
        Zmc = 2,
        /// <summary>
        /// 剖宫产
        /// </summary>
        [Description("剖宫产")]
        Pgc = 3,
        /// <summary>
        /// 难产
        /// </summary>
        [Description("难产")]
        nc = 4,
        /// <summary>
        /// 流产
        /// </summary>
        [Description("流产")]
        lc = 5,
        /// <summary>
        /// 引产
        /// </summary>
        [Description("引产")]
        yc = 6,
        /// <summary>
        /// 产前检查
        /// </summary>
        [Description("产前检查")]
        cqjc = 7,
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        qt = 9,
        /// <summary>
        /// 宫外孕
        /// </summary>
        [Description("宫外孕")]
        gwc = 10,
        /// <summary>
        /// 遗传基因检测
        /// </summary>
        [Description("遗传基因检测")]
        ycjyjc = 11,
        /// <summary>
        /// 四个月以上流引产
        /// </summary>
        [Description("四个月以上流产")]
        sgyyslc = 12,
        /// <summary>
        /// 四个月以下流产
        /// </summary>
        [Description("四个月以下流产")]
        sgyyxlc = 13
    }
    /// <summary>
    /// 计划生育手术类别
    /// </summary>
    public enum EnumSysslb
    {
        /// <summary>
        /// 放置宫内节育器
        /// </summary>
        [Description("放置宫内节育器")]
        fzgnjyq = 1,
        /// <summary>
        /// 取出宫内节育器
        /// </summary>
        [Description("取出宫内节育器")]
        qcgnjyq = 2,
        /// <summary>
        /// 流产术
        /// </summary>
        [Description("流产术")]
        lcs = 3,
        /// <summary>
        /// 引产术
        /// </summary>
        [Description("引产术")]
        ycs = 4,
        /// <summary>
        /// 绝育手术
        /// </summary>
        [Description("绝育手术")]
        jyss = 5,
        /// <summary>
        /// 绝育复通手术
        /// </summary>
        [Description("绝育复通手术")]
        jyftss = 6,
        /// <summary>
        /// 绝育手术（输精管）
        /// </summary>
        [Description("绝育手术（输精管）")]
        jysssjg = 7,
        /// <summary>
        /// 绝育复通手术（输精管）
        /// </summary>
        [Description("绝育复通手术（输精管）")]
        jyftsssjg = 8,
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        qt = 9,
        /// <summary>
        /// 皮下埋植术
        /// </summary>
        [Description("皮下埋植术")]
        pxmzs = 10,
        /// <summary>
        /// 取消皮下埋植术
        /// </summary>
        [Description("取消皮下埋植术")]
        qxpxmzs = 11,
        /// <summary>
        /// 输卵管结扎术
        /// </summary>
        [Description("输卵管结扎术")]
        srgjzs = 12
    }
    /// <summary>
    /// 晚育标志/早产标志
    /// </summary>
    public enum EnumWybz
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
    /// 医生类型
    /// </summary>
    public enum EnumYslx
    {
        [Description("住院医生")] //Resident
        ZyDoc = 1,
        [Description("主治医生")] //Attending
        ZzDoc = 2,
        [Description("主任医生")] //chief
        ZrDoc = 3
    }
    /// <summary>
    /// 异地标识
    /// </summary>
    public enum EnumYd
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
    /// 交易卡类型
    /// </summary>
    public enum EnumJykxzlx
    {
        /// <summary>
        /// 自费
        /// </summary>
        [Description("自费")]
        zf = 0,
        /// <summary>
        /// 医保
        /// </summary>
        [Description("医保")]
        yb = 1
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
