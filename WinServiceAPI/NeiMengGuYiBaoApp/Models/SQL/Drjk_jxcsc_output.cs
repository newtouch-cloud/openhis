using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.SQL
{
    public class Drjk_jxcsc_output : SqlBase
    {
        public string mlbm_id { get; set; }
        public string xm_id { get; set; }
        public string type { get; set; }
        public string OrganizeId { get; set; }
        public string OrganizeName { get; set; }
        public string issuccess { get; set; }
        public string log { get; set; }
        public string czydm { get; set; }
        public DateTime? czrq { get; set; }
        public int? zt { get; set; }
        public string zt_czy { get; set; }
        public DateTime? zt_rq { get; set; }
    }
}
