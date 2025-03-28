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
    [SugarTable("V_HIS_OPERATION", "HisOperationEntity")]
    public class HisOperationEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string SSDM { get; set; }
        public string SSMC { get; set; }
        public string SSJB { get; set; }
        public string SSDM_HIS { get; set; }
    }
}
