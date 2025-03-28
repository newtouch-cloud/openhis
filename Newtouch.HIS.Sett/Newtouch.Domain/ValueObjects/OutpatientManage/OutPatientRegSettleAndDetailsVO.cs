
using Newtouch.HIS.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 门诊结算和结算明细
    /// </summary>
    [NotMapped]
    public class OutPatientRegSettleAndDetailsVO: OutpatientRegistNonAttendanceEntity
    {
        /// <summary>
        /// 收费项目
        /// </summary>
        public string sfxm { get; set; }


    }
}
