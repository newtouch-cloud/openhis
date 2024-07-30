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
    /// 自负性质
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
    /// 自负性质 v2 
    /// 重庆再用
    /// </summary>
    public enum EnumZFXZv2 {
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

    /// <summary>
    /// 康复收费项目 计费方式
    /// </summary>
    public enum EnumCollectionMethod
    {
        /// <summary>
        /// 服务次数
        /// </summary>
        [Description("服务次数")]
        Service = 1,
        /// <summary>
        /// 时长
        /// </summary>
        [Description("时长")]
        Time
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
        yp = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("治疗项目")]
        zlxm,
        /// <summary>
        /// 
        /// </summary>
        [Description("非治疗项目")]
        flzxm
    }
    /// <summary>
    /// 收费父级分类
    /// </summary>
    public enum EnumSffjdlCode
    {
        [Description("药品费")]
        ypf=1001,
        [Description("检查费")]
        jcf,
        [Description("检验费")]
        jyf,
        [Description("治疗费")]
        zlf,
        [Description("床位费")]
        cwf,
        [Description("材料费")]
        clf,
        [Description("麻醉费")]
        mzf
    }
    public enum EnumSffjdlmc
    {
        药品费 = 1001,
        检查费,
        检验费,
        治疗费,
        床位费,
        材料费,
        麻醉费
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
    /// 菜单目标类型
    /// </summary>
    public enum EnumModuleTargetType
    {
        /// <summary>
        /// 无页面
        /// </summary>
        expand,
        /// <summary>
        /// 框架页
        /// </summary>
        iframe,
        /// <summary>
        /// 弹出页
        /// </summary>
        open,
        /// <summary>
        /// 新窗口
        /// </summary>
        blank,
        /// <summary>
        /// 子页面（不显示在菜单中）
        /// </summary>
        subpage,
        /// <summary>
        /// 当前窗口
        /// </summary>
        toplocation,
    }

    /// <summary>
    /// 床位类型 男女混
    /// </summary>
    public enum EnumWardBedType
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
        /// 混
        /// </summary>
        [Description("混")]
        Mix = 3
    }

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
    /// 出入库标志
    /// </summary>
    public enum EnumCrkbz
    {
        [Description("入库")]
        In = 0,
        [Description("出库")]
        Out = 1,
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

    public enum EnumZjlx
    {
        [Description("身份证")]
        Sfz = 1,
    }

    public enum EnumYpsx {
        [Description("麻醉药品")]
        Mzyp = 9,
        [Description("毒性药品")]
        Dxyp = 4,
        [Description("精一药品")]
        Jyyp = 5,
        [Description("精二药品")]
        Jeyp = 6,
    }

    public enum EnumKss {

        [Description("非限制使用药物")]
        Fxzsyyw = 0,
        [Description("限制使用药物")]
        Xzsyyw = 1,
        [Description("特殊使用药物")]
        Tssyyw = 2,
    }

	public enum EnumSSLX
	{

		[Description("诊断型")]
		zdx = 0,
		[Description("治疗型")]
		zlx = 1,
		[Description("操作型")]
		czx = 2,
		[Description("手术")]
		ss = 3,
	}
    /// <summary>
    /// 消息用户连接状态
    /// </summary>
	public enum EnumNoticeUserConnStu
	{

		[Description("已连接")]
		conn = 1,
		[Description("离线")]
        disconn = 2,
		[Description("重连")]
		reconn = 3,
	}
}
