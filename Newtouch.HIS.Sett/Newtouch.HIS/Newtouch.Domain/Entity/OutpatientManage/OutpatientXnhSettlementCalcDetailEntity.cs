using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 新农合门诊结算计算明细
    /// </summary>
    [Table("mz_xnh_settcalcDetail")]
    public class OutpatientXnhSettlementCalcDetailEntity : IEntity<OutpatientXnhSettlementCalcDetailEntity>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// mz_xnh_settResult表主键
        /// </summary>
        public string resultId { get; set; }

        /// <summary>
        /// 计算名称
        /// </summary>
        public string calcName { get; set; }

        /// <summary>
        /// 计算说明
        /// </summary>
        public string calcMemo { get; set; }

        /// <summary>
        /// 本次计算
        /// </summary>
        public string calcBefore { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public string calcAfter { get; set; }

        /// <summary>
        /// 状态 1-有效  0-无效
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改者
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}