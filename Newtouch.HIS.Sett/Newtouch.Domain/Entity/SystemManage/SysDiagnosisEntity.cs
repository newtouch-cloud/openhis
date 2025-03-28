using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统诊断
    /// </summary>
    [Table("xt_zd")]
    [Obsolete("please use the view")]
    public class SysDiagnosisEntity : IEntity<SysDiagnosisEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int zdbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short zdnm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string icd10 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string icd10fjm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zdmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 每次使用时＋1，可以使得常用的前置
        /// </summary>
        public int orderid { get; set; }

        /// <summary>
        /// 0 非常用   1 常用（显示时在排序号的基础上再加1000）
        /// </summary>
        public string cybz { get; set; }

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

    }
}
