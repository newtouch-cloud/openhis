using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Domain.Entity.CIS
{
    [Tenant(DBEnum.CisDb)]
    [SugarTable("xt_zyzd", "TCMDiagnosisEntity")]
    /// <summary>
    /// 中医诊断
    /// </summary>
    public class TCMDiagnosisEntity : IEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string zyzdId { get; set; }

        /// <summary>
        /// 关联就诊表
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// 枚举 1 主诊断 2 辅诊断
        /// </summary>
        public int zdlx { get; set; }

        /// <summary>
        /// 关联base库 系统诊断表
        /// </summary>
        public string zdCode { get; set; }

        /// <summary>
        /// 冗余字段
        /// </summary>
        public string? zdmc { get; set; }

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
        /// 中医诊断备注
        /// </summary>
        public string? zdbz { get; set; }
    }
}
