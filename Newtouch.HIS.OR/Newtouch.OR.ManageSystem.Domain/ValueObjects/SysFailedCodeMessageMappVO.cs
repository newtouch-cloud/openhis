using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.ValueObjects
{
    public class SysFailedCodeMessageMappVO
    {
        public string Id { get; set; }
        public string TopOrganizeId { get; set; }
        public string OrganizeId { get; set; }
        public string AppId { get; set; }
        public string code { get; set; }
        public string msg { get; set; }

    }
}
