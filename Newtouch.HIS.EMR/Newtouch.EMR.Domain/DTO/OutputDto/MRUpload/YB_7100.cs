using System.ComponentModel.DataAnnotations;

namespace Newtouch.EMR.Domain.DTO.OutputDto.MRUpload
{
    public class YB_7100
    {
        /// <summary>
        /// 就诊流水号
        /// </summary>
        /// <returns></returns>
        [Required]
        public string AKC190 { get; set; }

        /// <summary>
        /// 入院记录流水号
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKF263 { get; set; }

        /// <summary>
        /// 住院流水号
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKC191 { get; set; }

        /// <summary>
        /// 病区名称
        /// </summary>
        /// <returns></returns>
        [Required]
        public string AKE021 { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        /// <returns></returns>
        public string AKF001 { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        /// <returns></returns>
        [Required]
        public string AKF002 { get; set; }

        /// <summary>
        /// 病床号
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKB410 { get; set; }

        /// <summary>
        /// 入院日期时间
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKC192 { get; set; }

        /// <summary>
        /// 病史陈述者姓名
        /// </summary>
        /// <returns></returns>
        public string BKF110 { get; set; }

        /// <summary>
        /// 陈述者与患者关系代码
        /// </summary>
        /// <returns></returns>
        public string BKF654 { get; set; }

        /// <summary>
        /// 陈述内容是否可靠标识
        /// </summary>
        /// <returns></returns>
        public string BKF125 { get; set; }

        /// <summary>
        /// 主诉
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKF476 { get; set; }

        /// <summary>
        /// 现病史
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKF360 { get; set; }

        /// <summary>
        /// 一般健康状况标志
        /// </summary>
        /// <returns></returns>
        public string BKF385 { get; set; }

        /// <summary>
        /// 疾病史（含外伤）
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKF740 { get; set; }

        /// <summary>
        /// 患者传染性标志
        /// </summary>
        /// <returns></returns>
        public string BKF185 { get; set; }

        /// <summary>
        /// 传染病史
        /// </summary>
        /// <returns></returns>
        public string BKF150 { get; set; }

        /// <summary>
        /// 预防接种史
        /// </summary>
        /// <returns></returns>
        public string BKF409 { get; set; }

        /// <summary>
        /// 手术史
        /// </summary>
        /// <returns></returns>
        public string BKF285 { get; set; }

        /// <summary>
        /// 输血史
        /// </summary>
        /// <returns></returns>
        public string BKF299 { get; set; }

        /// <summary>
        /// 过敏史
        /// </summary>
        /// <returns></returns>
        public string BKF678 { get; set; }

        /// <summary>
        /// 个人史
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKF742 { get; set; }

        /// <summary>
        /// 婚育史
        /// </summary>
        /// <returns></returns>
        public string BKF189 { get; set; }

        /// <summary>
        /// 月经史
        /// </summary>
        /// <returns></returns>
        public string BKF410 { get; set; }

        /// <summary>
        /// 家族史
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKF744 { get; set; }

        /// <summary>
        /// 体格检查--体温（℃）
        /// </summary>
        /// <returns></returns>
        public string BKF352 { get; set; }

        /// <summary>
        /// 体格检查--脉率（次/mi数字）
        /// </summary>
        /// <returns></returns>
        public string BKF339 { get; set; }

        /// <summary>
        /// 体格检查--呼吸频率
        /// </summary>
        /// <returns></returns>
        public string BKF350 { get; set; }

        /// <summary>
        /// 体格检查--收缩压（mmHg）
        /// </summary>
        /// <returns></returns>
        public string BKF343 { get; set; }

        /// <summary>
        /// 体格检查--舒张压（mmHg）
        /// </summary>
        /// <returns></returns>
        public string BKF344 { get; set; }

        /// <summary>
        /// 体格检查--身高（cm）
        /// </summary>
        /// <returns></returns>
        public string BKF351 { get; set; }

        /// <summary>
        /// 体格检查--体重（kg）
        /// </summary>
        /// <returns></returns>
        public string BKF353 { get; set; }

        /// <summary>
        /// 体格检查--一般状况检查结果
        /// </summary>
        /// <returns></returns>
        public string BKF349 { get; set; }

        /// <summary>
        /// 体格检查--皮肤和粘膜检查结果
        /// </summary>
        /// <returns></returns>
        public string BKF340 { get; set; }

        /// <summary>
        /// 体格检查--全身浅表淋巴结检查结果
        /// </summary>
        /// <returns></returns>
        public string BKF341 { get; set; }

        /// <summary>
        /// 体格检查--头部及其器官检查结果
        /// </summary>
        /// <returns></returns>
        public string BKF346 { get; set; }

        /// <summary>
        /// 体格检查--颈部检查结果
        /// </summary>
        /// <returns></returns>
        public string BKF338 { get; set; }

        /// <summary>
        /// 体格检查--胸部检查结果
        /// </summary>
        /// <returns></returns>
        public string BKF348 { get; set; }

        /// <summary>
        /// 体格检查--腹部检查结果
        /// </summary>
        /// <returns></returns>
        public string BKF335 { get; set; }

        /// <summary>
        /// 体格检查--肛门指诊检查结果描述
        /// </summary>
        /// <returns></returns>
        public string BKF336 { get; set; }

        /// <summary>
        /// 体格检查--外生殖器检查结果
        /// </summary>
        /// <returns></returns>
        public string BKF347 { get; set; }

        /// <summary>
        /// 体格检查--脊柱检查结果
        /// </summary>
        /// <returns></returns>
        public string BKF337 { get; set; }

        /// <summary>
        /// 体格检查--四肢检查结果
        /// </summary>
        /// <returns></returns>
        public string BKF345 { get; set; }

        /// <summary>
        /// 体格检查--神经系统检查结果
        /// </summary>
        /// <returns></returns>
        public string BKF342 { get; set; }

        /// <summary>
        /// 专科情况
        /// </summary>
        /// <returns></returns>
        public string BKF547 { get; set; }

        /// <summary>
        /// 辅助检查结果
        /// </summary>
        /// <returns></returns>
        public string BKF746 { get; set; }

        /// <summary>
        /// 中医“四诊”观察结果描述
        /// </summary>
        /// <returns></returns>
        public string BKF458 { get; set; }

        /// <summary>
        /// 辨证分型代码
        /// </summary>
        /// <returns></returns>
        public string BKF097 { get; set; }

        /// <summary>
        /// 辩证分型名称
        /// </summary>
        /// <returns></returns>
        public string BKF099 { get; set; }

        /// <summary>
        /// 治则治法
        /// </summary>
        /// <returns></returns>
        public string BKF440 { get; set; }

        /// <summary>
        /// 接诊医生编号
        /// </summary>
        /// <returns></returns>
        public string BKF207 { get; set; }

        /// <summary>
        /// 接诊医生姓名
        /// </summary>
        /// <returns></returns>
        public string BKF208 { get; set; }

        /// <summary>
        /// 住院医师编号
        /// </summary>
        /// <returns></returns>
        public string BKF693 { get; set; }

        /// <summary>
        /// 住院医师姓名
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKF692 { get; set; }

        /// <summary>
        /// 主任医师编号
        /// </summary>
        /// <returns></returns>
        public string BKF695 { get; set; }

        /// <summary>
        /// 主任医师姓名
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKF696 { get; set; }

        /// <summary>
        /// 主治医师编号
        /// </summary>
        /// <returns></returns>
        public string BKF050 { get; set; }

        /// <summary>
        /// 主治医师姓名
        /// </summary>
        /// <returns></returns>
        [Required]
        public string AKC273 { get; set; }

        /// <summary>
        /// 主要症状
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKE013 { get; set; }

        /// <summary>
        /// 入院原因
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKF266 { get; set; }

        /// <summary>
        /// 入院途径
        /// </summary>
        /// <returns></returns>
        [Required]
        public string BKF818 { get; set; }

        /// <summary>
        /// (Apgar)评分值
        /// </summary>
        /// <returns></returns>
        public string BKF095 { get; set; }

        /// <summary>
        /// 饮食情况
        /// </summary>
        /// <returns></returns>
        public string BKF403 { get; set; }

        /// <summary>
        /// 发育程度
        /// </summary>
        /// <returns></returns>
        public string BKF158 { get; set; }

        /// <summary>
        /// 精神状态正常标志
        /// </summary>
        /// <returns></returns>
        public string BKF212 { get; set; }

        /// <summary>
        /// 睡眠状况
        /// </summary>
        /// <returns></returns>
        public string BKF323 { get; set; }

        /// <summary>
        /// 特殊情况
        /// </summary>
        /// <returns></returns>
        public string BKF334 { get; set; }

        /// <summary>
        /// 心理状态
        /// </summary>
        /// <returns></returns>
        public string BKF369 { get; set; }

        /// <summary>
        /// 营养状态
        /// </summary>
        /// <returns></returns>
        public string BKF407 { get; set; }

        /// <summary>
        /// 自理能力
        /// </summary>
        /// <returns></returns>
        public string BKF560 { get; set; }

        /// <summary>
        /// 护理观察项目名称
        /// </summary>
        /// <returns></returns>
        public string BKF177 { get; set; }

        /// <summary>
        /// 护理观察结果
        /// </summary>
        /// <returns></returns>
        public string BKF176 { get; set; }

        /// <summary>
        /// 吸烟标志
        /// </summary>
        /// <returns></returns>
        public string BKF358 { get; set; }

        /// <summary>
        /// 停止吸烟天数
        /// </summary>
        /// <returns></returns>
        public string BKF354 { get; set; }
        /// <summary>
        /// 吸烟状况
        /// </summary>
        /// <returns></returns>
        public string BKF359 { get; set; }
        /// <summary>
        /// 日吸烟量（支）
        /// </summary>
        /// <returns></returns>
        public string BKF258 { get; set; }
        /// <summary>
        /// 饮酒标志 0 否；1 是
        /// </summary>
        /// <returns></returns>
        public string BKF401 { get; set; }
        /// <summary>
        /// 饮酒频率
        /// </summary>
        /// <returns></returns>
        public string BKF402 { get; set; }
        /// <summary>
        /// 日饮酒量（mL）
        /// </summary>
        /// <returns></returns>
        public string BKF259 { get; set; }
        /// <summary>
        /// 评估日期时间 格式：yyyymmddhhmiss
        /// </summary>
        /// <returns></returns>
        public string BKF249 { get; set; }
        /// <summary>
        /// 责任护士姓名
        /// </summary>
        /// <returns></returns>
        public string BKF412 { get; set; }
    }
}
