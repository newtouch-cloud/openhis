using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("zy_jzjhmx")]
    public class HospAccountingPlanDetailEntity : IEntity<HospAccountingPlanDetailEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
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
        public string zyh { get; set; }

        /// <summary>
        /// 医嘱性质 1临时 2长期
        /// </summary>
        public string yzxz { get; set; }

        /// <summary>
        /// 医嘱类型 1药品 2治疗项目
        /// </summary>
        public string yzlx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 收费项目Code
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 项目单位时长
        /// </summary>
        public int? duration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 最后执行时间
        /// </summary>
        public DateTime? LastEexcutionTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int zxzt { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 医生
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 团体标志（是否团体治疗）
        /// </summary>
        public bool? ttbz { get; set; }

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
        public DateTime? PreStopDate { get; set; }

        /// <summary>
        /// 康复类别
        /// </summary>
        public string kflb { get; set; }

        /// <summary>
        /// 治疗量
        /// </summary>
        public int? zll { get; set; }
        /// <summary>
        /// 总次数（但住院长期意每天的最大次数，长期计划必须手动停止）
        /// </summary>
        public int? zcs { get; set; }

        /// <summary>
        /// 已执行次数
        /// </summary>
        public int? yzxcs { get; set; }
        public string yzId { get; set; }

        /// <summary>
        /// 执行科室
        /// </summary>
        public string zxks { get; set; }
    }
}
