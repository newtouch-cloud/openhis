using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Domain.Entity.LIS
{
    [Tenant(DBEnum.InterfaceDb)]
    [SugarTable("V_Lis_Dept", "LisDeptEntity")]
    public class LisDeptEntity
    {
        public string areaCode { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string source { get; set; }
        public string isUse { get; set; }
        public string mobile { get; set; }
    }
}
