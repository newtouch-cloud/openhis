using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.Entity.LIS
{
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("V_HIS_PATIENT_REQUEST", "HisPatientRequestEntity")]
    public class HisPatientRequestEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string SQLX { get; set; }
        public string TMH { get; set; }
        public string YLJGDM { get; set; }
        public string BRLX { get; set; }
        public string BRBH { get; set; }
        public string BRID { get; set; }
        public string BRKH { get; set; }
        public string ZYCS { get; set; }
        public string RYRQ { get; set; }
        public string BRXM { get; set; }
        public string BRXB { get; set; }
        public string BRCSRQ { get; set; }
        public string ZDMC { get; set; }
        public string SFLX { get; set; }
        public string SQBQ { get; set; }
        public string CWH { get; set; }
        public string SQBZ { get; set; }
        public string SQKS { get; set; }
        public string SQYS { get; set; }
        public string SQSJ { get; set; }
        public string JZBZ { get; set; }
        public string YSSQDH { get; set; }
        public string ZXKS { get; set; }
        public string SQDLB { get; set; }
        public string BBLXMC { get; set; }
        public string BBBZSM { get; set; }
        public string SQYY { get; set; }
        public string XMMS { get; set; }
        public string DYSJ { get; set; }
        public string DYR { get; set; }
        public string DYCS { get; set; }
        public string CYSJ { get; set; }
        public string CYR { get; set; }
        public string BBHGJJSJ { get; set; }
        public string BBJJHG { get; set; }
        public string ZFSJ { get; set; }
        public string ZFR { get; set; }
        public string SFBZ { get; set; }
        public string SFR { get; set; }
        public string SFSJ { get; set; }
        public string YSZJE { get; set; }
        public string SSZJE { get; set; }
        public string JMBZ { get; set; }
        public string BYBZ { get; set; }
        public string ABO { get; set; }
        public string RH { get; set; }
        public string YHYXSJ { get; set; }
        public string SXMD { get; set; }
        public string SXS { get; set; }
        public string RSS { get; set; }
        public string YYCS { get; set; }
        public string SCCS { get; set; }
        public string SXBLFY { get; set; }
        public string SXY { get; set; }
        public string ZDDM { get; set; }
        public string BRTXDZ { get; set; }
        public string BRMZ { get; set; }
        public string BRSFZH { get; set; }
        public string BRLXDH { get; set; }
        public string BRSG { get; set; }
        public string BRTZ { get; set; }
        public string SQDLY { get; set; }
        public string BBJSSJ { get; set; }
        public string BBJSR { get; set; }
        public string BZSM { get; set; }
        public string SQMXJLH { get; set; }
        public string SH { get; set; }
        public string SQXMDH { get; set; }
        public string SQXMMC { get; set; }
        public string SSZHMC { get; set; }
        public string XMJZJG { get; set; }
        public string XMSJJG { get; set; }
        public string XMSL { get; set; }
        public string XMZJE { get; set; }
        public string SQXMDM_HIS { get; set; }
        public string HISXGZD1 { get; set; }
        public string HISXGZD2 { get; set; }
        public string HISXGZD3 { get; set; }

    }
}
