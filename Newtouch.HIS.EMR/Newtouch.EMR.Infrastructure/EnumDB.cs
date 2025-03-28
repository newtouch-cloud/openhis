using System.ComponentModel;

namespace Newtouch.Infrastructure
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
        pub=0,
        /// <summary>
        /// 个人
        /// </summary>
        [Description("个人")]
        prv =1,
        /// <summary>
        /// 科室
        /// </summary>
        [Description("科室")]
        dept =2
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

    public enum EnumSex
    {
        [Description("男")]
        M =1,
        [Description("女")]
        F =2
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

    public enum EnumPlanStu {
        [Description("进行中")]
        zc = 0,
        [Description("已停止")]
        tz = 1,
        [Description("停止取消")]
        tzqx = 2,
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
    /// 门诊就诊状态
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
    /// 门诊类型 mjzbz
    /// </summary>
    public enum EnumOutPatientType
    {
        /// <summary>
        /// 普通门诊
        /// </summary>
        [Description("门诊")]
        generalOutpat = 1,
        /// <summary>
        /// 急诊
        /// </summary>
        [Description("急诊")]
        emerDiagnosis = 2,
        ///// <summary>
        ///// 专家门诊
        ///// </summary>
        //[Description("专家门诊")]
        //expertOutpat = 3,
        ///// <summary>
        ///// 专家门诊
        ///// </summary>
        //[Description("特病门诊")]
        //SpecialOutpat = 4,
        ///// <summary>
        ///// 重大疾病门诊
        ///// </summary>
        //[Description("重大疾病门诊")]
        //MajorDiseases = 5,
        ///// <summary>
        ///// 慢性病门诊
        ///// </summary>
        //[Description("慢性病门诊")]
        //ChronicDisease = 6
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
}