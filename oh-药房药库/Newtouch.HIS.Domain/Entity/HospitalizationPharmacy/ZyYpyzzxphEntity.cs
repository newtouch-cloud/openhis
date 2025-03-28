using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 住院药品医嘱执行批号
    /// </summary>
    [Table("zy_ypyzzxph")]
    public class ZyYpyzzxphEntity : IEntity<ZyYpyzzxphEntity>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 医嘱ID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// 医嘱执行ID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 最小单位数量
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// 发药药房代码
        /// </summary>
        public string fyyf { get; set; }

        /// <summary>
        /// 归架状态 0-未归架  1-已归架
        /// </summary>
        public string gjzt { get; set; }

        /// <summary>
        /// 组号
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// 住院药品信息ID，zy_ypyzxx表主键
        /// </summary>
        public long? zyypxxId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 状态  0-无效  1-有效
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人员
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改人员
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
