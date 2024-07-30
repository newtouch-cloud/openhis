using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 床位
    /// </summary>
    [Table("xt_cw")]
    public class SysWardBedEntity : IEntity<SysWardBedEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int cwId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cwCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cwmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// 0 非加床 1 加床
        /// </summary>
        public string jcbz { get; set; }

        /// <summary>
        /// from xt_sfxm
        /// </summary>
        public string cwf { get; set; }

        /// <summary>
        /// 医保四期项目库规定的床位费用from xt_sfxm
        /// </summary>
        public string djyyjsf { get; set; }

        /// <summary>
        /// from xt_sfxm
        /// </summary>
        public string bszlf { get; set; }

        /// <summary>
        /// 医保四期项目库规定的床位费用from xt_sfxm
        /// </summary>
        public string yymjkqjhf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 病房
        /// </summary>
        public string bfCode { get; set; }

        /// <summary>
        /// 床位类型 对应枚举EnumWardBedType
        /// </summary>
        public string cwlx { get; set; }

        /// <summary>
        /// 是否占用
        /// </summary>
        public bool? sfzy { get; set; }

        public string cwdj { get; set; }

    }
}
