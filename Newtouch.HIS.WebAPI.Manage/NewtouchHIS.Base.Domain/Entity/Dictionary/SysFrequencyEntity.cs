using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Base.Domain.Entity
{
    /// <summary>
    /// 频次
    /// </summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("xt_ypyf", "SysFrequencyEntity")]

    public class SysFrequencyEntity : IEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string yzpcId { get; set; }
        /// <summary>
        /// 频次编码
        /// </summary>
        public string yzpcCode { get; set; }
        /// <summary>
        /// 医嘱执行频次
        /// </summary>
        public string yzpcmc { get; set; }
        /// <summary>
        /// 执行次数
        /// </summary>
        public int? zxcs { get; set; }
        /// <summary>
        /// 执行周期
        /// </summary>
        public int? zxzq { get; set; }
        /// <summary>
        /// 执行周期单位
        /// </summary>
        public string? zxzqdw { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? zbz { get; set; }
        /// <summary>
        /// 执行时间点
        /// </summary>
        public string? zxsj { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string? yzpcmcsm { get; set; }
        public int? px { get; set; }

    }
}
