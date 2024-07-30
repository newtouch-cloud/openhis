using System;

namespace Newtouch.HIS.Sett.Request.Booking.Response
{
    public class CardInfoResp
    {
        /// <summary>
        /// 患者ID
        /// </summary>
        public int PatientNumber { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Brithday { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }
        
        /// <summary>
        /// 卡类型0自费卡，1医保卡
        /// </summary>
        public string CardType { get; set; }
        /// <summary>
        /// 卡类型名称
        /// </summary>
        public string CardTypeName { get; set; }
        /// <summary>
        /// 病人性质
        /// </summary>
        public string PatType { get; set; }
        /// <summary>
        /// 病人性质名称
        /// </summary>
        public string PatTypeName { get; set; }
        public string Zjh { get; set; }
    }

    public class PatCardInfoResp
    {
        /// <summary>
        /// 患者ID
        /// </summary>
        public int patid { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string xb { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime csrq { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// 卡类型0自费卡，1医保卡
        /// </summary>
        public string klx { get; set; }
        /// <summary>
        /// 卡类型名称
        /// </summary>
        public string klxmc { get; set; }
        /// <summary>
        /// 病人性质
        /// </summary>
        public string brxz { get; set; }
        /// <summary>
        /// 病人性质名称
        /// </summary>
        public string brxzmc { get; set; }
        public string zjh { get; set; }
    }
}
