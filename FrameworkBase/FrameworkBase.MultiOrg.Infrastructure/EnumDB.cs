using System.ComponentModel;

namespace FrameworkBase.MultiOrg.Infrastructure
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
    /// 收费大类 大类类别
    /// </summary>
    public enum EnumSfdlDllb
    {
        /// <summary>
        /// 药品
        /// </summary>
        [Description("药品")]
        yp = 1,
        /// <summary>
        /// 治疗项目
        /// </summary>
        [Description("治疗项目")]
        zlxm,
        /// <summary>
        /// 非治疗项目
        /// </summary>
        [Description("非治疗项目")]
        flzxm
    }

    /// <summary>
    /// 康复项目计价策略
    /// </summary>
    public enum EnumKfxmJjcl
    {
        /// <summary>
        /// 按时长
        /// </summary>
        [Description("按时长")]
        sc = 1,
        /// <summary>
        /// 按数量
        /// </summary>
        [Description("按数量")]
        sl = 2,
        /// <summary>
        /// 按面积
        /// </summary>
        [Description("按面积")]
        mj = 3
    }

}
