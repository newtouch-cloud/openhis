using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 目录对照主表
    /// </summary>
    [Table("TTCataloguesComparisonMain")]
    public class TTCataloguesComparisonMainEntity : IEntity<TTCataloguesComparisonMainEntity>
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
        /// 本地编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 第三方编码
        /// </summary>
        public string TTCode { get; set; }
        /// <summary>
        /// 第三方标示 如“yibao”
        /// </summary>
        public string TTMark { get; set; }
        /// <summary>
        /// 说明/备注
        /// </summary>
        public string Description { get; set; }
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
