using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.OutputDto.MRUpload
{
    /// <summary>
    /// 住院病案首页信息
    /// </summary>
    public class YB_7600
    {
        /// <summary>
        /// 就诊流水号
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("就诊流水号")]
        public string AKC190 { get; set; }

        /// <summary>
        /// 病案流水号
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("病案流水号")]
        public string BKF303 { get; set; }

        /// <summary>
        /// 住院流水号
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("住院流水号")]
        public string BKC191 { get; set; }

        /// <summary>
        /// 医疗机构名称
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("医疗机构名称")]
        public string AKB021 { get; set; }

        /// <summary>
        /// 医疗机构组织机构代码
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("医疗机构组织机构代码")]
        public string BKF388 { get; set; }

        /// <summary>
        /// 医疗费用支付方式代码
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("医疗费用支付方式代码")]
        public string BKF872 { get; set; }

        /// <summary>
        /// 住院次数
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("住院次数")]
        public string BKE076 { get; set; }

        /// <summary>
        /// 居民健康卡号
        /// </summary>
        /// <returns></returns>
        public string BKF636 { get; set; }

        /// <summary>
        /// 医保卡号
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("医保卡号")]
        public string AAZ500 { get; set; }

        /// <summary>
        /// 病案号
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("病案号")]
        public string BKF888 { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("患者姓名")]
        public string AAC003 { get; set; }

        /// <summary>
        /// 性别代码
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("性别代码")]
        public string AAC004 { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("出生日期")]
        public string AAC006 { get; set; }

        /// <summary>
        /// 年龄(岁)
        /// </summary>
        /// <returns></returns>
        public string BKF811 { get; set; }

        /// <summary>
        /// 国籍代码
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("国籍代码")]
        public string BKF638 { get; set; }

        /// <summary>
        /// 月龄(月)
        /// </summary>
        /// <returns></returns>
        public string BKF411 { get; set; }

        /// <summary>
        /// 新生儿出生体重(g)
        /// </summary>
        /// <returns></returns>
        public string BKF640 { get; set; }

        /// <summary>
        /// 新生儿入院体重(g)
        /// </summary>
        /// <returns></returns>
        public string BKF641 { get; set; }

        /// <summary>
        /// 出生地_省(区、市)
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("出生地_省")]
        public string BKF132 { get; set; }

        /// <summary>
        /// 出生地_市
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("出生地_市")]
        public string BKF133 { get; set; }

        /// <summary>
        /// 出生地_县
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("出生地_县")]
        public string BKF134 { get; set; }

        /// <summary>
        /// 籍贯_省(区、市)
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("籍贯_省(区、市)")]
        public string BKF643 { get; set; }

        /// <summary>
        /// 籍贯_市
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("籍贯_市")]
        public string BKF190 { get; set; }

        /// <summary>
        /// 民族编码
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("民族编码")]
        public string AAC005 { get; set; }

        /// <summary>
        /// 患者身份证件类别代码
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("患者身份证件类别代码")]
        public string AAC058 { get; set; }

        /// <summary>
        /// 患者身份证件号码
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("患者身份证件号码")]
        public string AAC002 { get; set; }

        /// <summary>
        /// 职业类别代码
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("职业类别代码")]
        public string BKF637 { get; set; }

        /// <summary>
        /// 婚姻状况代码
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("婚姻状况代码")]
        public string AAC017 { get; set; }

        /// <summary>
        /// 现住址_省(区、市)
        /// </summary>
        /// <returns></returns>
        public string BKF366 { get; set; }

        /// <summary>
        /// 现住址-市（地区、州）
        /// </summary>
        /// <returns></returns>
        public string BKF363 { get; set; }

        /// <summary>
        /// 现住址-县（区）
        /// </summary>
        /// <returns></returns>
        public string BKF364 { get; set; }

        /// <summary>
        /// 现住址-乡（镇、街道办事处）
        /// </summary>
        /// <returns></returns>
        public string BKF365 { get; set; }

        /// <summary>
        /// 现住址-村（街、路、弄等）
        /// </summary>
        /// <returns></returns>
        public string BKF361 { get; set; }

        /// <summary>
        /// 现住址-门牌号码
        /// </summary>
        /// <returns></returns>
        public string BKF362 { get; set; }

        /// <summary>
        /// 现住址_邮政编码
        /// </summary>
        /// <returns></returns>
        public string BKF649 { get; set; }

        /// <summary>
        /// 患者电话号码
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("患者电话号码")]
        public string BKC280 { get; set; }

        /// <summary>
        /// 户口地址-省（自治区、直辖市）
        /// </summary>
        /// <returns></returns>
        public string BKF180 { get; set; }

        /// <summary>
        /// 户口地址-市（地区、州）
        /// </summary>
        /// <returns></returns>
        public string BKF181 { get; set; }

        /// <summary>
        /// 户口地址-县（区）
        /// </summary>
        /// <returns></returns>
        public string BKF182 { get; set; }

        /// <summary>
        /// 户口地址-乡（镇、街道办事处）
        /// </summary>
        /// <returns></returns>
        public string BKF183 { get; set; }

        /// <summary>
        /// 户口地址-村（街、路、弄等）
        /// </summary>
        /// <returns></returns>
        public string BKF178 { get; set; }

        /// <summary>
        /// 户口地址-门牌号码
        /// </summary>
        /// <returns></returns>
        public string BKF179 { get; set; }

        /// <summary>
        /// 户口地址_邮政编码
        /// </summary>
        /// <returns></returns>
        public string BKF184 { get; set; }

        /// <summary>
        /// 工作单位名称
        /// </summary>
        /// <returns></returns>
        public string AAB004 { get; set; }

        /// <summary>
        /// 工作单位地址_省(自治区、直辖市)
        /// </summary>
        /// <returns></returns>
        public string BKF163 { get; set; }

        /// <summary>
        /// 工作单位地址_市(地区、州)
        /// </summary>
        /// <returns></returns>
        public string BKF164 { get; set; }

        /// <summary>
        /// 工作单位地址_县(区)
        /// </summary>
        /// <returns></returns>
        public string BKF165 { get; set; }

        /// <summary>
        /// 工作单位地址_乡(镇、街道办事处)
        /// </summary>
        /// <returns></returns>
        public string BKF166 { get; set; }

        /// <summary>
        /// 工作单位地址_村(街、路、弄等)
        /// </summary>
        /// <returns></returns>
        public string BKF161 { get; set; }

        /// <summary>
        /// 工作单位地址_门牌号码
        /// </summary>
        /// <returns></returns>
        public string BKF162 { get; set; }

        /// <summary>
        /// 工作单位地址_邮政编码
        /// </summary>
        /// <returns></returns>
        public string BKF652 { get; set; }

        /// <summary>
        /// 工作单位电话号码
        /// </summary>
        /// <returns></returns>
        public string BKF651 { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("联系人姓名")]
        public string AAE004 { get; set; }

        /// <summary>
        /// 联系人与患者的关系代码
        /// </summary>
        /// <returns></returns>
        public string BKF654 { get; set; }

        /// <summary>
        /// 联系人地址_省(自治区、直辖市)
        /// </summary>
        /// <returns></returns>
        public string BKF219 { get; set; }

        /// <summary>
        /// 联系人地址_市(地区、州)
        /// </summary>
        /// <returns></returns>
        public string BKF220 { get; set; }

        /// <summary>
        /// 联系人地址_县(区)
        /// </summary>
        /// <returns></returns>
        public string BKF221 { get; set; }

        /// <summary>
        /// 联系人地址_乡(镇、街道办事处)
        /// </summary>
        /// <returns></returns>
        public string BKF222 { get; set; }

        /// <summary>
        /// 联系人地址_村(街、路、弄等)
        /// </summary>
        /// <returns></returns>
        public string BKF217 { get; set; }

        /// <summary>
        /// 联系人地址_门牌号码
        /// </summary>
        /// <returns></returns>
        public string BKF218 { get; set; }

        /// <summary>
        /// 联系人电话号码
        /// </summary>
        /// <returns></returns>
        public string AAE005 { get; set; }

        /// <summary>
        /// 入院途径
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("入院途径")]
        public string BKF818 { get; set; }

        /// <summary>
        /// 入院日期时间
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("入院日期时间")]
        public string BKC192 { get; set; }

        /// <summary>
        /// 入院科室代码
        /// </summary>
        /// <returns></returns>
        public string BKF659 { get; set; }

        /// <summary>
        /// 入院科室名称
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("入院科室名称")]
        public string BKF660 { get; set; }

        /// <summary>
        /// 入院病房
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("入院病房")]
        public string BKF261 { get; set; }

        /// <summary>
        /// 转科科室代码
        /// </summary>
        /// <returns></returns>
        public string BKF551 { get; set; }

        /// <summary>
        /// 转科科室名称
        /// </summary>
        /// <returns></returns>
        public string BKF552 { get; set; }

        /// <summary>
        /// 出院日期时间
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("出院日期时间")]
        public string AKC194 { get; set; }

        /// <summary>
        /// 出院科室代码
        /// </summary>
        /// <returns></returns>
        public string BKF669 { get; set; }

        /// <summary>
        /// 出院科室名称
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("出院科室名称")]
        public string BKF670 { get; set; }

        /// <summary>
        /// 出院病房
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("出院病房")]
        public string BKF853 { get; set; }

        /// <summary>
        /// 实际住院天数
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("实际住院天数")]
        public string BKF668 { get; set; }

        /// <summary>
        /// 门（急）诊诊断（中医诊断）病名编码
        /// </summary>
        /// <returns></returns>
        public string BKF855 { get; set; }

        /// <summary>
        /// 门（急）诊诊断（中医诊断）名称
        /// </summary>
        /// <returns></returns>
        public string BKF854 { get; set; }

        /// <summary>
        /// 门（急）诊诊断（中医证候）证候编码
        /// </summary>
        /// <returns></returns>
        public string BKF241 { get; set; }

        /// <summary>
        /// 门（急）诊诊断（中医证候）名称
        /// </summary>
        /// <returns></returns>
        public string BKF240 { get; set; }

        /// <summary>
        /// 门（急）诊诊断（西医诊断）疾病编码
        /// </summary>
        /// <returns></returns>
        public string BKF857 { get; set; }

        /// <summary>
        /// 门（急）诊诊断（西医诊断）名称
        /// </summary>
        /// <returns></returns>
        public string BKF856 { get; set; }

        /// <summary>
        /// 实施临床路径标志代码
        /// </summary>
        /// <returns></returns>
        public string BKF858 { get; set; }

        /// <summary>
        /// 使用医疗机构中药制剂标志
        /// </summary>
        /// <returns></returns>
        public string BKF859 { get; set; }

        /// <summary>
        /// 使用中医诊疗设备标志
        /// </summary>
        /// <returns></returns>
        public string BKF860 { get; set; }

        /// <summary>
        /// 使用中医技术标志
        /// </summary>
        /// <returns></returns>
        public string BKF861 { get; set; }

        /// <summary>
        /// 辨证施护标志
        /// </summary>
        /// <returns></returns>
        public string BKF862 { get; set; }

        /// <summary>
        /// 损伤中毒的外部原因编码
        /// </summary>
        /// <returns></returns>
        public string BKF863 { get; set; }

        /// <summary>
        /// 病理诊断编码
        /// </summary>
        /// <returns></returns>
        public string BKF865 { get; set; }

        /// <summary>
        /// 病理诊断名称
        /// </summary>
        /// <returns></returns>
        public string BKF675 { get; set; }

        /// <summary>
        /// 病理号
        /// </summary>
        /// <returns></returns>
        public string BKF676 { get; set; }

        /// <summary>
        /// 药物过敏标志 1 是；2 否
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("药物过敏标志")]
        public string BKF677 { get; set; }

        /// <summary>
        /// 过敏药物 
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("过敏药物")]
        public string AKE001 { get; set; }

        /// <summary>
        /// 死亡患者尸检标志1 是；2 否；
        /// </summary>
        /// <returns></returns>
        public string BKF324 { get; set; }

        /// <summary>
        /// ABO血型代码
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("ABO血型代码")]
        public string BKF867 { get; set; }

        /// <summary>
        /// Rh(D)血型代码
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("Rh(D)血型代码")]
        public string BKF868 { get; set; }

        /// <summary>
        /// 科主任姓名
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("科主任姓名")]
        public string BKF684 { get; set; }

        /// <summary>
        /// 主任(副主任)医师姓名
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("主任(副主任)医师姓名")]
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
        [Description("主治医师姓名")]
        public string AKC273 { get; set; }

        /// <summary>
        /// 住院医师姓名
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("住院医师姓名")]
        public string BKF692 { get; set; }

        /// <summary>
        /// 责任护士姓名
        /// </summary>
        /// <returns></returns>
        public string BKF412 { get; set; }

        /// <summary>
        /// 进修医师姓名
        /// </summary>
        /// <returns></returns>
        public string BKF211 { get; set; }

        /// <summary>
        /// 实习医师姓名
        /// </summary>
        /// <returns></returns>
        public string BKF267 { get; set; }

        /// <summary>
        /// 病案编码员姓名
        /// </summary>
        /// <returns></returns>
        public string BKF105 { get; set; }

        /// <summary>
        /// 病案质量
        /// </summary>
        /// <returns></returns>
        public string BKF106 { get; set; }

        /// <summary>
        /// 质控医师姓名
        /// </summary>
        /// <returns></returns>
        public string BKF431 { get; set; }

        /// <summary>
        /// 质控护士姓名
        /// </summary>
        /// <returns></returns>
        public string BKF429 { get; set; }

        /// <summary>
        /// 质控日期
        /// </summary>
        /// <returns></returns>
        public string BKF430 { get; set; }

        /// <summary>
        /// 离院方式
        /// </summary>
        /// <returns></returns>
        [Required]
        [Description("离院方式")]
        public string BKF705 { get; set; }

        /// <summary>
        /// 拟接收医疗机构名称
        /// </summary>
        /// <returns></returns>
        public string BKF706 { get; set; }

        /// <summary>
        /// 出院31天内再住院标志
        /// </summary>
        /// <returns></returns>
        public string BKF137 { get; set; }

        /// <summary>
        /// 出院31天内再住院目的
        /// </summary>
        /// <returns></returns>
        public string BKF138 { get; set; }

        /// <summary>
        /// 颅脑损伤患者入院前昏迷时间-d
        /// </summary>
        /// <returns></returns>
        public string BKF226 { get; set; }

        /// <summary>
        /// 颅脑损伤患者入院前昏迷时间-h
        /// </summary>
        /// <returns></returns>
        public string BKF227 { get; set; }

        /// <summary>
        /// 颅脑损伤患者入院前昏迷时间-min
        /// </summary>
        /// <returns></returns>
        public string BKF228 { get; set; }

        /// <summary>
        /// 颅脑损伤患者入院后昏迷时间-d
        /// </summary>
        /// <returns></returns>
        public string BKF223 { get; set; }

        /// <summary>
        /// 颅脑损伤患者入院后昏迷时间-h
        /// </summary>
        /// <returns></returns>
        public string BKF224 { get; set; }

        /// <summary>
        /// 颅脑损伤患者入院后昏迷时间-min
        /// </summary>
        /// <returns></returns>
        public string BKF225 { get; set; }

        /// <summary>
        /// 出院情况(病情转归)
        /// </summary>
        /// <returns></returns>
        public string BKF758 { get; set; }

        /// <summary>
        /// 诊断符合情况-门诊与住院
        /// </summary>
        /// <returns></returns>
        public string BKF880 { get; set; }

        /// <summary>
        /// 诊断符合情况-入院与出院
        /// </summary>
        /// <returns></returns>
        public string BKF882 { get; set; }

        /// <summary>
        /// 诊断符合情况-术前与术后
        /// </summary>
        /// <returns></returns>
        public string BKF877 { get; set; }

        /// <summary>
        /// 诊断符合情况-临床与病理
        /// </summary>
        /// <returns></returns>
        public string BKF878 { get; set; }

        /// <summary>
        /// 诊断符合情况-放射与病理
        /// </summary>
        /// <returns></returns>
        public string BKF879 { get; set; }

        /// <summary>
        /// 抢救次数
        /// </summary>
        /// <returns></returns>
        public string BKF890 { get; set; }

        /// <summary>
        /// 抢救成功次数
        /// </summary>
        /// <returns></returns>
        public string BKF891 { get; set; }

        /// <summary>
        /// 住院总费用(元)
        /// </summary>
        /// <returns></returns>
        public decimal? BKF478 { get; set; }

        /// <summary>
        /// 住院总费用_自付金额(元)
        /// </summary>
        /// <returns></returns>
        public decimal? BKF479 { get; set; }

        /// <summary>
        /// 综合医疗服务费_一般医疗服务费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF563 { get; set; }

        /// <summary>
        /// 综合医疗服务费_一般治疗操作费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF564 { get; set; }

        /// <summary>
        /// 一般医疗服务费_中医辨证论治费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF386 { get; set; }

        /// <summary>
        /// 一般医疗服务费_中医辨证论治会诊费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF387 { get; set; }

        /// <summary>
        /// 综合医疗服务费_护理费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF561 { get; set; }

        /// <summary>
        /// 综合医疗服务费_其他费用
        /// </summary>
        /// <returns></returns>
        public decimal? BKF562 { get; set; }

        /// <summary>
        /// 诊断_病理诊断费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF413 { get; set; }

        /// <summary>
        /// 诊断_实验室诊断费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF415 { get; set; }

        /// <summary>
        /// 诊断_影像学诊断费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF416 { get; set; }

        /// <summary>
        /// 诊断_临床诊断项目费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF414 { get; set; }

        /// <summary>
        /// 治疗_非手术治疗项目费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF432 { get; set; }

        /// <summary>
        /// 治疗_非手术治疗项目费_临床物理治疗费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF433 { get; set; }

        /// <summary>
        /// 治疗_手术治疗费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF434 { get; set; }

        /// <summary>
        /// 治疗_手术治疗费_麻醉费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF435 { get; set; }

        /// <summary>
        /// 治疗_手术治疗费_手术费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF436 { get; set; }

        /// <summary>
        /// 康复类-康复费
        /// </summary>
        /// <returns></returns>
        public decimal? AAE019 { get; set; }

        /// <summary>
        /// 中医类_中医诊断费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF465 { get; set; }

        /// <summary>
        /// 中医类_中医治疗费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF461 { get; set; }

        /// <summary>
        /// 中医类_中医治疗费_中医外治费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF472 { get; set; }

        /// <summary>
        /// 中医类_中医治疗费_中医骨伤费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF469 { get; set; }

        /// <summary>
        /// 中医类_中医治疗费_针刺与灸法费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF467 { get; set; }

        /// <summary>
        /// 中医类_中医治疗费_中医推拿治疗费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF471 { get; set; }

        /// <summary>
        /// 中医类_中医治疗费_中医肛肠治疗费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF468 { get; set; }

        /// <summary>
        /// 中医类_中医治疗费_中医特殊治疗费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF470 { get; set; }

        /// <summary>
        /// 中医类_中医其他费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF462 { get; set; }

        /// <summary>
        /// 中医类_中医其他费_中医特殊调配加工费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF464 { get; set; }

        /// <summary>
        /// 中医类_中医其他费_辨证施膳费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF463 { get; set; }

        /// <summary>
        /// 西药类-西药费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF356 { get; set; }

        /// <summary>
        /// 西药类-西药费_抗菌药物费用
        /// </summary>
        /// <returns></returns>
        public decimal? BKF357 { get; set; }

        /// <summary>
        /// 中药类_中成药费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF453 { get; set; }

        /// <summary>
        /// 中成药费-医疗机构中药制剂费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF450 { get; set; }

        /// <summary>
        /// 中药类_中草药费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF452 { get; set; }

        /// <summary>
        /// 血液和血液制品类-血费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF376 { get; set; }

        /// <summary>
        /// 血液和血液制品类-白蛋白类制品费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF372 { get; set; }

        /// <summary>
        /// 血液和血液制品类-球蛋白类制品费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF374 { get; set; }

        /// <summary>
        /// 血液和血液制品类-凝血因子类制品费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF373 { get; set; }

        /// <summary>
        /// 血液和血液制品类-细胞因子类制品费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF375 { get; set; }

        /// <summary>
        /// 耗材类-检查用一次性医用材料费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF171 { get; set; }

        /// <summary>
        /// 耗材类-治疗用一次性医用材料费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF173 { get; set; }

        /// <summary>
        /// 耗材类-手术用一次性医用材料费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF172 { get; set; }

        /// <summary>
        /// 其他类-其他费
        /// </summary>
        /// <returns></returns>
        public decimal? BKF250 { get; set; }

        /// <summary>
        /// 体重（kg）
        /// </summary>
        /// <returns></returns>
        public string BKF353 { get; set; }
    }
}
