using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 目录对照子表
    /// </summary>
    [Table("TTCataloguesComparisonDetail")]
    public class TTCataloguesComparisonDetailEntity : IEntity<TTCataloguesComparisonDetailEntity>
    {
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 组织机构Id
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 关键主记录Id
        /// </summary>
        public string MainId { get; set; }
        /// <summary>
        /// 本地编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 本地名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 第三方编码
        /// </summary>
        public string TTCode { get; set; }
        /// <summary>
        /// 第三方名称
        /// </summary>
        public string TTName { get; set; }
        /// <summary>
        /// 第三方说明
        /// </summary>
        public string TTExplain { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? px { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建用户
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
