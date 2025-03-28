using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.APIRequest.Bljgh.Request
{
    public class BljghReq : RequestBase
    {
        public string bllx { get; set; }

        public string zyh { get; set; }
        public string blmc { get; set; }

        public string organizeId { get; set; }
        public string blId { get; set; }
        public string czr { get; set; }
        public string delzt { get; set; }
    }
}
