using System;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationManage
{
    public class InpatientSettPatInfoVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string  zyh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zybz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int patId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zjh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xb { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string brxz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? brxzbh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ybjylx { get; set; }
        public string brxzlb { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string  brxzmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ryrq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? zyyjjzh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 住院天数
        /// </summary>
        public decimal? zyts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? cyrq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ksCode { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int? nl
        {
            get
            {
                if (csny.HasValue)
                {
                    return (DateTime.Now.Year - csny.Value.Year) + 1;
                }
                return null;
            }
        }

        /***** 住院退费add *****/
        /// <summary>
        /// 入院诊断
        /// </summary>
        public string ryzdmc { get; set; }
        /// <summary>
        /// icd10
        /// </summary>
        public string ryzdicd10 { get; set; }
        /// <summary>
        /// 出院诊断
        /// </summary>
        public string cyzd { get; set; }
        /// <summary>
        /// 出院诊断ICD10
        /// </summary>
        public string cyzdicd10 { get; set; }
        /// <summary>
        /// 床位
        /// </summary>
        public string cw { get; set; }

        public string kh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CardType { get; set; }

        public string CardTypeName { get; set;}

		/// <summary>
		/// 账户余额
		/// </summary>
		public decimal? zhye { get; set; }
        /// <summary>
        /// 参保类别 来区分上海医保和国家医保
        /// </summary>
        public string cblb { get; set; }
    }
}
