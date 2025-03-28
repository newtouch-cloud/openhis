using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.Entity.PACS
{
    [Tenant(DBEnum.InterfaceDb)]
    [SugarTable("v_pacs_dept", "PacsDeptEntity")]
    public class PacsDeptEntity
    {
        public string hosCode { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string source { get; set; }
        public string sourceCode { get; set; }
    }
}
