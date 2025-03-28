using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationManage
{
	public class HosCqybJsPatInfoVO
	{
		/// <summary>
		/// 住院床日
		/// </summary>
		public string zycr { get; set; }
		/// <summary>
		/// 出院科室
		/// </summary>
		public string cyks { get; set; }
		/// <summary>
		/// 出院医生
		/// </summary>
		public string cyys { get; set; }
        /// <summary>
        /// 出院医师国家编码
        /// </summary>
        public string cyysgjbm { get; set; }
        public string cyrq { get; set; }

    }
    public class HosCqybJsPatInfoVO_cr
    {
        /// <summary>
        /// 入院日期
        /// </summary>
        public DateTime ryrq { get; set; }
        /// <summary>
        /// 出院日期
        /// </summary>
        public DateTime? cyrq { get; set; }
        /// <summary>
        /// 出院科室
        /// </summary>
        public string cyks { get; set; }
        /// <summary>
        /// 出院医生
        /// </summary>
        public string cyys { get; set; }
        /// <summary>
        /// 出院医师国家编码
        /// </summary>
        public string cyysgjbm { get; set; }

    }
}
