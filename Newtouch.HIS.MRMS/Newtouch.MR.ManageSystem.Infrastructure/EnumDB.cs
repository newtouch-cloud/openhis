using System.ComponentModel;

namespace Newtouch.MR.ManageSystem.Infrastructure
{
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
    public enum EnumSex
    {
        [Description("未知的性别")]
        Unknown = 0,
        [Description("男")]
        M = 1,
        [Description("女")]
        F = 2,
        [Description("未说明的性别")]
        Empty = 9
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
        [Description("已归档")]
        gd = 3
    }

    /// <summary>
    /// 病历状态 与EMR通用
    /// </summary>
    public enum EnumRecordStu
    {

        [Description("已提交")]
        ytj = 1,
        [Description("退回")]
        th = 2,
        [Description("已签收")]
        yqs = 3,
        [Description("未提交")]
        wtj = 4,
        //[Description("病案归档")]
        //bagd = 9
    }
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

    //public enum EnumRytj
    //{
    //    [Description("急诊")]
    //    JZ = 1,
    //    [Description("门诊")]
    //    MZ = 2,
    //    [Description("其他医疗机构转入")]
    //    QTOrg = 3,
    //    [Description("其他")]
    //    QT = 9
    //}
    /// <summary>
    /// 关系
    /// </summary>
    public enum EnumGx {
        [Description("本人或户主")]
        br = 1,
        [Description("配偶")]
        fq = 2,
        [Description("子")]
        z = 3,
        [Description("女")]
        n ,       
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
    /// 是否有无
    /// </summary>
    public enum EnumYorN
    {
        [Description("否")]
        N=1,
        [Description("是")]
        Y
    }
    /// <summary>
    /// 是否有无
    /// </summary>
    public enum EnumHorN
    {
        [Description("无")]
        Non = 1,
        [Description("有")]
        Has
    }
    /// <summary>
    ///入院病情 1	有 2	临床未确定 3	情况不明 4	无
    /// </summary>
    public enum EnumRybq {
        [Description("有")]
        y=1,
        [Description("临床未确定")]
        wqd,
        [Description("情况不明")]
        bm,
        [Description("无")]
        w
    }

    /// <summary>
    /// 出院情况
    /// 1	治愈 2	好转 3	未愈4	死亡5	其他
    /// </summary>
    public enum EnumCyqk
    {
        [Description("治愈")]
        zy=1,
        [Description("好转")]
        hz,
        [Description("未愈")]
        wy,
        [Description("死亡")]
        sw,
        [Description("其他")]
        qt
    }
    /// <summary>
    /// 诊断类型
    /// </summary>
    public enum EnumZdlx
    {
        [Description("主要诊断")]
        zy=1,
        [Description("次要诊断")]
        cy
    }


    /// <summary>
    /// 入院途径
    /// </summary>
    public enum EnumRYTJ
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
        jz,
        /// <summary>
        /// 其他医疗机构转入
        /// </summary>
        [Description("其他医疗机构转入")]
        qtyljg,
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
}