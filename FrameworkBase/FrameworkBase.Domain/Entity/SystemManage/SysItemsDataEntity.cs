using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.Domain.Entity
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-20 13:04
    /// 描 述：字典项
    /// </summary>
    [Table("Sys_ItemsDetail")]
    public class SysItemsDataEntity : IEntity<SysItemsDataEntity>
    {
        /// <summary>
        /// 明细主键
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 主表主键
        /// </summary>
        /// <returns></returns>
        public string ItemId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        /// <returns></returns>
        public string Code { get; set; }
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
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? px { get; set; }
        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
    }
}