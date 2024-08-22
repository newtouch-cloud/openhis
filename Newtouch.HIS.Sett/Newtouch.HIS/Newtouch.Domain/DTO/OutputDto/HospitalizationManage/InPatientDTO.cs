using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage
{
    public class InPatientDTO
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
        //public string zzdCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public string zzdicd10 { get; set; }

        /// <summary>
        /// 出院诊断
        /// </summary>
        //public string zzdmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public string cyqk { get; set; }
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
        /// 入院诊断
        /// </summary>
        public IList<ryzd> ryzd { get; set; }
        /// <summary>
        /// 出院诊断
        /// </summary>
        public IList<cyzd> cyzd { get; set; }
    }

    public class ryzd {
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
        public string zdpx { get; set; }
    }

    public class cyzd
    {
        /// <summary>
        /// 
        /// </summary>
        public string zzdCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zzdmc { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string cyqk { get; set; }
    }
}
