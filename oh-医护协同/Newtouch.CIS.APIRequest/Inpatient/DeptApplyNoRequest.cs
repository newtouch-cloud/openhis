using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.CIS.APIRequest.Inpatient
{
    /// <summary>
    /// 科室申请单
    /// </summary>
    public class DeptApplyNoRequest : RequestBase
    {
        public string SqdArray { get; set; }
        public string OrganizeId { get; set; }
        public string UserCode { set; get; }
    }
}
