using System.ComponentModel;

namespace NewtouchHIS.Base.Domain.EnumExtend
{
    public class ClientEnum
    {

    }
    public class SettClientEnum : ClientEnum
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
        /// 有重复定义
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

        ///// <summary>
        /////医嘱性质 1 临时医嘱   2 长期医嘱   3 出院医嘱
        ///// </summary>
        //public enum EnumYZXZ
        //{
        //    /// <summary>
        //    /// 临时医嘱
        //    /// </summary>
        //    LSYZ = 1,
        //    /// <summary>
        //    /// 长期医嘱
        //    /// </summary>
        //    CQYZ = 2,
        //    /// <summary>
        //    /// 出院医嘱
        //    /// </summary>
        //    CYYZ = 3
        //}


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
            jz = 24,
            /// <summary>
            /// 其他医疗机构转入
            /// </summary>
            [Description("转入住院")]
            qtyljg = 9925,
            /// <summary>
            /// 转外诊治住院
            /// </summary>
            [Description("转外诊治住院")]
            zwzzzy = 23,
            /// <summary>
            /// 重大疾病住院
            /// </summary>
            [Description("重大疾病住院")]
            dbry = 9902,
            /// <summary>
            /// 生育住院
            /// </summary>
            [Description("生育住院")]
            syzy = 52,
            /// <summary>
            /// 儿童两病住院
            /// </summary>
            [Description("儿童两病住院")]
            etlb = 9904,
            /// <summary>
            /// 耐多药结核住院
            /// </summary>
            [Description("耐多药结核住院")]
            ndyzy = 9907,
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

        public enum EnumOrgshzt
        {
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
            Success = 1,
            [Description("支付失败")]
            Failed = 2
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
            book = 1,
            [Description("已挂号")]
            reg = 2,
            [Description("预约已取消")]
            cancel = 3,
            [Description("当日挂号")]
            bookreg = 4,
            [Description("取消挂号")]
            regcancel = 5
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
        public enum EnumZfzt
        {

            [Description("正常处方")]
            zc = 0,
            [Description("退药或者其他作废处方")]
            ty = 1
        }

        /// <summary>
        /// 急诊状态
        /// 原EnumJzzt
        /// </summary>
        public enum EnumEJzzt
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

    public class CisClientEnum : ClientEnum
    {
        /// <summary>
        /// 证件类型
        /// </summary>
        public enum EnumZjlx
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
            other = 9
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
        /// 药品类别
        /// </summary>
        public enum EnumYpsx
        {
            [Description("麻精药品（麻醉、精一、精二）")]
            Mzyp = 9,
            [Description("毒性药品")]
            Dxyp = 4,
            [Description("血液制品")]
            Xyzp = 5,
            [Description("激素药品")]
            Jsyp = 6,
            [Description("贵重药品")]
            Gzyp = 7,
            [Description("抗肿瘤药物药品")]
            Kzlywyp = 8,
        }
        /// <summary>
        /// 抗生素级别
        /// </summary>
        public enum EnumKss
        {

            [Description("非限制使用药物")]
            Fxzsyyw = 0,
            [Description("限制使用药物")]
            Xzsyyw = 1,
            [Description("特殊使用药物")]
            Tssyyw = 2,
        }

        /// <summary>
        /// 就诊状态
        /// </summary>
        public enum EnumJzzt
        {
            /// <summary>
            /// 未就诊
            /// </summary>
            [Description("未就诊")]
            NotYetTreate = 1,
            /// <summary>
            /// 就诊中
            /// </summary>
            [Description("就诊中")]
            Treating = 2,
            /// <summary>
            /// 就诊结束
            /// </summary>
            [Description("就诊结束")]
            Treated = 3,
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

        public enum EnumYpyf
        {
            [Description("西医")]
            WM = 1,
            [Description("中医")]
            TCM = 2
        }

        /// <summary>
        /// 诊断类型
        /// </summary>
        public enum EnumZdlx
        {
            /// <summary>
            /// 主诊断
            /// </summary>
            [Description("主诊断")]
            Main = 1,
            /// <summary>
            /// 副诊断
            /// </summary>
            [Description("副诊断")]
            Deputy = 2
        }

        /// <summary>
        /// 处方模板类型
        /// （病历模板类型也是用此枚举）
        /// </summary>
        public enum EnumCfMbLx
        {
            /// <summary>
            /// 个人
            /// </summary>
            [Description("个人")]
            personal = 1,
            /// <summary>
            /// 科室
            /// </summary>
            [Description("科室")]
            department = 2,
            /// <summary>
            /// 全院
            /// </summary>
            [Description("全院")]
            hospital = 3,
        }

