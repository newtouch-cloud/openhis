using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 科室
    /// </summary>
    [Table("xt_ks")]
    [Obsolete("please use the view")]
    public class SysDepartmentEntity : IEntity<SysDepartmentEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int ksbh { get; set; }

        /// <summary>
        /// 门诊住院科室以数字组成，其他行政等科室以字母开头
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 0 通用 1 仅门诊 2 仅住院 3 门诊住院皆不可用（行政科室）
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 0 非医技科室 1 医技科室 
        /// </summary>
        public string yjbz { get; set; }

        /// <summary>
        /// 0 不可用 1 可用
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 0.未控制 1.控制
        /// </summary>
        public byte? xbkz { get; set; }

        /// <summary>
        /// 0.未设定 1.儿科
        /// </summary>
        public byte? tsksdy { get; set; }

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

    }
}
