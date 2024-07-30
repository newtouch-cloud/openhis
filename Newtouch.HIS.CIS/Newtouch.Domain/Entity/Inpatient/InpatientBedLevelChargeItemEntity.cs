using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 创 建：黄杉杉
    /// 日 期：2018-12-12 15:37
    /// 描 述：住院床位费用对应表
    /// </summary>
    [Table("zy_LevelChargedy")]
    public class InpatientBedLevelChargeItemEntity : IEntity<InpatientBedLevelChargeItemEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// BedCode
        /// </summary>
        /// <returns></returns>
        public string LevelCode { get; set; }

        public string sfxmCode { get; set; }
        /// <summary>
        /// ChargeNum
        /// </summary>
        /// <returns></returns>
        public int sl { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// 0无效1有效
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
    }
}