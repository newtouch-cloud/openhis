namespace NewtouchHIS.Lib.Base.Model.DRG
{
    /// <summary>
    /// 病案首页Drg分组信息
    /// </summary>
    public class DrgMedicalRecord
    {
        /// <summary>
        /// 病案首页号
        /// 唯一标识、不重复
        /// </summary>
        public String Index { get; set; }
        /// <summary>
        /// 性别代码
        /// </summary>
        public String gender { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int age { get; set; }
        /// <summary>
        /// 新生儿年龄
        /// </summary>
        public int ageDay { get; set; }
        /// <summary>
        /// 新生儿体重
        /// </summary>
        public int weight { get; set; }
        /// <summary>
        /// 出院科室
        /// </summary>
        public String dept { get; set; }
        /// <summary>
        /// 住院天数
        /// </summary>
        public int inHospitalTime { get; set; }
        /// <summary>
        /// 离院方式
        /// </summary>
        public String leavingType { get; set; }
        /// <summary>
        /// 诊断列表
        /// </summary>
        public String[] zdList { get; set; }
        /// <summary>
        /// 手术及操作列表
        /// </summary>
        public String[] ssList { get; set; }
        public String remark { get; set; }
        public String ToInputString()
        {
            String zdList_str = this.zdList == null || this.zdList.Length == 0 ? "" : "\"" + string.Join("|", this.zdList) + "\"";
            String ssList_str = this.ssList == null || this.ssList.Length == 0 ? "" : "\"" + string.Join("|", this.ssList) + "\"";
            return $"{this.Index},{this.gender},{this.age},{this.ageDay},{this.weight},{this.dept},{this.inHospitalTime},{this.leavingType},{zdList_str},{ssList_str},{this.remark ?? ""}";
        }
    }
    /// <summary>
    /// DRG分组病案信息
    /// （with版本选择）
    /// </summary>
    public class DrgAreaMedicalRecord : DrgMedicalRecord
    {
        /// <summary>
        /// 分组器版本
        /// </summary>
        public string version { get; set; }
        public int? versionPolicy { get; set; }
    }
    /// <summary>
    /// DRG分组病案信息
    /// （with版本选择，List&Array）
    /// </summary>
    public class DrgAreaMedicalRecordS
    {
        /// <summary>
        /// 分组器版本
        /// </summary>
        public string version { get; set; }
        public int? versionPolicy { get; set; }
        public List<DrgMedicalRecord>? medicalList { get; set; }
        public string[]? medicalArr { get; set; }
    }
}
