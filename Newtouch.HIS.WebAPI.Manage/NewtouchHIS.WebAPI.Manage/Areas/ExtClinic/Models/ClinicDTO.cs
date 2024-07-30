/**** 定义与 诊所接口一致 ******/

using Mapster;
using Mapster.Utils;
using Microsoft.Data.SqlClient.Server;
using System.Runtime.Serialization;

namespace NewtouchHIS.WebAPI.Manage.Areas.ExtClinic
{
    /// <summary>
    /// 诊所身份认证Token
    /// </summary>
    public class ClinicTokenDTO
    {
        public string token { get; set; }
    }

    /// <summary>
    /// 诊疗申请结果通知
    /// </summary>
    public class TreatedApplyResultDTO
    {
        /// <summary>
        /// 远程诊疗申请ID
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 就诊状态（0:未提交, 1:待确认、2:就诊中、3:已结束、4:已发药、5:已退回）
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 会议号
        /// </summary>
        public string? conferenceId { get; set; }
        /// <summary>
        /// 申请结果（0：通过:1：不通过）
        /// </summary>
        public string applicationResults { get; set; }
    }
    /// <summary>
    /// 诊所患者病历
    /// </summary>
    public class ClinicPatMedicalRecordDTO
    {
        public ClinicDicDTO? doctor { get; set; }
        public string? name { get; set; }
        /// <summary>
        /// 诊所申请Id
        /// </summary>
        public string? diagnosisId { get; set; }
        public string? department { get; set; }
        public string? createBy { get; set; }
        public DateTime? createDate { get; set; }
        /// <summary>
        /// 主诉
        /// </summary>
        public string? patientTell { get; set; }
        /// <summary>
        /// 现病史
        /// </summary>
        public string? nowHistory { get; set; }
        /// <summary>
        /// 既往史
        /// </summary>
        public string? beforeHistory { get; set; }
        /// <summary>
        /// 个人史
        /// </summary>
        public string? personalHistory { get; set; }
        /// <summary>
        /// 过敏史
        /// </summary>
        public string? allergyHistory { get; set; }
        /// <summary>
        /// 疾病史
        /// </summary>
        public string? diseaseHistory { get; set; }
        /// <summary>
        /// 婚孕史
        /// </summary>
        public string? pregnancyHistory { get; set; }
        /// <summary>
        /// 传染病史
        /// </summary>
        public string? infectiousHistory { get; set; }
        /// <summary>
        /// 月经史
        /// </summary>
        public string? lunariaHistory { get; set; }
        /// <summary>
        /// 家族史
        /// </summary>
        public string? familyHistory { get; set; }
        /// <summary>
        /// 手术史
        /// </summary>
        public string? surgeryHistory { get; set; }
        /// <summary>
        /// 输血史
        /// </summary>
        public string? transfusionHistory { get; set; }
        /// <summary>
        /// 体格检查
        /// </summary>
        public string? physiqueCheck { get; set; }
        /// <summary>
        /// 辅助检查
        /// </summary>
        public string? assistCheck { get; set; }
        /// <summary>
        /// 急诊诊断
        /// </summary>
        public string? emergencyDiagnose { get; set; }
        /// <summary>
        /// 急诊效果
        /// </summary>
        public string? emergencyEffect { get; set; }
        /// <summary>
        /// 其他检查
        /// </summary>
        public string? otherCheck { get; set; }
        /// <summary>
        /// 处理情况
        /// </summary>
        public string? handlingSituation { get; set; }
        /// <summary>
        /// 个体化健康教育
        /// </summary>
        public string? healthEducation { get; set; }
        /// <summary>
        /// 流行病学史
        /// </summary>
        public string? epidemicDisease { get; set; }
        /// <summary>
        /// 医嘱事项
        /// </summary>
        public string? doctorAdvice { get; set; }
        /// <summary>
        /// 西医诊断
        /// </summary>
        public string? westernDiagnose { get; set; }
        /// <summary>
        /// 中医诊断
        /// </summary>
        public string? chinaDiagnose { get; set; }
    }

