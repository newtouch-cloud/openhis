using System;

namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    public class PatiChargeLogicVO
    {
        public int brsfsfbh { get; set; }
        public string brxz { get; set; }
        public string brxzmc { get; set; }

        public string dl { get; set; }

        public string dlmc { get; set; }

        public string sfxm { get; set; }

        public string sfxmmc { get; set; }

        public byte sfjb { get; set; }

        public string fyfw { get; set; }

        public decimal zfbl { get; set; }

        /// <summary>
        /// 确定按自负比例计算出来的自负部分的性质   0 自负（同医保交易后自负现金）   1 自理
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 0 所有费用皆使用此比例，不判断上限
        /// </summary>
        public decimal fysx { get; set; }

        /// <summary>
        /// 0：通用，1：门诊，2：住院
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
