using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    /// <summary>
    /// 药品单位字典
    /// </summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("xt_ypdw", "DrugUnitEntity")]
    public class DrugUnitEntity :ISysEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string ypdwId { get; set; }
        public string ypdwCode { get; set; }
        public string ypdwmc { get; set; }
        public int? px { get; set; }
        
    }
}
