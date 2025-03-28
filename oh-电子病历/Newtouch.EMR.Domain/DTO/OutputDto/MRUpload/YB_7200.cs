using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.OutputDto.MRUpload
{
    /// <summary>
    /// 首次病程记录
    /// </summary>
    public class YB_7200
    {
        /// <summary>
        /// 就诊流水号
        /// 1
        /// </summary>
        [Required]
        public string AKC190 { get; set; }
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
        public string AKE021 { get; set; }
        /// <summary>
        /// 病床号
        /// 1
        /// </summary>
        [Required]
        public string BKB410 { get; set; }
        /// <summary>
        /// 记录日期时间 格式：yyyymmddhhmiss
        /// 1
        /// </summary>
        [Required]
        public string BKF737 { get; set; }
        /// <summary>
        /// 主诉
        /// 1
        /// </summary>
        [Required]
        public string BKF476 { get; set; }
        /// <summary>
        /// 病例特点
        /// 1
        /// </summary>
        [Required]
        public string BKF108 { get; set; }
        /// <summary>
        /// 中医“四诊”观察结果
        /// </summary>
        public string BKF458 { get; set; }
        /// <summary>
        /// 诊断依据
        /// </summary>
        public string BKF419 { get; set; }
        /// <summary>
        /// 初步诊断-西医诊断编码
        /// 诊断类别代码填写参考卫生部办公厅关于印发《疾病分类与代码（修订版）》的通知（卫办综发〔2011〕166 号）；仅上传主诊断即可
        /// </summary>
        public string BKF748 { get; set; }
        /// <summary>
        /// 初步诊断-西医诊断名称
        /// </summary>
        public string BKF126 { get; set; }
        /// <summary>
        /// 初步诊断-中医病名代码
        /// </summary>
        public string BKF128 { get; set; }
        /// <summary>
        /// 初步诊断-中医病名
        /// </summary>
        public string BKF127 { get; set; }
        /// <summary>
        /// 初步诊断-中医证候代码
        /// </summary>
        public string BKF130 { get; set; }
        /// <summary>
        /// 初步诊断-中医证候
        /// </summary>
        public string BKF129 { get; set; }
        /// <summary>
        /// 鉴别诊断-西医诊断编码
        /// </summary>
        public string BKF856 { get; set; }
        /// <summary>
        /// 鉴别诊断-西医诊断名称
        /// </summary>
        public string BKF857 { get; set; }
        /// <summary>
        /// 鉴别诊断-中医病名代码
        /// </summary>
        public string BKF855 { get; set; }
        /// <summary>
        /// 鉴别诊断-中医病名
        /// </summary>
        public string BKF854 { get; set; }
        /// <summary>
        /// 鉴别诊断-中医证候代码
        /// </summary>
        public string BKF204 { get; set; }
        /// <summary>
        /// 鉴别诊断-中医证候
        /// </summary>
        public string BKF203 { get; set; }
        /// <summary>
        /// 诊疗计划
        /// </summary>
        public string BKF421 { get; set; }
        /// <summary>
        /// 治则治法
        /// </summary>
        public string BKF440 { get; set; }
        /// <summary>
        /// 住院医师编号
        /// 1
        /// </summary>
        [Required]
        public string BKF693 { get; set; }
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
    }
}
