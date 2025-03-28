
using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class OutPatientBasicInfoVO
    {
        #region 系统基本信息
        /// <summary>
        /// 病人id
        /// </summary>
        public int patid { get; set; }
        public string brxz { get; set; }
        public string brxzmc { get; set; }
        public string pzh { get; set; }
        public string zjh { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string sfz { get; set; }
        public string csny { get; set; }
        public short nl { get; set; }
        public string blh { get; set; }
        public string dh { get; set; }
        public string dy { get; set; }
        public int? dybh { get; set; }
        public byte? hf { get; set; }
        public string zjlx { get; set; }
        public string brly { get; set; }

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

        public string phone { get; set; }

        public string lxrgx { get; set; }
        public string lxr { get; set; }
        public string lxrdh { get; set; }
        #endregion

        #region 系统大病项目
        public string db { get; set; }
        public string dbzd { get; set; }

        #endregion

        #region 门诊挂号
        public byte? fzbz { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }
        #endregion

        #region 卡表
        public string cardtype { get; set; }
        #endregion


    }
}
