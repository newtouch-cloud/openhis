using NewtouchHIS.Lib.Base.Attributes;
using NewtouchHIS.Lib.Base.EnumExtend;

namespace NewtouchHIS.Lib.Base.Model
{
    public class BusinessConfig
    {
        /// <summary>
        /// 在线时长限制
        /// </summary>
        public int? LoginStatusKeepMinute { get; set; } = null;
        /// <summary>
        /// 开启多端登录限制
        /// </summary>
        public bool? EnableMultipleLoginLimit { get; set; } = null;
        /// <summary>
        /// 同步登录数限制
        /// </summary>
        public int? MultipleLoginLimit_Count { get; set; } = null;
        /// <summary>
        /// 登录次数过多，账号自动锁定 的 时间段（单位：分）
        /// </summary>
        public int? LoginFailedTimesLimit_Minutes { get; set; } = null;
        /// <summary>
        /// 登录次数过多，账号自动锁定 的 最大次数
        /// </summary>
        public int? LoginFailedTimesLimit_Count { get; set; } = null;
        /// <summary>
        /// 顶级机构编码
        /// </summary>
        public string? Top_OrganizeId { get; set; } = null;
        /// <summary>
        /// 当前应用AppId
        /// </summary>
        public string? AppId { get; set; } = null;
        /// <summary>
        /// 应用名称
        /// </summary>
        public string? AppName { get; set; } = null;
        /// <summary>
        /// 是否启用验证码 
        /// </summary>
        public bool? Is_CheckChkCode { get; set; } = null;
        /// <summary>
        /// 用户token缓存标识
        /// </summary>
        public string? Cache_UserToken { get; set; } = null;
        /// <summary>
        /// 开启重点日志监控
        /// </summary>
        public bool? EnableLoggingMonitorAttr { get; set; } = false;
        /// <summary>
        /// 重点日志监控是否带Header，默认false
        /// </summary>
        public bool? EnableLoggingMonitorWithHeaderAttr { get; set; } = false;
        /// <summary>
        /// 各系统API接口
        /// </summary>
        public AppAPIHostConfig? AppAPIHost { get; set; } = null;
        /// <summary>
        /// 各系统API AppId
        /// </summary>
        public AppAPIHostConfig? AppAPIHostName { get; set; } = null;
        /// <summary>
        /// HIS老系统 统一授权Token换取地址
        /// </summary>
        public string? UALoginAddress { get; set; }
        /// <summary>
        /// 授权申请Token的AppId
        /// </summary>
        public string[]? AuthAppIds { get; set; } = null;
        /// <summary>
        /// DRG分组器版本
        /// </summary>
        public string? DrgGroupVersion { get; set; } = null;
        /// <summary>
        /// 跨域配置
        /// </summary>
        public string? CorsWithOrigins { get; set; } = null;
        /// <summary>
        /// 文件管理
        /// </summary>
        public FileManageConfig? FileConfig { get; set; } = null;
        /// <summary>
        /// 远程会诊第三方服务接口，为空则默认初始值
        /// </summary>
        public RemoteTreatedConfig? RemoteTreated { get; set; } = null;
    }

    public class AppAPIHostConfig
    {
        /// <summary>
        /// Web资源
        /// </summary>
        public string? SiteStaticHost { get; set; } = null;
        /// <summary>
        /// 后台管理Web
        /// </summary>
        [DbTag(DBEnum.BaseDb)]
        public string? SiteBaseWebHost { get; set; } = null;
        /// <summary>
        /// 后台管理API
        /// </summary>      
        [DbTag(DBEnum.BaseDb)]
        public string? SiteBaseAPIHost { get; set; } = null;

