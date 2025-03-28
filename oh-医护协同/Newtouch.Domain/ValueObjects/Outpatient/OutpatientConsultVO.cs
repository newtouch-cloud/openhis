using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    public class OutpatientConsultVO
    {
        /// <summary>
        /// 挂号诊室表主键ID
        /// </summary>
        public int ghzsId { get; set; }
        /// <summary>
        /// 挂号内码
        /// </summary>
        public int ghnm { get; set; }
        /// <summary>
        /// 就诊序号
        /// </summary>
        public Int16 jzxh { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 挂号日期
        /// </summary>
        public DateTime ghrq { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        public string ksCode { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// 医生工号
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// 医生名称
        /// </summary>
        public string ysmc { get; set; }
        /// <summary>
        /// 诊室编码
        /// </summary>
        public string zsCode { get; set; }
        /// <summary>
        /// 诊室名称
        /// </summary>
        public string zsmc { get; set; }
        /// <summary>
        /// 叫号状态
        /// </summary>
        public int? calledstu { get; set; }
		/// <summary>
		/// 诊室楼层
		/// </summary>
		public string zslc { get; set; }
		/// <summary>
		/// 诊室房号
		/// </summary>
		public string zsfh { get; set; }
	}
}
