using NewtouchHIS.Base.Domain.Entity;
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
    [SugarTable("V_HIS_DEPT", "HisDeptEntity")]
    public class HisDeptEntity 
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string KSDM { get; set; }
        public string KSMC { get; set; }
        public string KSLX { get; set; }
        public string KSDM_HIS { get; set; }
        public string YLJGDM { get; set; }
        public string KSZYLB { get; set; }
        public string KSLXDH { get; set; }
    }
}
