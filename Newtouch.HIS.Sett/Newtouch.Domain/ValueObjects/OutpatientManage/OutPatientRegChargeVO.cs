
using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 门诊挂号收费查询
    /// </summary>
    public class OutPatientRegChargeVO
    {
        #region 挂号收费查询的主记录

        /// <summary>
        /// 病例号
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// 结算内码
        /// </summary>
        public int jsnm { get; set; }
        /// <summary>
        /// 查询结算内码
        /// </summary>
        public int cxjsnm { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 挂号内码
        /// </summary>
        public int ghnm { get; set; }
        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }
        /// <summary>
        /// 建档人员
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// 人员名称
        /// </summary>
        public string rymc { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 收费项目名称
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// 医生名称
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// 是否退回
        /// </summary>
        public string isreturned { get; set; }
        /// <summary>
        /// 老的发票号
        /// </summary>
        public string oldfph { get; set; }
        /// <summary>
        /// isxfph
        /// </summary>
        public string isxfph { get; set; }
        /// <summary>
        /// 结算标志
        /// </summary>
        public string jsbz { get; set; }
        /// <summary>
        /// tCreatorCode
        /// </summary>
        public string tCreatorCode { get; set; }

        #endregion

    }
}
