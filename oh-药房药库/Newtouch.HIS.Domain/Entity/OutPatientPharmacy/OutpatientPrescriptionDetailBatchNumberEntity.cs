using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 门诊处方明细批号
    /// </summary>
    [Table("mz_cfmxph")]
    public class OutpatientPrescriptionDetailBatchNumberEntity : IEntity<OutpatientPrescriptionDetailBatchNumberEntity>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string cfmxphId { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string yp { get; set; }

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
        /// 有效期
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// 药品成组号
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// 门诊处方明细ID，mz_cfmx.Id
        /// </summary>
        public long? cfmxId { get; set; }

        /// <summary>
        /// 状态：1-有效 ；0-无效
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 创建者
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
        /// 最后修改人
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 归家状态：1-已归架；0-未归架
        /// </summary>
        public string gjzt { get; set; }

        /// <summary>
        /// 发药药房代码
        /// </summary>
        public string fyyf { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

    }
}
