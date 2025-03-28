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
    [SugarTable("V_HIS_USER_DEPT", "HisUserDeptEntity")]
    public class HisUserDeptEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string RYDM { get; set; }
        public string KSDM { get; set; }
    }
}