        /// <summary>
        /// 检验检查组套类型  （检验检查共用一个页面，只是参数（即type）不同）
        /// </summary>
        public enum EnumjyjczutaoLx
        {
            /// <summary>
            /// 检验
            /// </summary>
            [Description("检验")]
            jy = 1,
            /// <summary>
            /// 检查
            /// </summary>
            [Description("检查")]
            jc = 2
        }

        /// <summary>
        /// 检验检查模板类型  （检验检查共用一个页面，只是参数（即type）不同）
        /// </summary>
        public enum EnumjyjcmbLx
        {
            /// <summary>
            /// 检验
            /// </summary>
            [Description("检验")]
            jy = 1,
            /// <summary>
            /// 检查
            /// </summary>
            [Description("检查")]
            jc = 2
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
        /// 历史病历来源
        /// </summary>
        public enum Enumlsblly
        {
            /// <summary>
            /// 
            /// </summary>
            [Description("本系统")]
            self = 1,
            /// <summary>
            /// 
            /// </summary>
            [Description("api")]
            api,
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
        /// 饮食类别
        /// </summary>
        public enum Enumyslb
        {
            /// <summary>
            /// 
            /// </summary>
            [Description("回民普食")]
            hmps = 1,
            /// <summary>
            /// 
            /// </summary>
            [Description("回民半流")]
            hmbl = 2,
            /// <summary>
            /// 
            /// </summary>
            [Description("回民流质")]
            hmlz = 3,
            /// <summary>
            /// 
            /// </summary>
            [Description("一般普食")]
            ybps = 4,
            /// <summary>
            /// 
            /// </summary>
            [Description("一般半流")]
            ybbl = 5,
            /// <summary>
            /// 
            /// </summary>
            [Description("一般流质")]
            yblz = 6,
            /// <summary>
            /// 
            /// </summary>
            [Description("膳食自备")]
            sszb = 7,
            /// <summary>
            /// 
            /// </summary>
            [Description("禁食")]
            js = 8,
            /// <summary>
            /// 
            /// </summary>
            [Description("肠内营养")]
            cnyy = 9,
            /// <summary>
            /// 
            /// </summary>
            [Description("营养补充剂")]
            yybcj = 10,
        }


        /// <summary>
        /// 排队叫号状态
        /// </summary>
        public enum EmunQueueCalledStu
        {
            [Description("已签到")]
            sign = 1,
            [Description("已叫")]
            call = 2,
            [Description("过号")]
            pass = 3,
            [Description("应答")]
            reply = 4,
        }

        /// <summary>
        /// 门诊住院标志  0 通用 1 仅门诊 2 仅住院 3 系统（门诊住院都不可用）例如：家床
        /// </summary>
        public enum Enummzzybz
        {

            /// <summary>
            /// 仅门诊
            /// </summary>
            [Description("门诊")]
            mz = 1,
            /// <summary>
            /// 仅住院
            /// </summary>
            [Description("住院")]
            zy = 2,
            ///// <summary>
            ///// 通用
            ///// </summary>
            //[Description("通用")]
            //ty = 9,
        }
        public enum Enumzxzt
        {
            /// <summary>
            /// 未执行
            /// </summary>
            [Description("未执行")]
            wzx = 0,
            /// <summary>
            /// 已执行
            /// </summary>
            [Description("已执行")]
            yzx = 1,
            /// <summary>
            /// 取消执行
            /// </summary>
            [Description("取消执行")]
            yqx = 2

        }
        /// <summary>
        /// 分诊叫号状态
        /// </summary>
        public enum EmunConsultCalledStu
        {
            [Description("待叫号")]
            wait = 1,
            [Description("已叫")]
            call = 2,
            [Description("过号")]
            pass = 3,
            [Description("应答")]
            reply = 4,
            [Description("重新叫号")]
            recall = 5,
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

        #region 住院
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