        /// <summary>
        /// 结算系统Web
        /// </summary>
        [DbTag(DBEnum.SettDb)]
        public string? SiteSettWebHost { get; set; } = null;
        /// <summary>
        /// 结算系统API
        /// </summary>
        [DbTag(DBEnum.SettDb)]
        public string? SiteSettAPIHost { get; set; } = null;
        /// <summary>
        /// 医护协同Web
        /// </summary>
        [DbTag(DBEnum.CisDb)]
        public string? SiteCisWebHost { get; set; } = null;
        /// <summary>
        /// 医护协同API
        /// </summary>
        [DbTag(DBEnum.CisDb)]
        public string? SiteCisAPIHost { get; set; } = null;
        /// <summary>
        /// 药房药库Web
        /// </summary>
        [DbTag(DBEnum.PdsDb)]
        public string? SiteYfykWebHost { get; set; } = null;
        /// <summary>
        /// 药房药库API
        /// </summary>
        [DbTag(DBEnum.PdsDb)]
        public string? SiteYfykAPIHost { get; set; } = null;
        /// <summary>
        /// 电子病历Web
        /// </summary>
        [DbTag(DBEnum.EmrDb)]
        public string? SiteEmrWebHost { get; set; } = null;
        /// <summary>
        /// 电子病历API
        /// </summary>
        [DbTag(DBEnum.EmrDb)]
        public string? SiteEmrAPIHost { get; set; } = null;
        /// <summary>
        /// 病案系统Web
        /// </summary>
        [DbTag(DBEnum.MrmsDb)]
        public string? SiteMrmsWebHost { get; set; } = null;
        /// <summary>
        /// 病案系统API
        /// </summary>
        [DbTag(DBEnum.MrmsDb)]
        public string? SiteMrmsAPIHost { get; set; } = null;
        /// <summary>
        /// 手术系统Web
        /// </summary>
        [DbTag(DBEnum.OrDb)]
        public string? SiteOrWebHost { get; set; } = null;
        /// <summary>
        /// 手术系统API
        /// </summary>
        [DbTag(DBEnum.OrDb)]
        public string? SiteOrAPIHost { get; set; } = null;
        /// <summary>
        /// 物资耗材系统Web
        /// </summary>
        public string? SiteHerpWebHost { get; set; } = null;
        /// <summary>
        /// 物资耗材API
        /// </summary>
        public string? SiteHerpAPIHost { get; set; } = null;
        /// <summary>
        /// 质控系统Web
        /// </summary>
        [DbTag(DBEnum.MrQcDb)]
        public string? SiteMrqcWebHost { get; set; } = null;
        /// <summary>
        /// 质控系统API
        /// </summary>
        [DbTag(DBEnum.MrQcDb)]
        public string? SiteMrqcAPIHost { get; set; } = null;
        /// <summary>
        /// 统一平台
        /// </summary>
        [DbTag(DBEnum.UnionDb)]
        public string? HisSSOHost { get; set; } = null;
        /// <summary>
        /// 任务调度系统
        /// </summary>
        public string? ScheduleJobHost { get; set; } = null;
        /// <summary>
        /// 消息中心
        /// </summary>
        [DbTag(DBEnum.BaseDb)]
        public string? SiteNoticeCenterHost { get; set; } = null;
        /// <summary>
        /// 系统通用基础API（框架依赖）
        /// </summary>
        [DbTag(DBEnum.BaseDb)]
        public string? HisAppBaseAPIHost { get; set; } = null;
        /// <summary>
        /// 接口中心
        /// </summary>
        public string? SiteApiManageHost { get; set; } = null;
        /// <summary>
        /// 鉴权中心
        /// </summary>
        public string? SiteAuthCenterHost { get; set; } = null;
        /// <summary>
        /// 外部音视频接口地址
        /// </summary>
        public string? OuterMediaMeettingAPIHost { get; set; } = null;
        /// <summary>
        /// 云诊所
        /// </summary>
        public string? OuterClinicSystemAPIHost { get; set; } = null;
        /// <summary>
        /// 第三方PACS 接口
        /// </summary>
        public string? OuterPacsServiceHost { get; set; } = null;
        /// <summary>
        /// 第三方Lis 接口
        /// </summary>
        public string? OuterLisServiceHost { get; set; } = null;

    }
}
/// <summary>
/// 文件相关配置
/// </summary>
public class FileManageConfig
{
    /// <summary>
    /// 本地文件默认路径
    /// </summary>
    public string? LocalFileBaseDir { get; set; }
    /// <summary>
    /// 本地文件默认网络访问路径Host
    /// </summary>
    public string? LocalFileBaseNetUrl { get; set; }
    /// <summary>
    /// Drg上传目录
    /// </summary>
    public string? DrgDataFileDir { get; set; }
    /// <summary>
    /// Drg完整目录
    /// </summary>
    public string? DrgDataFileDirPath
    {
        get
        {
            return $"{LocalFileBaseDir}{DrgDataFileDir}";
        }
    }
    /// <summary>
    /// Drg目录（日期）
    /// </summary>
    public string? DrgDataFileDirPathWithDate
    {
        get
        {
            return $"{LocalFileBaseDir}{DrgDataFileDir}\\{DateTime.Now:yyyyMMdd}" + "\\";
        }
    }
}

public class RemoteTreatedConfig
{
    public string? SKey { get; set; }
    public string? Organization { get; set; }
}

public class AppAPIHostDic
{
    public string AppId { get; set; }
    public string Host { get; set; }
    public List<AppAPIMethods> Methods { get; set; }
}

public class AppAPIMethods
{
    public string Method { get; set; }
    public string Route { get; set; }
}

