using CQYiBaoInterface.Models.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiBaoScheduling.Model
{
    public class ResponseDTO
    {
       
        /// <summary>
        /// 交易状态 0:成功
        /// </summary>
        public string infcode { get; set; }
        public string err_msg { get; set; }
        public object output { get; set; }
        public setlinfo2304 setlinfo { get; set; }

    }
    public class Output_2304
    {
        public setlinfo2304 setlinfo { get; set; }
        /// <summary>
        ///  输出-结算基金分项信息（节点标识：setldetail）
        /// </summary>
        public List<setldetail> setldetail { get; set; }
    }

    public class setlinfo2304
    {
        /// <summary>
        /// 1|就诊ID|字符型|30|  |Y|  
        /// </summary> 
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 2|结算ID|字符型|30||Y|
        /// </summary> 
		public string setl_id { get; set; }

        /// <summary>
        /// 3|人员编号|字符型|30|  |Y|  
        /// </summary> 
		public string psn_no { get; set; }

        /// <summary>
        /// 4|人员姓名|字符型|50|  |Y|  
        /// </summary> 
		public string psn_name { get; set; }

        /// <summary>
        /// 5|人员证件类型|字符型|6|Y|Y|  
        /// </summary> 
		public string psn_cert_type { get; set; }

        /// <summary>
        /// 6|证件号码|字符型|50|  |Y|  
        /// </summary> 
        public string certno { get; set; }

        /// <summary>
        /// 7|性别|字符型|6|Y|  |  
        /// </summary> 
        public string gend { get; set; }

        /// <summary>
        /// 8|民族|字符型|3|Y|  |  
        /// </summary> 
        public string naty { get; set; }

        /// <summary>
        /// 9|出生日期|日期型|  |  |  |yyyy-MM-dd
        /// </summary> 
        public string brdy { get; set; }

        /// <summary>
        /// 10|年龄|数值型|4,1|  |  |  
        /// </summary> 
        public string age { get; set; }

        /// <summary>
        /// 11|险种类型|字符型|6|Y|  |  
        /// </summary> 
        public string insutype { get; set; }

        /// <summary>
        /// 12|人员类别|字符型|6|Y|Y|  
        /// </summary> 
        public string psn_type { get; set; }

        /// <summary>
        /// 13|公务员标志|字符型|3|Y|Y|  
        /// </summary> 
        public string cvlserv_flag { get; set; }

        /// <summary>
        /// 14|结算时间|日期时间型|  |  ||yyyy-MM-dd HH:mm:ss
        /// </summary> 
        public string setl_time { get; set; }

        /// <summary>
        /// 15|就诊凭证类型|字符型|3|Y|  |  
        /// </summary> 
        public string mdtrt_cert_type { get; set; }

        /// <summary>
        /// 16|医疗类别|字符型|6|Y|Y|  
        /// </summary> 
        public string med_type { get; set; }

        /// <summary>
        /// 17|医疗费总额|数值型|16,2|  |Y|  
        /// </summary> 
        public string medfee_sumamt { get; set; }

        /// <summary>
        /// 18|全自费金额|数值型|16,2|  |Y|  
        /// </summary> 
		public string fulamt_ownpay_amt { get; set; }

        /// <summary>
        /// 19|超限价自费费用|数值型|16,2|  |Y|  
        /// </summary> 
		public string overlmt_selfpay { get; set; }

        /// <summary>
        /// 20|先行自付金额|数值型|16,2|  |Y|  
        /// </summary> 
		public string preselfpay_amt { get; set; }

        /// <summary>
        /// 21|符合政策范围金额|数值型|16,2|  |Y|  
        /// </summary> 
		public string inscp_scp_amt { get; set; }

        /// <summary>
        /// 22|实际支付起付线|数值型|16,2|  |  |  
        /// </summary> 
		public string act_pay_dedc { get; set; }

        /// <summary>
        /// 23|基本医疗保险统筹基金支出|数值型|16,2|  |Y|  
        /// </summary> 
        public string hifp_pay { get; set; }

        /// <summary>
        /// 24|基本医疗保险统筹基金支付比例|数值型|5,4|  |Y|  
        /// </summary> 
        public string pool_prop_selfpay { get; set; }

        /// <summary>
        /// 25|公务员医疗补助资金支出|数值型|16,2|  |Y|  
        /// </summary> 
		public string cvlserv_pay { get; set; }

        /// <summary>
        /// 26|企业补充医疗保险基金支出|数值型|16,2|  |Y  |  
        /// </summary> 
        public string hifes_pay { get; set; }

        /// <summary>
        /// 27|居民大病保险资金支出|数值型|16,2|  |Y  |  
        /// </summary> 
        public string hifmi_pay { get; set; }

        /// <summary>
        /// 28|职工大额医疗费用补助基金支出|数值型|16,2|  |Y  |  
        /// </summary> 
        public string hifob_pay { get; set; }

        /// <summary>
        /// 29|医疗救助基金支出|数值型|16,2|  |Y  |  
        /// </summary> 
        public string maf_pay { get; set; }

        /// <summary>
        /// 30|医院负担金额|数值型|16,2|||
        /// </summary> 
        public string hosp_part_amt { get; set; }

        /// <summary>
        /// 31|其他支出|数值型|16,2|  |Y  |  
        /// </summary> 
        public string oth_pay { get; set; }

        /// <summary>
        /// 32|基金支付总额|数值型|16,2|  |Y|  
        /// </summary> 
        public string fund_pay_sumamt { get; set; }

        /// <summary>
        /// 33|个人负担总金额|数值型|16,2|  ||  
        /// </summary> 
		public string psn_part_amt { get; set; }

        /// <summary>
        /// 34|个人账户支出|数值型|16,2|  |Y|  
        /// </summary> 
        public string acct_pay { get; set; }

        /// <summary>
        /// 35|个人现金支出|数值型|16,2|  |Y|  
        /// </summary> 
        public string psn_cash_pay { get; set; }

        /// <summary>
        /// 36|余额|数值型|16,2||Y|
        /// </summary> 
        public string balc { get; set; }

        /// <summary>
        /// 37|个人账户共济支付金额|数值型|16,2||Y|
        /// </summary> 
        public string acct_mulaid_pay { get; set; }

        /// <summary>
        /// 38|医药机构结算ID|字符型|30||Y|存放发送方报文ID
        /// </summary> 
		public string medins_setl_id { get; set; }

        /// <summary>
        /// 39|清算经办机构|字符型|6||  |
        /// </summary> 
		public string clr_optins { get; set; }

        /// <summary>
        /// 40|清算方式|字符型|6|Y  |  |  
        /// </summary> 
        public string clr_way { get; set; }

        /// <summary>
        /// 41|清算类别|字符型|6|Y|Y  |
        /// </summary> 
        public string clr_type { get; set; }

    }

    public class setldetail 
    {
        /// <summary>
        /// 基金支付类型 字符型 6 Y Y
        /// </summary>
        public string fund_pay_type { get; set; }

        /// <summary>
        /// 符合政策范围金额 数值型 16,2 Y
        /// </summary>
        public decimal? inscp_scp_amt { get; set; }

        /// <summary>
        ///本次可支付限额金额 数值型 16,2 Y
        /// </summary>
        public decimal? crt_payb_lmt_amt { get; set; }

        /// <summary>
        /// 基金支付金额 数值型 16,2 Y
        /// </summary>
        public decimal? fund_payamt { get; set; }

        /// <summary>
        /// 基金支付类型名称 字符型 200
        /// </summary>
        public string fund_pay_type_name { get; set; }

        /// <summary>
        /// 结算过程信息 字符型 4000
        /// </summary>
        public string setl_proc_info { get; set; }
    }

    public class Log_CqYbScheduling:SqlBase
    {
        /// <summary>
        /// 交易状态 0:成功
        /// </summary>
        public string infcode { get; set; }
        public string err_msg { get; set; }
        /// <summary>
        /// 交易编号
        /// </summary>
        public string infno { get; set; }
        public string zyh { get; set; }
        public DateTime createtime { get; set; }
    }
    public class Drjk_prejs_output : SqlBase
    {
        /// <summary>
        /// 主键prejs_id
        /// </summary>
        public string prejs_id { get; set; }
        /// <summary>
        /// 1|就诊ID|字符型|30|  |Y|  
        /// </summary> 
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 2|结算ID|字符型|30||Y|
        /// </summary> 
		public string setl_id { get; set; }

        /// <summary>
        /// 3|人员编号|字符型|30|  |Y|  
        /// </summary> 
		public string psn_no { get; set; }

        /// <summary>
        /// 4|人员姓名|字符型|50|  |Y|  
        /// </summary> 
		public string psn_name { get; set; }

        /// <summary>
        /// 5|人员证件类型|字符型|6|Y|Y|  
        /// </summary> 
		public string psn_cert_type { get; set; }

        /// <summary>
        /// 6|证件号码|字符型|50|  |Y|  
        /// </summary> 
        public string certno { get; set; }

        /// <summary>
        /// 7|性别|字符型|6|Y|  |  
        /// </summary> 
        public string gend { get; set; }

        /// <summary>
        /// 8|民族|字符型|3|Y|  |  
        /// </summary> 
        public string naty { get; set; }

        /// <summary>
        /// 9|出生日期|日期型|  |  |  |yyyy-MM-dd
        /// </summary> 
        public string brdy { get; set; }

        /// <summary>
        /// 10|年龄|数值型|4,1|  |  |  
        /// </summary> 
        public string age { get; set; }

        /// <summary>
        /// 11|险种类型|字符型|6|Y|  |  
        /// </summary> 
        public string insutype { get; set; }

        /// <summary>
        /// 12|人员类别|字符型|6|Y|Y|  
        /// </summary> 
        public string psn_type { get; set; }

        /// <summary>
        /// 13|公务员标志|字符型|3|Y|Y|  
        /// </summary> 
        public string cvlserv_flag { get; set; }

        /// <summary>
        /// 14|结算时间|日期时间型|  |  ||yyyy-MM-dd HH:mm:ss
        /// </summary> 
        public string setl_time { get; set; }

        /// <summary>
        /// 15|就诊凭证类型|字符型|3|Y|  |  
        /// </summary> 
        public string mdtrt_cert_type { get; set; }

        /// <summary>
        /// 16|医疗类别|字符型|6|Y|Y|  
        /// </summary> 
        public string med_type { get; set; }

        /// <summary>
        /// 17|医疗费总额|数值型|16,2|  |Y|  
        /// </summary> 
        public string medfee_sumamt { get; set; }

        /// <summary>
        /// 18|全自费金额|数值型|16,2|  |Y|  
        /// </summary> 
		public string fulamt_ownpay_amt { get; set; }

        /// <summary>
        /// 19|超限价自费费用|数值型|16,2|  |Y|  
        /// </summary> 
		public string overlmt_selfpay { get; set; }

        /// <summary>
        /// 20|先行自付金额|数值型|16,2|  |Y|  
        /// </summary> 
		public string preselfpay_amt { get; set; }

        /// <summary>
        /// 21|符合政策范围金额|数值型|16,2|  |Y|  
        /// </summary> 
		public string inscp_scp_amt { get; set; }

        /// <summary>
        /// 22|实际支付起付线|数值型|16,2|  |  |  
        /// </summary> 
		public string act_pay_dedc { get; set; }

        /// <summary>
        /// 23|基本医疗保险统筹基金支出|数值型|16,2|  |Y|  
        /// </summary> 
        public string hifp_pay { get; set; }

        /// <summary>
        /// 24|基本医疗保险统筹基金支付比例|数值型|5,4|  |Y|  
        /// </summary> 
        public string pool_prop_selfpay { get; set; }

        /// <summary>
        /// 25|公务员医疗补助资金支出|数值型|16,2|  |Y|  
        /// </summary> 
		public string cvlserv_pay { get; set; }

        /// <summary>
        /// 26|企业补充医疗保险基金支出|数值型|16,2|  |Y  |  
        /// </summary> 
        public string hifes_pay { get; set; }

        /// <summary>
        /// 27|居民大病保险资金支出|数值型|16,2|  |Y  |  
        /// </summary> 
        public string hifmi_pay { get; set; }

        /// <summary>
        /// 28|职工大额医疗费用补助基金支出|数值型|16,2|  |Y  |  
        /// </summary> 
        public string hifob_pay { get; set; }

        /// <summary>
        /// 29|医疗救助基金支出|数值型|16,2|  |Y  |  
        /// </summary> 
        public string maf_pay { get; set; }

        /// <summary>
        /// 30|医院负担金额|数值型|16,2|||
        /// </summary> 
        public string hosp_part_amt { get; set; }

        /// <summary>
        /// 31|其他支出|数值型|16,2|  |Y  |  
        /// </summary> 
        public string oth_pay { get; set; }

        /// <summary>
        /// 32|基金支付总额|数值型|16,2|  |Y|  
        /// </summary> 
        public string fund_pay_sumamt { get; set; }

        /// <summary>
        /// 33|个人负担总金额|数值型|16,2|  ||  
        /// </summary> 
		public string psn_part_amt { get; set; }

        /// <summary>
        /// 34|个人账户支出|数值型|16,2|  |Y|  
        /// </summary> 
        public string acct_pay { get; set; }

        /// <summary>
        /// 35|个人现金支出|数值型|16,2|  |Y|  
        /// </summary> 
        public string psn_cash_pay { get; set; }



        /// <summary>
        /// 36|余额|数值型|16,2||Y|
        /// </summary> 
        public string balc { get; set; }

        /// <summary>
        /// 37|个人账户共济支付金额|数值型|16,2||Y|
        /// </summary> 
        public string acct_mulaid_pay { get; set; }

        /// <summary>
        /// 38|医药机构结算ID|字符型|30||Y|存放发送方报文ID
        /// </summary> 
		public string medins_setl_id { get; set; }

        /// <summary>
        /// 39|清算经办机构|字符型|6||  |
        /// </summary> 
		public string clr_optins { get; set; }

        /// <summary>
        /// 40|清算方式|字符型|6|Y  |  |  
        /// </summary> 
        public string clr_way { get; set; }

        /// <summary>
        /// 41|清算类别|字符型|6|Y|Y  |
        /// </summary> 
        public string clr_type { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string zyh { get; set; }


        /// <summary>
        /// 操作员
        /// </summary>
        public string czydm { get; set; }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime? czrq { get; set; }
        /// <summary>
        /// 状态 1 正常 0作废
        /// </summary>
        public int zt { get; set; }
        /// <summary>
        /// 状态操作员
        /// </summary>
        public string zt_czy { get; set; }
        /// <summary>
        /// 状态日期
        /// </summary>
        public DateTime? zt_rq { get; set; }
    }
    #region  门诊就诊记录
    public class TB_YL_MZ_Medical_Record : SqlBase
    {
        public string JZLSH { get; set; }
        public string WSJGDM { get; set; }
        public string CISID { get; set; }
        public string YLJGDM { get; set; }
        public string KH { get; set; }
        public string HZXM { get; set; }
        public string CSRQ { get; set; }
        public string XB { get; set; }
        public string JZLX { get; set; }
        public string BXLX { get; set; }
        public string TXBZ { get; set; }
        public string YBZHBZ { get; set; }
        public string WDBZ { get; set; }
        public string JZKSBM { get; set; }
        public string JZKSMC { get; set; }
        public string JZKSRQ { get; set; }
        public string ZZYSGH { get; set; }
        public string ZZYSXM { get; set; }
        public string ZZYSSFZ { get; set; }
        public string JZZDBM { get; set; }
        public string JZZDBMCY { get; set; }
        public string BMLX { get; set; }
        public string JZZDSM { get; set; }
        public string ZS { get; set; }
        public string ZZMS { get; set; }
        public string XGBZ { get; set; }
        public string MJ { get; set; }
        public string YLYL1 { get; set; }
        public string YLYL2 { get; set; }
    }
    #endregion
    #region 处方明细表
    public class TB_CIS_Prescription_Detail : SqlBase
    {
        public string CYH { get; set; }
        public string CFMXH { get; set; }
        public string WSJGDM { get; set; }
        public string YLJGDM { get; set; }
        public string JZLSH { get; set; }
        public string CFLX { get; set; }
        public string XMLX { get; set; }
        public string KH { get; set; }
        public string JZKSDM { get; set; }
        public string JZKSMC { get; set; }
        public string KFYS { get; set; }
        public string KFYSXM { get; set; }
        public string KFYSSFZ { get; set; }
        public string KFRQ { get; set; }
        public string XMBM { get; set; }
        public string MXXMBMYB { get; set; }
        public string XMMC { get; set; }
        public string SFYP { get; set; }
        public string JXDM { get; set; }
        public string SCPH { get; set; }
        public string YXQZ { get; set; }
        public string YPGG { get; set; }
        public string YPYF { get; set; }
        public string YF { get; set; }
        public string YYTS { get; set; }
        public string YPSL { get; set; }
        public string YPDW { get; set; }
        public string CFTS { get; set; }
        public string YZZH { get; set; }
        public string SYPCDM { get; set; }
        public string SYPC { get; set; }
        public string JL { get; set; }
        public string DW { get; set; }
        public decimal? MCSL { get; set; }
        public string MCDW { get; set; }
        public string DPYSGH { get; set; }
        public string DPYSXM { get; set; }
        public string HDYSGH { get; set; }
        public string HDYSXM { get; set; }
        public string FYYSGH { get; set; }
        public string FYYSXM { get; set; }
        public string JYDM { get; set; }
        public string SFPS { get; set; }
        public string JCBW { get; set; }
        public string BZ { get; set; }
        public string XGBZ { get; set; }
        public string MJ { get; set; }
        public string YLYL1 { get; set; }
        public string YLYL2 { get; set; }
    }
    #endregion
    #region 收费明细表
    public class TB_HIS_MZ_Fee_Detail : SqlBase
    {
        public string SFMXID { get; set; }
        public string TFBZ { get; set; }
        public string WSJGDM { get; set; }
        public string YLJGDM { get; set; }
        public string JZLSH { get; set; }
        public string STFBH { get; set; }
        public string KH { get; set; }
        public string ZLLX { get; set; }
        public string BXLX { get; set; }
        public string CFIDH { get; set; }
        public string FPH { get; set; }
        public string MXFYLB { get; set; }
        public string STFSJ { get; set; }
        public string MXXMBM { get; set; }
        public string MXXMBMYB { get; set; }
        public string MXXMMC { get; set; }
        public string GSXMSM { get; set; }
        public string GSXMDW { get; set; }
        public string SCPH { get; set; }
        public string YXQZ { get; set; }
        public string MXXMDJ { get; set; }
        public string MXXMSL { get; set; }
        public string MXXMJE { get; set; }
        public string XGBZ { get; set; }
        public string YLYL1 { get; set; }
        public string YLYL2 { get; set; }
    }
    #endregion
    #region 医学影像检查报告
    public class TB_RIS_Report : SqlBase
    {
        public string StudyUid { get; set; }
        public string InstanceUid { get; set; }
        public string WSJGDM { get; set; }
        public string YLJGDM { get; set; }
        public string JZLSH { get; set; }
        public string MZZYBZ { get; set; }
        public string KH { get; set; }
        public string BRXM { get; set; }
        public string BRXB { get; set; }
        public string PatientID { get; set; }
        public string JCXMDM { get; set; }
        public string MXXMBMYB { get; set; }
        public string SQDH { get; set; }
        public string KDSJ { get; set; }
        public string JYSJ { get; set; }
        public string ExamType { get; set; }
        public string SBBM { get; set; }
        public string YQBM { get; set; }
        public string SQKS { get; set; }
        public string SQKSMC { get; set; }
        public string SQRSJ { get; set; }
        public string SQRGH { get; set; }
        public string SQRXM { get; set; }
        public string SQRSFZ { get; set; }
        public string JCKS { get; set; }
        public string JCKSMC { get; set; }
        public string JCYSGH { get; set; }
        public string JCYS { get; set; }
        public string JCYSSFZ { get; set; }
        public string BGSJ { get; set; }
        public string BGRGH { get; set; }
        public string BGRXM { get; set; }
        public string BGRSFZ { get; set; }
        public string SHSJ { get; set; }
        public string SHRGH { get; set; }
        public string SHRXM { get; set; }
        public string SHRSFZ { get; set; }
        public string JCBW { get; set; }
        public string BWACR { get; set; }
        public string JCMC { get; set; }
        public string ZYJCXX1 { get; set; }
        public string ZYJCXX2 { get; set; }
        public string ZYJCXX3 { get; set; }
        public string YYS { get; set; }
        public string BGLCZD { get; set; }
        public string YXBX { get; set; }
        public string YXZD { get; set; }
        public string BZHJY { get; set; }
        public string SFYYY { get; set; }
        public string XGBZ { get; set; }
        public string MJ { get; set; }
        public string YLYL1 { get; set; }
        public string YLYL2 { get; set; }
    }
    #endregion
}