    public class ClinicDicDTO
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public string? value { get; set; }
    }
    /// <summary>
    /// 诊所处方信息
    /// </summary>
    public class ClinicRecipelDTO
    {
        /// <summary>
        /// 处方名称
        /// </summary>
        public string? name { get; set; }
        /// <summary>
        /// 处方类别
        /// value:recipelType_0：西药处方  recipelType_1：中药处方
        /// </summary>
        public ClinicDicDTO? recipelType { get; set; }
        /// <summary>
        /// 中药用药剂量 
        /// 例如：1剂（不带单位）
        /// </summary>
        public string? dosage { get; set; }
        /// <summary>
        /// 中药用药用法
        /// value:
        /// chineseMedicineRecipelUse_0  煎服
        /// chineseMedicineRecipelUse_1  熬服
        /// chineseMedicineRecipelUse_2  水冲服
        /// chineseMedicineRecipelUse_3  泡服
        /// chineseMedicineRecipelUse_4  冲服
        /// chineseMedicineRecipelUse_5  穴位注射
        /// chineseMedicineRecipelUse_6  水煎服
        /// </summary>
        public ClinicDicDTO? recipelUse { get; set; }
        /// <summary>
        /// 频次用量
        /// value:
        /// chineseMedicineRecipelFrequency_0  一日一剂
        /// chineseMedicineRecipelFrequency_1  一日两剂
        /// chineseMedicineRecipelFrequency_2  一日三剂
        /// </summary>
        public ClinicDicDTO? frequency { get; set; }
        /// <summary>
        /// 使用频次
        /// value:
        /// 例如：一日一次
        /// </summary>
        public ClinicDicDTO? takeFrequency { get; set; }
        /// <summary>
        /// 单次用量
        /// 例如：100ml（不带单位）
        /// </summary>
        public int? singleDosage { get; set; }
        /// <summary>
        /// 处方总价
        /// </summary>
        public decimal? fee { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string? remarks { get; set; }
        /// <summary>
        /// 医嘱
        /// </summary>
        public string? entrust { get; set; }
        /// <summary>
        /// 中药备注事项
        /// </summary>
        public string? chinessNotes { get; set; }
    }
    /// <summary>
    /// 处方明细
    /// </summary>
    public class ClinicRecipelDetailDTO
    {
        /// <summary>
        /// 药品类型
        /// 1 中药  2 西药）
        /// </summary>
        public int? stuffType { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal? unitPrice { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal? allFee { get; set; }

        /// <summary>
        /// 西药用法
        /// value:
        /// westernMedicineUse_0  口服
        /// westernMedicineUse_1  静脉注射
        /// westernMedicineUse_2  饭前
        /// westernMedicineUse_3  饭后
        /// westernMedicineUse_4  皮下注射
        /// westernMedicineUse_5  含化
        /// westernMedicineUse_6  外敷
        /// westernMedicineUse_7  静脉滴注
        /// </summary>
        public ClinicDicDTO? westernMedicineUse { get; set; }
        /// <summary>
        /// 中药用法
        /// value:
        /// chineseMedicineRecipelUse_0  煎服
        /// chineseMedicineRecipelUse_1  熬服
        /// chineseMedicineRecipelUse_2  水冲服
        /// chineseMedicineRecipelUse_3  泡服
        /// chineseMedicineRecipelUse_4  冲服
        /// chineseMedicineRecipelUse_5  穴位注射
        /// chineseMedicineRecipelUse_6  水煎服
        /// </summary>
        public ClinicDicDTO? chineseMedicineUse { get; set; }
        /// <summary>
        /// 西药频次用量
        /// chineseMedicineRecipelTakeFrequency_0.5   每日四次
        /// chineseMedicineRecipelTakeFrequency_1.5   两日一次
        /// chineseMedicineRecipelTakeFrequency_1      每日一次
        /// chineseMedicineRecipelTakeFrequency_2      每日两次
        /// chineseMedicineRecipelTakeFrequency_3      每日三次
        /// </summary>
        public ClinicDicDTO? frequency { get; set; }
        /// <summary>
        /// 西药使用天数
        /// name 
        /// </summary>
        public ClinicDicDTO? days { get; set; }
        /// <summary>
        /// 最小单位总量
        /// </summary>
        public decimal? minTotal { get; set; }

        /// <summary>
        /// 单次用量
        /// </summary>
        public decimal? singleDosage { get; set; }
        /// <summary>
        /// 用药总量
        /// 使用最小单位总量，如：一次1g，一天三次，服用三天，该药品用量为9g
        /// </summary>
        public decimal? total { get; set; }
        public ClinicDrugInfoDTO? drugStuffId { get; set; }
    }

    public class ClinicDrugInfoDTO
    {
        /// <summary>
        /// 药品id
        /// </summary>
        public string? drugStuffId { get; set; }
        /// <summary>
        /// 药品名称
        /// </summary>
        public string? name { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal? price { get; set; }
        /// <summary>
        /// 拆开后零售价
        /// </summary>
        public decimal? retailPrice { get; set; }
        /// <summary>
        /// 剂量单位
        /// value:
        /// medicalDosisUnit_0  g
        ///medicalDosisUnit_1 ml
        ///medicalDosisUnit_2 l
        ///medicalDosisUnit_3 mg
        ///medicalDosisUnit_4 m
        ///medicalDosisUnit_5  %
        ///medicalDosisUnit_6 ug
        ///medicalDosisUnit_7 片
        /// </summary>
        public ClinicDicDTO? dosisUnit { get; set; }
        /// <summary>
        /// 制剂单位
        /// medicalPreparationUnit_0   片 
        /// medicalPreparationUnit_1   粒 
        /// medicalPreparationUnit_2  袋 
        /// medicalPreparationUnit_3  g 
        /// medicalPreparationUnit_4  个 
        /// medicalPreparationUnit_5  支 
        /// medicalPreparationUnit_6  ml 
        /// medicalPreparationUnit_7  mm 
        /// medicalPreparationUnit_8  mg 
        /// medicalPreparationUnit_9  瓶 
        /// medicalPreparationUnit_10 本 
        /// medicalPreparationUnit_11 ug 
        /// medicalPreparationUnit_12 丸
        /// </summary>
        public ClinicDicDTO? preparationUnit { get; set; }
        /// <summary>
        /// 包装单位
        /// medicalPackUnit_0   包
        /// medicalPackUnit_1 袋
        ///medicalPackUnit_2 盒
        ///medicalPackUnit_3 箱
        ///medicalPackUnit_5 板
        ///medicalPackUnit_6 罐
        ///medicalPackUnit_7 支
        ///medicalPackUnit_8 个
        ///medicalPackUnit_9 瓶
        ///medicalPackUnit_10 g
        ///medicalPackUnit_11 本
        /// </summary>
        public ClinicDicDTO? pack { get; set; }
        
    }

    public class ClinicPatRecipelData
    {
        public ClinicRecipelDTO? recipelInfo { get; set; }
        public List<ClinicRecipelDetailDTO>? recipelDetailEvtList { get; set; }
    }

    public class ClinicPatRecipelDataSync
    {
        public List<ClinicPatRecipelData>? recipelInfoEvtList { get; set; }
        public string id { get; set; }
    }
}
