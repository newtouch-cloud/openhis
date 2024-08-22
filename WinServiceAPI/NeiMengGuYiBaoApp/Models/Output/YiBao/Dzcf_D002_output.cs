using NeiMengGuYiBaoApp.Models.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Dzcf_D002_output : SqlBase
    {
        public string mzh { get; set; }
        public string cfh { get; set; }
        public string OrganizeId { get; set; }
        public string InputContent { get; set; }
        public string originalValue { get; set; }
        public string originalRxFile { get; set; }
        public string rxFile { get; set; }
        public string signDigest { get; set; }
        public string signCertSn { get; set; }
        public string signCertDn { get; set; }
        public string czydm { get; set; }
        public DateTime? czrq { get; set; }
        public int? zt { get; set; }
        public string zt_czy { get; set; }
        public DateTime? zt_rq { get; set; }
    }
}
