using Newtouch.HIS.API.Common;
using System;

namespace Newtouch.CIS.APIRequest.Dto
{
    public class OutpatientCFDRequestDTO : RequestBase
    {

        public string printTemplate { get; set; }

        
        public string jsonstr { get; set; }
    }
}
