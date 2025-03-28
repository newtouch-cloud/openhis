using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// 分诊医生
    /// </summary>
    public class OutpatientConsultDoctorVO
    {
        /// <summary>
        /// 诊室编码
        /// </summary>
        public string zsCode { get; set; }
        /// <summary>
        /// 诊室名称
        /// </summary>
        public string zsmc { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? rq { get; set; }
        /// <summary>
        /// 医生工号
        /// </summary>
        public string gh { get; set; }
        /// <summary>
        /// 医生姓名
        /// </summary>
        public string ysxm { get; set; }
		/// <summary>
		/// 诊室楼层数
		/// </summary>
		public string zslc { get; set; }
		/// <summary>
		/// 诊室房号
		/// </summary>
		public string zsfh { get; set; }

	}
}