        /// <summary>
        /// 医嘱类型
        /// </summary>
        public enum EnumYzlx
        {
            ///// <summary>
            ///// 通用
            ///// </summary>
            //[Description("通用")]
            //Ty = 1,
            /// <summary>
            /// 药品
            /// </summary>
            [Description("药品")]
            Yp = 2,
            /// <summary>
            /// 文字
            /// </summary>
            [Description("文字")]
            Wz = 3,
            /// <summary>
            /// 出院带药
            /// </summary>
            [Description("出院带药")]
            Cydy = 4,
            /// <summary>
            /// 项目
            /// </summary>
            [Description("项目")]
            sfxm = 5,
            /// <summary>
            /// 项目
            /// </summary>
            [Description("检验")]
            jy = 6,
            /// <summary>
            /// 项目
            /// </summary>
            [Description("检查")]
            jc = 7,
            /// <summary>
            /// 项目
            /// </summary>
            [Description("膳食医嘱")]
            ssyz = 8,
            /// <summary>
            /// 手术
            /// </summary>
            [Description("手术")]
            oper = 9,
            /// <summary>
            /// 中草药
            /// </summary>
            [Description("中草药")]
            zcy = 10,
            /// <summary>
            /// 康复
            /// </summary>
            [Description("康复")]
            rehab = 11

        }
        /// <summary>
        /// 医嘱状态
        /// </summary>
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
        /// 药品单位类别
        /// </summary>
        public enum EnumYPdwlb
        {
            /// <summary>
            /// 剂量单位
            /// </summary>
            Jldw = 1,
            /// <summary>
            /// 药库单位
            /// </summary>
            Ykdw = 2,
            /// <summary>
            /// 门诊单位
            /// </summary>
            Mzdw = 3,
            /// <summary>
            /// 住院单位
            /// </summary>
            Zydw = 4,
            /// <summary>
            /// 进货单位
            /// </summary>
            Jhdw = 5,
            /// <summary>
            /// 最小单位
            /// </summary>
            Zxdw = 6
        }
        /// <summary>
        /// 套餐范围
        /// </summary>
        public enum EnumTcfw
        {
            [Description("个人")]
            Person = 1,
            [Description("科室")]
            Dept = 2,
            [Description("病区")]
            Ward = 3,
            [Description("全院")]
            Hosp = 4
        }
        /// <summary>
        /// 护理病人状态（护理生命体征录入用）
        /// </summary>
        public enum EnumHlbrzt
        {
            [Description("入院")]
            Ry = 1,
            [Description("在院")]
            Zy = 2,
            [Description("出院")]
            Cy = 3,
            [Description("请假")]
            Qj = 4,
            [Description("转入")]
            Zr = 5,
            [Description("转出")]
            Zc = 6,
            [Description("拒测")]
            Jc = 7,
            [Description("返院")]
            Fy = 8,
            [Description("复测")]
            Fc = 9,
            [Description("卧床")]
            Wc = 10,
            [Description("死亡")]
            Sw = 11,
            [Description("手术")]
            ss = 12,
            [Description("回室")]
            hs = 13
        }
        /// <summary>
        /// 执行周期单位
        /// </summary>
        public enum EnumZxzqdw
        {
            /// <summary>
            /// 天
            /// </summary>
            Day = 1,
            /// <summary>
            /// 小时
            /// </summary>
            Hour = 2,
            /// <summary>
            /// 分钟
            /// </summary>
            Minute = 3,
            /// <summary>
            /// 不规则周期
            /// </summary>
            Unruly = 4
        }
        /// <summary>
        /// 发药退药标志
        /// </summary>
        public enum EnumMedSTflag
        {
            [Description("发药")]
            Receive = 1,
            [Description("全退")]
            AllReturn = 2,
            [Description("部分退")]
            PartReturn = 3
        }
        /// <summary>
        /// 医嘱性质
        /// </summary>
        public enum EnumYzxz
        {
            [Description("临时")]
            Ls = 1,
            [Description("长期")]
            Cq = 2
        }
        /// <summary>
        /// 床位记录状态
        /// </summary>
        public enum EnumCwjlzt
        {
            [Description("当前")]
            Dq = 1,
            [Description("转床")]
            Zc = 2,
            [Description("转区")]
            Zq = 3,
            [Description("出区")]
            Cq = 4,
            [Description("取消入区")]
            QxRq = 5
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
        /// 护理级别
        /// </summary> 
        public enum EnumHljb
        {
            /// <summary>
            /// I级
            /// </summary>
            [Description("I级")]
            I = 1,
            /// <summary>
            /// II级
            /// </summary>
            [Description("II级")]
            II = 2,
            /// <summary>
            /// III级
            /// </summary>
            [Description("III级")]
            III = 3,
            /// <summary>
            /// 特级
            /// </summary>
            [Description("特级")]
            T = 4
        }
        /// <summary>
        /// 结算状态
        /// </summary>
        public enum EnumJszt
        {
            [Description("已结算")]
            Yjs = 1,
            [Description("未结算")]
            Wjs = 2
        }
        /// <summary>
        /// 固定执行标准
        /// </summary>
        public enum EnumGdzxbz
        {
            /// <summary>
            /// 固定
            /// </summary>
            Fixed = 1,
            /// <summary>
            /// 不固定
            /// </summary>
            Nonfixed = 0
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
        //EnumDB已包含
        //public enum EnumZdlx
        //{
        //    MainDiag=1,
        //    SedDiag=2
        //}

