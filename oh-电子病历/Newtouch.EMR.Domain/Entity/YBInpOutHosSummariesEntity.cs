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
    /// 描 述：出院小结
    /// </summary>
    [Table("YB_Inp_OutHosSummaries")]
    public class YBInpOutHosSummariesEntity : IEntity<YBInpOutHosSummariesEntity>
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
        /// BKF636
        /// </summary>
        /// <returns></returns>
        public string BKF636 { get; set; }

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
        /// 入院日期时间格式：yyymmddhhmiss
        /// </summary>
        /// <returns></returns>
        [Description("入院日期时间")]
        [Required]
        public string BKC192 { get; set; }

        /// <summary>
        /// 出院日期时间格式：yyymmddhhmiss
        /// </summary>
        /// <returns></returns>
        [Description("出院日期时间")]
        [Required]
        public string AKC194 { get; set; }

        /// <summary>
        /// 实际住院天数
        /// </summary>
        /// <returns></returns>
        [Description("实际住院天数")]
        [Required]
        public string BKF668 { get; set; }

        /// <summary>
        /// 入院情况
        /// </summary>
        /// <returns></returns>
        [Description("入院情况")]
        [Required]
        public string BKF264 { get; set; }

        /// <summary>
        /// 阳性辅助检查结果
        /// </summary>
        /// <returns></returns>
        public string BKF378 { get; set; }

        /// <summary>
        /// 诊疗过程描述
        /// </summary>
        /// <returns></returns>
        public string BKF420 { get; set; }

        /// <summary>
        /// 出院情况
        /// </summary>
        /// <returns></returns>
        [Description("出院情况")]
        [Required]
        public string BKF758 { get; set; }

        /// <summary>
        /// 出院时症状与体征
        /// </summary>
        /// <returns></returns>
        public string BKF139 { get; set; }

        /// <summary>
        /// 出院医嘱
        /// </summary>
        /// <returns></returns>
        public string BKF759 { get; set; }

        /// <summary>
        /// 治疗结果代码
        /// </summary>
        /// <returns></returns>
        public string BKF439 { get; set; }

        /// <summary>
        /// 住院医生编号
        /// </summary>
        /// <returns></returns>
        public string BKF050 { get; set; }

        /// <summary>
        /// 住院医生姓名
        /// </summary>
        /// <returns></returns>
        [Description("住院医生姓名")]
        [Required]
        public string BKF692 { get; set; }

        /// <summary>
        /// 上级医生姓名
        /// </summary>
        /// <returns></returns>
        [Description("上级医生姓名")]
        [Required]
        public string BKF267 { get; set; }

        /// <summary>
        /// 签字日期时间 ( 住院医师)格式：yyyymmddhhmiss
        /// </summary>
        /// <returns></returns>
        public string BKF254 { get; set; }

        /// <summary>
        /// 中医四诊观察结果描述
        /// </summary>
        /// <returns></returns>
        public string BKF473 { get; set; }

        /// <summary>
        /// 治则治法
        /// </summary>
        /// <returns></returns>
        public string BKF440 { get; set; }

        /// <summary>
        /// 中药煎煮方法
        /// </summary>
        /// <returns></returns>
        public string BKF451 { get; set; }

        /// <summary>
        /// 中药用药方法
        /// </summary>
        /// <returns></returns>
        public string BKF457 { get; set; }
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