using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.PDS.Requset.Stock
{
    public class WithdrawalPrepareReturnDTO : RequestBase
    {
        public string OrganizeId { get; set; }
        public string Djh { get; set; }
        public string yhgh { get; set; }
        public string thzt { get; set; }
    }
}
