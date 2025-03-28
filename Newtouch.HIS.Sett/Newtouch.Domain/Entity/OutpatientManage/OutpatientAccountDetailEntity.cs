using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 门诊记账计划明细
    /// </summary>
    [Table("mz_jzjhmx")]
    public class OutpatientAccountDetailEntity : IEntity<OutpatientAccountDetailEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string jzjhmxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jzjhId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int zlsc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? jzsj { get; set; }

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
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }

        public string ys { get; set; }
        public string ks { get; set; }
        public string ysmc { get; set; }
        public string ksmc { get; set; }
        /// <summary>
        /// 团体标志
        /// </summary>
        public bool? ttbz { get; set; }
        public string kflb { get; set; }

        /// <summary>
        /// 治疗量
        /// </summary>
        public int? zll { get; set; }

        /// <summary>
        /// 最后执行时间
        /// </summary>
        public DateTime? LastEexcutionTime { get; set; }

        /// <summary>
        /// 执行状态 见枚举EnumJzjhZXZT
        /// </summary>
        public int? zxzt { get; set; }
        public string yzxz { get; set; }
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 总次数
        /// </summary>
        public int? zcs { get; set; }

        /// <summary>
        /// 已执行次数
        /// </summary>
        public int? yzxcs { get; set; }

        /// <summary>
        /// 执行科室
        /// </summary>
        public string zxks { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

    }
}
