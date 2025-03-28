using Newtouch.HIS.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 治疗记录
    /// </summary>
    [NotMapped]
    public class SyncTreatmentServiceRecordVO : SyncTreatmentServiceRecordEntity
    {
        /// <summary>
        /// 字典
        /// </summary>
        public string kflb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StaffId { get; set; }
        public string StaffName { get; set; }

        /// <summary>
        /// 科室Code
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 收费大类Code
        /// </summary>
        public string sfdlCode { get; set; }

        /// <summary>
        /// 收费大类名称
        /// </summary>
        public string sfdlmc { get; set; }

        /// <summary>
        /// 单价
        /// </summary>z
        public decimal? dj { get; set; }

        /// <summary>
        /// calc
        /// </summary>
        public int sl
        {
            get
            {
                if (units > 0)
                {
                    return (int)units;
                }
                else if (durationPerUnit > 0 && minutes > 0)
                {
                    return (int)(minutes / durationPerUnit);
                }
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// durationPerUnit
        /// </summary>
        public int? duration
        {
            get
            {
                if (durationPerUnit > 0)
                {
                    return durationPerUnit;
                }
                else if (units > 0 && minutes > 0)
                {
                    return (int)(minutes / units);
                }
                return 0;
            }
        }
    }
}
