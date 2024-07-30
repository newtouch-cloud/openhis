using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.DTO.OutputDto.Purchase
{
    public class OutputYY131
    {
        public OutputHead HEAD { get; set; }
        public OutputDetailYY131 DETAIL { get; set; }
    }
    public class OutputDetailYY131
    {
        public List<OutputStructYY131> STRUCT { get; set; }
    }
    public class OutputStructYY131
    {
        public string PSMXBH { get; set; }
        public string HCTBDM { get; set; }
        public string CLJG { get; set; }
        public string CLQKMS { get; set; }

    }
}
