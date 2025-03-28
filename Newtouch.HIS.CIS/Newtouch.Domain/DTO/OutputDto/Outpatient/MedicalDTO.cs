using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Domain.DTO.OutputDto
{
    [XmlRoot("INFO")]
    public class MedicalDTO
    {
        public MedicalMESSAGE MESSAGE { get; set; }
        public MedicalDATA DATA { get; set; }
    }
    public class MedicalMESSAGE
    {
        public string VERSION { get; set; }
    }
    public class MedicalDATA
    {
        public MedicalBEAN BEAN { get; set; }
    }
    public class MedicalBEAN
    {
        public string CRYZT { get; set; }
        public string YLJG_DM { get; set; }
        public string YLJG_MC { get; set; }
        public string ZYH { get; set; }
        public string BAH { get; set; }
        public string ZYCS { get; set; }
        public string RYSJ { get; set; }
        public string CYSJ { get; set; }
        public string JKKH { get; set; }
        public string YLFFFS_BM { get; set; }
        public string YLFFFS_MC { get; set; }
        public string XM { get; set; }
        public string XB_BM { get; set; }
        public string XB_MC { get; set; }
        public string CSRQ { get; set; }
        public string NL { get; set; }
        public string GJ_BM { get; set; }
        public string GJ_MC { get; set; }
        public string HY_BM { get; set; }
        public string HY_MC { get; set; }
        public string ZY_BM { get; set; }
        public string ZY_MC { get; set; }
        public string MZ_BM { get; set; }
        public string MZ_MC { get; set; }
        public string ZJLX_BM { get; set; }
        public string ZJLX_MC { get; set; }
        public string ZJHM { get; set; }
        public string GJS_BM { get; set; }
        public string GJS_MC { get; set; }
        public string CSDZ_SHENG { get; set; }
        public string CSDZ_SHI { get; set; }
        public string CSDZ_QX { get; set; }
        public string CSDZ_XXDZ { get; set; }
        public string HKDZ_SHENG { get; set; }
        public string HKDZ_SHI { get; set; }
        public string HKDZ_QX { get; set; }
        public string HKDZ_XXDZ { get; set; }
        public string HKDZ_YZBM { get; set; }
        public string XZZ_SHENG { get; set; }
        public string XZZ_SHI { get; set; }
        public string XZZ_QX { get; set; }
        public string XZZ_XXDZ { get; set; }
        public string XZZ_DH { get; set; }
        public string XZZ_YZBM { get; set; }
        public string GZDW_JDZ { get; set; }
        public string GZDW_DH { get; set; }
        public string GZDW_YZBM { get; set; }
        public string LXR_XM { get; set; }
        public string LXR_YHZGX_BM { get; set; }
        public string LXR_YHZGX_MC { get; set; }
        public string LXRDZ_SHENG { get; set; }
        public string LXRDZ_SHI { get; set; }
        public string LXRDZ_QX { get; set; }
        public string LXRDZ_XXDZ { get; set; }
        public string LXR_DH { get; set; }
        public string SFWRJSS_BM { get; set; }
        public string SFWRJSS_MC { get; set; }
        public string RYTJ_BM { get; set; }
        public string RYTJ_MC { get; set; }
        public string RYKB_BM { get; set; }
        public string RYKB_MC { get; set; }
        public string RYBF { get; set; }
        public List<MedicalZKKB> ZKKB { get; set; }
        public string CYKB_BM { get; set; }
        public string CYKB_MC { get; set; }
        public string CYBF { get; set; }
        public string SJZYTS { get; set; }
        public string MJZZD_BM { get; set; }
        public string MJZZD_MC { get; set; }
        public string CYZYZD_BM { get; set; }
        public string CYZYZD_MC { get; set; }
        public string CYZYZD_RYBQ_BM { get; set; }
        public string CYZYZD_RYBQ_MC { get; set; }
        public List<MedicalCYQTZD> CYQTZD { get; set; }
        public string BLZD_BM { get; set; }
        public string BLZD_MC { get; set; }
        public string BLH { get; set; }
        public string SSZDWBYY_BM { get; set; }
        public string SSZDWBYY_MC { get; set; }
        public string YWYWGM_BM { get; set; }
        public string YWYWGM_MC { get; set; }
        public string GMYWMC { get; set; }
        public string KZR_BM { get; set; }
        public string KZR_MC { get; set; }
        public string ZRYS_BM { get; set; }
        public string ZRYS_MC { get; set; }
        public string ZZYS_BM { get; set; }
        public string ZZYS_MC { get; set; }
        public string ZYYS_BM { get; set; }
        public string ZYYS_MC { get; set; }
        public string ZRHS_BM { get; set; }
        public string ZRHS_MC { get; set; }
        public string JXYS_MC { get; set; }
        public string SXYS_MC { get; set; }
        public string BMY_BM { get; set; }
        public string BMY_MC { get; set; }
        public string BAZL_BM { get; set; }
        public string BAZL_MC { get; set; }
        public string ZKYS_BM { get; set; }
        public string ZKYS_MC { get; set; }
        public string ZKHS_BM { get; set; }
        public string ZKHS_MC { get; set; }
        public string ZKRQ { get; set; }
        public string SWHZSJ_BM { get; set; }
        public string SWHZSJ_MC { get; set; }
        public string ABOXX_BM { get; set; }
        public string ABOXX_MC { get; set; }
        public string RHXX_BM { get; set; }
        public string RHXX_MC { get; set; }
        public string ZYSSCZ_BM { get; set; }
        public string ZYSSCZ_MC { get; set; }
        public string ZYSSCZ_RQ { get; set; }
        public string ZYSSCZ_JB_BM { get; set; }
        public string ZYSSCZ_JB_MC { get; set; }
        public string ZYSSCZ_SZ { get; set; }
        public string ZYSSCZ_YZ { get; set; }
        public string ZYSSCZ_EZ { get; set; }
        public string ZYSSCZ_QKYHDJ_BM { get; set; }
        public string ZYSSCZ_QKYHDJ_MC { get; set; }
        public string ZYSSCZ_MZFS_BM { get; set; }
        public string ZYSSCZ_MZFS_MC { get; set; }
        public string ZYSSCZ_MZYS { get; set; }
        public List<MedicalQTSSCZ> QTSSCZ { get; set; }
        public string NLBZYZSDNL { get; set; }
        public string XSECSTZ1 { get; set; }
        public string XSECSTZ2 { get; set; }
        public string XSECSTZ3 { get; set; }
        public string XSECSTZ4 { get; set; }
        public string XSECSTZ5 { get; set; }
        public string XSERYTZ { get; set; }
        public string LNSSHZ_RYQ_HMSJT { get; set; }
        public string LNSSHZ_RYQ_HMSJXS { get; set; }
        public string LNSSHZ_RYQ_HMSJFZ { get; set; }
        public string LNSSHZ_RYH_HMSJT { get; set; }
        public string LNSSHZ_RYH_HMSJXS { get; set; }
        public string LNSSHZ_RYH_HMSJFZ { get; set; }
        public string YCHXJSYSJ { get; set; }

        public string SFYCY31RNZZYJH_BM { get; set; }
        public string SFYCY31RNZZYJH_MC { get; set; }
        public string CY31TZZYJHMD { get; set; }
        public string LYFS_BM { get; set; }
        public string LYFS_MC { get; set; }
        public string YZZYJG_MC { get; set; }
        public string ZYZFY { get; set; }
        public string ZYZFY_ZFJE { get; set; }
        public string YBYLFWF { get; set; }
        public string YBZLCZF { get; set; }
        public string HLF { get; set; }
        public string ZHYLFWLQTFY { get; set; }
        public string BLZDF { get; set; }
        public string SYSZDF { get; set; }
        public string YXXZDF { get; set; }
        public string LCZDXMF { get; set; }
        public string FSSZLXMF { get; set; }
        public string FSSZLXMF_LCWLZLF { get; set; }
        public string SSZLF { get; set; }
        public string SSZLF_MZF { get; set; }
        public string SSZLF_SSF { get; set; }
        public string KFF { get; set; }
        public string ZYZLF { get; set; }
        public string XYF { get; set; }
        public string XYF_KJYWF { get; set; }
        public string ZCHYF { get; set; }
        public string ZCAYF { get; set; }
        public string XF { get; set; }
        public string BDBLZPF { get; set; }
        public string QDBLZPF { get; set; }
        public string NXYZLZPF { get; set; }
        public string XBYZLZPF { get; set; }
        public string JCYYCXYYCLF { get; set; }
        public string ZLYYCXYYCLF { get; set; }
        public string SSYYCXYYCLF { get; set; }
        public string QTF { get; set; }

    }
    public class MedicalZKKB
    {
        public List<MedicalZKKBROW> ROW;
    }
    public class MedicalZKKBROW
    {
        
    }
    public class MedicalCYQTZD
    {
        public List<MedicalCYQTZDROW> ROW;
    }
    public class MedicalCYQTZDROW
    {
       
    }
    public class MedicalQTSSCZ
    {
        public List<MedicalQTSSCZROW> ROW;
    }
    public class MedicalQTSSCZROW
    {

    }
}
