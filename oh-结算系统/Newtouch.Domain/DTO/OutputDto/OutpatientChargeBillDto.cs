using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    /// <summary>
    /// 门诊收费票据
    /// </summary>
    public class OutpatientChargeBillDto
    {
        public List<mzsfpj_dlInfo> mzsfpj_dlInfo { get; set; }
        public orgInfo orgInfo { get; set; }
        public List<mzsfpj_xmInfo> mzsfpj_xmInfo { get; set; }
    }
    public class mzsfpj_dlInfo {
        public int jsmxnm { get; set; }
        public decimal sl { get; set; }
        public decimal jyje { get; set; }
        public decimal dj { get; set; }
        public decimal zje { get; set; }
        public int feeType { get; set; }
        public string dw { get; set; }
        public string cfh { get; set; }
        public string sfxmCode { get; set; }
        public string mc { get; set; }
        public string dlmc { get; set; }
        public string ypgg { get; set; }
    }

    public class orgInfo {
        public string Name { get; set; }
    }
    public class mzsfpj_xmInfo
    {
        public string fph { get; set; }
        public decimal zje { get; set; }
        public decimal zkhzje { get; set; }
        public string zkhdxje { get; set; }
        public decimal zl { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public decimal ysk { get; set; }
        public string sky { get; set; }
        public DateTime CreateTime { get; set; }
        public string brxzmc { get; set; }
        public string xjzffs { get; set; }
        public decimal XJZF { get; set; }
        public string mzh { get; set; }
        public decimal TCZF { get; set; }
        public decimal GWYBZ { get; set; }
        public decimal DBZF { get; set; }
        public decimal MZBC { get; set; }
        public string JSBC { get; set; }
        public string DDBC { get; set; }
        public decimal ZHZF { get; set; }
    }
    /// <summary>
    /// 重庆cs端发票打印
    /// </summary>
    public class CqOutpatientChargeBillDto
    {
        public List<Cqmzsfpj_dlInfo> mzsfpj_dlInfo { get; set; }
        public patInfo patInfo { get; set; }
    }
    public class Cqmzsfpj_dlInfo
    {
        public string a { get; set; }
        public decimal jyje { get; set; }
        public string kh { get; set; }
        public string CreatorCode { get; set; }

        public string ksName { get; set; }
        public string dlmc { get; set; }
        public string patid { get; set; }
        public string mzh { get; set; }
        public string fph { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string nl { get; set; }
        public string csny { get; set; }
        public string sfrq { get; set; }
        public string feeType { get; set; }
        public string zje { get; set; }
        public string cqtczf { get; set; }
        public string zhzf { get; set; }
        public string gwybz { get; set; }
        public string delpje { get; set; }
        public string lsqfxgwyff { get; set; }

        public string zhdy { get; set; }

        public string sytc { get; set; }

        public string cqxjzf { get; set; }

        public string zhye { get; set; }
        public string dbzddyljgdz { get; set; }
        public string mzjzje { get; set; }
        public string qtzc { get; set; }
    }
    public class patInfo
    {
        public string cjr { get; set; }

        public string xm { get; set; }
    }
}
