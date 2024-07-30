using Newtouch.CIS.APIRequest.Dto;
using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.CIS.APIRequest.Prescription
{
    public class ReviewRequest : RequestBase
    {
        public string orgId { get; set; }
        public string zyh { get; set; }
        public string rygh { get; set; }
        public string username { get; set; }
        public string GetMAC { get; set; }
    }
    
}
