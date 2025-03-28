using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.OutputDto.MRUpload
{
    /// <summary>
    /// 出院小结信息
    /// </summary>
    public class YB_7500
    {
        /// <summary>
        /// 就诊流水号 
        /// 1
        /// 医疗机构内部记录患者本次就诊信息的唯一标识，与医保系统关联必须
        /// </summary>
        [Required]
        public string AKC190 { get; set; }
        /// <summary>
        /// 居民健康卡号
        /// </summary>
        public string BKF636 { get; set; }
        /// <summary>
        /// 住院流水号 
        /// 1
        /// </summary>
        [Required]
        public string BKC191 { get; set; }
        /// <summary>
        /// 科室代码
        /// </summary>
        public string AKF001 { get; set; }
        /// <summary>
        /// 科室名称
        /// 1
        /// </summary>
        [Required]
        public string AKF002 { get; set; }
        /// <summary>
        /// 病区名称
        /// 1
        /// </summary>
        [Required]
        public string AKE021 { get; set; }
        /// <summary>
        /// 病床号
        /// 1
        /// </summary>
        [Required]
        public string BKB410 { get; set; }
        /// <summary>
        /// 入院日期 格式：yyyymmddhhmiss
        /// 1
        /// </summary>
        [Required]
        public string BKC192 { get; set; }
        /// <summary>
        /// 出院日期 格式：yyyymmddhhmiss
        /// 1
        /// </summary>
        [Required]
        public string AKC194 { get; set; }
        /// <summary>
        /// 实际住院天数
        /// 1
        /// </summary>
        [Required]
        public string BKF668 { get; set; }
        /// <summary>
        /// 入院情况
        /// 1
        /// </summary>
        [Required]
        public string BKF264 { get; set; }
        /// <summary>
        /// 阳性辅助检查结果
        /// </summary>
        public string BKF378 { get; set; }
        /// <summary>
        /// 诊疗过程描述
        /// </summary>
        public string BKF420 { get; set; }
        /// <summary>
        /// 出院情况
        /// 1
        /// </summary>
        [Required]
        public string BKF758 { get; set; }
        /// <summary>
        /// 出院时症状与体征
        /// </summary>
        public string BKF139 { get; set; }
        /// <summary>
        /// 出院医嘱
        /// </summary>
        public string BKF759 { get; set; }
        /// <summary>
        /// 治疗结果代码
        /// </summary>
        public string BKF439 { get; set; }
        /// <summary>
        /// 住院医生编号
        /// </summary>
        public string BKF050 { get; set; }
        /// <summary>
        /// 住院医师姓名
        /// 1
        /// </summary>
        [Required]
        public string BKF692 { get; set; }
        /// <summary>
        /// 上级医师姓名
        /// 1
        /// </summary>
        [Required]
        public string BKF267 { get; set; }
        /// <summary>
        /// 签字日期时间(住院医师)
        /// </summary>
        public string BKF254 { get; set; }
        /// <summary>
        /// 中医四诊观察结果描述
        /// </summary>
        public string BKF473 { get; set; }
        /// <summary>
        /// 治则治法
        /// </summary>
        public string BKF440 { get; set; }
        /// <summary>
        /// 中药煎煮方法
        /// </summary>
        public string BKF451 { get; set; }
        /// <summary>
        /// 中药用药方法
        /// </summary>
        public string BKF457 { get; set; }
    }
}
