using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.Entity.PatientCenter
{
    [Tenant(DBEnum.SettDb)]
    [SugarTable("xt_brdz", "SysPatientAddressEntity")]
    public partial class SysPatientAddressEntity : IEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public int patid { get; set; }
        public string xm { get; set; }
        public string dh { get; set; }
        public string xian_sheng { get; set; }
        public string xian_shi { get; set; }
        public string xian_xian { get; set; }
        public string xian_dz { get; set; }
    }
}
