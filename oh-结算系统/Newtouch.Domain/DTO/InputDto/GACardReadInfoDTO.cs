using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO
{
    /// <summary>
    /// 贵安 医保读卡 卡信息
    /// </summary>
    public class GACardReadInfoDTO
    {
        /// <summary>
        /// 个人编号
        /// </summary>
        public string prm_aac001 { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string prm_aac002 { get; set; }

        /// <summary>
        /// 分中心编号
        /// </summary>
        public string prm_yab003 { get; set; }

		/// <summary>
		/// ////////////////////////重庆医保使用字段
		/// </summary>
		public string sfzh { get; set; }
		public string kh { get; set; }
		public string cblb { get; set; }
    }
}
