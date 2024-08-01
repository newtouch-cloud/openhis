using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.Domain.Entity
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:13
    /// 描 述：系统菜单
    /// </summary>
    [Table("Sys_Module")]
    public class SysModuleEntity : IEntity<SysModuleEntity>
    {
        /// <summary>
        /// 模块主键
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        /// <returns></returns>
        public string ParentId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// EnName
        /// </summary>
        /// <returns></returns>
        public string EnName { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        /// <returns></returns>
        public string Code { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        /// <returns></returns>
        public string Icon { get; set; }
        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public string UrlAddress { get; set; }
        /// <summary>
        /// 目标
        /// </summary>
        /// <returns></returns>
        public string Target { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? px { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 最后修改用户
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
    }
}