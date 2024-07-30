using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Domain.Entity.PACS
{

    [Tenant(DBEnum.InterfaceDb)]
    [SugarTable("v_pacs_feeitem", "PacsFeeitemEntity")]
    public class PacsFeeitemVEntity
    {
        public string OrganizeId { get; set; }
        public string fymc { get; set; }
        public string fydm { get; set; }
        public string? pym { get; set; }
        public decimal? money { get; set; }
        public string? sfbz { get; set; }
    }
}
