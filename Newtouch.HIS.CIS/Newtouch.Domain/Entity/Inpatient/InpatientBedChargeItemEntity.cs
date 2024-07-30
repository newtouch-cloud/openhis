using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2018-06-28 15:37
    /// 描 述：住院床位费用绑定
    /// </summary>
    [Table("zy_cwbdfy")]
    public class InpatientBedChargeItemEntity : IEntity<InpatientBedChargeItemEntity>
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
        public string BedCode { get; set; }
        /// <summary>
        /// ChargeCode
        /// </summary>
        /// <returns></returns>
        public string ChargeCode { get; set; }
        /// <summary>
        /// ChargeName
        /// </summary>
        /// <returns></returns>
        public string ChargeName { get; set; }
        /// <summary>
        /// ChargeItem
        /// </summary>
        /// <returns></returns>
        public string ChargeItem { get; set; }
        /// <summary>
        /// ChargeUtity
        /// </summary>
        /// <returns></returns>
        public string ChargeUtity { get; set; }
        /// <summary>
        /// ChargeNum
        /// </summary>
        /// <returns></returns>
        public int ChargeNum { get; set; }
        /// <summary>
        /// Memo
        /// </summary>
        /// <returns></returns>
        public string Memo { get; set; }
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