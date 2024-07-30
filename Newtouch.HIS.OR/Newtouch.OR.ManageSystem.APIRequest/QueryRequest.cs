using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.HIS.API.Common;

namespace Newtouch.OR.ManageSystem.APIRequest
{
    public class QueryRequest: RequestBase
    {
        public string OrganizeId { get; set; }
        public string ksrq { get; set; }
        public string jsrq { get; set; }
        public string zyh { get; set; }
        /// <summary>
        /// 1 待登记 2已登记 3 登记作废
        /// </summary>
        public string djzt { get; set; }

    }
}
