
using System;

namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    /// <summary>
    /// 补打和重打发票  结算明细对象
    /// </summary>
    public class OutPatientReprintOrSuppPrintSettleDetailVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string sfxm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? dj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? fwfdj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? sl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? je { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? zfbl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int xmnm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zfxz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ghnm { get; set; }
        /// <summary>
        /// 单价和服务费单价
        /// </summary>
        public decimal? djandfwfdj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int patid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string yp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// 单价单位
        /// </summary>
        public string djdw { get; set; }
        /// <summary>
        /// 领药窗口
        /// </summary>
        public string lyck { get; set; }
        /// <summary>
        /// 拆零数
        /// </summary>
        public short? cls { get; set; }   //表是smallint
        /// <summary>
        /// 成组号
        /// </summary>
        public string czh { get; set; }
        /// <summary>
        /// 药品标志代码
        /// </summary>
        public string ypbzdm { get; set; }
        /// <summary>
        /// 处方内码
        /// </summary>
        public int cfnm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string kh { get; set; }

    }
}
