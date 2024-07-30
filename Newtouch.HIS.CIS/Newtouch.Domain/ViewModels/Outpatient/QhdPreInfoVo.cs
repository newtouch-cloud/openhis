using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ViewModels
{
    public class QhdPreInfoVo
    {

    }

    public class OrganizationData
    {
        public string orgCode { get; set; }
        public string orgName { get; set; }
    }

    public class CommonRequestData
    {
        public string xmldata { get; set; }
    }

    public class InpatientInfo
    {
        public string zyh { get; set; }
        public string xm { get; set; }
        public string sex { get; set; }
        public string age { get; set; }
        public string brxzdm { get; set; }
        public string brxzmc { get; set; }
    }
}
