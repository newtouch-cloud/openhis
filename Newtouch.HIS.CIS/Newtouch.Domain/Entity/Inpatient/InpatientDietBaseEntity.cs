using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{

    [Table("zy_DietBase")]
    public class InpatientDietBaseEntity : IEntity<InpatientDietBaseEntity>
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
        /// ChargeCode
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// ChargeName
        /// </summary>
        /// <returns></returns>
        public string DietType { get; set; }
        /// <summary>
        /// ChargeItem
        /// </summary>
        /// <returns></returns>
        public string DietGroup { get; set; }
        /// <summary>
        /// ChargeNum
        /// </summary>
        /// <returns></returns>
        public string py { get; set; }
        /// <summary>
        /// ParentId
        /// </summary>
        /// <returns></returns>
        public string ParentId { get; set; }
        /// <summary>
        /// ParentId
        /// </summary>
        /// <returns></returns>
        public Boolean bdsfxm { get; set; }
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

        public string DietBigType { get; set; }
    }
}