
using System;

namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    public class PrintSettleVO
    {
        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string danwei { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? dj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? zfbl { get; set; }
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
        public int ghnm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sfxm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }
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
        public string yp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? fwfdj { get; set; }
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

        public string kh { get; set; }
    }
}
