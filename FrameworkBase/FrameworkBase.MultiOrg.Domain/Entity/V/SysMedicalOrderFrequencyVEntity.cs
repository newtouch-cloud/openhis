using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 医嘱频次
    /// </summary>
    [Table("V_S_xt_yzpc")]
    public class SysMedicalOrderFrequencyVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int yzpcId { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string yzpcCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string yzpcmc { get; set; }

        /// <summary>
        /// 执行次数
        /// </summary>
        public int? zxcs { get; set; }

        /// <summary>
        /// 执行周期 数
        /// </summary>
        public int? zxzq { get; set; }

        /// <summary>
        /// 执行周期 的 单位
        /// </summary>
        public string zxzqdw { get; set; }

        /// <summary>
        /// 周标志
        /// </summary>
        public string zbz { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public string zxsj { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 医嘱频次名称说明
        /// </summary>
        public string yzpcmcsm { get; set; }

    }
}
