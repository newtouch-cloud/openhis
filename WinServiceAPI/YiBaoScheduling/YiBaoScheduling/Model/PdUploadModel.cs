using CQYiBaoInterface.Models.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiBaoScheduling.Model
{
    public class PdUploadModel
    {
    }
    #region 住院就诊记录表
    public class TB_YL_ZY_Medical_Record : SqlBase
    {
        public string JZLSH { get; set; }
        public string WSJGDM { get; set; }
        public string YLJGDM { get; set; }
        public string CISID { get; set; }
        public string BAH { get; set; }
        public string KH { get; set; }
        public string HZXM { get; set; }
        public string CSRQ { get; set; }
        public int XB { get; set; }
        public string JZLX { get; set; }
        public string BXLX { get; set; }
        public string TXBZ { get; set; }
        public string WDBZ { get; set; }
        public string JZKSBM { get; set; }
        public string JZKSMC { get; set; }
        public string CYKSBM { get; set; }
        public string CYKSMC { get; set; }
        public string ZDBM { get; set; }
        public string BMLX { get; set; }
        public DateTime RYSJ { get; set; }
        public DateTime CYSJ { get; set; }
        public int XGBZ { get; set; }
        public string MJ { get; set; }
        public string YLYL1 { get; set; }
        public string YLYL2 { get; set; }
    }
    #endregion

    #region 住院医嘱明细表
    public class TB_CIS_DrAdvice_Detail : SqlBase
    {
        public string YZID { get; set; }
        public string WSJGDM { get; set; }
        public string YLJGDM { get; set; }
        public string JZLSH { get; set; }
        public string CXBZ { get; set; }
        public string JZLX { get; set; }
        public string KH { get; set; }
        public string BQ { get; set; }
        public string XDKSBM { get; set; }
        public string XDKSMC { get; set; }
        public string XDRGH { get; set; }
        public string XDRXM { get; set; }
        public string XDRSFZ { get; set; }
        public DateTime YZXDSJ { get; set; }
        public string ZXKSBM { get; set; }
        public string ZXKSMC { get; set; }
        public string ZXRGH { get; set; }
        public string ZXRXM { get; set; }
        public string YZZXSFZ { get; set; }
        public DateTime YZZXSJ { get; set; }
        public DateTime? YZZZSJ { get; set; }
        public string YZSM { get; set; }
        public string YZZH { get; set; }
        public string YZLB { get; set; }
        public string YZMXBM { get; set; }
        public string MXXMBMYB { get; set; }
        public string YZMXMC { get; set; }
        public string SCPH { get; set; }
        public string YXQZ { get; set; }
        public string YZLX { get; set; }
        public string YPGG { get; set; }
        public string YPYF { get; set; }
        public string YF { get; set; }
        public string YYPDDM { get; set; }
        public string YYPD { get; set; }
        public decimal? JL { get; set; }
        public string DW { get; set; }
        public decimal? MCSL { get; set; }
        public string MCDW { get; set; }
        public decimal? YYTS { get; set; }
        public string SFPS { get; set; }
        public decimal? YPSL { get; set; }
        public string YPDW { get; set; }
        public string JYDM { get; set; }
        public string JCBW { get; set; }
        public string BZ { get; set; }
        public int XGBZ { get; set; }
        public string MJ { get; set; }
        public string YLYL1 { get; set; }
        public string YLYL2 { get; set; }
    }

    #endregion

    #region 住院收费明细表
    public class TB_HIS_ZY_Fee_Detail : SqlBase
    {
        public string SFMXID { get; set; }
        public string TFBZ { get; set; }
        public string WSJGDM { get; set; }
        public string YLJGDM { get; set; }
        public string JZLSH { get; set; }
        public string KH { get; set; }
        public string YZID { get; set; }
        public string FPH { get; set; }
        public string BXLX { get; set; }
        public string MXFYLB { get; set; }
        public DateTime STFSJ { get; set; }
        public string MXXMBM { get; set; }
        public string MXXMBMYB { get; set; }
        public string MXXMMC { get; set; }
        public string GSXMSM { get; set; }
        public string GSXMDW { get; set; }
        public string SCPH { get; set; }
        public string YXQZ { get; set; }
        public decimal MXXMDJ { get; set; }
        public decimal MXXMSL { get; set; }
        public decimal MXXMJE { get; set; }
        public int XGBZ { get; set; }
        public string YLYL1 { get; set; }
        public string YLYL2 { get; set; }
    }

    #endregion

    #region 手术明细表
    public class TB_Operation_Detail : SqlBase
    {
        public string SSMXLSH { get; set; }
        public string WSJGDM { get; set; }
        public string YLJGDM { get; set; }
        public string KH { get; set; }
        public string JZLSH { get; set; }
        public string MZZYBZ { get; set; }
        public string RJSSBZ { get; set; }
        public string ZQSSBZ { get; set; }
        public string SSLX { get; set; }
        public string SSCZBM { get; set; }
        public string SSCZMC { get; set; }
        public string SSQZD { get; set; }
        public string SSHZD { get; set; }
        public DateTime SSKSSJ { get; set; }
        public DateTime SSJSSJ { get; set; }
        public string SSJB { get; set; }
        public string SSYSGH { get; set; }
        public string SSYSXM { get; set; }
        public string SSYSSFZ { get; set; }
        public string SSYSZ1GH { get; set; }
        public string SSYSZ1XM { get; set; }
        public string SSZISFZ { get; set; }
        public string SSYSZ2GH { get; set; }
        public string SSYSZ2XM { get; set; }
        public string SSZ2SFZ { get; set; }
        public string MZYSGH { get; set; }
        public string MZYSXM { get; set; }
        public string MZYSSFZ { get; set; }
        public string MZFS { get; set; }
        public string QKYHDJ { get; set; }
        public string YFYKJ { get; set; }
        public string TBJXBZ { get; set; }
        public string SSXH { get; set; }
        public string ZDYS { get; set; }
        public string YYXSU { get; set; }
        public string SSLY { get; set; }
        public string MZFY { get; set; }
        public string BFZBZ { get; set; }
        public string SSBFZ { get; set; }
        public string SSZH { get; set; }
        public string ZCBZ { get; set; }
        public string BDQP { get; set; }
        public string BDSLFH { get; set; }
        public string CFSSS { get; set; }
        public string MJ { get; set; }
        public int XGBZ { get; set; }
        public string YLYL1 { get; set; }
        public string YLYL2 { get; set; }
    }

    #endregion

    #region 医院的科室字典表
    public class TB_DIC_Department: SqlBase
    {
        public string YYKSDM { get; set; }
        public string YYKSMC { get; set; }
        public string WSJGDM { get; set; }
        public string KSTYBZ { get; set; }
        public string WSJDM { get; set; }
        public string YLJGDM { get; set; }
        public string YBDM { get; set; }
        public string SM { get; set; }
    }

    #endregion

    #region 医护人员字典表
    public class TB_DIC_Practitioner:SqlBase
    {
        public string GH { get; set; }
        public string WSJGDM { get; set; }
        public string RYZTBZ { get; set; }
        public string YLJGDM { get; set; }
        public string GHLY { get; set; }
        public string ZCM { get; set; }
        public string XM { get; set; }
        public string ZJHM { get; set; }
        public string ZJLX { get; set; }
        public string SSKS { get; set; }
        public string ZWDM { get; set; }
        public string ZHIW { get; set; }
        public string ZCDM { get; set; }
        public string ZHIC { get; set; }
        public string CSRQ { get; set; }
        public string LB { get; set; }
        public string ZYBM { get; set; }
        public string ZYLB { get; set; }
        public string QKBZ { get; set; }
        public string WHCD { get; set; }
        public string ZHUANY { get; set; }
        public string YLYL1 { get; set; }
        public string YLYL2 { get; set; }
    }

    #endregion

    #region 药品目录字典表
    public class TB_DIC_MEDICINES:SqlBase
    {
        public string YYZBDM { get; set; }
        public string WSJGDM { get; set; }
        public string YBMLBM { get; set; }
        public string YLJGDM { get; set; }
        public string SYBZ { get; set; }
        public string TYMC { get; set; }
        public string YWMC { get; set; }
        public string JXDA { get; set; }
        public string YPPZWH { get; set; }
        public string BZJX { get; set; }
        public string YNZJBZ { get; set; }
        public string TBSM { get; set; }
        public string JYBZ { get; set; }
        public string TJFL { get; set; }
        public string KSSBZ { get; set; }
        public string KJYWFJ { get; set; }
        public string ZSJBZ { get; set; }
        public decimal? GSJG { get; set; }
        public string GGDW { get; set; }
        public string BZSM { get; set; }
    }

    #endregion

    #region 非药品目录字典表
    public class TB_DIC_Materials : SqlBase
    {
        public string YYZBDM { get; set; }
        public string WSJGDM { get; set; }
        public string MXXMBMYB { get; set; }
        public string XMMC { get; set; }
        public string YLJGDM { get; set; }
        public string SFDW { get; set; }
        public decimal SFDJ { get; set; }
        public string SFXDLB { get; set; }
        public string GZHCBZ { get; set; }
        public string ZRCLBZ { get; set; }
        public string SYBZ { get; set; }
        public string YNZJBZ { get; set; }
        public string TBSM { get; set; }
        public string BZSM { get; set; }
    }
    #endregion

    #region 日志记录
    public class Log_TbHealthyUpload : SqlBase
    {
        /// <summary>
        /// 交易状态 0:成功
        /// </summary>
        public string TabName { get; set; }
        public string status { get; set; }
        public string statusName { get; set; }
        /// <summary>
        /// 交易编号
        /// </summary>
        public string err_msg { get; set; }
        public DateTime createtime { get; set; }
    }
    #endregion
}
