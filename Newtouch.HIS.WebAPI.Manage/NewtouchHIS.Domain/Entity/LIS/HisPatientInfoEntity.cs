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
    [SugarTable("V_HIS_PATIENT_INFO", "HisPatientInfoEntity")]
    public  class HisPatientInfoEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string YLJGDM { get; set; }
        public string BRBH { get; set; }
        public string BRID { get; set; }
        public string BRKH { get; set; }
        public string ZYCS { get; set; }
        public DateTime? RYRQ { get; set; }
        public string BRXM { get; set; }
        public string BRXB { get; set; }
        public DateTime? BRCSRQ { get; set; }
        public string BRNL { get; set; }
        public string BRLX { get; set; }
        public string SFLX { get; set; }
        public string SQYS { get; set; }
        public string SQKS { get; set; }
        public string SQBQ { get; set; }
        public string BRCWH { get; set; }
        public string ZDMC { get; set; }
        public string ZDDM { get; set; }
        public string BRTXDZ { get; set; }
        public string BRMZ { get; set; }
        public string BRSFZH { get; set; }
        public string BRLXDH { get; set; }
        public string BRSG { get; set; }
        public string BRTZ { get; set; }
        public string ABO { get; set; }
        public string RH { get; set; }
        public string QTHM { get; set; }
        public string SSDM { get; set; }
        public string HZSSD { get; set; }
        public string CHXH { get; set; }
        public string SXS { get; set; }
        public string RSS { get; set; }
        public string YYCS { get; set; }
        public string SCCS { get; set; }

    }
}
