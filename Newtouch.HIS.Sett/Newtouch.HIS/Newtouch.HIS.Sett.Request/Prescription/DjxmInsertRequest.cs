using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// 处方状态查询
    /// </summary>
    public class DjxmInsertRequest : RequestBase
    {
        public string zyh { get; set; }
        public string sfxm { get; set; }
        public string dl { get; set; }
        public string ys { get; set; }
        public string ks { get; set; }
        public string ysmc { get; set; }
        public string ksmc { get; set; }
        public decimal dj { get; set; }
        public string bq { get; set; }
        public string cw { get; set; }
        public decimal sl { get; set; }
        public string dw { get; set; }
        public string zxks { get; set; }
        public string zfxz { get; set; }
        public decimal zfbl { get; set; }
        public string OrganizeId { get; set; }
        public string yzh { get; set; }
    }
}