        /// <summary>
        /// 体温测量说明
        /// </summary>
        public enum EnumTwclfs
        {
            /// <summary>
            /// 口温
            /// </summary>
            [Description("口温")]
            kw = 1,
            /// <summary>
            /// 腋温
            /// </summary>
            [Description("腋温")]
            yw = 2,
            /// <summary>
            /// 肛温
            /// </summary>
            [Description("肛温")]
            gw = 1,
        }
        /// <summary>
        /// 膳食类别
        /// </summary>
        public enum EnumSSLB
        {
            /// <summary>
            /// 类型
            /// </summary>
            [Description("类型")]
            lx = 1,
            /// <summary>
            /// 特殊要求
            /// </summary>
            [Description("特殊要求")]
            tsyq = 2,
            /// <summary>
            /// 膳食型号
            /// </summary>
            [Description("膳食型号")]
            ssxh = 3,
        }

        /// <summary>
        /// 模板使用权限
        /// </summary>
        public enum Enummbqx
        {
            /// <summary>
            /// 通用
            /// </summary>
            [Description("通用")]
            pub = 0,
            /// <summary>
            /// 个人
            /// </summary>
            [Description("个人")]
            prv = 1,
            /// <summary>
            /// 科室
            /// </summary>
            [Description("科室")]
            dept = 2
        }

        /// <summary>
        /// 病历类型
        /// </summary>
        public enum EnumBllx
        {
            /// <summary>
            /// 住院病历
            /// </summary>
            [Description("住院病历")]
            zybl = 1,

            /// <summary>
            /// 病程记录
            /// </summary>
            [Description("病程记录")]
            bcjl = 2,

            /// <summary>
            /// 医疗文书
            /// </summary>
            [Description("医疗文书")]
            ylws = 3,

            /// <summary>
            /// 护理记录
            /// </summary>
            [Description("护理记录")]
            hljl = 4,

            /// <summary>
            /// 病案首页
            /// </summary>
            /// 
            [Description("病案首页")]
            basy = 5,

            /// <summary>
            /// 康复评估
            /// </summary>
            [Description("康复评估")]
            kfpg = 6
        }

        //public enum EnumCYQK
        //{
        //    /// <summary>
        //    /// 治愈
        //    /// </summary>
        //    [Description("治愈")]
        //    zy = 1,
        //    /// <summary>
        //    /// 好转
        //    /// </summary>
        //    [Description("好转")]
        //    hz = 2,
        //    /// <summary>
        //    /// 未愈
        //    /// </summary>
        //    [Description("未愈")]
        //    wy = 3,
        //    /// <summary>
        //    /// 死亡
        //    /// </summary>
        //    [Description("死亡")]
        //    sw = 4,
        //    /// <summary>
        //    /// 其他
        //    /// </summary>
        //    [Description("其他")]
        //    qt = 5,
        //    /// <summary>
        //    /// 转院
        //    /// </summary>
        //    [Description("转院")]
        //    zhuanyuan = 6

        //}
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

        //皮试测试
        public enum EnumPSCS
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
        /// 皮试结果
        /// </summary>
        public enum EnumPsResult
        {
            [Description("阴性")]
            negative = 0,
            [Description("阳性")]
            positive = 1,
        }

        /// <summary>
        /// 科室备药状态
        /// </summary>
        public enum EnumKSBYZT
        {
            [Description("已申请")]
            sq = 0,
            [Description("已提交")]
            sh = 1,
            [Description("已审核")]
            tj = 2,
            [Description("已发药")]
            fy = 3,
            [Description("已退回")]
            th = 4,
            [Description("已作废")]
            zf = 5,
        }

        /// <summary>
        /// 科室备药退回状态
        /// </summary>
        public enum EnumKSBYTHZT
        {
            [Description("保存")]
            sq = 1,
            [Description("审核")]
            sh = 2,
            [Description("提交")]
            tj = 3,
            [Description("发药")]
            fy = 4,
            [Description("撤回")]
            ch = 5,
            [Description("退回")]
            th = 6,
            [Description("作废")]
            zf = 7,
        }
        /// <summary>
        /// 药品来源
        /// </summary>
        public enum EnumYply
        {
            [Description("科室备药")]
            ksby = 1,
            [Description("药房")]
            yf = 2
        }

