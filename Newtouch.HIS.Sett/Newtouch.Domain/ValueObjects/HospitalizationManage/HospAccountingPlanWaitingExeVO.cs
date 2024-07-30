using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 待执行 住院记账 计划  VO
    /// </summary>
    public class HospAccountingPlanWaitingExeVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string jzjhId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string jzjhmxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// 医嘱性质 1临时 2长期
        /// </summary>
        public string yzxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? sl { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal? dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? zje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 最后执行时间
        /// </summary>
        public DateTime? LastEexcutionTime { get; set; }
        public string kflb { get; set; }
        public int? zcs { get; set; }
        public int? yzxcs { get; set; }
        public int? zll { get; set; }
        public string zlsgh { get; set; }
        public int? dtzxcs { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime entryTime { get; set; }
		/// <summary>
		/// 执行科室
		/// </summary>
	    public string zxks { get; set; }
	}
}
