using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 处方明细（可在排药使用）
    /// </summary>
    [NotMapped]
    public class CfmxVo : MzCfmxEntity
    {
        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 医生嘱托
        /// </summary>
        public string yszt { get; set; }

        /// <summary>
        /// 收费时间
        /// </summary>
        public DateTime? sfsj { get; set; }

        /// <summary>
        /// 单位数量
        /// </summary>
        public string slStr { get; set; }

    }
}