        /// <summary>
        /// 皮试
        /// </summary>
        public enum EnumPs
        {
            [Description("阴")]
            yin = 0,
            [Description("阳")]
            yang = 1,
        }
        #endregion
    }
    #region Emr
    public class EmrClientEnum : ClientEnum
    {
        /// <summary>
        /// 模板使用权限
        /// </summary>
        public enum Enummbqx
        {
            /// <summary>
            /// 通用
            /// </summary>
            [Description("通用")]
            pub = 0,
            /// <summary>
            /// 个人
            /// </summary>
            [Description("个人")]
            prv = 1,
            /// <summary>
            /// 科室
            /// </summary>
            [Description("科室")]
            dept = 2
        }
        /// <summary>
        /// 模板权限分配
        /// </summary>
        public enum EnummbqxFp
        {
            /// <summary>
            /// 无任何权限
            /// </summary>
            [Description("无权限")]
            non = 0,
            /// <summary>
            /// 只读
            /// </summary>
            [Description("只读")]
            read = 1,
            /// <summary>
            /// 读写
            /// </summary>
            [Description("读写")]
            edit = 2
        }

        /// <summary>
        /// 病历状态
        /// </summary>
        public enum EnumRecordStu
        {
            [Description("未提交")]
            wtj = 0,
            [Description("已提交")]
            ytj = 1,
            [Description("退回")]
            th = 2,
            [Description("已签收")]
            yqs = 3,
            //[Description("病案归档")]
            //bagd = 4
        }

        public enum EnumPlanStu
        {
            [Description("进行中")]
            zc = 0,
            [Description("已停止")]
            tz = 1,
            [Description("停止取消")]
            tzqx = 2,
        }

        /// <summary>
        /// 病历大类及模板 业务标志
        /// </summary>
        public enum EnumMzbz
        {
            /// <summary>
            /// 住院
            /// </summary>
            [Description("住院")]
            zy = 0,
            /// <summary>
            /// 门诊
            /// </summary>
            [Description("门诊")]
            mz = 1,
            /// <summary>
            /// 不限
            /// </summary>
            [Description("不限")]
            all = 2,
        }

        /// <summary>
        /// 模板加载方式
        /// </summary>
        public enum EnummbqxTempLoadWay
        {
            /// <summary>
            /// 通用
            /// </summary>
            [Description("都昌编辑器")]
            DcWriter = 0,
            /// <summary>
            /// 个人
            /// </summary>
            [Description("独立视图")]
            View = 1
        }

        /// <summary>
        /// 使用标志
        /// </summary>
        public enum EnumSybz
        {
            [Description("是")]
            Y = 1,
            [Description("否")]
            N = 0
        }
        public enum EnumBlys
        {
            [Description("目录")]
            ml = -1,
            [Description("文本")]
            wb = 0,
            [Description("下拉")]
            xl = 1,
            [Description("下拉多选")]
            xldx = 2,
            [Description("日期(yyyy-MM-dd)")]
            rq = 3,
            [Description("时间(HH:mm)")]
            sj = 4,
            [Description("日期时间(yyyy-MM-dd HH:mm)")]
            rqsj = 5,
            [Description("数值")]
            sz = 6
        }
        public enum ApplyStatusEnum
        {
            /// <summary>
            /// 未批准
            /// </summary>
            [Description("未批准")]
            wpz = 0,
            /// <summary>
            /// 已审批
            /// </summary>
            [Description("已审批")]
            ysp = 1,
            /// <summary>
            /// 退回
            /// </summary>
            [Description("退回")]
            th = 2
        }

        public enum EnumMRSex
        {
            [Description("男")]
            M = 1,
            [Description("女")]
            F = 2
        }
        /// <summary>
        /// 病案状态
        /// </summary>
        public enum Enumbazt
        {
            [Description("待录入")]
            dlr = 1,
            [Description("已录入")]
            lrz = 2,
            //[Description("已归档")]
            //gd = 3
        }

        ///// <summary>
        ///// 病历状态 与EMR通用
        ///// </summary>
        //public enum EnumRecordStu
        //{
        //    [Description("未提交")]
        //    wtj = 0,
        //    [Description("已提交")]
        //    ytj = 1,
        //    [Description("退回")]
        //    th = 2,
        //    [Description("已签收")]
        //    yqs = 3,
        //    //[Description("病案归档")]
        //    //bagd = 4
        //}
        /// <summary>
        /// 血型
        /// </summary>
        public enum EnumBloodType
        {
            [Description("A")]
            A = 1,
            [Description("B")]
            B = 2,
            [Description("O")]
            O = 3,
            [Description("AB")]
            AB = 4,
            [Description("不详")]
            BX = 5,
            [Description("未查")]
            WC = 6
        }

