using System;

namespace Newtouch.HIS.Domain.DTO
{
    /// <summary>
    /// 住院病人患者（接口用）
    /// </summary>
    public class InPatientInfoDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? nl { get; set; }

        public string nlshow { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zzdCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zzdicd10 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zzdmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string csny { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ryrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cyrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bqmc { get; set; }
        public string cwmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zybzmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string wb { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string zjlx { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public string zjlxValue { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        public string zjh { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string idCardNo { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string sexValue { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 医生
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// 医生
        /// </summary>
        public string ysxm { get; set; }

        //危重级别？
        //入院方式？

        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// 卡类型
        /// </summary>
        public string CardType { get; set; }
        /// <summary>
        /// 卡类型
        /// </summary>
        public string CardTypeName { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        public string contPerName { get; set; }
        /// <summary>
        /// 紧急联系人电话
        /// </summary>
        public string contPerPhoneNum { get; set; }
        /// <summary>
        /// 紧急联系人关系
        /// </summary>
        public string contPerRel { get; set; }
        /// <summary>
        /// 紧急联系人关系
        /// </summary>
        public string contPerRelValue { get; set; }

        public string ryfs { get; set; }
        /// <summary>
        /// 是否已上传
        /// </summary>
        public string issc { get; set; }
        public string doctor { get; set; }
        public string inHosDays { get; set; }
        public string zzdmc2 { get; set; }
        public string zzdmc3 { get; set; }
        public string cyzd { get; set; }
        public string zy { get; set; }
        public string xzz { get; set; }
        public string cyfs { get; set; }
        public string jzDoctor { get; set; }
        public string pkbz { get; set; }

    }

    /// <summary>
    /// 住院病人患者（患者查询用）
    /// </summary>
    public class InPatientInfoVO : InPatientInfoDTO
    {
        /// <summary>
        /// 已结金额
        /// </summary>
        public decimal? yjfyje { get; set; }
        /// <summary>
        /// 未结金额
        /// </summary>
        public decimal? wjfyje { get; set; }
        /// <summary>
        /// 参保地编码
        /// </summary>
        public string cbdbm { get; set; }
        public string jzh { get; set; }

        public string mzmc { get; set; }
        public string rybq { get; set; }
        public string lxrdh { get; set; }
        public decimal? zyfy { get; set; }
        public decimal? yjj { get; set; }
        /// <summary>
        /// 卡性质:0 自费 1:医保
        /// </summary>
        public string brxzlb { get; set; }
    }

    public class patFeeVo
    {
        public decimal? je { get; set; }
        public string zyh { get; set; }
    }
}
