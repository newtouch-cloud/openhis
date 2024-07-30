namespace NewtouchHIS.Lib.Base.Model.DRG
{
    public class DrgGroupResult
    {
        /// <summary>
        /// 带入输入对象MedicalRecord的Index字段，建议使用病案号或住院号，保持唯一性
        /// </summary>
        public String Index;

        /// <summary>
        /// 分组结果，“分组成功”代表分组成功，其他定义见DrgGroupStatus类
        /// </summary>
        public String status;
        /// <summary>
        /// 分组过程，列表格式，包括ICD编码转换信息、名称信息，以及DRG分组每一步操作的记录，是什么结果，应用了哪些规则
        /// </summary>
        public List<String> messages;
        /// <summary>
        /// 主要诊断大类，分组成功或分入歧义组是有值
        /// </summary>
        public String mdc;
        /// <summary>
        /// 核心DRG组代码，校验通过后有值，分入歧义组时为QY，没有分到组时为00
        /// </summary>
        public String adrg;
        /// <summary>
        /// DRG组代码，校验通过后有值，分入歧义组时为*QY，没有分到组时为00
        /// </summary>
        public String drg;
        public DrgMedicalRecord record;
    }


    /// <summary>
    /// 完整的drg分组结果
    /// </summary>
    public class DrgMedicalRecordResult : DrgMedicalRecord
    {
        /// <summary>
        /// 分组过程，列表格式，包括ICD编码转换信息、名称信息，以及DRG分组每一步操作的记录，是什么结果，应用了哪些规则
        /// </summary>
        public List<String> messages { get; set; }
        /// <summary>
        /// 分组结果，“分组成功”代表分组成功，其他定义见DrgGroupStatus类
        /// </summary>
        public String status { get; set; }
        /// <summary>
        /// 主要诊断大类，分组成功或分入歧义组是有值
        /// </summary>
        public String mdc { get; set; }
        /// <summary>
        /// 核心DRG组代码，校验通过后有值，分入歧义组时为QY，没有分到组时为00
        /// </summary>
        public String adrg { get; set; }
        /// <summary>
        /// DRG组代码，校验通过后有值，分入歧义组时为*QY，没有分到组时为00
        /// </summary>
        public String drg { get; set; }

    }
    /// <summary>
    /// 文件上传drg分组结果
    /// </summary>
    public class DrgGroupFileResult : DrgMedicalRecordResult
    {
        public string file { get; set; }
        public List<DrgMedicalRecordResult> rows { get; set; }
    }

}
