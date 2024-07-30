using CQYiBaoInterface.Models.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Output
{
    public class Dzcf_D003_output : SqlBase
    {
        public string mzh { get; set; }
        public string cfh { get; set; }
        public string OrganizeId { get; set; }
        public string InputContent { get; set; }
        public string rxTraceCode { get; set; }
        public string hiRxno { get; set; }
        public string rxStasCodg { get; set; }
        public string rxStasName { get; set; }
        public string czydm { get; set; }
        public DateTime? czrq { get; set; }
        public int? zt { get; set; }
        public string zt_czy { get; set; }
        public DateTime? zt_rq { get; set; }
    }
}
