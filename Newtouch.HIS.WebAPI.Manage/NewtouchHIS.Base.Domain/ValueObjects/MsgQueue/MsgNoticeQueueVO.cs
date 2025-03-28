using NewtouchHIS.Base.Domain.Entity;
using System.ComponentModel.DataAnnotations;
using static NewtouchHIS.Lib.Base.BaseEnum;

namespace NewtouchHIS.Base.Domain.ValueObjects
{
    public class MsgNoticeQueueVO : IEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 消息Id 本次唯一
        /// 便于收件人太多分次发送
        /// </summary>
        [Required(ErrorMessage = "消息Id 不可为空")]
        public string NoticeId { get; set; }
        /// <summary>
        /// 发信人
        /// </summary>
        [Required(ErrorMessage = "发信人不可为空")]
        public string SendFrom { get; set; }
        /// <summary>
        /// 消息组编号
        /// </summary>
        [Required(ErrorMessage = "消息组编号不可为空")]
        public string NoticeGroupId { get; set; }
        /// <summary>
        /// 相关业务类型 GroupYwlxEnum
        /// </summary>
        [Required(ErrorMessage = "相关业务类型不可为空")]
        public int GroupYwlx { get; set; }
        /// <summary>
        /// 决定了通知对象范围  MsgNoticeRangeEnum
        /// </summary>
        [Required(ErrorMessage = "消息适用范围限制不可为空")]
        public int NoticeRange { get; set; }
        /// <summary>
        /// 显示主题 
        /// 如 您有新的质控消息，请即时查看
        /// </summary>
        [Required(ErrorMessage = "消息主题不可为空")]
        public string? Title { get; set; }
        /// <summary>
        /// 详细内容
        /// 展开显示
        /// </summary>
        public string? Content { get; set; }
        /// <summary>
        /// 内容关联数据 Json格式 可转化对象
        /// </summary>
        public string? ContentData { get; set; }
        /// <summary>
        /// 默认跳转路径 
        /// 不为空时要求 AppId 不可为空
        /// </summary>
        public string? OpenPath { get; set; }
        /// <summary>
        /// 路径参数
        /// 格式： zyh={0}，id={1}
        /// </summary>
        public string? OpenPathPara { get; set; }
        /// <summary>
        /// 收信人 默认人员工号，当收件人类型为用户组时，字段为空
        /// 多人则 , 分割，长度大于1000 时，分次发送
        /// </summary>
        [Required(ErrorMessage = "收信人不可为空")]
        public string Recipient { get; set; }
        /// <summary>
        /// 收件人类型 用户 / 用户组
        /// </summary>
        [Required(ErrorMessage = "消息适用范围限制不可为空")]
        public int RecipientType { get; set; }
        /// <summary>
        /// 队列执行类型 默认立即执行
        /// MsgQueueExecType
        /// </summary>
        [Required(ErrorMessage = "队列执行类型不可为空")]
        public int QueueExecType { get; set; } = (int)MsgQueueExecTypeEnum.Immediately;
        /// <summary>
        /// 执行表达式
        /// </summary>
        public string? ExecCron { get; set; }
        /// <summary>
        /// 消息状态 
        /// NoticeStuEnum
        /// </summary>
        public int NoticeStu { get; set; } = (int)NoticeStuEnum.UnSend;
        public int? px { get; set; }
        /// <summary>
        /// 应用Id
        /// </summary>
        public string? AppId { get; set; }

    }

    public class MsgNoticeQueueBasicVO
    {
        public string? OrganizeId { get; set; }
        /// <summary>
        /// 消息Id 本次唯一
        /// 便于收件人太多分次发送
        /// </summary>
        public string? NoticeId { get; set; }
        /// <summary>
        /// 发信人
        /// </summary>
        public string? SendFrom { get; set; }
        /// <summary>
        /// 消息组编号
        /// </summary>
        public string? NoticeGroupId { get; set; }
        /// <summary>
        /// 相关业务类型 GroupYwlxEnum
        /// </summary>
        public int? GroupYwlx { get; set; }
        /// <summary>
        /// 决定了通知对象范围  MsgNoticeRangeEnum
        /// </summary>
        public int? NoticeRange { get; set; }
        /// <summary>
        /// 显示主题 
        /// 如 您有新的质控消息，请即时查看
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// 详细内容
        /// 展开显示
        /// </summary>
        public string? Content { get; set; }
        /// <summary>
        /// 内容关联数据 Json格式 可转化对象
        /// </summary>
        public string? ContentData { get; set; }
        /// <summary>
        /// 默认跳转路径 
        /// 不为空时要求 AppId 不可为空
        /// </summary>
        public string? OpenPath { get; set; }
        /// <summary>
        /// 路径参数
        /// 格式： zyh={0}，id={1}
        /// </summary>
        public string? OpenPathPara { get; set; }
        /// <summary>
        /// 收信人 默认人员工号，当收件人类型为用户组时，字段为空
        /// 多人则 , 分割，长度大于1000 时，分次发送
        /// </summary>
        public string? Recipient { get; set; }
        /// <summary>
        /// 收件人类型 用户 / 用户组
        /// </summary>
        public int? RecipientType { get; set; }
        /// <summary>
        /// 队列执行类型 默认立即执行
        /// MsgQueueExecType
        /// </summary>
        public int? QueueExecType { get; set; }
        /// <summary>
        /// 执行表达式
        /// </summary>
        public string? ExecCron { get; set; }
        /// <summary>
        /// 消息状态 
        /// NoticeStuEnum
        /// </summary>
        public int? NoticeStu { get; set; }
        public int? px { get; set; }
        /// <summary>
        /// 应用Id
        /// </summary>
        public string? AppId { get; set; }
        public DateTime? CreateTime { get; set; }
        public string? CreatorCode { get; set; }

        public DateTime? LastModifyTime { get; set; }

        public string? LastModifierCode { get; set; }
        public string? zt { get; set; } = "1";
    }
    /// <summary>
    /// 消息通知查询
    /// </summary>
    public class MsgNoticeQueueQueryVO
    {
        public int? NoticeStu { get; set; }
        public DateTime? ksrq { get; set; }
        public DateTime? jsrq { get; set; }
        public string? OrganizeId { get; set; }
        public string? SendFrom { get; set; }
        public string? Recipient { get; set; }
        public string? keyword { get; set; }
        public int? msgtype { get; set; } = 1;
    }
    public class MsgNoticeQueueQueryRspoVO : MsgNoticeQueueEntity
    {
        public string GroupTag { get; set; }
        public string GroupName { get; set; }
        public string GroupDesc { get; set; }
        public string? SendFromName { get; set; }
        public string? RecipientName { get; set; }
        public string? AppName { get; set; }
    }
    public class MsgNoticeQueueModelVO<T> : MsgNoticeQueueQueryRspoVO
    {
        

        public T? NoticeData { get; set; }
        /// <summary>
        /// 消息提醒展示
        /// </summary>
        public string? NoticeShow { get; set; }
        /// <summary>
        /// 关键关系人编号 
        /// 门诊：门诊号；住院：住院号
        /// </summary>
        public string? NoticePrimaryPartyCode { get; set; }
        /// <summary>
        /// 关键关系人
        /// </summary>
        public string? NoticePrimaryParty { get; set; }
    }


    #region old
    public class MsgQueueVO
    {
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string AppId { get; set; }
        public int msgtypecode { get; set; }
        public string msgcontent { get; set; }
        public string msgformat { get; set; }
        public int? msgstu { get; set; }
        public string patno { get; set; }
        public string ywlsh { get; set; }
        public string ks { get; set; }
        public string bq { get; set; }
        public string czys { get; set; }
        public string dutylimit { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string CreatorCode { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
    }
    #endregion
}
