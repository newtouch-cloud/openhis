using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class PatientCenterVO
    {
        public List<PatientBasicAndCardInfoVO> basic { get; set; }
        public List<HosPatientVo> zyinfolist { get; set; }
        public List<OutpatientVO> mzinfolist { get; set; }
    }
    public class HosPatientVo : HospPatientInfoVO
    {
        public string nlshow { get; set; }
        public string cwmc { get; set; }
        /// <summary>
        /// 住院次序
        /// </summary>
        public int zycx { get; set; }
        /// <summary>
        /// 入院科室
        /// </summary>
        public string ryks { get; set; }
        public string ryksmc { get; set; }
        /// <summary>
        /// 入院病区
        /// </summary>
        public string ryward { get; set; }
        public string rywardname { get; set; }
        /// <summary>
        /// 当前病区
        /// </summary>
        public string ward { get; set; }
        public string wardname { get; set; }
        /// <summary>
        /// 主治医生
        /// </summary>
        public string zzys { get; set; }
        public string zzysmc { get; set; }
        /// <summary>
        /// 住院医生
        /// </summary>
        public string zyys { get; set; }
        public string zyysmc { get; set; }
        /// <summary>
        /// 主任医生
        /// </summary>
        public string zrys { get; set; }
        public string zrysmc { get; set; }
        /// <summary>
        /// 过敏史
        /// </summary>
        public string gms { get; set; }
        public string rysj { get; set; }
        public string cqsj { get; set; }
        public string cysj { get; set; }
        public string cyjdrq2 { get; set; }
        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal? zhye { get; set; }
        public decimal? zfy { get; set; }
        /// <summary>
        /// 待缴费用
        /// </summary>
        public decimal? djfy { get; set; }
        /// <summary>
        /// 已结费用
        /// </summary>
        public decimal? yjfy { get; set; }
        public string cyzddm { get; set; }
        public string cyzdmc { get; set; }
    }


    /// <summary>
    /// 门诊患者视图
    /// </summary>
    public class OutpatientVO
    {
        /// <summary>
		/// 门诊号
		/// </summary>
		public string mzh { get; set; }

        public int patid { get; set; }
        public string brxz { get; set; }
        public int? ghnm { get; set; }
        /// <summary>
        /// 挂号来源
        /// </summary>
        public string ghly { get; set; }
        public string mjzbz { get; set; }
        public string ks { get; set; }
        public string ys { get; set; }
        public string zdicd10 { get; set; }
        public string zdmc { get; set; }
        public string nlshow { get; set; }

        public DateTime? ghrq { get; set; }

        /// <summary>
        /// 结算内码
        /// </summary>
        public int? jsnm { get; set; }
        /// <summary>
        /// 收费日期
        /// </summary>
        public DateTime? sfrq { get; set; }
        /// <summary>
        /// 结算人员
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 结算人员姓名
        /// </summary>
        public string CreatorUserName { get; set; }

        /// <summary>
        /// 门诊挂号 或 门诊收费记账
        /// </summary>
        public string jslx { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 医保结算 医保结算号
        /// </summary>
        public string ybjsh { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal jszje { get; set; }

        /// <summary>
        /// 结算支付
        /// </summary>
        public decimal jsxjzf { get; set; }
        /// <summary>
        /// 现金支付方式名称
        /// </summary>
        public string xjzffsmc { get; set; }

        public string jzid { get; set; }
        public string pch { get; set; }
        public string yllb { get; set; }
        public string bzbm { get; set; }
    }


    public class PatOperation {

    }
}
