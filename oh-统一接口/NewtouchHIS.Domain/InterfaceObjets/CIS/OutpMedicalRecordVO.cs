namespace NewtouchHIS.Domain.InterfaceObjets.CIS
{
    /// <summary>
    /// 门诊患者病历
    /// </summary> 
    public class OutpMedicalRecordVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string? OrganizeId { get; set; }
        public string? mzh { get; set; }

        /// <summary>
        /// 冗余挂号信息  枚举 1 普通门诊 2 急诊 3专家
        /// </summary>
        public int? mjzbz { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string? blh { get; set; }        

        /// <summary>
        /// 主诉
        /// </summary>
        public string? zs { get; set; }

        /// <summary>
        /// 发病时间
        /// </summary>
        public DateTime? fbsj { get; set; }

        /// <summary>
        /// 现病史
        /// </summary>
        public string? xbs { get; set; }

        /// <summary>
        /// 既往史
        /// </summary>
        public string? jws { get; set; }

        /// <summary>
        /// 查体
        /// </summary>
        public string? ct { get; set; }
        /// <summary>
        /// 处理
        /// </summary>
        public string? clfa { get; set; }
        /// <summary>
        /// 辅助检查
        /// </summary>
        public string? fzjc { get; set; }
        /// <summary>
        /// 月经史
        /// </summary>
        public string? yjs { get; set; }
        /// <summary>
        /// 过敏史
        /// </summary>
        public string? gms { get; set; }
        /// <summary>
        /// 一般检查
        /// </summary>
        public OutpGeneralInspectionVO? ybjc { get; set; }
        public List<OutpWMDiagnosisVO>? xyzd { get; set; }
        public List<OutpTCMDiagnosisVO>? zyzd { get; set; }
    }
    /// <summary>
    /// 门诊病历-一般检查
    /// </summary>
    public class OutpGeneralInspectionVO
    {
        #region 一般检查
        /// <summary>
        /// 体重
        /// </summary>
        public decimal? tizhong { get; set; }

        /// <summary>
        /// 体温
        /// </summary>
        public decimal? tiwen { get; set; }

        /// <summary>
        /// 脉搏
        /// </summary>
        public decimal? maibo { get; set; }

        /// <summary>
        /// 血糖测量方式 餐后 空腹 随机 
        /// EnumXtclfs
        /// </summary>
        public string? xuetangclfs { get; set; }

        /// <summary>
        /// 血糖
        /// </summary>
        public decimal? xuetang { get; set; }
        /// <summary>
        /// 身高
        /// </summary>
        public decimal? shengao { get; set; }
        /// <summary>
        /// 收缩压
        /// </summary>
        public decimal? shousuoya { get; set; }
        /// <summary>
        /// 舒张压
        /// </summary>
        public decimal? shuzhangya { get; set; }
        /// <summary>
        /// 呼吸
        /// </summary>
        public decimal? huxi { get; set; }
        /// <summary>
        /// 婚姻状况 EnumMarriageStu
        /// </summary>
        public string? hy { get; set; }
        #endregion
    }

    /// <summary>
    /// 门诊西医诊断
    /// </summary>
    public class OutpWMDiagnosisVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string? zdCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zdmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? zdlx { get; set; }
        /// <summary>
        /// 疑似标志
        /// </summary>
        public string? ysbz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? icd10 { get; set; }
        /// <summary>
        /// 西医诊断备注
        /// </summary>
        public string? zdbz { get; set; }
    }
    /// <summary>
    /// 门诊中医诊断
    /// </summary>
    public class OutpTCMDiagnosisVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string? zdCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? zdmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int zdlx { get; set; }
        /// <summary>
        /// 疑似标志
        /// </summary>
        public string? ysbz { get; set; }

        /// <summary>
        /// 症候编码
        /// </summary>
        public string? zhCode { get; set; }

        /// <summary>
        /// 症候名称
        /// </summary>
        public string? zhmc { get; set; }
        /// <summary>
        /// icd10
        /// </summary>
        public string? icd10 { get; set; }
        /// <summary>
        /// 中医诊断备注
        /// </summary>
        public string? zdbz { get; set; }
    }
}
