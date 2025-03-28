using NewtouchHIS.Base.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.ValueObjects
{
    /// <summary>
    /// 消息组视图
    /// 声明与Entity一致
    /// </summary>
    public class MsgNoticeGroupVO: IEntity
    {
        [Required(ErrorMessage = "主键Id不可为空")]
        public string Id { get; set; }
        //[Required(ErrorMessage = "机构Id不可为空")]
        //public string OrganizeId { get; set; }
        /// <summary>
        /// 组标签
        /// 系统消息 / 院内消息 /科室内部消息  质控消息
        /// </summary>
        [Required(ErrorMessage = "组标签不可为空")]
        public string GroupTag { get; set; }
        /// <summary>
        /// 组名称
        /// </summary>
        [Required(ErrorMessage = "消息组名称不可为空")]
        public string GroupName { get; set; }
        /// <summary>
        /// 消息组说明
        /// </summary>
        public string? GroupDesc { get; set; }
        /// <summary>
        /// 相关业务类型 GroupYwlxEnum
        /// </summary>
        [Required(ErrorMessage = "相关业务类型不可为空")]
        public int GroupYwlx { get; set; }
        /// <summary>
        /// 决定了通知对象范围  MsgNoticeRangeEnum
        /// </summary>
        [Required(ErrorMessage = "通知对象范围不可为空")]
        public int NoticeRange { get; set; }
        /// <summary>
        /// 显示主题 
        /// 如 您有新的质控消息，请即时查看
        /// </summary>
        public string? DefaultTitle { get; set; }
        /// <summary>
        /// 详细内容
        /// 展开显示
        /// </summary>
        public string? DefaultContent { get; set; }
        /// <summary>
        /// 内容关联数据 Json格式 可转化对象
        /// </summary>
        public string? DefaultContentData { get; set; }
        /// <summary>
        /// 默认跳转路径 
        /// 不为空时要求 AppId 不可为空
        /// </summary>
        public string? DefaultOpenPath { get; set; }
        /// <summary>
        /// 路径参数
        /// 格式： zyh={0}，id={1}
        /// </summary>
        public string? DefaultOpenPathPara { get; set; }
        /// <summary>
        /// 应用Id
        /// </summary>
        public string? AppId { get; set; }
        //public string CreatorCode { get; set; }
        //public DateTime CreateTime { get; set; } = DateTime.Now;
        //public DateTime? LastModifyTime { get; set; }
        //public string? LastModifierCode { get; set; }
        //public string zt { get; set; } = "1";
    }
    /// <summary>
    /// 消息组基础信息 
    /// 查询/返回用
    /// </summary>
    public class MsgNoticeGroupBasicVO
    {
        public string? Id { get; set; }
        public string? OrganizeId { get; set; }
        /// <summary>
        /// 组标签
        /// 系统消息 / 院内消息 /科室内部消息  质控消息
        /// </summary>
        public string? GroupTag { get; set; }
        /// <summary>
        /// 组名称
        /// </summary>
        public string? GroupName { get; set; }
        /// <summary>
        /// 消息组说明
        /// </summary>
        public string? GroupDesc { get; set; }
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
        public string? DefaultTitle { get; set; }
        /// <summary>
        /// 详细内容
        /// 展开显示
        /// </summary>
        public string? DefaultContent { get; set; }
        /// <summary>
        /// 内容关联数据 Json格式 可转化对象
        /// </summary>
        public string? DefaultContentData { get; set; }
        /// <summary>
        /// 默认跳转路径 
        /// 不为空时要求 AppId 不可为空
        /// </summary>
        public string? DefaultOpenPath { get; set; }
        /// <summary>
        /// 路径参数
        /// 格式： zyh={0}，id={1}
        /// </summary>
        public string? DefaultOpenPathPara { get; set; }
        /// <summary>
        /// 应用Id
        /// </summary>
        public string? AppId { get; set; }
        public string? CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string? LastModifierCode { get; set; }
        public string? zt { get; set; }
    }
}
