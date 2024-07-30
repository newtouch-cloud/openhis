using System.ComponentModel;

namespace NewtouchHIS.Lib.Base
{
    public class BaseEnum
    {
        /// <summary>
        /// 模块类型
        /// </summary>
        public enum ModuTypeEnum
        {
            [Description("应用级")]
            App = 0,
            [Description("业务模块")]
            Bus = 1,

        }
        /// <summary>
        /// 权限等级
        /// </summary>
        public enum AuthLevEnum
        {
            [Description("拒绝访问")]
            deny = 0,
            [Description("游客")]
            guest = 1001,
            [Description("受限用户")]
            limituser = 1002,
            [Description("普通用户")]
            user = 1003,
            [Description("VIP用户")]
            vip = 1004,
            [Description("信任用户")]
            trust = 1,
            [Description("管理用户")]
            admin = 9998,
            [Description("超级管理")]
            root = 9999,
        }
        /// <summary>
        /// 授权类型
        /// </summary>
        public enum AuthTypeEnum
        {
            [Description("Web")]
            Web = 1,
            [Description("WebAPI")]
            WebApi = 2
        }

        /// <summary>
        /// 消息状态
        /// </summary>
        [Description("消息状态")]
        public enum NoticeStuEnum
        {
            [Description("未发送")]
            UnSend = 0,
            [Description("已发送")]
            Send = 1,
            [Description("已读")]
            Read = 2,
            [Description("待处理")]
            Wait = 9
        }

        /// <summary>
        /// 消息适用范围限制
        /// </summary>
        [Description("消息组适用范围")]
        public enum MsgNoticeRangeEnum
        {
            [Description("无限制")]
            None = 0,
            [Description("全院")]
            Org = 1,
            [Description("指定用户")]
            User = 2,
            [Description("消息组内")]
            Group = 10,
            [Description("科室范围")]
            Dept = 20,
            [Description("科室内部")]
            DeptGroup = 21,
            [Description("病区范围")]
            Area = 30,
            [Description("病区内部")]
            AreaGroup = 31,
        }
        /// <summary>
        /// 队列执行类型
        /// </summary>
        [Description("队列执行类型")]
        public enum MsgQueueExecTypeEnum
        {
            [Description("即时任务")]
            Immediately = 1,
            [Description("延时任务")]
            Waitime = 2,
            [Description("定时任务")]
            Fixedtime = 3,
        }


        [Description("收信人类型")]
        public enum RecipientTypeEnum
        {
            [Description("用户")]
            user = 1,
            [Description("用户组")]
            usergroup = 2,
        }
        /// <summary>
        /// 消息组业务类型
        /// </summary>
        [Description("消息组业务类型")]
        public enum GroupYwlxEnum
        {
            [Description("通用")]
            None = 0,
            [Description("门诊")]
            Mz = 1,
            [Description("住院")]
            Zy = 2,
        }


        public enum ExpireType
        {
            /// <summary>
            /// 绝对过期
            /// 注：即自创建一段时间后就过期
            /// </summary>
            Absolute,

            /// <summary>
            /// 相对过期
            /// 注：即该键未被访问后一段时间后过期，若此键一直被访问则过期时间自动延长
            /// </summary>
            Relative,
        }

        public enum CacheType
        {
            /// <summary>
            /// 使用内存缓存(不支持分布式)
            /// </summary>
            Memory,

            /// <summary>
            /// 使用Redis缓存(支持分布式)
            /// </summary>
            Redis
        }
    }
}
 