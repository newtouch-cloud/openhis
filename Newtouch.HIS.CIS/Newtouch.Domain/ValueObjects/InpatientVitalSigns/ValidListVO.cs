using System;
using System.Collections.Generic;

namespace Newtouch.Domain.ValueObjects.InpatientVitalSigns
{
    public class ValidListVO
    {
        public DateTime rq { get; set; }
        public List<times> times { get; set; }
    }

    public class times {
        public int sj { get; set; }
        public decimal? tw { get; set; }
        public int? xysz { get; set; }
        public int? xyxz { get; set; }
        public int? mb { get; set; }
        public int? hx { get; set; }
    }
}
