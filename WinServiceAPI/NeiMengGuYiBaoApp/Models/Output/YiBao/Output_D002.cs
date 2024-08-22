using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_D002 : OutputBase
    {
        public string rxFile { get; set; }
        public string signDigest { get; set; }
        public string signCertSn { get; set; }
        public string signCertDn { get; set; }
    }
}
