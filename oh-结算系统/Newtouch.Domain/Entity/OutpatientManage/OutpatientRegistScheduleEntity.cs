using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 门诊挂号排班
    /// </summary>
    [Table("mz_ghpb")]
    public class OutpatientRegistScheduleEntity : IEntity<OutpatientRegistScheduleEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ghpbId { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 0 所有医生
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 0无 1全天 2上午 3下午
        /// </summary>
        public string zyi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zsan { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zsi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zwu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zlv { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zri { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 总号量
        /// </summary>
        public int? zhl { get; set; }

        /// <summary>
        /// 挂号专病
        /// </summary>
        public string ghzb { get; set; }

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
        /// 挂号项目
        /// </summary>
        public string ghlx { get; set; }

        /// <summary>
        /// 诊疗项目
        /// </summary>
        public string zlxm { get; set; }

        /// <summary>
        /// 门急诊标志 门诊 急诊 专家
        /// </summary>
        public string mjzbz { get; set; }

    }
}
