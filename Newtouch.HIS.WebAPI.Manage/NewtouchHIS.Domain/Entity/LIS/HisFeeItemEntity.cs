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
    [SugarTable("V_HIS_FEE_ITEM", "HisFeeItemEntity")]
    public class HisFeeItemEntity
    {
        public string SFXMDM { get; set; }
        public string SFXMMC { get; set; }
        public decimal SFXMDJ { get; set; }
        public string SFXMLX { get; set; }
        public string SFXMZXKS { get; set; }
        public string SFXMDM_HIS { get; set; }

    }
}
