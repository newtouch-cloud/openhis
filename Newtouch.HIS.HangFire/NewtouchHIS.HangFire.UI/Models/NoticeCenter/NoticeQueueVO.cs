using System.ComponentModel.DataAnnotations;
using static NewtouchHIS.Lib.Base.BaseEnum;

namespace NewtouchHIS.HangFire.UI.Models
{
    public class MsgNoticeQueueBasicVO
    {
        public string? Id { get; set; } 

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
        public string Recipient { get; set; }
        /// <summary>
        /// 收件人类型 用户 / 用户组
        /// </summary>
        public int? RecipientType { get; set; }
        /// <summary>
        /// 队列执行类型 默认立即执行
        /// MsgQueueExecType
        /// </summary>
        public int? QueueExecType { get; set; } = (int)MsgQueueExecTypeEnum.Immediately;
        /// <summary>
        /// 执行表达式
        /// </summary>
        public string? ExecCron { get; set; }
        /// <summary>
        /// 消息状态 
        /// NoticeStuEnum
        /// </summary>
        public int? NoticeStu { get; set; } = (int)NoticeStuEnum.UnSend;
        public int? px { get; set; }
        /// <summary>
        /// 应用Id
        /// </summary>
        public string? AppId { get; set; }
        public string? OrganizeId { get; set; }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? CreatorCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? LastModifierCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? zt { get; set; }
    }
}
