using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("mz_cyyp")]
    public class CommonDrugEntity : IEntity<CommonDrugEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string cyypId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 枚举 1 科室常用 2 个人常用
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 关联base库的科室表
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// 关联base库的人员表
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// 关联base库的系统药品表
        /// </summary>
        public string zdCode { get; set; }

        /// <summary>
        /// 冗余字段
        /// </summary>
        public string zdmc { get; set; }

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

    }
}
