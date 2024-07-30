using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 财务收据
    /// </summary>
    [Table("cw_sj")]
    public class FinanceReceiptEntity : IEntity<FinanceReceiptEntity>
    {
        /// <summary>
        /// 财务收据ID
        /// </summary>
        [Key]
        public string cwsjId { get; set; }

        /// <summary>
        /// 人员
        /// </summary>
        public string ry { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 首字母
        /// </summary>
        public string szm { get; set; }

        /// <summary>
        /// 起始收据号
        /// </summary>
        public long qssjh { get; set; }

        /// <summary>
        /// 当前收据号
        /// </summary>
        public long dqsjh { get; set; }

        /// <summary>
        /// 结束收据号
        /// </summary>
        public long? jssjh { get; set; }

        /// <summary>
        /// 创建用户ID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户ID
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
