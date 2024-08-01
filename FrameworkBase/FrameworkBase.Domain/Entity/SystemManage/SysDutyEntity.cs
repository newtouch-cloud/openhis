using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.Domain.Entity
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:08
    /// 描 述：系统岗位
    /// </summary>
    [Table("Sys_Duty")]
    public class SysDutyEntity : IEntity<SysDutyEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// Code
        /// </summary>
        /// <returns></returns>
        public string Code { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// px
        /// </summary>
        /// <returns></returns>
        public int? px { get; set; }
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
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
    }
}