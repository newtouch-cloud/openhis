using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Base.Domain.Entity
{
    /// <summary>
    /// 药品用法
    /// </summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("xt_ypyf", "DrugUsageEntity")]
    public class DrugUsageEntity : ISysEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string yfId { get; set; }
        /// <summary>
        /// 用法编码
        /// </summary>
        public string yfCode { get; set; }
        /// <summary>
        /// 用法名称
        /// </summary>
        public string yfmc { get; set; }
        public string? py { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }
        /// <summary>
        /// 1 西药 2 中药
        /// </summary>
        public string yplx { get; set; }

    }
}
