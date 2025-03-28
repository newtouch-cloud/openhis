using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.Entity.LIS
{
    [Tenant(DBEnum.InterfaceDb)]
    [SugarTable("V_Lis_Doctor", "LisDoctorEntity")]
    public class LisDoctorEntity
    {
        public string areaCode { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string source { get; set; }
        public string isUse { get; set; }
        public string mobile { get; set; }
        public string dptCode { get; set; }
    }
}
