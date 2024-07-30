using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_cfmbmx")]
    public class PresTemplateDetailEntity : IEntity<PresTemplateDetailEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string mxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 关联模板表
        /// </summary>
        public string mbId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// 冗余字段
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 冗余代码
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 针对康复处方
        /// </summary>
        public int? mczll { get; set; }

        /// <summary>
        /// 针对药品
        /// </summary>
        public decimal? mcjl { get; set; }

        /// <summary>
        /// 针对药品
        /// </summary>
        public string mcjldw { get; set; }

        /// <summary>
        /// 针对药品
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pcCode { get; set; }

        /// <summary>
        /// 针对药品
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 针对康复处方
        /// </summary>
        public int? zl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

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
        public string zt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 执行科室（药品的领药部门 yfbmCode）
        /// </summary>
        public string zxks { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }
        
    }
}