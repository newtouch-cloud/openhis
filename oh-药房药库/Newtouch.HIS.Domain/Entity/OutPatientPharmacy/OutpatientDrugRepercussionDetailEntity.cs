using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// mz_tfmx 门诊退药明细
    /// </summary>
    [Table("mz_tfmx")]
    public class OutpatientDrugRepercussionDetailEntity : IEntity<OutpatientDrugRepercussionDetailEntity>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long tfmxId { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 最小单位数量
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 退药人员
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 状态 0-无效 1-有效
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建人员
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改人员
        /// </summary>
        public string LastModiFierCode { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}
