using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.CIS.APIRequest
{
    public class UpdatebrxzRequest : JSONRequestBase
    {
        public string zyh { get; set; }
        public string mzh { get; set; }
        [Required]
        public string brxzCode { get; set; }
        public string brxzmc { get; set; }
    }
}