        /// <summary>
        /// 死亡患者尸检
        /// </summary>
        public enum EnumSwhzsj
        {
            [Description("是")]
            Y = 1,
            [Description("否")]
            N = 2
        }

        /// <summary>
        /// RH
        /// </summary>
        public enum EnumBloodTypeRH
        {
            [Description("阴")]
            yin = 1,
            [Description("阳")]
            yang = 2,
            [Description("不详")]
            BX = 3,
            [Description("未查")]
            WC = 4
        }

        /// <summary>
        /// 关系
        /// </summary>
        public enum EnumGx
        {
            [Description("本人或户主")]
            br = 1,
            [Description("配偶")]
            fq = 2,
            [Description("子")]
            z = 3,
            [Description("女")]
            n,
            [Description("孙子、孙女或外孙子、外孙女")]
            sun,
            [Description("父母")]
            fm,
            [Description("祖父母或外祖父母")]
            zfm,
            [Description("兄、弟、姐、妹")]
            xdjm,
            [Description("其他")]
            qt
        }
        /// <summary>
        /// 是或否
        /// </summary>
        public enum EnumYorN
        {
            [Description("否")]
            N = 1,
            [Description("是")]
            Y = 2
        }


        /// <summary>
        /// 是或否
        /// </summary>
        public enum EnumNewYorN
        {
            [Description("是")]
            Y = 1,
            [Description("否")]
            N = 2
        }

        public enum EnumYork
        {
            [Description("急诊")]
            y = 1,
            [Description("门诊")]
            lcwqd = 2,
            [Description("其他医疗机构转入")]
            qkbm = 3,
            [Description("其他")]
            w = 4
        }
        /// <summary>
        /// 否或是
        /// </summary>
        public enum EnumNorY
        {
            [Description("否")]
            N = 1,
            [Description("是")]
            Y = 2
        }
        /// <summary>
        /// 有或无
        /// </summary>
        public enum EnumHorN
        {
            [Description("无")]
            Non = 1,
            [Description("有")]
            Has = 2
        }
        /// <summary>
        /// 输血反应
        /// 1.是  2.否  3.未输
        /// </summary>
        public enum EumSYFY
        {
            [Description("是")]
            y = 1,
            [Description("否")]
            n = 2,
            [Description("未输")]
            Non = 3
        }
        /// <summary>
        ///入院病情 1	有 2	临床未确定 3	情况不明 4	无
        /// </summary>
        public enum EnumRybq
        {
            [Description("有")]
            y = 1,
            [Description("临床未确定")]
            lcwqd = 2,
            [Description("情况不明")]
            qkbm = 3,
            [Description("无")]
            w = 4
        }

        /// <summary>
        /// 出院情况
        /// 1	治愈 2	好转 3	未愈4	死亡5	其他
        /// </summary>
        public enum EnumCyqk
        {
            [Description("治愈")]
            zy = 1,
            [Description("好转")]
            hz = 2,
            [Description("未愈")]
            wy = 3,
            [Description("死亡")]
            sw = 4,
            [Description("其他")]
            qt = 5
        }
        /// <summary>
        /// 诊断类型-主从
        /// </summary>
        public enum EnumZdlxbs
        {
            [Description("主要诊断")]
            zy = 1,
            [Description("次要诊断")]
            cy = 2
        }
        /// <summary>
        /// 诊断类型
        /// </summary>
        public enum EnumZdlx2
        {
            [Description("西医诊断")]
            WM = 1,
            [Description("中医诊断")]
            TCM = 2

        }

