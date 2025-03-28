using System;

namespace Newtouch.HIS.Domain.ValueObjects.NonTreatmentItemManage
{
    public class PatientInfoVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int patId { get; set; }

        ///// <summary>
        ///// 出生年月
        ///// </summary>
        //public DateTime? csny { get; set; }

        ///// <summary>
        ///// 年龄
        ///// </summary>
        //public int? nl
        //{
        //    get
        //    {
        //        if (csny.HasValue)
        //        {
        //            return (DateTime.Now.Year - csny.Value.Year) + 1;
        //        }
        //        return null;
        //    }
        //}
    }
}
