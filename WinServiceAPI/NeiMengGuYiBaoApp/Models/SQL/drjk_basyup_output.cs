using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.SQL
{
    public class drjk_basyup_output : SqlBase
    {
        public string zyh { get; set; }
        public string incontent { get; set; }
        public string outcontent { get; set; }
        public string sclb { get; set; }
        public DateTime czrq { get; set; }
        public string czydm { get; set; }
        public int zt { get; set; }
        public string zt_czy { get; set; }
        public DateTime zt_rq { get; set; }
    }
}