        /// <summary>
        /// 入院途径
        /// </summary>
        public enum EnumMrRYTJ
        {
            /// <summary>
            /// 门诊
            /// </summary>
            [Description("门诊")]
            mz = 1,
            /// <summary>
            /// 急诊
            /// </summary>
            [Description("急诊")]
            jz = 2,
            /// <summary>
            /// 其他医疗机构转入
            /// </summary>
            [Description("其他医疗机构转入")]
            qtyljg = 3,
            /// <summary>
            /// 120
            /// </summary>
            //[Description("120")]
            //jj120,
            /// <summary>
            /// 其他
            /// </summary>
            [Description("其他")]
            Else = 9,
        }
        /// <summary>
        /// 病案质量
        /// </summary>
        public enum EnumBazl
        {
            [Description("甲")]
            I = 1,
            [Description("乙")]
            II,
            [Description("丙")]
            III
        }
        /// <summary>
        /// 离院方式
        /// </summary>
        public enum EnumLyfs
        {
            [Description("医嘱离院")]
            yzly = 1,
            /// <summary>
            /// 急诊
            /// </summary>
            [Description("医嘱转院")]
            yzzy,
            [Description("医嘱转社区卫生服务机构/乡镇卫生院")]
            yzzsq,
            [Description("非医嘱离院")]
            fyz,
            [Description("死亡")]
            sw,
            [Description("其他")]
            other = 9,
        }
        /// <summary>
        /// 病情分型
        /// </summary>
        public enum EnumBqfx
        {
            [Description("病危")]
            bw = 1,
            [Description("病重")]
            bz,
            [Description("疑难")]
            yn,
            [Description("抢救")]
            qj,
            [Description("一般")]
            yb
        }
        /// <summary>
        /// 临床路径变异原因
        /// </summary>
        public enum EnumBYYY
        {
            [Description("正变异")]
            zby = 1,
            [Description("负变异")]
            fby,
            [Description("系统原因")]
            xtyy
        }
        /// <summary>
        /// 检查情况
        /// </summary>
        public enum EnumJBQK
        {
            [Description("CT")]
            CT = 1,
            [Description("PETCT")]
            PETCT = 2,
            [Description("双源")]
            SY = 3,
            [Description("B超")]
            Bc = 4,
            [Description("X片")]
            Xp = 5,
            [Description("超声心动图")]
            CSXDT = 6,
            [Description("MRI")]
            MRI = 7
        }
        /// <summary>
        /// 检查情况
        /// </summary>
        public enum EnumJCQK
        {
            [Description("阳性")]
            Yang = 1,
            [Description("阴性")]
            Non = 2,
            [Description("未做")]
            Yin = 3,


        }

        /// <summary>
        ///抗菌药物使用情况 
        /// </summary>
        public enum EnumKJYWSYQK
        {
            [Description("Ⅰ种")]
            first = 1,
            [Description("Ⅱ联")]
            second = 2,
            [Description("Ⅲ联")]
            third = 3,
            [Description("Ⅳ联")]
            forth = 4,
            [Description(">Ⅳ联")]
            more = 5
        }

        /// <summary>
        ///输液反映 0未输 2无 3有
        /// </summary>
        public enum EnumSYFY
        {
            [Description("未输")]
            first = 0,
            [Description("无")]
            second = 1,
            [Description("有")]
            third = 2
        }

        /// <summary>
        ///输血反映 0未输 2无 3有
        /// </summary>
        public enum EnumSXFY
        {
            [Description("是")]
            Y = 1,
            [Description("否")]
            N = 2,
            [Description("未输")]
            none = 3
        }

        /// <summary>
        ///住院跌倒伤害程度 0未输 2无 3有
        /// </summary>
        public enum EnumZYDD_SHCD
        {
            [Description("一级")]
            first = 1,
            [Description("二级")]
            second = 2,
            [Description("三级")]
            third = 3,
            [Description("未造成伤害")]
            none = 4
        }

        /// <summary>
        ///跌倒或坠床的原因
        /// </summary>
        public enum EnumZYDD_YY
        {
            [Description("健康原因")]
            jkyy = 1,
            [Description("治疗，药物，麻醉原因")]
            zlywmzyy = 2,
            [Description("环境因素")]
            hjys = 3,
            [Description("其他原因")]
            qtyy = 4
        }

        /// <summary>
        ///压疮分期
        /// </summary>
        public enum EnumYCFQ
        {
            [Description("1期")]
            first = 1,
            [Description("2期")]
            second = 2,
            [Description("3期")]
            third = 3,
            [Description("4期")]
            forth = 4
        }



    }
    #endregion


    #region OR
    public class OrClientEnum : ClientEnum
    {
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
            no = 0,
            [Description("是")]
            yes = 1
        }

        /// <summary>
        /// 是否有菌
        /// </summary>
        public enum EnumIsjun
        {
            [Description("否")]
            no = 0,
            [Description("是")]
            yes = 1
        }
    }
    #endregion

    #region PDS
    public class PdsClientEnum : ClientEnum
    {/// <summary>
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
            /// 批量出库
            /// </summary>
            [Description("批量出库")] // 根据出库方式统计单据信息
            piliangchuku = 14,
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
            /// 未发
            /// </summary>
            [Description("未发")]
            None = 0,

