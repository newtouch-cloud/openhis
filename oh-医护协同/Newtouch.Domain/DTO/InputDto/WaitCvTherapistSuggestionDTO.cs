using System;

namespace Newtouch.Domain.DTO
{
    /// <summary>
    /// 待转换治疗建议
    /// </summary>
    public class WaitCvTherapistSuggestionDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public string jyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmCode { get; set; }
        public string sfxmmc { get; set; }

        public decimal mczll { get; set; }
        public decimal sl { get; set; }

        public string pc { get; set; }
        public string pcmc { get; set; }
	    public string zxcs { get; set; }
	    public string zxzq { get; set; }
	    public string zxzqdw { get; set; }

		public string zxks { get; set; }
        public string zxksmc { get; set; }

        /// <summary>
        /// 开立日期
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 开立人员
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? dwjls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? jjcl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }

        public string zhbz { get; set; }
    }
}

