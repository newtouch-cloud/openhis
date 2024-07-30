using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2021-03-01 15:24
    /// 描 述：病程记录
    /// </summary>
    [Table("YB_Inp_CourseDisease")]
    public class YBInpCourseDiseaseEntity : IEntity<YBInpCourseDiseaseEntity>
    {
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 就诊流水号
        /// </summary>
        /// <returns></returns>
        [Description("就诊流水号")]
        [Required]
        public string AKC190 { get; set; }

        /// <summary>
        /// 住院流水号
        /// </summary>
        /// <returns></returns>
        [Description("住院流水号")]
        [Required]
        public string BKC191 { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        /// <returns></returns>
        public string AKF001 { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        /// <returns></returns>
        [Description("科室名称")]
        [Required]
        public string AKF002 { get; set; }

        /// <summary>
        /// 病区名称
        /// </summary>
        /// <returns></returns>
        [Description("病区名称")]
        [Required]
        public string AKE021 { get; set; }

        /// <summary>
        /// 病床号
        /// </summary>
        /// <returns></returns>
        [Description("病床号")]
        [Required]
        public string BKB410 { get; set; }

        /// <summary>
        /// 记录日期时间 格式：yyyymmddhhmiss
        /// </summary>
        /// <returns></returns>
        [Description("记录日期时间")]
        [Required]
        public string BKF737 { get; set; }

        /// <summary>
        /// 主诉
        /// </summary>
        /// <returns></returns>
        [Description("主诉")]
        [Required]
        public string BKF476 { get; set; }

        /// <summary>
        /// 病例特点
        /// </summary>
        /// <returns></returns>
        [Description("病例特点")]
        [Required]
        public string BKF108 { get; set; }

        /// <summary>
        /// 中医“四诊”观察结果
        /// </summary>
        /// <returns></returns>
        public string BKF458 { get; set; }

        /// <summary>
        /// 诊断依据
        /// </summary>
        /// <returns></returns>
        public string BKF419 { get; set; }

        /// <summary>
        /// 初步诊断-西医诊断编码
        /// </summary>
        /// <returns></returns>
        public string BKF748 { get; set; }

        /// <summary>
        /// 初步诊断-西医诊断名称
        /// </summary>
        /// <returns></returns>
        public string BKF126 { get; set; }

        /// <summary>
        /// 初步诊断-中医病名代码
        /// </summary>
        /// <returns></returns>
        public string BKF128 { get; set; }

        /// <summary>
        /// 初步诊断-中医病名
        /// </summary>
        /// <returns></returns>
        public string BKF127 { get; set; }

        /// <summary>
        /// 初步诊断-中医证候代码
        /// </summary>
        /// <returns></returns>
        public string BKF130 { get; set; }

        /// <summary>
        /// 初步诊断-中医证候
        /// </summary>
        /// <returns></returns>
        public string BKF129 { get; set; }

        /// <summary>
        /// 鉴别诊断-西医诊断编码
        /// </summary>
        /// <returns></returns>
        public string BKF856 { get; set; }

        /// <summary>
        /// 鉴别诊断-西医诊断名称
        /// </summary>
        /// <returns></returns>
        public string BKF857 { get; set; }

        /// <summary>
        /// 鉴别诊断-中医病名代码
        /// </summary>
        /// <returns></returns>
        public string BKF855 { get; set; }

        /// <summary>
        /// 鉴别诊断-中医病名
        /// </summary>
        /// <returns></returns>
        public string BKF854 { get; set; }

        /// <summary>
        /// 鉴别诊断-中医证候代码
        /// </summary>
        /// <returns></returns>
        public string BKF204 { get; set; }

        /// <summary>
        /// 鉴别诊断-中医证候
        /// </summary>
        /// <returns></returns>
        public string BKF203 { get; set; }

        /// <summary>
        /// 诊疗计划
        /// </summary>
        /// <returns></returns>
        public string BKF421 { get; set; }

        /// <summary>
        /// 治则治法
        /// </summary>
        /// <returns></returns>
        public string BKF440 { get; set; }

        /// <summary>
        /// 住院医师编号
        /// </summary>
        /// <returns></returns>
        [Description("住院医师编号")]
        [Required]
        public string BKF693 { get; set; }

        /// <summary>
        /// 住院医师姓名
        /// </summary>
        /// <returns></returns>
        [Description("住院医师姓名")]
        [Required]
        public string BKF692 { get; set; }

        /// <summary>
        /// 上级医师姓名
        /// </summary>
        /// <returns></returns>
        [Description("上级医师姓名")]
        [Required]
        public string BKF267 { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

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