            /// <summary>
            /// 已发部分
            /// </summary>
            [Description("已发部分")]
            Part,

            /// <summary>
            /// 已全发
            /// </summary>
            [Description("已全发")]
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


    }
    #endregion

    #region Hrp
    public class HrpClientEnum : ClientEnum
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
            Refusal = -1,

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
            [Description("新增")] XZ = 1,
            [Description("作废")] ZF = 3,
        }

        //4.1.2 采购单类型
        public enum EnumCGDLX
        {
            [Description("医院自行采购单")] YYZXCGD = 1,
            [Description("托管药库采购单")] TGYKCGD = 2,
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
            [Description("按单配送")] ADPS = 1,
            [Description("按需配送")] AXPS = 2,
        }

        //4.1.5 退货类型

        public enum EnumTHLX
        {
            [Description("正常退货")] ZCTH = 1,
            [Description("补差价退货")] BCJTH = 2,

        }

        //4.1.6 配送验收类型
        public enum EnumPSYSLX
        {
            [Description("预验收")] YYS = 1,
            [Description("实验收")] SYS = 2,

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
            [Description("待确认")] DQR = 0,
            [Description("作废")] ZF = 1,
            [Description("已确认，未验收")] YQR = 10,

        }
        //4.1.9 采购明细状态
        public enum EnumCGMXZT
        {
            [Description("待提交")] DTJ = 0,
            [Description("已作废")] YZF = 1,
            [Description("待审核")] DSH = 10,
            [Description("审核不通过")] SHBTG = 11,
            [Description("待企业接受确认")] DQYJSQR = 20,
            [Description("企业已接收确认配送")] QYYJSQRPS = 21,
            [Description("企业已接收确认不予配送")] QYYJSQRBYPS = 22,
            [Description("企业已配送完成")] QYYPSWC = 23,
        }

        //4.1.10 企业库存
        public enum EnumQYKC
        {
            [Description("充足")] CZ = 1,
            [Description("有货， 限量")] YHXL = 2,
            [Description("有货， 少量")] YHSL = 3,
            [Description("暂无货")] ZWH = 4,
            [Description("不予披露")] BYPL = 5,
        }

        //4.1.11 发票状态
        public enum EnumFPZT
        {
            [Description("待确认")] DQR = 0,
            [Description("已作废")] YZF = 1,
            [Description("未验收")] WYS = 10,
            [Description("已验收")] YYS = 20,
            [Description("已拒收")] YJS = 21,
            [Description("已支付")] FPYZF = 30,
        }

        //4.1.12 配送明细状态
        public enum EnumPSMXZT
        {
            [Description("待确认")] DQR = 0,
            [Description("已作废")] YZF = 1,
            [Description("已提交未验收")] YTJWYS = 10,
            [Description("验收中")] YSZ = 20,
            [Description("验收入库完成")] YSRKWC = 30,
        }

        //4.1.13 退货明细状态
        public enum EnumTHMXZT
        {
            [Description("待确认")] DQR = 0,
            [Description("已作废")] YZF = 1,
            [Description("已确认待企业接受")] YQRDQYJS = 10,
            [Description("企业已确认退货处理")] QYYQRTHCL = 20,
            [Description("企业已确认不予处理")] QYYQRBYCL = 21,
        }

        //4.1.14 证件类型
        public enum EnumZJLX
        {
            [Description("组织机构代码证")] ZZJGDMZ = 1,
            [Description("营业执照")] YYZZ = 2,
            [Description("经营许可证")] JYXKZ = 3,
            [Description("生产许可证")] SCXKZ = 4,
        }

        //4.1.15 配送明细条码类型
        public enum EnumPSMXTMLX
        {
            [Description("GS1 条码")] GSI = 1,
            [Description("HIBC 条码")] HIBC = 2,
            [Description("其他")] QT = 99,
        }

        //4.1.16 发票验收结果
        public enum EnumFPYSJG
        {
            [Description("验收通过")] YSTG = 1,
            [Description("验收不通过")] YSBTG = 0,
        }

        //4.1.17 采购单状态
        public enum EnumCGDZT
        {
            [Description("待提交")] DTJ = 0,
            [Description("已作废")] YZF = 1,
            [Description("已提交")] YTJ = 20,
        }

        //4.1.18 退货单状态
        public enum EnumTHDZT
        {
            [Description("待确认")] DQR = 0,
            [Description("已作废")] YZF = 1,
            [Description("已确认")] YQR = 10,

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
            [Description("否")] F = 0,
            [Description("是")] S = 1,
        }


        #endregion
    }
    #endregion
}
