using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 门诊处方药品操作记录
    /// </summary>
    [Table("mz_cfypczjl")]
    public class MzCfypczjlEntity : IEntity<MzCfypczjlEntity>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 门诊处方明细ID mz_cfmx表主键
        /// </summary>
        public long mzcfmxId { get; set; }

        /// <summary>
        /// 操作类型 1：发药 2：退药
        /// </summary>
        public string operateType { get; set; }

        /// <summary>
        /// 药品编号
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 操作数量  最小单位
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 修改用户
        /// </summary>
        public string LastModifierCode { get; set; }

    }
}
