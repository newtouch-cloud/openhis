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
    [SugarTable("V_HIS_DOCTOR", "HisDeptEntity")]
    public class HisDoctorEntity 
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string RYDM { get; set; }
        public string RYXM { get; set; }
        public string RYGW { get; set; }
        public string RYDM_HIS { get; set; }
        public string RYLXDH { get; set; }
        public string RYKSJS { get; set; }
        public string RYZC { get; set; }
    }
}
