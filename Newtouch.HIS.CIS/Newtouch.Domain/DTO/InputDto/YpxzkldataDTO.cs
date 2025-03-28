using System.Collections.Generic;

namespace Newtouch.Domain.DTO
{
    public class YpxzkldataDTO
    {
        public string mbzd { get; set; }
        public List<cfdata> cfdata { get; set; }
    }
    public class cfdata
    {
        public string ypCode { get; set; }
        public decimal sl { get; set; }
    }
}