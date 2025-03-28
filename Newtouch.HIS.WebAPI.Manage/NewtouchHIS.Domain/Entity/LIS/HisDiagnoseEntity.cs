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
    [SugarTable("V_HIS_DIAGNOSE", "HisDiagnoseEntity")]
    public class HisDiagnoseEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string ZDDM { get; set; }
        public string ZDMC { get; set; }
        public string ZDDM_ICD10 { get; set; }
    }
}
