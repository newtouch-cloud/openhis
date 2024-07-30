using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity.V
{
    /// <summary>
    /// 
    /// </summary>
    [Table("V_S_xt_yzpc")]
    public class SysMedicalOrderFrequencyVEntity
    {       
        /// <summary>
        /// 
        /// </summary>
        public int yzpcId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yzpcCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yzpcmc { get; set; }

        /// <summary>
        /// 执行次数
        /// </summary>
        public int zxcs { get; set; }

        /// <summary>
        /// 执行周期
        /// </summary>
        public int zxzq { get; set; }

        /// <summary>
        /// 执行周期单位
        /// </summary>
        public string zxzqdw { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public string zxsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
