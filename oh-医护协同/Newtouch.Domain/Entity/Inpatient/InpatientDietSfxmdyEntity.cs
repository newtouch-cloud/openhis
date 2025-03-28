using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{

    [Table("zy_Dietsfxmdy")]
    public class InpatientDietSfxmdyEntity : IEntity<InpatientDietSfxmdyEntity>
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
        public string baseId { get; set; }
        /// <summary>
        /// ChargeCode
        /// </summary>
        /// <returns></returns>
        public string sfxmCode { get; set; }
        /// <summary>
        /// ChargeName
        /// </summary>
        /// <returns></returns>
        public string sfxmmc { get; set; }
        /// <summary>
        /// ChargeItem
        /// </summary>
        /// <returns></returns>
        public int sl { get; set; }
        /// <summary>
        /// ChargeUtity
        /// </summary>
        /// <returns></returns>
        public string dw { get; set; }
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