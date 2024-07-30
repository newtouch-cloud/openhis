using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Domain.DTO.OutputDto
{
    [XmlRoot("INFO")]
    public class DRGDIPDTO
    {
        public DRGDIPMESSAGE MESSAGE { get; set; }
        public DRGDIPDATA DATA { get; set; }
    }
    public class DRGDIPMESSAGE
    {
        public string VERSION { get; set; }
    }
    public class DRGDIPDATA
    {
        public DRGDIPBEAN BEAN { get; set; }
    }
    public class DRGDIPBEAN
    {
        public DRGDIPJBXX JBXX { get; set; }
        public DRGDIPZYXX ZYXX { get; set; }
        public DRGDIPSFXX SFXX { get; set; }
        public DRGDIPQTXX QTXX { get; set; }
    }
    public class DRGDIPJBXX
    {
        public string CRYZT { get; set; }
        public string SHENG_BM { get; set; }
        public string SHENG_MC { get; set; }
        public string SHI_BM { get; set; }
        public string SHI_MC { get; set; }
        public string ZYH_MZH { get; set; }
        public string DDYLJG_BM { get; set; }
        public string DDYLJG_MC { get; set; }
        public string YBJSDJ_BM { get; set; }
        public string YBJSDJ_MC { get; set; }
        public string YBBH { get; set; }
        public string BAH { get; set; }
        public string SBSJ { get; set; }
        public string XM { get; set; }
        public string XB_BM { get; set; }
        public string XB_MC { get; set; }
        public string CSRQ { get; set; }
        public string NL_S { get; set; }
        public string NL_T { get; set; }
        public string GJ_BM { get; set; }
        public string GJ_MC { get; set; }
        public string MZ_MC { get; set; }
        public string MZ_BM { get; set; }
        public string HZZJ_LB_BM { get; set; }
        public string HZZJ_LB_MC { get; set; }
        public string HZZJ_HM { get; set; }
        public string HZZY_BM { get; set; }
        public string HZZY_MC { get; set; }
        public string XZZ_SHENG { get; set; }
        public string XZZ_SHI { get; set; }
        public string XZZ_QX { get; set; }
        public string XZZ_XXDZ { get; set; }
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
        public string YBLX_BM { get; set; }
        public string YBLX_MC { get; set; }
        public string TSRYLX_BM { get; set; }
        public string TSRYLX_MC { get; set; }
        public string CBD { get; set; }
        public string XSE_RYLX_BM { get; set; }
        public string XSE_RYLX_MC { get; set; }
        public string XSE_CSTZ { get; set; }
        public string XSE_RYTZ { get; set; }
    }
    public class DRGDIPZYXX{
        public string ZYYLLX_BM { get; set; }
        public string ZYYLLX_MC { get; set; }
        public string RYTJ_BM { get; set; }
        public string RYTJ_MC { get; set; }
        public string ZLLB_BM { get; set; }
        public string ZLLB_MC { get; set; }
        public string RYSJ { get; set; }
        public string RYKB_BM { get; set; }
        public string RYKB_MC { get; set; }
        public string CYSJ { get; set; }
        public string CYKB_BM { get; set; }
        public string CYKB_MC { get; set; }
        public string SJZYTS { get; set; }
        public string MJZ_XYZD_BM { get; set; }
        public string MJZ_XYZD_MC { get; set; }
        public string MJZ_ZYZD_BM { get; set; }
        public string MJZ_ZYZD_MC { get; set; }
        public string CYXY_ZYZD_BM { get; set; }
        public string CYXY_ZYZD_MC { get; set; }
        public string CYXY_ZYZD_RYBQ_BM { get; set; }
        public string CYXY_ZYZD_RYBQ_MC { get; set; }
        public string CYZY_ZYZD_BM { get; set; }
        public string CYZY_ZYZD_MC { get; set; }
        public string CYZY_ZYZD_RYBQ_BM { get; set; }
        public string CYZY_ZYZD_RYBQ_MC { get; set; }
        public string CYZY_ZYZH_BM { get; set; }
        public string CYZY_ZYZH_MC { get; set; }
        public string CYZY_ZYZH_RYBQ_BM { get; set; }
        public string CYZY_ZYZH_RYBQ_MC { get; set; }
        public string ZDDMJS { get; set; }
        public string ZYSSCZ_BM { get; set; }
        public string ZYSSCZ_MC { get; set; }
        public string ZYSSCZ_MZFS_BM { get; set; }
        public string ZYSSCZ_MZFS_MC { get; set; }
        public string ZYSSCZ_SZYS_BM { get; set; }
        public string ZYSSCZ_SZYS_MC { get; set; }
        public string ZYSSCZ_MZYS_BM { get; set; }
        public string ZYSSCZ_MZYS_MC { get; set; }
        public string ZYSSCZ_KSSJ { get; set; }
        public string ZYSSCZ_JSSJ { get; set; }
        public string ZYSSCZ_MZKSSJ { get; set; }
        public string ZYSSCZ_MZJSSJ { get; set; }
        public string SSJCZDMJS { get; set; }
        public string HXJSYSJ_T { get; set; }
        public string HXJSYSJ_XS { get; set; }
        public string HXJSYSJ_FZ { get; set; }
        public string LNSSHZ_RYQ_HMSJ_T { get; set; }
        public string LNSSHZ_RYQ_HMSJ_XS { get; set; }
        public string LNSSHZ_RYQ_HMSJ_FZ { get; set; }
        public string LNSSHZ_RYH_HMSJ_T { get; set; }
        public string LNSSHZ_RYH_HMSJ_XS { get; set; }
        public string LNSSHZ_RYH_HMSJ_FZ { get; set; }
        public string TJHLTS { get; set; }
        public string YJHLTS { get; set; }
        public string EJHLTS { get; set; }
        public string SJHLTS { get; set; }
        public string LYFS_BM { get; set; }
        public string LYFS_MC { get; set; }
        public string ZYNJSJG_BM { get; set; }
        public string ZYNJSJG_MC { get; set; }
        public string SFY31TNZZYJH_BM { get; set; }
        public string SFY31TNZZYJH_MC { get; set; }
        public string CY31TNZZYJHMD { get; set; }
        public string ZZYS_BM { get; set; }
        public string ZZYS_MC { get; set; }
        public string ZRHS_BM { get; set; }
        public string ZRHS_MC { get; set; }
    }
    public class DRGDIPSFXX {
        public string YWLSH { get; set; }
        public string PJDM { get; set; }
        public string PJHM { get; set; }
        public string JSQJ_KSRQ { get; set; }
        public string JSQJ_JSRQ { get; set; }
        public string CWF_ZJE { get; set; }
        public string CWF_ZFJE { get; set; }
        public string CWF_JLJE { get; set; }
        public string CWF_YLJE { get; set; }
        public string CWF_QTJE { get; set; }
        public string ZCF_ZJE { get; set; }
        public string ZCF_JLJE { get; set; }
        public string ZCF_YLJE { get; set; }
        public string ZCF_ZFJE { get; set; }
        public string ZCF_QTJE { get; set; }
        public string JCF_ZJE { get; set; }
        public string JCF_JLJE { get; set; }
        public string JCF_YLJE { get; set; }
        public string JCF_ZFJE { get; set; }
        public string JCF_QTJE { get; set; }
        public string HYF_ZJE { get; set; }
        public string HYF_JLJE { get; set; }
        public string HYF_YLJE { get; set; }
        public string HYF_ZFJE { get; set; }
        public string HYF_QTJE { get; set; }
        public string ZLF_ZJE { get; set; }
        public string ZLF_JLJE { get; set; }
        public string ZLF_YLJE { get; set; }
        public string ZLF_ZFJE { get; set; }
        public string ZLF_QTJE { get; set; }
        public string SSF_ZJE { get; set; }
        public string SSF_JLJE { get; set; }
        public string SSF_YLJE { get; set; }
        public string SSF_ZFJE { get; set; }
        public string SSF_QTJE { get; set; }
        public string HLF_ZJE { get; set; }
        public string HLF_JLJE { get; set; }
        public string HLF_YLJE { get; set; }
        public string HLF_ZFJE { get; set; }
        public string HLF_QTJE { get; set; }
        public string WSCLF_ZJE { get; set; }
        public string WSCLF_JLJE { get; set; }
        public string WSCLF_YLJE { get; set; }
        public string WSCLF_ZFJE { get; set; }
        public string WSCLF_QTJE { get; set; }
        public string XYF_ZJE { get; set; }
        public string XYF_JLJE { get; set; }
        public string XYF_YLJE { get; set; }
        public string XYF_ZFJE { get; set; }
        public string XYF_QTJE { get; set; }
        public string ZYYP_ZJE { get; set; }
        public string ZYYP_JLJE { get; set; }
        public string ZYYP_YLJE { get; set; }
        public string ZYYP_ZFJE { get; set; }
        public string ZYYP_QTJE { get; set; }
        public string ZCYF_ZJE { get; set; }
        public string ZCYF_JLJE { get; set; }
        public string ZCYF_YLJE { get; set; }
        public string ZCYF_ZFJE { get; set; }
        public string ZCYF_QTJE { get; set; }
        public string YBZLF_ZJE { get; set; }
        public string YBZLF_JLJE { get; set; }
        public string YBZLF_YLJE { get; set; }
        public string YBZLF_ZFJE { get; set; }
        public string YBZLF_QTJE { get; set; }
        public string GHF_ZJE { get; set; }
        public string GHF_JLJE { get; set; }
        public string GHF_YLJE { get; set; }
        public string GHF_ZFJE { get; set; }
        public string GHF_QTJE { get; set; }
        public string QTF_ZJE { get; set; }
        public string QTF_JLJE { get; set; }
        public string QTF_YLJE { get; set; }
        public string QTF_ZFJE { get; set; }
        public string QTF_QTJE { get; set; }
        public string ABZSF_BZBM { get; set; }
        public string ABZSF_BZMC { get; set; }
        public string ABZSF_ZJE { get; set; }
        public string ABZSF_JLJE { get; set; }
        public string ABZSF_YLJE { get; set; }
        public string ABZSF_ZFJE { get; set; }
        public string ABZSF_QTJE { get; set; }
        public string ZEHJ_ZJE { get; set; }
        public string ZEHJ_JLJE { get; set; }
        public string ZEHJ_YLJE { get; set; }
        public string ZEHJ_ZFJE { get; set; }
        public string ZEHJ_QTJE { get; set; }
        public string YBTCJJZF { get; set; }
        public string BCYLBXZF_ZGDEBZ { get; set; }
        public string BCYLBXZF_JMDBBX { get; set; }
        public string BCYLBXZF_GWYYLBZ { get; set; }
        public string YLJZZF { get; set; }
        public string GRFD_GRZFU { get; set; }
        public string GRFD_GRZFE { get; set; }
        public string QTZF_QYBC { get; set; }
        public string QTZF_SYBX { get; set; }
        public string QTZF_QTBCZF { get; set; }
        public string GRZF_GRZHZF { get; set; }
        public string GRZF_GRXJZF { get; set; }
        public string YBZFFS_BM { get; set; }
        public string YBZFFS_MC { get; set; }
    }
    public class DRGDIPQTXX {
        public string DDYLJG_TBBM_BM { get; set; }
        public string DDYLJG_TBBM_MC { get; set; }
        public string DDYLJG_TBR_BM { get; set; }
        public string DDYLJG_TBR_MC { get; set; }
        public string YBJBJG_BM { get; set; }
        public string YBJBJG_MC { get; set; }
        public string YBJBJG_JBR_BM { get; set; }
        public string YBJBJG_JBR_MC { get; set; }
    }

}
