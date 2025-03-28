using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class PatientBasicAndCardInfoVO
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }

        #region 病人基本信息
        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime csny { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        public string zjh { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string xb { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public Int16 nl { get; set; }
        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string yddh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }
        /// <summary>
        /// 地域
        /// </summary>
        public string dy { get; set; }
        /// <summary>
        /// 婚否
        /// </summary>
        public byte? hf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zjlx { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string wechat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zjlxfs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cs_sheng { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cs_shi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cs_xian { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string hu_sheng { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string hu_shi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string hu_xian { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string hu_dz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xian_sheng { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xian_shi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xian_xian { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xian_dz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xl { get; set; }

        /// <summary>
        /// xt_gj.gj
        /// </summary>
        public string gj { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string dh { get; set; }
        #endregion
    }
}
