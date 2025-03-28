using Newtouch.Common;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 住院病人信息 VO
    /// </summary>
    public class HospPatientInfoVO
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// 状态标志
        /// </summary>
        public string zybz { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// 病人性质
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// 病人性质名称
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// 病人的医保交易类型
        /// </summary>
        public string ybjylx { get; set; }

        /// <summary>
        /// 入院日期
        /// </summary>
        public DateTime? ryrq { get; set; }

        /// <summary>
        /// 出院日期
        /// </summary>
        public DateTime? cyrq { get; set; }

        /// <summary>
        /// 凭证号
        /// </summary>
        public string pzh { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// patid，用xt_brjbxx表赋值
        /// </summary>
        public int? patid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 住院预交金 账户
        /// </summary>
        public int? zyyjjzh { get; set; }

        /// <summary>
        /// 住院预交金 账户编号
        /// </summary>
        public int? zyyjjzhbh { get; set; }

        /// <summary>
        /// 住院预交金 账户 帐户性质
        /// </summary>
        public string zyyjjzhzhxz { get; set; }

        /// <summary>
        /// 入院诊断 诊断编号
        /// </summary>
        public string ryzdCode { get; set; }

        /// <summary>
        /// 入院诊断 icd10
        /// </summary>
        public string ryzdicd10 { get; set; }

        /// <summary>
        /// 入院诊断 名称
        /// </summary>
        public string ryzdmc { get; set; }

        /// <summary>
        /// 住院天数
        /// </summary>
        public decimal? zyts { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public string zjlx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// 证件类型 名称
        /// </summary>
        public string zjlxmc
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(zjlx))
                {
                    int intZjlx = (int)EnumZJLX.sfz;
                    int.TryParse(this.zjlx, out intZjlx);
                    if (Enum.IsDefined(typeof(EnumZJLX), intZjlx))
                    {
                        return ((EnumZJLX)intZjlx).GetDescription();
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// 手机号
        /// </summary>
        public string phone { get; set; }

        public string ysgh { get; set; }
        public string ysmc { get; set; }

        public DateTime? cqrq { get; set; }
    }
}
