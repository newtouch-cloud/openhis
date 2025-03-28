using System;

namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    /// <summary>
    /// 门诊发药查询页面 查询主表信息
    /// </summary>
    public class fyQueryMainInfo
    {
        public int cfmxId { get; set; }
        public string yp { get; set; }

        public string lyyf { get; set; }
        public int cfnm { get; set; }
        public string fph { get; set; }
        public string cfh { get; set; }
        public string xm { get; set; }
        public string kh { get; set; }
        public int nl { get; set; }
        public string brxzmc { get; set; }
        public DateTime fyrq { get; set; }
        public string fyry { get; set; }
        public string fyrymc { get; set; }
        public string fyck { get; set; }
        public DateTime sfrq { get; set; }
        public string ksmc { get; set; }
    }
}
